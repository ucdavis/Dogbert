using System.Collections.Generic;
using Dogbert2.Core.Domain;

namespace Dogbert2.Services
{
    public interface IPdfService
    {
        /// <summary>
        /// SRS Generator
        /// </summary>
        /// <param name="project">Project to generate Srs from</param>
        /// <param name="draft">Draft Indicator</param>
        /// <returns>Generated Srs</returns>
        byte[] GeneratePdf(Project project, bool draft);
        /// <summary>
        /// Watermark the pdf file
        /// </summary>
        /// <param name="pdf">Pdf file to watermark</param>
        /// <param name="watermarkTxt">Watermark text</param>
        /// <returns>Watermarked file</returns>
        byte[] WatermarkPdf(byte[] pdf, string watermarkTxt);
        /// <summary>
        /// Merge pdf files
        /// </summary>
        /// <param name="files">Files to be merged together</param>
        /// <returns>Merged file</returns>
        byte[] PdfMerger(List<byte[]> files);
    }
}
