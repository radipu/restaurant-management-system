using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.Model
{
    public class CutomeTileItem:TileItem
    {
        public int all_recipe { get; set; }
        public int PackageId { set; get; }
        public string CategoryId { set; get; }
        public double AddPrice { set; get; }
        public string SubCategory { set; get; }
        public int Items { set; get; }
        public string OptionName { set; get; }
        public int AllRecipe { set; get; }
        public int RecipeTypeId { set; get; }
        public int ShowOption { set; get; }
        public int SortOrder { set; get; }
        public List<FreeOptionMD> FreeOptionMds { set; get; }
        public RecipePackageButton RecipePackage { set; get; }
        public string SubCategoryId { get; set; }
        public PackageCategoryButton PackageCategoryButton { get; set; }
    }
}
