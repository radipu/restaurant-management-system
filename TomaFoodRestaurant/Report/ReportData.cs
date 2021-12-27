using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Report
{
    public class ReportData
    {
        
        public int  Index { get; set; }
        public string Name { get; set; }
        public int ItemQty { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }

        public int Qty { get; set; }
        public string ItemName { get; set; }
        public string Header { get; set; }

        public string FontName { get; set; }
        public string FontSize { get; set; }
        public string OptionName { get; set; }
        public double Discount { get; set; }
        public double DeliveryCharge { get; set; }
        public double CardFee { get; set; }
        public string DiscountPercent { get; set; }
        public string Group { get; set; }
        public int RowHeight { get; set; }
        public string GroupTitle { get; set; }
        public string Footer { get; set; }
        public string Id { get; set; }
        public int CatId { get; set; }
        public int SortOrder { get; set; }

        public int ReceipeTypeId { get; set; }
        public string KitchineOption { get; set; }
    }
}
