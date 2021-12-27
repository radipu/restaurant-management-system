using System;
using System.Collections.Generic;
using System.Data;

using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;
using System.Windows.Forms;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.BLL
{
    public class PostCodeBLL
    {
        public List<PostCodeModel> GetPostCodeInformation()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                PostCodeDAO aPostCodeDao = new PostCodeDAO();
                return aPostCodeDao.GetPostCodeInformation();
            }
            else
            {
                MySqlPostCodeDAO aPostCodeDao = new MySqlPostCodeDAO();
                return aPostCodeDao.GetPostCodeInformation();
            }

        }
        public PostCodeModel GetPostCodeInformation(string postCode)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                PostCodeDAO aPostCodeDao = new PostCodeDAO();
                return aPostCodeDao.GetPostCodeInformationByPostCode(postCode);

            }
            else
            {
                MySqlPostCodeDAO aPostCodeDao = new MySqlPostCodeDAO();
                return aPostCodeDao.GetPostCodeInformationByPostCode(postCode);
            }

        }
        public void UpdatePostcode(RestaurantUsers aRestaurantUsers)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                PostCodeDAO aPostCodeDao = new PostCodeDAO(); aPostCodeDao.UpdatePostcode(aRestaurantUsers);
            }
            else
            {
                MySqlPostCodeDAO aPostCodeDao = new MySqlPostCodeDAO();
                aPostCodeDao.UpdatePostcode(aRestaurantUsers);
            }

        }

        public void UpdateFormattedAddressInPostcode(PostCodeModel aPostCodeModel)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                PostCodeDAO aPostCodeDao = new PostCodeDAO();
                aPostCodeDao.UpdateFormattedAddressInPostcode(aPostCodeModel);
            }
            else
            {
                MySqlPostCodeDAO aPostCodeDao = new MySqlPostCodeDAO();
                aPostCodeDao.UpdateFormattedAddressInPostcode(aPostCodeModel);
            }
        }

        public string GetFormattedAddress(string postcode)
        {
            // Post the data to the right place.
            string url = "https://maps.googleapis.com/maps/api/geocode/json?key=AIzaSyApix2ESHbE0pgbE1hyuVKgDHOEisTsgZs&address=" + postcode;
            Uri target = new Uri(url);
            WebRequest request = WebRequest.Create(target);

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";


            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        result = readStream.ReadToEnd();
                    }

                }
            }
            // dynamic stuff = JObject.Parse(result);

            dynamic array = (((JsonConvert.DeserializeObject(result)) as dynamic)["results"]);
            string item = "";
            int inc = 1;
            foreach (JObject ject in array)
            {
                if (inc == 1)
                {
                    item += ject["formatted_address"].ToString();
                    return item;

                }

                inc++;
            }
            return item;

        }

        public string GetFormattedAddressByPostcode(string postcode)
        {
            // Post the data to the right place.
            string url = GlobalVars.apiUrl + "postcode/get_postcode_details?postcode=" + postcode.Replace(" ", "");
            Uri target = new Uri(url);
            WebRequest request = WebRequest.Create(target);

            request.Method = "GET";
            request.Headers.Add("x-api-key", "muzahid");
            request.ContentType = "application/json";
            var FULL_ADDRESS = "";
            string result = string.Empty;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            result = readStream.ReadToEnd();
                        }

                    }
                }

                var jo = JObject.Parse(result);
                FULL_ADDRESS = jo["FULL_ADDRESS"].ToString();

            } catch (Exception ex) {
                FULL_ADDRESS = "";
            }

            return FULL_ADDRESS;

        }


        private List<Postcode> ReaderToReadPostcodeJson(JArray _postcode)
        {
            //_postcode = _postcode.ToArray();
            List<Postcode> postcodeArray = new List<Postcode>();
            foreach (var oReader in _postcode)
            {
                Postcode arcs_postcode = new Postcode();
                if (oReader["id"] != null)
                {
                    arcs_postcode.Id = Convert.ToInt32(oReader["id"]);
                }

                if (oReader["HouseName"] != null)
                {
                    arcs_postcode.HouseName = Convert.ToString(oReader["HouseName"]);
                }

                if (oReader["HouseNumber"] != null)
                {
                    arcs_postcode.HouseNumber = Convert.ToString(oReader["HouseNumber"]);
                }

                if (oReader["AddressLine1"] != null)
                {
                    arcs_postcode.AddressLine1 = Convert.ToString(oReader["AddressLine1"]);
                }

                if (oReader["AddressLine2"] != null)
                {
                    arcs_postcode.AddressLine2 = Convert.ToString(oReader["AddressLine2"]);
                }

                if (oReader["Town"] != null)
                {
                    arcs_postcode.Town = Convert.ToString(oReader["Town"]);
                }

                if (oReader["Postcode"] != null)
                {
                    arcs_postcode.PostCode = Convert.ToString(oReader["Postcode"]);
                }
                if (oReader["latitude"] != null)
                {
                    arcs_postcode.Latitude = Convert.ToString(oReader["latitude"]);
                }
                if (oReader["longitude"] != null)
                {
                    arcs_postcode.Longitude = Convert.ToString(oReader["longitude"]);
                }

                postcodeArray.Add(arcs_postcode);
            }
            return postcodeArray;
        }

        public Postcode GetAddressInformation(string _houseNo, string _address, string _postcode)
        {
            Postcode text = new Postcode();
            MySqlPostCodeDAO aPostCodeDao = new MySqlPostCodeDAO();
            text =  aPostCodeDao.getAddressFromLocalDb(_address,_postcode);

            if (text.Longitude == null)
            {
                string liveData = "";
                try
                {
                    var _postString = new { address = _address, houseNo = _houseNo, postcode = _postcode };
                    string postString = JsonConvert.SerializeObject(_postString);

                    Uri target = new Uri(GlobalVars.apiUrl + "postcode/address_details");
                    var request = (HttpWebRequest)WebRequest.Create(target);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = postString.Length;
                    request.Headers.Add("x-api-key", "muzahid");
                    StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                    requestWriter.Write(postString);
                    requestWriter.Close();

                    using (var response = request.GetResponse())
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            liveData = reader.ReadToEnd();
                        }
                    }

                    var jo = JObject.Parse(liveData);
                    text.Latitude = jo["latitude"].ToString();
                    text.Longitude = jo["longitude"].ToString();

                    if (text.Latitude != null)
                    {
                        CustomerAddress _newaddress = new CustomerAddress
                        {
                            CustomerId = 0,
                            HouseNumber = _houseNo,
                            Address = _address,
                            Town = "",
                            PostCode = _postcode.Replace(" ", String.Empty),
                            Latitude = text.Latitude,
                            Longitude = text.Longitude
                        };
                        aPostCodeDao.insertCustomerAddress(_newaddress);
                    }
                }
                catch (Exception ex)
                { }


            }
            return text;
        }

        public void GetPostcodeByCovarageArea()
        {
            MySqlPostCodeDAO aPostCodeDao = new MySqlPostCodeDAO();
            aPostCodeDao.createAddressTable();
            string text = "";
          NewProgressBar bar = new NewProgressBar(100);
          bar.Show();
          try
          {
             
              bar.progressBar(10);
              CoverageAreaBLL aCoverageAreaBll = new CoverageAreaBLL(); 
              List<AreaCoverage> aAreaCoverages = aCoverageAreaBll.GetCoverageArea();
              string cArea = "";
              foreach (AreaCoverage cover in aAreaCoverages)
              {
                  cArea +=  cover.Postcode + ",";
              }
              cArea = cArea.Substring(0, cArea.Length - 1);
              string necArea = cArea.Replace(",", @""",""");

             string postString = @"{""area"":[""" + necArea + "\"]}".ToString().Replace(@"\", "");
              
              //string postString = @"{""area"":[""RM11DL"",""RM11DR"",""RM12QD""]}";
               Uri target = new Uri(GlobalVars.apiUrl + "postcode/get_postcode_by_area");
             //   Uri target = new Uri("http://tomafood-net.test/api/v1/postcode/get_postcode_by_area");
                var request = (HttpWebRequest)WebRequest.Create(target);
              request.Method = "POST";
              request.ContentType = "application/json";
              request.ContentLength = postString.Length;
              request.Headers.Add("x-api-key", "muzahid");
              StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
              requestWriter.Write(postString);
              requestWriter.Close();
              bar.progressBar(20);
              using (var response = request.GetResponse())
              {
                  using (var reader = new StreamReader(response.GetResponseStream()))
                  {
                      text = reader.ReadToEnd();
                  }
              }               
          }
          catch (Exception ex)
          {          
          }
          bar.progressBar(50);
          string json = text;

          try
          {
              json = text.Replace("\\", "");
              json = text.Replace("[\"", "");
              json = text.Replace("\"]", "");
              JArray objects = JArray.Parse(json);
              List<Postcode> postcode_array = ReaderToReadPostcodeJson(objects);
                //  DatabaseProcessbar processbar = new DatabaseProcessbar(File.ReadAllText("Config/postcode.txt").ToString(), json);
                //  processbar.TopLevel = false;
                //  loadingPannel.Controls.Add(processbar);
                //  processbar.Dock = DockStyle.Fill;
                //  processbar.Show();
                //  MySqlPostcode MySqlPostcode = new MySqlPostcode();
                //  MySqlPostcode.InsertPostcodeArray(postcode_array);
                //  MySqlPostcode.InsertPostcodeData(json);
                if (postcode_array.Count() > 0)
                {
                   
                    bar.progressBar(60);
                    foreach (Postcode postcode in postcode_array)
                    {
                        CustomerAddress _address = new CustomerAddress
                        {
                            CustomerId = 0,
                            HouseNumber = postcode.HouseNumber,
                            Address = postcode.AddressLine1,
                            Town = postcode.Town,
                            PostCode = postcode.PostCode.Replace(" ", String.Empty),
                            Latitude = postcode.Latitude,
                            Longitude = postcode.Longitude
                        };
                        aPostCodeDao.insertCustomerAddress(_address);
                    }
                }

                bar.progressBar(100);
          }
          catch (Exception exception)
          {
              bar.Close();
              ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
              aErrorReportBll.SendErrorReport(exception.ToString());
          }
      }

        internal bool insertCustomerAddress(int customerId, string orderAddress)
        {
            MySqlCustomerDAO aCustomerDao = new MySqlCustomerDAO();
            RestaurantUsers _aResUser = aCustomerDao.GetUserByUserId(customerId);

            MySqlPostCodeDAO aPostCodeDao = new MySqlPostCodeDAO(); 
            Postcode _orderPostcode = GetAddressInformation(_aResUser.House, _aResUser.Address, _aResUser.Postcode.Replace(" ", String.Empty));
            if (_orderPostcode.Latitude != null) {
                CustomerAddress _address = new CustomerAddress
                {
                    CustomerId = customerId,
                    HouseNumber = _aResUser.House,
                    Address = _aResUser.Address,
                    Town = _aResUser.City,
                    PostCode = _aResUser.Postcode.Replace(" ", String.Empty),
                    Latitude = _orderPostcode.Latitude,
                    Longitude = _orderPostcode.Longitude
                };
                return aPostCodeDao.insertCustomerAddress(_address);
            }
            return false;
        }
    }
}
