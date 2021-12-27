using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TomaFoodRestaurant.Model
{
 public   class OptionJsonConverter
    {
     
     public string Serialize(List<OptionJson> jsons)
     {
        var json= JsonConvert.SerializeObject(jsons);

         return json;
     }
     public List<OptionJson> DeSerialize(string strInput)
     {
         try
         {
             var json = JsonConvert.DeserializeObject<List<OptionJson>>(strInput);
             return json;
         }
         catch (Exception)
         {
             List<OptionJson> newJoList=new List<OptionJson>();
             newJoList.Add(new OptionJson
             {
                 optionName = strInput,NoOption = false,optionId = "1",optionPrice = 0.00,optionQty = 1
             });

             return newJoList;
         }
        

        }
    }
}
