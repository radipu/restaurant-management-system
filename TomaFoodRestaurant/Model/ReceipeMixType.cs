using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
    public class ReceipeMixType
    {
        public ReceipeCategoryButton CategoryButton { get; set; }
        public ReceipeTypeButton ReceipeTypeButton { get; set; }

        public int SortOder { get; set; }
        public string Type { get; set; }

    }
}
