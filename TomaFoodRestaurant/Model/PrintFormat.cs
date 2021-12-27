using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text;
using System.Text.RegularExpressions;

namespace TomaFoodRestaurant.Model
{
   public  class PrintFormat
    {
        private int lineLength = 0;
        private string dashedLine = "";
        private string solidLine = "";


        public PrintFormat(int lineLength =23)
        {
            this.lineLength = 23;
            string str = "";
            for (int i = 0; i <= lineLength; i++)
            {
                str += "-";
            }
            dashedLine = str;
            string str1 = "";
            for (int i = 0; i <= lineLength; i++)
            {
                str1 += "_";
            }
            solidLine = str1;
        }


        public string CreateDashedLine()
        {
          
            return dashedLine;
        }
        public string CreateDashedLineForKitchen()
        {
            string str = "";
            for (int i = 0; i <= 30; i++)
            {
                str += "-";
            }
            return str;
        }

        public string CreateSolidLine()
        {
            return solidLine;
        }

        public string CenterTextWithDashed(string text)
        {
            string str = dashedLine.Remove(0, text.Length);
            str = str.Insert(str.Length / 2, text);
            return str;
        }

        public string CenterTextWithWhiteSpace(string text)
        {
            string str = "";
            int lengh = 11 - (text.Length / 2);
            for (int i = 0; i < lengh; i++)
            {
                str += " ";
            }
            str += text;
           
            return str;
        }


        public string alignment_setting(string str, int align_length = 8, int blankSpace = 0)
        {
            try
            {
                string new_str = "", leftStr = "", rightStr = "";
                int full_langth = 23, rightAlignment = 0;
                int str_langth = str.Length;

                string blank_space = "";
                leftStr = str.Substring(0, (str_langth - align_length) - 1).Trim();
                rightAlignment = full_langth - align_length;

                rightStr = str.Substring(leftStr.Length, (str_langth - leftStr.Length)).Trim();
                for (int i = (leftStr.Length + blankSpace); i < rightAlignment; i++)
                {
                    blank_space += " ";
                }
                new_str = leftStr + blank_space + rightStr;
                if (blankSpace > 0)
                {
                    return "\r\n  " + new_str;
                }

                return new_str;
            }
            catch (Exception ex)
            {
                return "";

            }
        }

        public string get_leftString(string str)
        {
            string new_str = str;
            int full_langth = 23;
            int str_langth = str.Length;
            if (str_langth > 18)
            {
                new_str = str.Substring(0, 15) + " ..";
            }

            return new_str;
        }

        public string get_fullString(string str)
        {
            string newStr = "";

            string[] strArray = str.Split(' ');
            int arrayLeng = strArray.Length;
            int newLen = 0;
            for (int i = 0; i < arrayLeng; i++)
            {
                newLen += strArray[i].Length +1;
                if (newLen < 18)
                {
                    newStr += strArray[i] + " ";
                }
                else
                {
                    newLen = strArray[i].Length + 1;
                    newStr += "\n" + strArray[i] + " ";
                }
                
            }
            return newStr;
        }

        public string get_fullStringForkitchen(string str)
        {
            string newStr = "";

            string[] strArray = str.Split(' ');
            int arrayLeng = strArray.Length;
            int newLen = 0;
            for (int i = 0; i < arrayLeng; i++)
            {
                newLen += strArray[i].Length + 1;
                if (newLen < 24)
                {
                    newStr += strArray[i] + " ";
                }
                else
                {
                    newLen = strArray[i].Length + 1;
                    newStr += "\n" + strArray[i] + " ";
                }

            }
            return newStr;
        }


        public string get_alignmentString(string str, int priceLength=0)
        {
            string newStr = "";
            try
            {
                if (str.Length <= 23 & priceLength > 0)
                {
                    return alignment_setting(str.Trim(), priceLength);
                }
                string[] strArray = str.Split(' ');
                int arrayLeng = strArray.Length;
                int newLen = 0;
                int fixLen = 22;
                if (priceLength > 0)
                {
                    fixLen = 18;
                }
                for (int i = 0; i < arrayLeng; i++)
                {
                    newLen += strArray[i].Length + 1;
                    if (newLen < fixLen)
                    {
                        newStr += strArray[i] + " ";
                    }
                    else
                    {
                        newLen = (" " + " " + strArray[i] + " ").Length;
                        newStr += "\r\n" + " " + " " + strArray[i] + " ";
                    }
                }


                int index = newStr.LastIndexOf("\n");
                string nnn = newStr;

                if (priceLength == 0)
                {
                    return nnn;
                }
                if (index != -1 && index != 0)
                {
                    nnn = newStr.Substring(0, index - 2);
                    string last_line = newStr.Substring(index + 3, newStr.Length - (index + 3));
                    string newlast_line = last_line;
                    if (last_line.Trim().Length < 8)
                    {
                        int newIndex = nnn.LastIndexOf("\n");
                        newlast_line = nnn.Substring(newIndex + 3, nnn.Length - (newIndex + 3));
                        newlast_line += " " + last_line;
                        //if (newIndex < 3)
                        //{
                        //    nnn = "";
                        //}
                        //else {
                        nnn = newStr.Substring(0, newIndex - 2);
                        //}   


                    }

                    nnn += alignment_setting(newlast_line, priceLength, 2);
                }


                return nnn;

            }

            catch(Exception ex)
            {
                return str+" ";
            }
        }

    }
}
