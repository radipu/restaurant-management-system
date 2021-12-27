using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq; 
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.Report.ReportViewMethod;

namespace TomaFoodRestaurant.Report
{
    public class DynamicReportMethod
    {
        private PrintReport reportPrint;
        public ReportViewer CommonMethodview(string ReportPath, ReportDataSource aSource, string name, List<ReportParameter> parameter, ReportViewerForm aReportViewerUi, bool IsPrint)
        {
            aReportViewerUi = new ReportViewerForm();
            reportPrint = new PrintReport(); 
            aReportViewerUi.reportView.LocalReport.ReportEmbeddedResource = ReportPath;
            aSource.Name = aSource.Name;
            aSource.Value = aSource.Value;
            aReportViewerUi.reportView.LocalReport.DataSources.Clear();
            aReportViewerUi.reportView.LocalReport.DataSources.Add(aSource);
            aReportViewerUi.reportView.LocalReport.SetParameters(parameter.ToList());
            aReportViewerUi.reportView.LocalReport.Refresh();
            aReportViewerUi.reportView.RefreshReport();
            
            // T For Testing 
            if (IsPrint)
            {
               // aReportViewerUi.reportView.d;
             //   aReportViewerUi.reportView.PrintDialog();

                //aReportViewerUi.Show();
               reportPrint.PrintToPrinter(aReportViewerUi.reportView.LocalReport);
            
            }
            return aReportViewerUi.reportView;



        }

        GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
        GlobalUrl urls = new GlobalUrl();
        public ReportViewer GetReceipprint(ReportDataSource source, List<ReportData> parList, bool IsPrint)
        {
            List<ReportParameter> parameter = new List<ReportParameter>();
            urls = aGlobalUrlBll.GetUrls();

            string recieptFnt = GlobalSetting.RestaurantInformation.RecieptFont;
            double ConvertPxToPT_font = (Convert.ToDouble(recieptFnt) * 1.5);

            string reciept_font = ((ConvertPxToPT_font * 72) / 70).ToString("F03");
            List<ReportData> data = (List<ReportData>)source.Value;
            data = data.OrderBy(a => a.SortOrder).ToList();
            foreach (ReportData s in parList){
                parameter.Add(new ReportParameter("Header", s.Header));
                parameter.Add(new ReportParameter("TotalPrice", s.TotalPrice.ToString("N2")));
                parameter.Add(new ReportParameter("Qty", s.ItemQty.ToString()));
                parameter.Add(new ReportParameter("FontName", "tahoma, sans-serif"));
                parameter.Add(new ReportParameter("FontSize", reciept_font + "pt"));
                parameter.Add(new ReportParameter("Discount", s.Discount.ToString("N2")));
                parameter.Add(new ReportParameter("DeliveryCharge", s.DeliveryCharge.ToString("N2")));
                parameter.Add(new ReportParameter("MenuSeperation", 0.ToString()));
                var MenuSeperation = GlobalSetting.RestaurantInformation;if (MenuSeperation.MenuSeparation == 3)
                {
                    parameter.Add(new ReportParameter("MenuSeperation", "3"));
                }
                string discount = "( % " + Convert.ToDouble(s.DiscountPercent).ToString("N2") + " ) ";
                if (s.DiscountPercent == "0.00" || s.DiscountPercent == "0")
                {
                    discount = " ";
                }
                parameter.Add(new ReportParameter("DiscountPercent", discount));
                parameter.Add(new ReportParameter("CardFee", s.CardFee.ToString("N2")));
                parameter.Add(new ReportParameter("Footer", s.Footer));

                int paperMinHeight = GlobalSetting.RestaurantInformation.RecieptMinHeight;
                string lineHeight = "0";
                if (data.Count < paperMinHeight)
                {

                    List<ReportData> emptyData = new List<ReportData>();
                    for (int i = 1; i < paperMinHeight - data.Count; i++)
                    {emptyData.Add(new ReportData() { RowHeight = i });

                    }
                    data = emptyData.Concat(data).ToList();
                    source.Value = data;
                    lineHeight = "1";}
                parameter.Add(new ReportParameter("ReportHeight", lineHeight));
                }source.Value = data.ToList();
            source.Name = "ReceiptDataSet";
            ReportViewerForm aReportViewer = new ReportViewerForm();
            string reportPage = @"TomaFoodRestaurant.Report.ReceiptPrint"+GlobalSetting.RestaurantInformation.MenuSeparation+".rdlc";
         
            return CommonMethodview(reportPage, source, "ReceiptDataSet", parameter.ToList(), aReportViewer, IsPrint);
        }

        public ReportViewer GetKichenPrint(ReportDataSource source, List<ReportData> parList, bool IsPrint)
        {
            List<ReportParameter> parameter = new List<ReportParameter>();
            urls = aGlobalUrlBll.GetUrls();

            string reciept_font = GlobalSetting.RestaurantInformation.RecieptFont;
            reciept_font = (Convert.ToDouble(reciept_font) * 2).ToString();List<ReportData> data = (List<ReportData>)source.Value;
            foreach (ReportData s in parList)
            {
                parameter.Add(new ReportParameter("Header", s.Header));
                parameter.Add(new ReportParameter("TotalPrice", s.TotalPrice.ToString("N2")));
                parameter.Add(new ReportParameter("Qty", s.ItemQty.ToString()));
                parameter.Add(new ReportParameter("FontName", "tahoma, sans-serif"));
                parameter.Add(new ReportParameter("FontSize", reciept_font + "pt"));
                parameter.Add(new ReportParameter("Discount", s.Discount.ToString("N2")));
                parameter.Add(new ReportParameter("DeliveryCharge", s.DeliveryCharge.ToString("N2")));
                parameter.Add(new ReportParameter("MenuSeperation", 0.ToString()));
                var MenuSeperation = GlobalSetting.RestaurantInformation;
                if (MenuSeperation.MenuSeparation == 3)
                {
                    parameter.Add(new ReportParameter("MenuSeperation", "3"));
                }
                string discount = "( % " + Convert.ToDouble(s.DiscountPercent).ToString("N2") + " ) ";
                if (s.DiscountPercent == "0.00" || s.DiscountPercent == "0")
                {
                    discount = " ";
                }
                parameter.Add(new ReportParameter("DiscountPercent", discount)); parameter.Add(new ReportParameter("CardFee", s.CardFee.ToString("N2")));
                parameter.Add(new ReportParameter("Footer", s.Footer));

                int paperMinHeight = GlobalSetting.RestaurantInformation.RecieptMinHeight;
                string lineHeight = "0";
                if (data.Count < paperMinHeight)
                {

                    List<ReportData> emptyData = new List<ReportData>();
                    for (int i = 1; i < paperMinHeight - data.Count; i++)
                    {
                        emptyData.Add(new ReportData() { RowHeight = i });

                    }
                    data = emptyData.Concat(data).ToList();
                    source.Value = data;
                    lineHeight = "1";
                }
                parameter.Add(new ReportParameter("ReportHeight", lineHeight));
            }
            source.Value = data.ToList();
            source.Name = "ReceiptDataSet";
            ReportViewerForm aReportViewer = new ReportViewerForm();
            string reportPage = @"TomaFoodRestaurant.Report.KichinePrint.KichinePrint" + GlobalSetting.RestaurantInformation.MenuSeparation + ".rdlc";

            return CommonMethodview(reportPage, source, "ReceiptDataSet", parameter.ToList(), aReportViewer, IsPrint);
        }

  
    
    }
}
