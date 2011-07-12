﻿using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using iTextSharp.text;
using iTextSharp.text.pdf;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

using iTextSharp;
using iTextSharp.text.html;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Srs class
    /// </summary>
    public class SrsController : ApplicationController
    {
        private readonly IRepository<Project> _projectRepository;

        public SrsController(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Get the SRS for a project
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public FileResult Index(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            var pdf = GeneratePdf(project);
            return File(pdf, "application/pdf", "doc.pdf");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <remakrs>
        /// http://itextsharp.com/
        /// http://www.mikesdotnetting.com/Article/80/Create-PDFs-in-ASP.NET-getting-started-with-iTextSharp
        /// </remakrs>
        /// <param name="project"></param>
        /// <returns></returns>
        private byte[] GeneratePdf(Project project)
        {
            var doc = new iTextSharp.text.Document(PageSize.LETTER, 36 /* left */, 36 /* right */, 62 /* top */, 52 /* bottom */);

            var ms = new MemoryStream();
            var writer = PdfWriter.GetInstance(doc, ms);

            // add in the header/footer
            writer.PageEvent = new pdfPage(project);

            doc.Open();

            // add information
            AddDemoText(doc);
           
            doc.Close();

            var bytes = ms.ToArray();
            return bytes;
        }

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

        private void AddCoverpage(Document doc, Project project)
        {
            
        }

        private void AddSection(Document doc)
        {
            throw new NotImplementedException();
        }

        private void AddText(Document doc)
        {
            throw new NotImplementedException();            
        }

        private void AddHtmlText(Document doc)
        {
            throw new NotImplementedException();
        }

    }

    /// <summary>
    /// Used for applying the header and footer
    /// </summary>
    /// <remarks>
    /// http://www.developerbarn.com/blogs/richyrich/32-using-itextsharp-generate-pdf-header-footer.html
    /// </remarks>
    public class pdfPage : iTextSharp.text.pdf.PdfPageEventHelper
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
            var y = document.LeftMargin;    // move it inside the margins, width determined above in table width
            // put it 10 above the end of the top margin
            var x = document.PageSize.Height - document.TopMargin + 30;

            table.WriteSelectedRows(0, -1, y, x, writer.DirectContent);
        }

        /// <summary>
        /// Writes the page footers
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            var table = new PdfPTable(2);
            table.TotalWidth = CalculateWidth(document);

            var leftcell = new PdfPCell(new Phrase(string.Format("{0} of {1}", document.PageNumber, "unknown"), _font));
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
}
