using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Helpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using UCDArch.Core.PersistanceSupport;
using System.Linq;
using File = Dogbert2.Core.Domain.File;

namespace Dogbert2.Services
{
    public class PdfService : IPdfService
    {
        private readonly IRepository<File> _fileRepository;
        private readonly IRepository<SectionType> _sectionTypeRepository;

        public PdfService(IRepository<File> fileRepository, IRepository<SectionType> sectionTypeRepository)
        {
            _fileRepository = fileRepository;
            _sectionTypeRepository = sectionTypeRepository;
        }

        #region Srs Helper Properties
        // base color
        private CMYKColor _baseColor = new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f);

        // standard body font
        private readonly Font _font = new Font(Font.FontFamily.TIMES_ROMAN, 10);
        private readonly Font _boldFont = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.BOLD);
        private readonly Font _italicFont = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.ITALIC);
        private readonly Font _headerFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f));
        private readonly Font _subHeaderFont = new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f));
        private readonly Font _sectionHeaderFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f));
        private readonly Font _captionFont = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.NORMAL, new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f));
        private readonly Font _tableHeaderFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.WHITE);

        // fonts for the cover page
        private readonly BaseFont _dateBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
        private readonly Font _coverProjNameFont = new Font(Font.FontFamily.HELVETICA, 30f, Font.BOLD, BaseColor.BLACK);
        private readonly Font _coverSmallFont = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD, new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f));
        private readonly Font _coverFont = new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD, new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f));
        private readonly Font _disclaimerFont = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD, BaseColor.LIGHT_GRAY);

        // width of the content
        private float _pageWidth;
        private float _pageHeight;

        // html tag types reuqired to be scanned
        private readonly List<string> blockTags = new List<string>() { "p", "ul", "ol" };
        private readonly List<string> listTags = new List<string>() { "ul", "ol" };
        #endregion

        // current documents
        private Document _doc;
        private Project _project;

        private float _currentHeight = 0f;
        
        #region Generator Functions
        private void InitializeDocument()
        {
            _doc = new iTextSharp.text.Document(PageSize.LETTER, 36 /* left */, 36 /* right */, 62 /* top */, 52 /* bottom */);
            // set the variable for the page's actual content size
            _pageWidth = _doc.PageSize.Width - (_doc.LeftMargin + _doc.RightMargin);
            _pageHeight = _doc.PageSize.Height - (_doc.TopMargin + _doc.BottomMargin);
        }

        /// <summary>
        /// Generates a pdf document
        /// </summary>
        /// <remakrs>
        /// http://itextsharp.com/
        /// http://www.mikesdotnetting.com/Article/80/Create-PDFs-in-ASP.NET-getting-started-with-iTextSharp
        /// </remakrs>
        /// <param name="project"></param>
        /// <returns></returns>
        public byte[] GeneratePdf(Project project, bool draft)
        {
            InitializeDocument();
            _project = project;

            var ms = new MemoryStream();
            var writer = PdfWriter.GetInstance(_doc, ms);

            // add in the header/footer
            writer.PageEvent = new pdfPage(project);

            _doc.Open();

            // add information
            AddCoverpage(writer.DirectContent);

            // get all section types
            var sections = _sectionTypeRepository.Queryable.OrderBy(a => a.Order);
            foreach (var sec in sections)
            {
                // see if the project has this if it's not speical
                if (!sec.IsSpecial)
                {
                    var txt = project.ProjectSections.Where(a => a.SectionType == sec).FirstOrDefault();

                    // project has this text in it
                    if (txt != null)
                    {
                        var table = InitializeTable();
                        var cell = CreateCell();

                        // add the header
                        AddSectionHeader(sec.Name, cell);

                        // add the txt
                        AddHtmlText(txt.Text, cell);

                        table.AddCell(cell);

                        AddToPage(table);
                    }
                }
                // deal with specialized 
                else
                {
                    var table = new PdfPTable(1);
                    table.TotalWidth = _pageWidth;
                    table.LockedWidth = true;
                    table.KeepTogether = true;
                    table.SplitLate = false;

                    var headerCell = CreateCell();
                    AddSectionHeader(sec.Name, headerCell);

                    table.AddCell(headerCell);

                    switch (sec.Id)
                    {
                        case "GL":
                            AddGlossary(table, _project);
                            break;
                        case "RQ":
                            AddRequirementTable(table, _project);
                            break;
                        case "UC":
                            AddUseCases(table, _project);
                            break;
                    }

                    AddToPage(table);
                }
            }

            _doc.Close();

            var bytes = ms.ToArray();
            return bytes;
        }

        #region Cover Page
        private void AddCoverpage(PdfContentByte cb)
        {
            // adds in the date box
            DrawBoxForCover(cb, _doc);            

            // add in the worker information
            AddWorkerInformation(cb, _doc, _project);

            // add in the project and client information
            AddProjectInformation(cb, _doc, _project);

            // add the disclaimer information
            AddDisclaimerInformation(cb, _doc, _project);

            // call new page
            _doc.NewPage();
        }

        /// <summary>
        /// Draws the box on the cover page that contains the date
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="doc"></param>
        /// <param name="project"></param>
        private void DrawBoxForCover(PdfContentByte cb, Document doc)
        {
            // set the colors
            cb.SetColorStroke(_baseColor);
            cb.SetColorFill(_baseColor);

            // calculate the top left corner of the box
            var x = doc.LeftMargin;
            var y = doc.BottomMargin + 678;

            var height = 60;

            // move the cursor to starting position
            cb.MoveTo(x, y);

            // draw the top line
            cb.LineTo(x + _pageWidth, y);

            // draw the right line
            cb.LineTo(x + _pageWidth, y - height);

            // draw the bottom line
            cb.LineTo(x, y - height);

            // draw the left line
            cb.LineTo(x, y);

            cb.ClosePathFillStroke();
            cb.Stroke();

            // add the text inside the box
            cb.BeginText();
            cb.SetFontAndSize(_dateBaseFont, 26f);
            cb.SetColorStroke(BaseColor.WHITE);
            cb.SetColorFill(BaseColor.WHITE);
            cb.SetTextMatrix(x + 10, y - 40);
            cb.ShowText(string.Format("{0:MMMM dd, yyyy}", DateTime.Now));
            cb.EndText();
        }

        /// <summary>
        /// Adds in worker information, Department(s), Unit(s), and PM/Lead Programmer
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="doc"></param>
        /// <param name="project"></param>
        private void AddWorkerInformation(PdfContentByte cb, Document doc, Project project)
        {
            var x = doc.LeftMargin;
            // height to bottom of worker information box
            var y = doc.BottomMargin + 520;

            var department = new Paragraph(project.WorkgroupDepts, _coverFont);
            var workgroups = new Paragraph(project.WorkgroupNames, _coverFont);
            var pm = new Paragraph(string.Format("Project Manager: {0}", project.ProjectManager != null ? project.ProjectManager.FullName : "n/a"), _coverSmallFont);
            var lp = new Paragraph(string.Format("Lead Programmer: {0}", project.LeadProgrammer != null ? project.LeadProgrammer.FullName : "n/a"), _coverSmallFont);

            var ct = new ColumnText(cb);
            ct.SetSimpleColumn(x, y, x + _pageWidth, y + 98);
            ct.Alignment = Element.ALIGN_LEFT | Element.ALIGN_TOP;

            ct.AddElement(department);
            ct.AddElement(workgroups);
            ct.AddElement(pm);
            ct.AddElement(lp);

            ct.Go();

        }

        /// <summary>
        /// Adds in the project name, and clien information
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="doc"></param>
        /// <param name="project"></param>
        private void AddProjectInformation(PdfContentByte cb, Document doc, Project project)
        {
            var x = doc.LeftMargin;
            var y = doc.BottomMargin + 185;

            var projName = new Paragraph(project.Name, _coverProjNameFont);

            var client = new Paragraph(string.Format("[Client: {0}]", project.Unit), _coverSmallFont);
            var contact = new Paragraph(string.Format("[Contact: {0}]", project.Contact), _coverSmallFont);

            var ct = new ColumnText(cb);
            ct.SetSimpleColumn(x, y, x + _pageWidth, y + 285);
            ct.Alignment = Element.ALIGN_LEFT | Element.ALIGN_TOP;

            ct.AddElement(projName);
            ct.AddElement(client);
            ct.AddElement(contact);

            ct.Go();
        }

        /// <summary>
        /// Add in the light grey disclaimer on the bottom of the front page
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="doc"></param>
        /// <param name="project"></param>
        private void AddDisclaimerInformation(PdfContentByte cb, Document doc, Project project)
        {
            // lower left coordinate of container
            var x = doc.LeftMargin;
            var y = doc.BottomMargin + 25;

            // create the paragraph for text
            var paragraph = new Paragraph(string.Format(Messages.SRSDisclaimer, project.Name), _disclaimerFont);

            var ct = new ColumnText(cb);
            ct.SetSimpleColumn(x, y, x + _pageWidth, y + 60);

            ct.AddElement(paragraph);
            ct.Alignment = Element.ALIGN_LEFT | Element.ALIGN_TOP;

            ct.Go();
        }
        #endregion

        private void AddSectionHeader(string title, PdfPCell pCell = null)
        {
            var table = new PdfPTable(1);
            table.TotalWidth = _pageWidth;
            table.LockedWidth = true;
            table.SpacingAfter = 10f;

            var cell = CreateCell();//new PdfPCell();
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;

            cell.BorderWidthBottom = 3;
            cell.BorderColorBottom = _baseColor;
            cell.PaddingTop = 16f;
            cell.PaddingBottom = 10f;

            var paragraph = new Paragraph(title, _headerFont);
            cell.AddElement(paragraph);

            table.AddCell(cell);
            
            if (pCell != null) pCell.AddElement(table);
            else _doc.Add(table);
        }

        /// <summary>
        /// Adds the table to the document taking into account what is currently on the page
        /// </summary>
        /// <param name="table"></param>
        private void AddToPage(PdfPTable table)
        {
            // is current content over 80%
            var isOverEighty = _currentHeight > (_pageHeight*.8);

            // will new content overrun page
            var willOverrun = (_currentHeight + table.TotalHeight) > _pageHeight;

            // overrun amount
            var overrunAmt = willOverrun ? (_currentHeight + table.TotalHeight)%_pageHeight : -1;

            // % overrun of page
            var percentOverrun = overrunAmt > 0 ? overrunAmt/_pageHeight : -1;

            // number of whole pages to house table
            var numPages = Math.Floor(table.TotalHeight/_pageHeight);

            if (isOverEighty)
            {
                // add new page
                _doc.NewPage();

                // just set the height to whatever is left on the last page of the table
                _currentHeight = table.TotalHeight % _pageHeight;
            }
            // will overrun but only by like 10% and take less than one whole page
            else if (willOverrun && percentOverrun < .10 && numPages == 0)
            {
                // add new page
                _doc.NewPage();

                // just set the height to whatever is left on the last page of the table
                _currentHeight = table.TotalHeight%_pageHeight;
            }
            // current contents less than 80% and will overrun, but take more than 10% of next page
            else if (willOverrun)
            {
                // add the height of what is left on the last page
                _currentHeight = (_currentHeight + table.TotalHeight) % _pageHeight;
            }
            // should not overrun
            else
            {
                _currentHeight += table.TotalHeight;
            }
            
            _doc.Add(table);
        }

        #region Initializers
        /// <summary>
        /// Initializes a pdf p table
        /// </summary>
        /// <param name="columns"># of columns</param>
        /// <returns></returns>
        private PdfPTable InitializeTable(int columns = 1)
        {
            var table = new PdfPTable(columns);

            // set the styles
            table.TotalWidth = _pageWidth;
            table.LockedWidth = true;
            table.SpacingAfter = 2f;

            return table;
        }
        /// <summary>
        /// Creates a formatted cell for a table header
        /// </summary>
        /// <param name="text">Text to be put in cell</param>
        /// <param name="backgroundColor">Background color</param>
        /// <param name="borderWidth">Border width</param>
        /// <param name="padding">Padding</param>
        /// <returns></returns>
        private PdfPCell CreateTableHeader(string text, CMYKColor backgroundColor = null, float? borderWidth = null, float? padding = null)
        {
            if (backgroundColor == null) backgroundColor = _baseColor;
            if (!borderWidth.HasValue) borderWidth = 0;
            if (!padding.HasValue) padding = 5;

            var chunk = new Chunk(text, _tableHeaderFont);
            var phrase = new Phrase(chunk);
            var cell = new PdfPCell(phrase) { BackgroundColor = backgroundColor, BorderWidth = borderWidth.Value, Padding = padding.Value };

            return cell;
        }
        /// <summary>
        /// Defaults to creating cell with no border and no padding
        /// </summary>
        /// <returns></returns>
        private PdfPCell CreateCell(Chunk chunk = null, Phrase phrase = null
                                   , float? borderLeft = null, float? borderTop = null, float? borderRight = null, float? borderBottom = null
                                   , float? borderAll = null                       
                                   , float? paddingLeft = null, float? paddingTop = null, float? paddingRight = null, float? paddingBottom = null
                                   , float? paddingAll = null)
        {
            if (!borderLeft.HasValue) borderLeft = 0;
            if (!borderTop.HasValue) borderTop = 0;
            if (!borderRight.HasValue) borderRight = 0;
            if (!borderBottom.HasValue) borderBottom = 0;

            if (borderAll.HasValue)
            {
                borderLeft = borderAll;
                borderTop = borderAll;
                borderRight = borderAll;
                borderBottom = borderAll;
            }

            if (!paddingLeft.HasValue) paddingLeft = 0;
            if (!paddingTop.HasValue) paddingTop = 0;
            if (!paddingRight.HasValue) paddingRight = 0;
            if (!paddingBottom.HasValue) paddingBottom = 0;

            if (paddingAll.HasValue)
            {
                paddingLeft = paddingAll;
                paddingTop = paddingAll;
                paddingRight = paddingAll;
                paddingBottom = paddingAll;
            }

            if (chunk != null) phrase = new Phrase(chunk);

            var cell = new PdfPCell(phrase)
            {
                BorderWidthLeft = borderLeft.Value,
                BorderWidthTop = borderTop.Value,
                BorderWidthRight = borderRight.Value,
                BorderWidthBottom = borderBottom.Value,
                PaddingLeft = paddingLeft.Value,
                PaddingTop = paddingTop.Value,
                PaddingRight = paddingRight.Value,
                PaddingBottom = paddingBottom.Value,
            };

            return cell;
        }
        #endregion

        #region Html Text
        /// <summary>
        /// Parse out the html and format it using iTextSharp's stuff
        /// </summary>
        /// <remarks>
        /// http://blog.dmbcllc.com/2009/07/28/itextsharp-html-to-pdf-parsing-html/ (not being used)
        /// </remarks>
        /// <param name="text"></param>
        private void AddHtmlText(string text, PdfPCell pCell = null)
        {
            var elements = new List<HtmlElement>(); 

            var reader = new XmlTextReader(text, XmlNodeType.Element, null);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // opening element
                    case XmlNodeType.Element:
                        
                        elements.Add(new HtmlElement(reader.Name, true));

                        break;
                    // closing element
                    case XmlNodeType.EndElement:

                        elements.Add(new HtmlElement(reader.Name, true, true));

                        // is this a closing for a full iTextsharp element?
                        if (IsReadyToProcess(reader.Name))
                        {

                            if (listTags.Contains(reader.Name))
                            {
                                // build the list object
                                var paragraph = BuildListObject(elements, reader.Name, pCell);

                                if (pCell == null) _doc.Add(paragraph);

                                //if (pCell != null) pCell.AddElement(paragraph);
                                //else _doc.Add(paragraph);

                                elements.Clear();
                            }
                            else
                            {
                                var paragraph = BuildDocObject(elements, pCell);
                                if (pCell != null) pCell.AddElement(paragraph);
                                else _doc.Add(paragraph);

                                elements.Clear();
                            }
                            
                        }
                        
                        break;
                    case XmlNodeType.Text:
                        
                        // add the text into the stack
                        elements.Add(new HtmlElement(reader.Value));

                        break;
                    case XmlNodeType.Whitespace:

                        // do nothing for now

                        break;
                }
            }
        }

        /// <summary>
        /// Determines if readed end of block element that cooresponds to iTextSharp objects
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private bool IsReadyToProcess(string tag)
        {
            return blockTags.Contains(tag.ToLower());
        }

        private Paragraph BuildDocObject(List<HtmlElement> elements, PdfPCell pCell = null)
        {
            var docObjs = new Stack<HtmlElement>();
            var paragraph = new Paragraph();

            // set spacing
            paragraph.SpacingBefore = 5;
            paragraph.FirstLineIndent = 36;

            foreach (var a in elements)
            {
                var obj = HandleTag(docObjs, a, pCell);

                if (obj != null) paragraph.Add(obj);
            }
            return paragraph;
        }

        private List BuildListObject(List<HtmlElement> elements, string listType, PdfPCell pCell = null)
        {
            var docObjs = new Stack<HtmlElement>();
            var lt = listType == "ol" ? List.ORDERED : List.UNORDERED;

            // remove the ol objects
            elements.RemoveAll(a => a.Value == listType);

            var list = new iTextSharp.text.List(lt);
            if (lt == List.UNORDERED) list.SetListSymbol("\u2022");
            list.IndentationLeft = 10f;

            foreach (var a in elements)
            {
                var obj = HandleTag(docObjs, a, pCell);
                if (obj != null)
                {
                    list.Add(new ListItem(obj));
                }
            }

            if (pCell != null) pCell.AddElement(list);

            return list;
        }

        /// <summary>
        /// Generates new phrase element with all sub formatting
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private Phrase HandleTag(Stack<HtmlElement> elements, HtmlElement element, PdfPCell pCell = null)
        {
            // opening element
            if (element.IsElement && !element.IsClose)
            {
                element.Phrase = new Phrase();
                element.Phrase.Font = _font;

                // set styling if any exists
                if (element.Value == "em")
                {
                    element.Phrase.Font = _italicFont;
                }
                else if (element.Value == "strong")
                {
                    element.Phrase.Font = _boldFont;
                }

                elements.Push(element);
            }
            // closing element
            else if (element.IsElement && element.IsClose)
            {
                var obj = elements.Pop();

                // no more objects in stack, add to paragraph
                if (elements.Count == 0)
                {
                    return obj.Phrase;
                }
                // still just a child, add it to the next object and put it back
                else
                {
                    var parent = elements.Pop();
                    parent.Phrase.Add(obj.Phrase);
                    elements.Push(parent);
                }
            }
            // deal with text
            else
            {
                var obj = elements.Pop();

                // scan for an image token
                var result = ScanForImage(element.Value, pCell);

                obj.Phrase.Add(result);

                // put it back on the stack because we're not done
                elements.Push(obj);
            }

            return null;
        }
        #endregion

        #region Image
        public string ScanForImage(string txt, PdfPCell pCell = null)
        {
            // does not contain the image tag
            if (!txt.Contains("{Image-")) return txt;

            // parse out all the image tags
            foreach (int pos in txt.IndexOfAll("{Image-"))
            {
                var token = txt.Substring(pos, txt.IndexOf("}", pos)+1);

                var begin = token.IndexOf("-")+1;
                var length = token.IndexOf("}") - begin;

                // get the id
                var id = token.Substring(begin, length);
                int fileId;
                if (int.TryParse(id, out fileId))
                {
                    AddImage(fileId, pCell);
                }

            }

            // replace all {Image-*} tags with string empty
            var rgx = new Regex("{Image-.+}");
            var result = rgx.Replace(txt, string.Empty);

            return result;
        }
        public void AddImage(int fileId, PdfPCell pCell = null)
        {
            var table = new PdfPTable(1);
            table.KeepTogether = true;

            var cell = new PdfPCell();
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            cell.Padding = 10;

            var file = _fileRepository.GetNullableById(Convert.ToInt32(fileId));
            if (file != null)
            {
                var ms = new MemoryStream(file.Contents);

                // add the image into the document
                var img = Image.GetInstance(ms);
                img.Alignment = Element.ALIGN_CENTER;

                // scale the image, because it is too big
                //img.ScaleToFit(_pageWidth, 250f);

                var paragraph = new Paragraph(file.Caption, _captionFont);
                paragraph.Alignment = Element.ALIGN_CENTER;

                cell.AddElement(img);
                cell.AddElement(paragraph);
            }

            table.AddCell(cell);

            if (pCell != null) pCell.AddElement(table);
            else _doc.Add(table);
        }
        #endregion

        #region Specialized Sections
        private void AddGlossary(PdfPTable table, Project project)
        {
            // build the html object so we can use the other list builder
            var elements = new List<HtmlElement>();
            
            // open the list
            elements.Add(new HtmlElement("ul", true, false));

            foreach (var term in project.ProjectTerms.OrderBy(a => a.Term))
            {
                // open li
                elements.Add(new HtmlElement("li", true, false));

                // strong term name
                elements.Add(new HtmlElement("strong", true, false));

                // throw in the term
                elements.Add(new HtmlElement(term.Term));

                // strong term name
                elements.Add(new HtmlElement("strong", true, true));

                elements.Add(new HtmlElement(string.Format("- {0}", term.Definition)));

                // add the reference if there is one
                if (!string.IsNullOrWhiteSpace(term.Src))
                {
                    elements.Add(new HtmlElement("em", true));
                    elements.Add(new HtmlElement(string.Format(" [{0}]", term.Src)));
                    elements.Add(new HtmlElement("em", true, true));
                }

                // close li
                elements.Add(new HtmlElement("li", true, true));
            }
            
            // close the list
            elements.Add(new HtmlElement("ul", true, true));

            var cell = CreateCell();
            BuildListObject(elements, "ul", cell);

            table.AddCell(cell);
        }
        private void AddRequirementTable(PdfPTable table, Project project)
        {
            foreach (var cat in project.Requirements.Select(a => a.RequirementCategory).Distinct())
            {
                // add in the table
                var reqTable = new PdfPTable(4);
                reqTable.TotalWidth = _pageWidth;
                reqTable.LockedWidth = true;
                reqTable.SetWidths(new float[]{1f, 2f, 5f, 1f});
                reqTable.SpacingAfter = 2f;
                reqTable.KeepTogether = true;
                //reqTable.SplitLate = false;   // not sure what this does

                // add the category header
                var subheaderCell = CreateCell(chunk: new Chunk(cat.Name, _subHeaderFont), paddingAll:10);
                subheaderCell.Colspan = 4;
                reqTable.AddCell(subheaderCell);

                // put the headers on the table
                reqTable.AddCell(CreateTableHeader("Id"));
                reqTable.AddCell(CreateTableHeader("Priority"));
                reqTable.AddCell(CreateTableHeader("Description"));
                reqTable.AddCell(CreateTableHeader("Type"));
            
                foreach (var req in project.Requirements.Where(a=>a.RequirementCategory == cat).OrderBy(a => a.RequirementCategory).ThenBy(a => a.Order))
                {
                    var cell1 = CreateCell(chunk: new Chunk(req.RequirementId, _font), paddingAll:5);
                    var cell2 = CreateCell(chunk: new Chunk(req.PriorityType.Name, _font), paddingAll: 5);
                    var cell3 = CreateCell(chunk: new Chunk(req.Description, _font), paddingAll: 5);
                    var cell4 = CreateCell(chunk: new Chunk(req.RequirementType.Id, _font), paddingAll: 5);

                    reqTable.AddCell(cell1);
                    reqTable.AddCell(cell2);
                    reqTable.AddCell(cell3);
                    reqTable.AddCell(cell4);
                }

                //_doc.Add(table);
                var cell = CreateCell();
                cell.AddElement(reqTable);
                table.AddCell(cell);
            }
        }
        private void AddUseCases(PdfPTable table, Project project)
        {
            // create a new table for each category of use cases
            foreach (var category in project.UseCases.Select(a => a.RequirementCategory).Distinct())
            {
                // add in the category name
                var categoryTable = new PdfPTable(1);
                categoryTable.TotalWidth = _pageWidth;
                categoryTable.LockedWidth = true;
                categoryTable.KeepTogether = true;
                categoryTable.SplitLate = false;

                // add in the category header
                categoryTable.AddCell(CreateCell(chunk: new Chunk(category.Name, _subHeaderFont), paddingAll:10));

                // deal with each use case in category
                foreach (var useCase in project.UseCases.Where(a => a.RequirementCategory == category))
                {
                    categoryTable.AddCell(BuildUseCaseTable(useCase));
                }

                var cell = CreateCell();
                cell.AddElement(categoryTable);
                table.AddCell(cell);
            }
        }
        private PdfPCell BuildUseCaseTable(UseCase useCase)
        {
            // set variables for current cells in this table
            var padding = 5;
            var border = 0;

            // table for each of the use cases now
            var useCaseTable = new PdfPTable(1);
            useCaseTable.TotalWidth = _pageWidth;
            useCaseTable.LockedWidth = true;
            useCaseTable.KeepTogether = true;
            useCaseTable.SplitLate = false;

            var nameCell = CreateCell(chunk: new Chunk(string.Format("{0} ({1})",useCase.Name, useCase.UseCaseId), _sectionHeaderFont), paddingAll: padding, borderBottom:1);
            nameCell.BorderColorBottom = _baseColor;

            var descriptionCell = CreateCell(paddingAll: padding, borderAll: border);
            descriptionCell.AddElement(new Paragraph("Description", _sectionHeaderFont));
            descriptionCell.AddElement(new Paragraph(useCase.Description, _font));

            var rolesCell = CreateCell(paddingAll: padding, borderAll: border);
            rolesCell.AddElement(new Paragraph("Roles", _sectionHeaderFont));
            rolesCell.AddElement(new Paragraph(useCase.Roles, _font));

            // preconditions
            var preconditions = new List<HtmlElement>();
            preconditions.Add(new HtmlElement("ul", true));

            foreach (var a in useCase.Preconditions)
            {
                // opening li
                preconditions.Add(new HtmlElement("li", true));

                // put in the txt
                preconditions.Add(new HtmlElement(a.Description));

                // closing li
                preconditions.Add(new HtmlElement("li", true, true));
            }

            // close the list
            preconditions.Add(new HtmlElement("ul", true, true));

            // create the cell
            var preconditionCell = CreateCell(paddingAll: padding, borderAll: border);
            preconditionCell.AddElement(new Paragraph("Preconditions", _sectionHeaderFont));
            BuildListObject(preconditions, "ul", preconditionCell);

            // postconditions
            var postconditions = new List<HtmlElement>();
            postconditions.Add(new HtmlElement("ul", true));

            foreach (var a in useCase.Postconditions)
            {
                // opening li
                postconditions.Add(new HtmlElement("li", true));

                // put in the txt
                postconditions.Add(new HtmlElement(a.Description));

                // closing li
                postconditions.Add(new HtmlElement("li", true, true));
            }

            // close the list
            postconditions.Add(new HtmlElement("ul", true, true));

            // create the cell
            var postconditionCell = CreateCell(paddingAll: padding, borderAll: border);
            postconditionCell.AddElement(new Paragraph("Postconditions", _sectionHeaderFont));
            BuildListObject(postconditions, "ul", postconditionCell);

            // steps
            var steps = new List<HtmlElement>();
            steps.Add(new HtmlElement("ol", true));

            foreach (var a in useCase.UseCaseSteps.OrderBy(b => b.Order))
            {
                steps.Add(new HtmlElement("li", true));

                if (a.Optional)
                {
                    steps.Add(new HtmlElement(string.Format("{0}*", a.Description)));
                }
                else
                {
                    steps.Add(new HtmlElement(a.Description));
                }

                steps.Add(new HtmlElement("li", true, true));
            }

            // close the list
            steps.Add(new HtmlElement("ol", true, true));

            // create the cell
            var stepsCell = CreateCell(paddingAll: padding, borderAll: border);
            stepsCell.AddElement(new Paragraph("Steps", _sectionHeaderFont));
            BuildListObject(steps, "ol", stepsCell);
            stepsCell.PaddingBottom = 15;

            useCaseTable.AddCell(nameCell);
            useCaseTable.AddCell(descriptionCell);
            useCaseTable.AddCell(rolesCell);
            useCaseTable.AddCell(preconditionCell);
            useCaseTable.AddCell(postconditionCell);
            useCaseTable.AddCell(stepsCell);
            
            var cell = CreateCell();
            cell.AddElement(useCaseTable);
            return cell;

        }
        #endregion
        #endregion

        #region Watermark Functions
        public byte[] WatermarkPdf(byte[] pdf, string watermarkTxt)
        {
            return WriteToPdf(pdf, watermarkTxt);
        }
        /// <summary>
        /// Adds a watermark to all pages in the pdf, even takes into account orientation
        /// </summary>
        /// <remarks>
        /// http://footheory.com/blogs/donnfelker/archive/2008/05/11/using-itextsharp-to-watermark-write-text-to-existing-pdf-s.aspx
        /// </remarks>
        /// <param name="sourceFile"></param>
        /// <param name="stringToWriteToPdf"></param>
        /// <returns></returns>
        public byte[] WriteToPdf(byte[] pdf, string stringToWriteToPdf)
        {
            var reader = new PdfReader(pdf);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                //
                // PDFStamper is the class we use from iTextSharp to alter an existing PDF.
                //
                PdfStamper pdfStamper = new PdfStamper(reader, memoryStream);

                for (int i = 1; i <= reader.NumberOfPages; i++) // Must start at 1 because 0 is not an actual page.
                {
                    //
                    // If you ask for the page size with the method getPageSize(), you always get a
                    // Rectangle object without rotation (rot. 0 degrees)—in other words, the paper size
                    // without orientation. That’s fine if that’s what you’re expecting; but if you reuse
                    // the page, you need to know its orientation. You can ask for it separately with
                    // getPageRotation(), or you can use getPageSizeWithRotation(). - (Manning Java iText Book)
                    //   
                    //
                    Rectangle pageSize = reader.GetPageSizeWithRotation(i);

                    //
                    // Gets the content ABOVE the PDF, Another option is GetUnderContent(...)  
                    // which will place the text below the PDF content. 
                    //
                    PdfContentByte pdfPageContents = pdfStamper.GetUnderContent(i);
                    pdfPageContents.BeginText(); // Start working with text.

                    //
                    // Create a font to work with 
                    //
                    BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, Encoding.ASCII.EncodingName, false);
                    pdfPageContents.SetFontAndSize(baseFont, 100); // 40 point font
                    //pdfPageContents.SetRGBColorFill(255, 0, 0); // Sets the color of the font, RED in this instance
                    pdfPageContents.SetColorFill(BaseColor.LIGHT_GRAY);


                    //
                    // Angle of the text. This will give us the angle so we can angle the text diagonally 
                     // from the bottom left corner to the top right corner through the use of simple trigonometry. 
                    //
                    float textAngle =
                        (float) GetHypotenuseAngleInDegreesFrom(pageSize.Height, pageSize.Width);

                    //
                    // Note: The x,y of the Pdf Matrix is from bottom left corner. 
                    // This command tells iTextSharp to write the text at a certain location with a certain angle.
                    // Again, this will angle the text from bottom left corner to top right corner and it will 
                    // place the text in the middle of the page. 
                    //
                    pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, stringToWriteToPdf,
                                                    pageSize.Width/2,
                                                    pageSize.Height/2,
                                                    textAngle);

                    pdfPageContents.EndText(); // Done working with text
                }
                pdfStamper.FormFlattening = true; // enable this if you want the PDF flattened. 
                pdfStamper.Close(); // Always close the stamper or you'll have a 0 byte stream. 


                return memoryStream.ToArray();
            }
        }
        /// <summary>
        /// Calculates the angle for the watermark
        /// </summary>
        /// <remarks>
        /// http://footheory.com/blogs/donnfelker/archive/2008/05/11/using-itextsharp-to-watermark-write-text-to-existing-pdf-s.aspx
        /// </remarks>
        /// <param name="opposite"></param>
        /// <param name="adjacent"></param>
        /// <returns></returns>
        private double GetHypotenuseAngleInDegreesFrom(double opposite, double adjacent)
        {
            //http://www.regentsprep.org/Regents/Math/rtritrig/LtrigA.htm
            // Tan <angle> = opposite/adjacent
            // Math.Atan2: http://msdn.microsoft.com/en-us/library/system.math.atan2(VS.80).aspx 

            double radians = Math.Atan2(opposite, adjacent); // Get Radians for Atan2
            double angle = radians*(180/Math.PI); // Change back to degrees
            return angle;
        }
        #endregion

        #region Merge Pdfs
        public byte[] PdfMerger(List<byte[]> files)
        {
            int pageOffset = 0;
            int f = 0;

            Document document = null;
            PdfCopy writer = null;
            var ms = new MemoryStream();

            foreach (var file in files)
            {
                var reader = new PdfReader(file);
                reader.ConsolidateNamedDestinations();
                // we retrieve the total number of pages
                int n = reader.NumberOfPages;

                pageOffset += n;

                if (f == 0)
                {
                    // step 1: creation of a document-object
                    document = new Document(reader.GetPageSizeWithRotation(1));
                    // step 2: we create a writer that listens to the document
                    writer = new PdfCopy(document, ms);
                    // step 3: we open the document
                    document.Open();
                }
                // step 4: we add content
                for (int i = 0; i < n; )
                {
                    ++i;
                    if (writer != null)
                    {
                        PdfImportedPage page = writer.GetImportedPage(reader, i);
                        writer.AddPage(page);
                    }
                }
                PRAcroForm form = reader.AcroForm;
                if (form != null && writer != null)
                {
                    writer.CopyAcroForm(reader);
                }

                f++;
            }

            // step 5: we close the document
            if (document != null)
            {
                document.Close();
            }

            return ms.ToArray();
        }
        #endregion

    }

    #region Support Classes
    /// <summary>
    /// Used for applying the header and footer
    /// </summary>
    /// <remarks>
    /// Used for just how to use this event helper
    /// http://www.developerbarn.com/blogs/richyrich/32-using-itextsharp-generate-pdf-header-footer.html
    /// 
    /// For putting the Total number of pages
    /// http://www.mazsoft.com/blog/post/2008/04/30/Code-sample-for-using-iTextSharp-PDF-library.aspx
    /// </remarks>
    public class pdfPage : PdfPageEventHelper
    {
        private readonly Project _project;

        // template for the total page numbers
        private PdfTemplate template;
        private PdfContentByte cb;

        public pdfPage(Project project)
        {
            _project = project;
        }

        private Font _font = new Font(Font.FontFamily.TIMES_ROMAN, 12);

        /// <summary>
        /// Writes the header
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            // only put the header on the non cover pages
            if (document.PageNumber > 1)
            {
                var table = new PdfPTable(3);
                table.TotalWidth = CalculateWidth(document);

                var leftcell = new PdfPCell(new Phrase(string.Format("{0:MMMM dd, yyyy}", DateTime.Now), _font));
                var centercell = new PdfPCell(new Phrase(_project.Name));
                var rightcell = new PdfPCell(new Phrase(string.Empty));

                // format the cell
                leftcell.HorizontalAlignment = Element.ALIGN_LEFT;
                centercell.HorizontalAlignment = Element.ALIGN_CENTER;
                SetHeaderCellStyles(leftcell, centercell, rightcell);

                table.AddCell(leftcell);
                table.AddCell(centercell);
                table.AddCell(rightcell);

                // determine the location to place the object
                var y = document.LeftMargin; // move it inside the margins, width determined above in table width
                // put it 10 above the end of the top margin
                var x = document.PageSize.Height - document.TopMargin + 30;

                table.WriteSelectedRows(0, -1, y, x, writer.DirectContent);
            }
        }

        /// <summary>
        /// Writes the page footers
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            // only put the footer on the non cover pages
            if (document.PageNumber > 1)
            {
                var table = new PdfPTable(2);
                table.TotalWidth = CalculateWidth(document);

                var leftcell = new PdfPCell(new Phrase(string.Format("{0} of ", document.PageNumber), _font));
                var rightcell = new PdfPCell(new Phrase(_project.WorkgroupNames));

                leftcell.HorizontalAlignment = Element.ALIGN_LEFT;
                rightcell.HorizontalAlignment = Element.ALIGN_RIGHT;
                SetFooterCellStyles(leftcell, rightcell);

                table.AddCell(leftcell);
                table.AddCell(rightcell);

                var y = document.LeftMargin;
                var x = document.BottomMargin;

                table.WriteSelectedRows(0, -1, y, x, writer.DirectContent);

                // add this in for the total # of pages
                cb.AddTemplate(template, 67, 33.5f);
            }
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            cb = writer.DirectContent;
            template = cb.CreateTemplate(50, 50);

            base.OnOpenDocument(writer, document);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            template.BeginText();
            template.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED), 12);
            template.SetTextMatrix(0, 0);
            template.ShowText((writer.PageNumber - 1).ToString());
            template.EndText();

            base.OnCloseDocument(writer, document);
        }

        /// <summary>
        /// Calculates the width of the header/footer style
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private float CalculateWidth(Document document)
        {
            return document.PageSize.Width - (document.LeftMargin + document.RightMargin);
        }

        private void SetHeaderCellStyles(params PdfPCell[] cells)
        {
            foreach (var cell in cells)
            {
                cell.BorderWidthTop = 0;
                cell.BorderWidthLeft = 0;
                cell.BorderWidthRight = 0;
                cell.PaddingBottom = 10;
            }
        }

        private void SetFooterCellStyles(params PdfPCell[] cells)
        {
            foreach (var cell in cells)
            {
                cell.BorderWidthBottom = 0;
                cell.BorderWidthLeft = 0;
                cell.BorderWidthRight = 0;
                cell.PaddingTop = 10;
            }
        }
    }

    public class HtmlElement
    {
        public HtmlElement(string value, bool isElement = false, bool isclose = false)
        {
            Value = value;
            IsElement = isElement;
            IsClose = isclose;
        }

        public string Value { get; set; }

        /// <summary>
        /// Whether or not it's an html element (not text)
        /// </summary>
        public bool IsElement { get; set; }

        /// <summary>
        /// Is this a closing element
        /// </summary>
        public bool IsClose { get; set; }

        public Phrase Phrase { get; set; }
    }
    #endregion
}