using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace TomaFoodRestaurant.Report
{
    public  class PrintReport
    {

        private  int m_currentPageIndex;
        private  IList<Stream> m_streams;
     
        public  Stream CreateStream(string name,string fileNameExtension, Encoding encoding,string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        public  void Export(LocalReport report, bool print = true)
        {
            try
            {
                string deviceInfo =
             @"<DeviceInfo >
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>3.5in</PageWidth>
                <PageHeight>8.5in</PageHeight>
                <MarginTop>0.0in</MarginTop>
                <MarginLeft>0.0in</MarginLeft>
                <MarginRight>0.0in</MarginRight>
                <MarginBottom>0.0in</MarginBottom>
            </DeviceInfo>";

        //        string deviceInfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>EMF</OutputFormat>" +
        //"  <PageWidth>3.5in</PageWidth>" +
        // "  <PageHeight>8.83in</PageHeight>" +
        // "  <MarginTop>0.0in</MarginTop>" +// "  <MarginLeft>0.0in</MarginLeft>" +
        //  "  <MarginRight>0in</MarginRight>" +
        //  "  <MarginBottom>0in</MarginBottom>" +
        //"</DeviceInfo>";
                Warning[] warnings;
                m_streams = new List<Stream>();

                report.Render("Image", deviceInfo, CreateStream,out warnings);
              
                foreach (Stream stream in m_streams)
                {
                    stream.Position = 0;
                }
                
                if (print)
                {
                    Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }

        }


        // Handler for PrintPageEvents
        public  void PrintPage(object sender, PrintPageEventArgs ev)
        {
            float x = 1;
            float y = 0;
            float width = 300.0F; // max width I found through trial and error
            float height = 700F;
           
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            
            ev.Graphics.DrawImage(pageImage, new RectangleF(x,y,width,height));
           
            // y += ev.Graphics.MeasureString(pageImage, drawFontArial12Bold).Height;

            //// Prepare for the next page. Make sure we haven't hit the end.
          m_currentPageIndex++;
           ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
        string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }
            return string.Empty;
        }
        public  void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {

                printDoc.PrinterSettings.PrinterName = GetDefaultPrinter();
               
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public  void PrintToPrinter(LocalReport report)
        {
            Export(report);
        }

        public  void DisposePrint()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
    }

}
