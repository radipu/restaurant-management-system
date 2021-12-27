using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
    public class TextDocument : PrintDocument
    {
        private string[] text;

        public string[] Text;

        public int PageNumber;

        public int Offset;

        public TextDocument(string str)
        {
            int count = 0;
            StringReader countLine = new StringReader(str);
            while (countLine.ReadLine() != null)
            {
                count++;
            }

            this.Text = new string[count + 1];

            //this.Text = new string[1000];

            StringReader reader = new StringReader(str);
            string line;
            int i = 0; ;
            while ((line = reader.ReadLine()) != null)
            {

                this.Text[i] += line;
                i++;
            }
        }

        public TextDocument(string str, int pagelength)
        {
            int count = 0;
            StringReader countLine = new StringReader(str);
            while (countLine.ReadLine() != null)
            {
                count++;
            }

            this.Text = new string[count + 1];

            StringReader reader = new StringReader(str);
            string line;
            int i = 0; ;
            while ((line = reader.ReadLine()) != null)
            {

                this.Text[i] += line;
                i++;
            }
        }

    }
}
