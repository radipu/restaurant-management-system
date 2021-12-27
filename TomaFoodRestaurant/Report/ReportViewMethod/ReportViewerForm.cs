using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Report.ReportViewMethod
{
    public partial class ReportViewerForm : Form
    {
        public ReportViewerForm()
        {
            InitializeComponent();
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {

            this.reportView.RefreshReport();
        }
    }
}
