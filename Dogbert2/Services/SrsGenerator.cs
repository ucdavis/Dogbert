using System;
using System.Collections.Generic;
using System.IO;
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
    public class SrsGenerator : ISrsGenerator
    {
        private readonly IRepository<ProjectText> _projectTextRepository;
        private readonly IRepository<File> _fileRepository;

        public SrsGenerator(IRepository<ProjectText> projectTextRepository, IRepository<File> fileRepository)
        {
            _projectTextRepository = projectTextRepository;
            _fileRepository = fileRepository;
        }

        // base color
        private CMYKColor _baseColor = new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f);

        // standard body font
        private Font _font = new Font(Font.FontFamily.TIMES_ROMAN, 10);
        private Font _boldFont = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.BOLD);
        private Font _italicFont = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.ITALIC);
        private Font _headerFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f));
        private Font _captionFont = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.NORMAL, new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f));

        // fonts for the cover page
        private BaseFont _dateBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
        private Font _coverProjNameFont = new Font(Font.FontFamily.HELVETICA, 30f, Font.BOLD, BaseColor.BLACK);
        private Font _coverSmallFont = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD, new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f));
        private Font _coverFont = new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD, new CMYKColor(0.9922f, 0.4264f, 0.0000f, 0.4941f));
        private Font _disclaimerFont = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD, BaseColor.LIGHT_GRAY);

        // width of the content
        private float _pageWidth;
       
        /// <summary>
        /// Generates a pdf document
        /// </summary>
        /// <remakrs>
        /// http://itextsharp.com/
        /// http://www.mikesdotnetting.com/Article/80/Create-PDFs-in-ASP.NET-getting-started-with-iTextSharp
        /// </remakrs>
        /// <param name="project"></param>
        /// <returns></returns>
        public byte[] GeneratePdf(Project project)
        {
            var doc = new iTextSharp.text.Document(PageSize.LETTER, 36 /* left */, 36 /* right */, 62 /* top */, 52 /* bottom */);

            // set the variable for the page's actual content size
            _pageWidth = doc.PageSize.Width - (doc.LeftMargin + doc.RightMargin);

            var ms = new MemoryStream();
            var writer = PdfWriter.GetInstance(doc, ms);

            // add in the header/footer
            writer.PageEvent = new pdfPage(project);

            doc.Open();

            // add information
            AddCoverpage(writer.DirectContent, doc, project);

            foreach (var txt in project.ProjectTexts.OrderBy(b => b.TextType.Order))
            {
                AddSectionHeader(doc, txt.TextType.Name);
                AddHtmlText(doc, txt.Text);
            }

            doc.Close();

            var bytes = ms.ToArray();
            return bytes;
        }

        #region Cover Page
        private void AddCoverpage(PdfContentByte cb, Document doc, Project project)
        {
            // adds in the date box
            DrawBoxForCover(cb, doc);            

            // add in the worker information
            AddWorkerInformation(cb, doc, project);

            // add in the project and client information
            AddProjectInformation(cb, doc, project);

            // add the disclaimer information
            AddDisclaimerInformation(cb, doc, project);

            doc.NewPage();
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

        public void AddSectionHeader(Document document, string title)
        {
            var table = new PdfPTable(1);
            table.TotalWidth = _pageWidth;
            table.LockedWidth = true;
            
            var cell = new PdfPCell();
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
            document.Add(table);
        }

        #region Html Text
        private List<string> blockTags = new List<string>() { "p", "ul", "ol" };
        private List<string> listTags = new List<string>() { "ul", "ol" };
        
        /// <summary>
        /// Parse out the html and format it using iTextSharp's stuff
        /// </summary>
        /// <remarks>
        /// http://blog.dmbcllc.com/2009/07/28/itextsharp-html-to-pdf-parsing-html/ (not being used)
        /// </remarks>
        /// <param name="text"></param>
        private void AddHtmlText(Document doc, string text)
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
                                var paragraph = BuildListObject(elements, reader.Name, doc);
                                doc.Add(paragraph);

                                elements.Clear();
                            }
                            else
                            {
                                var paragraph = BuildDocObject(elements, doc);
                                doc.Add(paragraph);

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

        private Paragraph BuildDocObject(List<HtmlElement> elements, Document document)
        {
            var docObjs = new Stack<HtmlElement>();
            var paragraph = new Paragraph();

            // set spacing
            paragraph.SpacingBefore = 5;
            paragraph.FirstLineIndent = 36;

            foreach (var a in elements)
            {
                var obj = HandleTag(docObjs, a, document);

                if (obj != null) paragraph.Add(obj);
            }
            return paragraph;
        }
       
        private List BuildListObject(List<HtmlElement> elements, string listType, Document document)
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
                var obj = HandleTag(docObjs, a, document);
                if (obj != null)
                {
                    list.Add(new ListItem(obj));
                }
            }

            return list;
        }

        /// <summary>
        /// Generates new phrase element with all sub formatting
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private Phrase HandleTag(Stack<HtmlElement> elements, HtmlElement element, Document document)
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
                var result = ScanForImage(document, element.Value);

                obj.Phrase.Add(result);

                // put it back on the stack because we're not done
                elements.Push(obj);
            }

            return null;
        }
        #endregion

        #region Image
        public string ScanForImage(Document document, string txt)
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
                    AddImage(document, fileId);
                }

            }

            // replace all {Image-*} tags with string empty
            var rgx = new Regex("{Image-.+}");
            var result = rgx.Replace(txt, string.Empty);

            return result;
        }
        public void AddImage(Document document, int fileId)
        {
            var table = new PdfPTable(1);
            //table.TotalWidth = _pageWidth;
            //table.LockedWidth = true;
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

                // scale the image
                img.ScaleToFit(_pageWidth, 250f);

                var paragraph = new Paragraph(file.Caption, _captionFont);
                paragraph.Alignment = Element.ALIGN_CENTER;

                cell.AddElement(img);
                cell.AddElement(paragraph);
            }

            table.AddCell(cell);
            document.Add(table);
        }
        #endregion

        /// <summary>
        /// Adds in some demo text to fill a page
        /// </summary>
        /// <param name="doc"></param>
        private void AddDemoText(Document doc)
        {
            var stanardFont = new Font(Font.FontFamily.TIMES_ROMAN, 11);

            doc.Add(new Paragraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla massa augue, aliquet scelerisque sodales eu, porttitor eu libero. Aenean auctor sapien sit amet libero convallis quis commodo nisl suscipit. Cras dapibus commodo orci, placerat rhoncus lorem suscipit in. Mauris libero elit, auctor eget dignissim nec, feugiat eu dolor. Nullam adipiscing massa sit amet ipsum posuere egestas. Quisque est turpis, tempor vel blandit non, venenatis id nunc. Nam ornare nulla quis erat viverra eleifend dictum purus ornare. Curabitur eget dui nec enim euismod imperdiet sed eget elit. Nulla quis nunc sem, a imperdiet tortor. Phasellus lectus turpis, malesuada eget laoreet hendrerit, dignissim at nunc. Etiam in tortor nisi, eu tempor nunc. Ut interdum ipsum eget nunc laoreet pellentesque. Nunc consectetur, purus sit amet posuere scelerisque, velit eros aliquam quam, non mollis neque orci aliquam elit. In hac habitasse platea dictumst. In non erat id lorem pellentesque tempus id ac magna. Vestibulum enim metus, convallis vitae varius a, blandit in lorem.", stanardFont));
            doc.Add(new Paragraph("Pellentesque mattis luctus laoreet. Nullam magna massa, dapibus et adipiscing ut, mattis et ligula. Nunc pretium posuere nisi, sed dapibus magna malesuada ut. Aenean lacinia ipsum non lorem dignissim at facilisis eros tincidunt. Nam eleifend, erat id aliquet ullamcorper, turpis lorem blandit nulla, in pulvinar velit diam non justo. Ut justo lorem, hendrerit quis hendrerit eu, tincidunt at sapien. Sed iaculis quam vitae neque auctor vel aliquet dui dictum. Nam vulputate eros est. Nullam aliquam metus quis metus aliquam ornare. Aenean ultricies felis at quam venenatis vel mollis diam facilisis. Aenean rutrum odio non nulla congue facilisis. Quisque non elit a sem fringilla dignissim eget at sem. Donec vulputate eros in libero tincidunt tincidunt. Fusce vitae mi et lorem imperdiet pulvinar. Nunc ac tellus urna. Nunc sapien magna, mollis nec imperdiet sit amet, rutrum eget turpis. Morbi vitae eros turpis. Vestibulum non massa non purus condimentum fermentum eget quis turpis.", stanardFont));
            doc.Add(new Paragraph("Cras ac enim et neque rutrum accumsan sit amet non mauris. Curabitur tempor adipiscing eros in hendrerit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ac lobortis nisl. Donec pretium rutrum enim sagittis pulvinar. Aliquam vitae mauris at tellus dictum facilisis vel nec ante. Integer neque risus, placerat a pharetra ac, placerat sit amet mi. In sed ipsum felis, nec porta dolor. Aliquam non nulla non risus bibendum lobortis. Integer pretium, mi vitae accumsan blandit, est leo luctus libero, eget aliquet dolor neque a augue. Nullam id purus ipsum, in ultricies leo.", stanardFont));
            doc.Add(new Paragraph("Vestibulum hendrerit interdum orci, a faucibus enim pellentesque non. Ut non lobortis erat. Aliquam vestibulum augue sit amet libero scelerisque et blandit libero suscipit. Nunc nulla eros, aliquet eget mollis nec, iaculis quis tortor. Cras sapien enim, posuere sollicitudin blandit ut, auctor vitae augue. Mauris aliquam, ligula ac commodo ultricies, libero risus viverra erat, at dapibus dolor nibh ultrices massa. Morbi urna eros, fringilla sit amet fermentum ullamcorper, posuere consequat ante. Maecenas justo ligula, aliquam nec malesuada et, posuere ac magna. Duis eget tellus turpis. Maecenas diam justo, varius ut pulvinar ac, lacinia nec lectus. Nam ultricies mattis urna ullamcorper lacinia. In volutpat iaculis lacus, sit amet posuere eros fermentum non. Phasellus a magna quam, eget gravida erat. Morbi laoreet lectus in justo blandit ullamcorper. Proin a cursus ante. Mauris ultrices eros tristique velit viverra semper. Mauris vel risus faucibus lacus condimentum ullamcorper id eget mauris.", stanardFont));
            doc.Add(new Paragraph("Vestibulum aliquet consectetur turpis. Morbi est sapien, interdum eu dignissim dignissim, aliquam id est. Ut ligula ante, placerat bibendum tincidunt non, elementum eget magna. Ut consectetur sem a est malesuada eleifend. Etiam a nibh at dui sagittis scelerisque. Curabitur sit amet nibh lectus, sed sagittis nunc. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Vestibulum facilisis urna at odio rutrum elementum. Curabitur metus lorem, feugiat a rutrum id, hendrerit eget justo. Integer blandit tincidunt dui nec sodales. Nulla gravida viverra eros et iaculis. Duis massa metus, ornare nec bibendum ut, rhoncus nec sapien. Donec libero augue, tristique vel mollis in, mollis ac risus. Nam luctus, est sit amet imperdiet vulputate, orci turpis tincidunt ipsum, vel vulputate massa quam ut orci. Nullam turpis mauris, aliquet faucibus tincidunt quis, hendrerit sed nibh. Proin pellentesque tincidunt nibh, ut posuere nunc gravida rhoncus. Ut tincidunt lacinia arcu in tristique. Maecenas congue lacinia tristique. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae;", stanardFont));
            doc.Add(new Paragraph("Curabitur ac lacus vel urna tempus ullamcorper. Aenean non magna vitae diam semper euismod nec non lacus. Praesent facilisis, sapien id dignissim mattis, neque neque faucibus sem, ac rutrum neque enim a ligula. Curabitur aliquet venenatis libero, a porttitor dui venenatis ut. Phasellus ligula purus, tincidunt laoreet mattis in, faucibus id nisl. Curabitur in dui non risus faucibus faucibus. Donec mi lectus, aliquam sed mattis id, placerat sed diam. Curabitur eu purus arcu. Donec accumsan purus quis metus imperdiet at dictum urna pretium. Proin id faucibus nibh. Pellentesque non venenatis lectus. Praesent convallis nunc eget quam luctus mattis. Vestibulum vitae quam justo. Ut consequat condimentum urna, sit amet laoreet enim scelerisque nec. Maecenas convallis, augue id fringilla varius, ipsum ipsum semper lectus, eu tincidunt turpis tortor vitae ante. Morbi eu erat non quam tristique lobortis ac sed arcu. Vivamus sapien quam, mattis at fringilla at, sagittis vel diam.", stanardFont));
            doc.Add(new Paragraph("Suspendisse potenti. Sed euismod mauris sit amet elit gravida interdum. Aliquam eleifend diam et nisl lacinia convallis at non lorem. Morbi nisl arcu, pellentesque id egestas eget, tincidunt non odio. Curabitur facilisis lorem et metus convallis facilisis. Vestibulum mollis pretium urna vel facilisis. Aliquam mattis libero quis tellus congue sit amet vestibulum massa molestie. Morbi consectetur lacus in elit vehicula in vehicula nibh placerat. Cras pharetra mi non odio sollicitudin non eleifend turpis luctus. Ut fringilla ultricies lorem, ac ullamcorper justo interdum sit amet. Morbi vehicula nibh eget felis consequat pulvinar. Integer interdum ullamcorper tempus. Donec ut pharetra purus. Mauris odio ante, porttitor non faucibus eget, lacinia non turpis. Cras tincidunt ante et nulla ultrices non venenatis est aliquam. Etiam ut sapien turpis, a porttitor turpis. Fusce sit amet urna at magna consequat bibendum sit amet vitae turpis. Etiam sollicitudin pulvinar sem, at blandit purus placerat in. Donec bibendum volutpat sapien, vel iaculis risus condimentum non. Mauris vestibulum est at tortor vehicula nec eleifend ligula viverra.", stanardFont));
            doc.Add(new Paragraph("Integer auctor vestibulum dui sed consectetur. Aliquam erat volutpat. Fusce ultrices, dui sit amet tincidunt vestibulum, enim tellus sodales leo, quis iaculis purus nisi id velit. Nunc lacus dolor, viverra at consectetur vel, rutrum ut ante. Donec nec purus at ipsum gravida posuere vel sit amet elit. Maecenas malesuada eleifend pharetra. Cras est urna, sodales in iaculis a, pellentesque nec augue. Nulla facilisi. Integer a diam nibh. Cras sed sapien massa. Integer lacus urna, condimentum vitae vulputate id, tempor eget magna. Sed id erat in ante venenatis malesuada non sit amet purus.", stanardFont));
        }
    }

    /// <summary>
    /// Used for applying the header and footer
    /// </summary>
    /// <remarks>
    /// http://www.developerbarn.com/blogs/richyrich/32-using-itextsharp-generate-pdf-header-footer.html
    /// </remarks>
    public class pdfPage : PdfPageEventHelper
    {
        private readonly Project _project;

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

                var leftcell =
                    new PdfPCell(new Phrase(string.Format("{0} of {1}", document.PageNumber, "unknown"), _font));
                var rightcell = new PdfPCell(new Phrase(_project.WorkgroupNames));

                leftcell.HorizontalAlignment = Element.ALIGN_LEFT;
                rightcell.HorizontalAlignment = Element.ALIGN_RIGHT;
                SetFooterCellStyles(leftcell, rightcell);

                table.AddCell(leftcell);
                table.AddCell(rightcell);

                var y = document.LeftMargin;
                var x = document.BottomMargin;

                table.WriteSelectedRows(0, -1, y, x, writer.DirectContent);
            }
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
}