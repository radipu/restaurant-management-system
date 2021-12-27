using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Report
{
    public partial class ReportPrintViewer : Form
    {
        public ReportPrintViewer()
        {
            InitializeComponent();
        }

        private void ReportPrintViewer_Load(object sender, EventArgs e)
        {

            this.reportViewer.RefreshReport();
        }
    }
}
