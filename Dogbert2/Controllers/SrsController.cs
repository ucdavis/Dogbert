using System;
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
            var doc = new iTextSharp.text.Document(PageSize.A4);
            var ms = new MemoryStream();
            var writer = PdfWriter.GetInstance(doc, ms);

            // add in the header/footer
            writer.PageEvent = new pdfPage(project);

            doc.Open();

            // add information
            doc.Add(new Paragraph("hello world"));
           
            doc.Close();

            var bytes = ms.ToArray();
            return bytes;
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

        /// <summary>
        /// Writes the header
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            var table = new PdfPTable(3);
            table.TotalWidth = document.PageSize.Width - (document.LeftMargin + document.RightMargin);

            var leftcell = new PdfPCell(new Phrase(string.Format("{0:MMMM dd, yyyy}", DateTime.Now)));
            var centercell = new PdfPCell(new Phrase(_project.Name));
            var rightcell = new PdfPCell(new Phrase(string.Empty));

            // format the cell
            leftcell.HorizontalAlignment = Element.ALIGN_LEFT;
            centercell.HorizontalAlignment = Element.ALIGN_CENTER;
            SetHeaderCellStyles(leftcell, centercell, rightcell);

            table.AddCell(leftcell);
            table.AddCell(centercell);
            table.AddCell(rightcell);
            table.WriteSelectedRows(0, -1, document.LeftMargin, (document.PageSize.Height - document.TopMargin), writer.DirectContent);
        }

        /// <summary>
        /// Writes the page footers
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            var table = new PdfPTable(2);
            table.TotalWidth = document.PageSize.Width - (document.LeftMargin + document.RightMargin);

            var leftcell = new PdfPCell(new Phrase(string.Format("{0} of {1}", document.PageNumber, "unknown")));
            var rightcell = new PdfPCell(new Phrase(_project.WorkgroupNames));

            leftcell.HorizontalAlignment = Element.ALIGN_LEFT;
            rightcell.HorizontalAlignment = Element.ALIGN_RIGHT;
            SetFooterCellStyles(leftcell, rightcell);

            table.AddCell(leftcell);
            table.AddCell(rightcell);

            table.WriteSelectedRows(0, -1, document.LeftMargin, (document.BottomMargin + document.BottomMargin), writer.DirectContent);
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
