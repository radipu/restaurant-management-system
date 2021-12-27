using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{

      public class StringTokenizer
      {


          private int CurrIndex;
          private int NumTokens;
          private ArrayList tokens;
          private string StrSource;
          private string StrDelimiter;


          public StringTokenizer(string source, string delimiter)
          {
              this.tokens = new ArrayList(10);
              this.StrSource = source;
              this.StrDelimiter = delimiter;

              if (delimiter.Length == 0)
              {
                  this.StrDelimiter = " ";
              }
              this.Tokenize();
          }


          public StringTokenizer(string source, char[] delimiter)
              : this(source, new string(delimiter))
          {
          }


          public StringTokenizer(string source)
              : this(source, "")
          {
          }

          public StringTokenizer()
              : this("", "")
          {
          }
          private void Tokenize()
          {
              string TempSource = this.StrSource;
              string Tok = "";
              this.NumTokens = 0;
              this.tokens.Clear();
              this.CurrIndex = 0;

              if (TempSource.IndexOf(this.StrDelimiter) < 0 && TempSource.Length > 0)
              {
                  this.NumTokens = 1;
                  this.CurrIndex = 0;
                  this.tokens.Add(TempSource);
                  this.tokens.TrimToSize();
                  TempSource = "";
              }
              else if (TempSource.IndexOf(this.StrDelimiter) < 0 && TempSource.Length <= 0)
              {
                  this.NumTokens = 0;
                  this.CurrIndex = 0;
                  this.tokens.TrimToSize();
              }
              while (TempSource.IndexOf(this.StrDelimiter) >= 0)
              {

                  if (TempSource.IndexOf(this.StrDelimiter) == 0)
                  {
                      if (TempSource.Length > this.StrDelimiter.Length)
                      {
                          TempSource = TempSource.Substring(this.StrDelimiter.Length);
                      }
                      else
                      {
                          TempSource = "";
                      }
                  }
                  else
                  {
                      Tok = TempSource.Substring(0, TempSource.IndexOf(this.StrDelimiter));
                      this.tokens.Add(Tok);
                      if (TempSource.Length > (this.StrDelimiter.Length + Tok.Length))
                      {
                          TempSource = TempSource.Substring(this.StrDelimiter.Length + Tok.Length);
                      }
                      else
                      {
                          TempSource = "";
                      }
                  }
              }

              if (TempSource.Length > 0)
              {
                  this.tokens.Add(TempSource);
              }
              this.tokens.TrimToSize();
              this.NumTokens = this.tokens.Count;
          }

          public void NewSource(string newSrc)
          {
              this.StrSource = newSrc;
              this.Tokenize();
          }

          public void NewDelim(string newDel)
          {
              if (newDel.Length == 0)
              {
                  this.StrDelimiter = " ";
              }
              else
              {
                  this.StrDelimiter = newDel;
              }
              this.Tokenize();
          }
          public void NewDelim(char[] newDel)
          {
              string temp = new String(newDel);
              if (temp.Length == 0)
              {
                  this.StrDelimiter = " ";
              }
              else
              {
                  this.StrDelimiter = temp;
              }
              this.Tokenize();
          }

          public int CountTokens()
          {
              return this.tokens.Count;
          }

          public bool HasMoreTokens()
          {
              if (this.CurrIndex <= (this.tokens.Count - 1))
              {
                  return true;
              }
              else
              {
                  return false;
              }
          }
          public string NextToken()
          {
              String RetString = "";
              if (this.CurrIndex <= (this.tokens.Count - 1))
              {
                  RetString = (string)tokens[CurrIndex];
                  this.CurrIndex++;
                  return RetString;
              }
              else
              {
                  return null;
              }
          }
          public string Source
          {
              get
              {
                  return this.StrSource;
              }
          }
          public string Delim
          {
              get
              {
                  return this.StrDelimiter;
              }
          }
    }
}
