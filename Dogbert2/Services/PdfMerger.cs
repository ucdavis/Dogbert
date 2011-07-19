using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Dogbert2.Services
{
    /// <summary>
    /// Code based off from http://stackoverflow.com/questions/566899/is-there-a-straight-forward-way-to-append-one-pdf-doc-to-another-using-itextsharp
    /// </summary>
    public class PdfMerger
    {
        public static byte[] MergePdfs(List<byte[]> files)
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

            }

            // step 5: we close the document
            if (document != null)
            {
                document.Close();
            }

            return ms.ToArray();
        }
    }
}