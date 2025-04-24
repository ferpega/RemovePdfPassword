using iText.Kernel.Pdf;
using System.Security;

namespace RemovePdfPassword
{
    public class RemovePdfPasswordService
    {
        public void Execute(string filename, string password, string outputfile)
        {
            var writerProps = new WriterProperties();
            var readerProps = new ReaderProperties()
                .SetPassword(System.Text.Encoding.UTF8.GetBytes(password));

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }
            if (!Path.Exists(Path.GetDirectoryName(outputfile)))
            {
                throw new FileNotFoundException(outputfile);
            }

            using (var reader = new PdfReader(filename, readerProps))
            using (var writer = new PdfWriter(outputfile, writerProps))
            using (var pdfDoc = new PdfDocument(reader, writer))
            {
            }
        }
    }
}
