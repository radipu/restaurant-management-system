using NReco.PdfGenerator;
using RawPrint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
    class NewPrint
    {
        internal static void printRecipt(string str, string printerName, int printCopy, int numOfLine =15)
        {
            //var Renderer = new IronPdf.HtmlToPdf();


            //Renderer.PrintOptions.SetCustomPaperSizeInInches(2.61, numOfLine*0.6);
            //Renderer.PrintOptions.PrintHtmlBackgrounds = false;
            //Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            //Renderer.PrintOptions.Title = "Recipt print";
            //Renderer.PrintOptions.EnableJavaScript = false;
            //Renderer.PrintOptions.RenderDelay = 50; //ms
            //Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Screen;
            //Renderer.PrintOptions.DPI = 300;
            //Renderer.PrintOptions.FitToPaperWidth = true;
            //Renderer.PrintOptions.JpegQuality = 80;
            //Renderer.PrintOptions.GrayScale = false;
            //Renderer.PrintOptions.FitToPaperWidth = true;//Renderer.PrintOptions.InputEncoding = System.Text.Encoding.UTF8;
            //Renderer.PrintOptions.Zoom = 100;
            //Renderer.PrintOptions.CreatePdfFormsFromHtml = true;
            //Renderer.PrintOptions.MarginTop = 0;  //millimeters
            //Renderer.PrintOptions.MarginLeft = 0;  //millimeters
            //Renderer.PrintOptions.MarginRight = 0;  //millimeters
            //Renderer.PrintOptions.MarginBottom = 0;  //millimeters
            //Renderer.PrintOptions.FirstPageNumber = 1; //use 2 if a coverpage  will be appended
            //var PDF = Renderer.RenderHtmlAsPdf(str);
            var OutputPath = "print.pdf";
            //PDF.SaveAs(OutputPath); 
            string executableName = Application.ExecutablePath;
            FileInfo executableFileInfo = new FileInfo(executableName);
            string executableDirectoryName = executableFileInfo.DirectoryName;
            string Filepath = executableDirectoryName + "\\" + OutputPath;




           // string output_path_pdf = HttpContext.Server.MapPath("~/PDF_RESULT/print.pdf");

            HtmlToPdfConverter pdfConverter = new HtmlToPdfConverter();
            pdfConverter.PageWidth = 68;
            pdfConverter.PageHeight = numOfLine * 11;
            pdfConverter.Margins = new PageMargins { Top = 0, Bottom = 0, Left = 0, Right = 0 };
         //   pdfConverter.GeneratePdfFromFiles(new string[] { URL }, null, output_path_pdf);
            pdfConverter.TempFilesPath = executableDirectoryName;
            pdfConverter.GeneratePdf(str,null,"print.pdf");
             
         
          //  string Filepath = executableDirectoryName + "\\" + OutputPath;
            // The name of the PDF that will be printed (just to be shown in the print queue)
            string Filename = OutputPath;

            IPrinter iPrinter = new Printer();
            // Print the file
            for (int i = 0; i < printCopy; i++)
            {
                iPrinter.PrintRawFile(printerName, Filepath, Filename);
            }



        }
  
    }
}
