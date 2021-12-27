using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
    public class ReceipeCategoryButton:Button
    {
        public int CategoryId { set; get; }
        public int ParentCategoryId { set; get; }
        public int ReceipeTypeId { set; get; }
        public int RestaurantId { set; get; }
        public int SortOrder { set; get; }
        public int MaxRow { set; get; }
        public string Color { set; get; }
        public int CategoryWidth { set; get; }
        public int CategoryHeight { set; get; }
        public string CategoryName { set; get; }
        public string Description { set; get; }
        public int HasSubcategory { set; get; }

    }
}
