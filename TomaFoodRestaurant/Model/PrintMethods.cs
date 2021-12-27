using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;

namespace TomaFoodRestaurant.Model
{public  class PrintMethods
    {
        private PrintDocument printDoc;

        private String printBody;
      //  private PrinterConfig printerConfig;
        public int pagesize = 0;
        private int fontSize = 16;
        PrintController printController;
        private bool kitchenCopy = false;
        private bool isPrint = false;
        private bool isBill = false;


        public PrintMethods(bool kitchen_Copy = false, bool isprint = false, int font_Size = 16, bool isBillPrint = false)
        {
            printDoc = new PrintDocument();
            kitchenCopy = kitchen_Copy;
            isPrint = isprint;
            fontSize = font_Size;
            printController = new StandardPrintController();
            printDoc.PrintController = printController;
            isBill = isBillPrint;
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);   
        }

       private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
       {

           try
           {
               Font printFont = new Font("Lucida Console", 13, FontStyle.Bold);
               Font printFont2 = new Font("Lucida Console", 13, FontStyle.Bold);

               //printFont = new Font("Inconsolata", 13, FontStyle.Regular);
               //printFont2 = new Font("Inconsolata", 13, FontStyle.Regular);

               int printHeight;
               int printWidth;

               if (kitchenCopy && !isPrint)
               {
                   printFont = new Font("Tahoma", fontSize, FontStyle.Bold);
                   printFont2 = new Font("Tahoma", fontSize, FontStyle.Bold);
               }


                if (isBill)
                {
                    printFont = new Font("Lucida Console", fontSize, FontStyle.Italic);
                    printFont2 = new Font("Lucida Console", fontSize, FontStyle.Italic);

                }


               printHeight = ((PrintDocument)sender).DefaultPageSettings.PaperSize.Height - ((PrintDocument)sender).DefaultPageSettings.Margins.Top - ((PrintDocument)sender).DefaultPageSettings.Margins.Bottom;
               printWidth = ((PrintDocument)sender).DefaultPageSettings.PaperSize.Width - ((PrintDocument)sender).DefaultPageSettings.Margins.Left - ((PrintDocument)sender).DefaultPageSettings.Margins.Right;
               RectangleF printArea = new RectangleF(0, 10, 300, printHeight);

               double hight = 0;


               TextDocument doc = new TextDocument(printBody);
               float lineHeight = printFont.GetHeight(e.Graphics);


               float x = e.MarginBounds.Left;
               float y = 0;
               if (pagesize == 0)
               {
                   y = 10;
               }
               else
               {
                   y = 0;
               }
               doc.Offset = pagesize;
               doc.PageNumber += 1; 

               while ((y + lineHeight) < e.MarginBounds.Bottom && doc.Offset <= doc.Text.GetUpperBound(0))
               {
                   if (kitchenCopy)
                   {
                       e.Graphics.DrawString(doc.Text[doc.Offset], printFont, Brushes.Black, 0, y);
                   }
                   else
                   {

                       e.Graphics.DrawString(doc.Text[doc.Offset], printFont, Brushes.Black, 0, y);
                   }
                   doc.Offset += 1;
                   y += lineHeight;
               }

               if (doc.Offset < doc.Text.GetUpperBound(0))
               {
                   e.HasMorePages = true;
               }
               else
               {
                   doc.Offset = 0;
               }
               pagesize = doc.Offset;
           }
           catch (Exception ex) {
                File.AppendAllText("Config/log.txt", " \n\n Printing issue  :: " + ex.Message.ToString() + "\n\n");

            }

       }






       public void USBPrint(String printBody, string printDestiNation, int printCopy = 1)
       {
            PrintFormat strPrintFormatter = new PrintFormat(23);
            this.printBody = printBody;

           try
           {
               printDoc.DefaultPageSettings.PrinterSettings.PrinterName = printDestiNation;
               for (int i = 1; i <= printCopy; i++) {
                   printDoc.Print();
               }
                   
           }
           catch (Exception ex)
           {
               MessageBox.Show("Printer is not working.");
           }


       }

      

    }
    
    
}
