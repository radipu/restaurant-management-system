using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
   public class ShopingCardModel
    {
       public double columnTotal { get; set; }
       public GeneralInformation aGeneralInformation { get; set; }
       public int columnQty { get; set; }
       public string customTotalTextBox { get; set; }
       public string btnShopingAdd { get; set; }
       public string totalAmountLabel { get; set; }
    }
}
