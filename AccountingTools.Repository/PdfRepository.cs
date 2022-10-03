using AccountingTools.Repository.Interface;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Text;

namespace AccountingTools.Repository
{
    public class PdfRepository : IPdfRepository
    {
        public List<string> RedPdf(MemoryStream pdf, string password = null)
        {
            pdf.Position = 0;
            List<string> pages = new List<string>();
            ReaderProperties readerProperties = new ReaderProperties();
            if (password != null)
                readerProperties.SetPassword(Encoding.ASCII.GetBytes(password));

            var document = new PdfDocument(new PdfReader(pdf, readerProperties));
            for (int numerOfPage = 1; numerOfPage <= document.GetNumberOfPages(); numerOfPage++)
            {
                PdfPage page = document.GetPage(numerOfPage);
                var text = PdfTextExtractor.GetTextFromPage(page);
                pages.Add(text);
            }
            return pages;
        }
    }
}
