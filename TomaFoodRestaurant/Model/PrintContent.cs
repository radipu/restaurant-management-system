using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
  public  class PrintContent
    {
        private String m_sStringLine;
        private bool m_bIsNewLine;
        public PrintContent()
        {
            m_sStringLine = String.Empty;

        }

        public String StringLine
        {
            get { return m_sStringLine; }
            set { m_sStringLine = value; }
        }
    }
}
