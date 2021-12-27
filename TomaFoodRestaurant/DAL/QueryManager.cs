using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL
{
  public  class QueryManager
  {
      public static string DbType = GlobalSetting.DbType;
     public static string GetRecipeByItemIdQuery(int recipeId)
      {

          if (DbType == "SQLITE")
          {
              return String.Format("SELECT * FROM rcs_recipe where id= {0};", recipeId);
          }
          else
          {
              return String.Format("SELECT * FROM rcs_recipe where id= {0};", recipeId);
          }
      }

     public static string GetRecipeOptionByOptionName(string optionName)
     {

         if (DbType == "SQLITE")
         {
             return String.Format("select *  FROM   rcs_option_item where title='{0}' ;", optionName);
         }
         else
         {
             return String.Format("select *  FROM   rcs_option_item where title='{0}' ;", optionName);
         }
     }
    }
}
