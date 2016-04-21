using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketches
{
    class Program
    {
        static void Main(string[] args)
        {

            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            path += "\\Downloads\\iA_Mortgage_Payment_Calculator.pdf";
            PdfReader reader = new PdfReader(path);
            String PDFContents = PdfTextExtractor.GetTextFromPage(reader, 1);
            Console.WriteLine("File length:" + reader.FileLength);
            Console.WriteLine("No. Of pages" + reader.NumberOfPages);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();

            for (int i = 1; i < reader.NumberOfPages; i++)
            {
                sb.Clear();
                sb = sb.Append(PdfTextExtractor.GetTextFromPage(reader, i, its));
                Console.WriteLine("Contents of Page{0}:{1}", i, sb);
            }
            try
            {
                reader.Close();
            }
            catch
            {
                Console.WriteLine("Exception Occured while reading PDF File....!");
            }
        }
    }
}
    

