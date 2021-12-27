﻿using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;

namespace TomaFoodRestaurant.Model
{
    public class PrintToPrinter : Form //Basher
    {

        public void PrintReceipt(string str, PrinterSetup printer, int printCopy = 1)
        {
            for (int i = printCopy; i >= 1; i--) {
                GeneratePdfDevExpressPrintableComponentLink(str, printer);
            }
        }

        public void GeneratePdfDevExpressPrintableComponentLink_old(string content, PrinterSetup printer)
        {
            CompositeLink link = new CompositeLink(new DevExpress.XtraPrinting.PrintingSystem());
            PrintableComponentLink pcLink = new PrintableComponentLink();

            string htmlString = "<html><body><table cellspacing='5' border=1 ><tr><td>";
            htmlString = htmlString + content; 
            htmlString = htmlString + "</td></tr></table></body></html>";

            RichEditControl rec = new RichEditControl();
            rec.Size = new Size(240, 1000);
            rec.HtmlText = htmlString;

            Section section = rec.Document.Sections[0];
            section.Margins.Left = printer.printerMargin;
            section.Margins.Top = 2;
            section.Margins.Right = 2;
            section.Margins.Bottom = 30;

            pcLink.Component = rec;
            link.Links.Add(pcLink);
            link.ShowPreview();

            try
            {
                link.Print(printer.PrinterName);
            }
            catch (InvalidPrinterException exc)
            {
                MessageBox.Show("Error: " + exc.Message.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }

        }
   
        public void GeneratePdfDevExpressPrintableComponentLink(string content, PrinterSetup printer)
        {
            RichEditControl rec = new RichEditControl();
            rec.Size = new Size(240, 1000);
            rec.HtmlText = content;

            Section section = rec.Document.Sections[0];

            section.Margins.Left = 10;  //printer.printerMargin;
            section.Margins.Top = 2;
            section.Margins.Right = 2;
            section.Margins.Bottom = 30; 

            var printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem();
            var printableComponentLink1 = new PrintableComponentLink();

            printingSystem1.ShowPrintStatusDialog = false;
            printingSystem1.ShowMarginsWarning = false;
 
            printingSystem1.Links.AddRange(new object[] { printableComponentLink1 });
            printableComponentLink1.Component = rec;           
            try
            {
                Console.WriteLine("printing");
                Console.WriteLine(DateTime.Now.ToString("HH : mm : ss"));
                printableComponentLink1.Print(printer.PrinterName);
                Console.WriteLine("after print");
                Console.WriteLine(DateTime.Now.ToString("HH : mm : ss"));
            }
            catch (InvalidPrinterException exc)
            {
                MessageBox.Show("Error: " + exc.Message.ToString());
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }
      
    }
}
