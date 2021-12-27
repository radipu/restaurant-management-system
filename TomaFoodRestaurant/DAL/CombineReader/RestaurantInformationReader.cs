using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.CombineReader
{
    public class RestaurantInformationReader
    {
        public RestaurantInformation ReadRestaurantInformation(DataTable oReader, int i)
        {
            RestaurantInformation arcs_restaurant = new RestaurantInformation();

            arcs_restaurant.Id = Convert.ToInt32(oReader.Rows[i]["id"]);

            arcs_restaurant.RestaurantCategoryId = Convert.ToInt32(oReader.Rows[i]["restaurant_category_id"]);

            arcs_restaurant.ThankYouMsg = Convert.ToString(oReader.Rows[i]["theme_name"]);

            arcs_restaurant.Owner = Convert.ToInt32(oReader.Rows[i]["owner"]);

            arcs_restaurant.RestaurantType = Convert.ToString(oReader.Rows[i]["restaurant_type"]);

            arcs_restaurant.RestaurantName = Convert.ToString(oReader.Rows[i]["restaurant_name"]);

            arcs_restaurant.RestaurantSummary = Convert.ToString(oReader.Rows[i]["restaurant_summary"]);

            arcs_restaurant.House = Convert.ToString(oReader.Rows[i]["house"]);

            arcs_restaurant.Address = Convert.ToString(oReader.Rows[i]["address"]);

            arcs_restaurant.City = Convert.ToString(oReader.Rows[i]["city"]);

            arcs_restaurant.Country = Convert.ToString(oReader.Rows[i]["county"]);

            arcs_restaurant.Postcode = Convert.ToString(oReader.Rows[i]["postcode"]);

            arcs_restaurant.Phone = Convert.ToString(oReader.Rows[i]["phone"]);

            arcs_restaurant.Fax = Convert.ToString(oReader.Rows[i]["fax"]);

            arcs_restaurant.Email = Convert.ToString(oReader.Rows[i]["email"]);

            arcs_restaurant.Website = Convert.ToString(oReader.Rows[i]["website"]);

            arcs_restaurant.Status = Convert.ToString(oReader.Rows[i]["status"]);

            arcs_restaurant.Vat = Convert.ToDouble(oReader.Rows[i]["vat"]);

            arcs_restaurant.MenuMaxRow = Convert.ToInt32(oReader.Rows[i]["menu_max_row"]);

            arcs_restaurant.Expire = Convert.ToInt64(oReader.Rows[i]["expire"]);

            arcs_restaurant.PackageMaxRow = Convert.ToInt32(oReader.Rows[i]["package_max_row"]);

            arcs_restaurant.VatRegNo = Convert.ToString(oReader.Rows[i]["vat_reg_no"]);

            arcs_restaurant.Logo = Convert.ToString(oReader.Rows[i]["logo"]);

            arcs_restaurant.MenuDrag = Convert.ToInt64(oReader.Rows[i]["menu_drag"]);

            arcs_restaurant.MinOrder = Convert.ToDouble(oReader.Rows[i]["min_order"]);

            arcs_restaurant.DeliveryFrom = Convert.ToString(oReader.Rows[i]["delivery_from"]);

            arcs_restaurant.DeliveryTo = Convert.ToString(oReader.Rows[i]["delivery_to"]);

            arcs_restaurant.CollectionFrom = Convert.ToString(oReader.Rows[i]["collection_from"]);

            arcs_restaurant.CollectionTo = Convert.ToString(oReader.Rows[i]["collection_to"]);

            try
            {
                //string time = Convert.ToString(oReader.Rows[i]["delivery_time"]);

                //string mynumber = Regex.Replace(time, @"\D", "");
                //arcs_restaurant.DeliveryTime = Convert.ToDouble(mynumber);
                arcs_restaurant.DeliveryTime = Convert.ToString(oReader.Rows[i]["delivery_time"]);


            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }



            arcs_restaurant.IsHalal = Convert.ToInt64(oReader.Rows[i]["is_halal"]);

            arcs_restaurant.ReportClosingHour = Convert.ToInt32(oReader.Rows[i]["report_closing_hour"]);

            arcs_restaurant.ReportClosingMin = Convert.ToInt32(oReader.Rows[i]["report_closing_min"]);

            arcs_restaurant.Url = Convert.ToString(oReader.Rows[i]["url"]);

            arcs_restaurant.ThankYouMsg = Convert.ToString(oReader.Rows[i]["thank_you_msg"]);

            arcs_restaurant.DiscountType = Convert.ToString(oReader.Rows[i]["discount_type"]);

            arcs_restaurant.DeliveryCharge = Convert.ToDouble(oReader.Rows[i]["delivery_charge"]);

            arcs_restaurant.DescriptionText = Convert.ToString(oReader.Rows[i]["description"]);

            arcs_restaurant.CardFee = Convert.ToDouble(oReader.Rows[i]["card_fee"]);

            arcs_restaurant.CardMinOrder = Convert.ToDouble(oReader.Rows[i]["card_min_order"]);

            arcs_restaurant.IsBusy = Convert.ToInt32(oReader.Rows[i]["is_busy"]);

            arcs_restaurant.PaymentOption = Convert.ToString(oReader.Rows[i]["payment_option"]);

            arcs_restaurant.MinOrderDelivery = Convert.ToDouble(oReader.Rows[i]["min_order_delivery"]);

            arcs_restaurant.ServiceOption = Convert.ToString(oReader.Rows[i]["service_option"]);

            arcs_restaurant.DefaultOrderType = Convert.ToString(oReader.Rows[i]["default_order_type"]);

            arcs_restaurant.InLogo = Convert.ToString(oReader.Rows[i]["in_logo"]);

            arcs_restaurant.RecieptOption = Convert.ToString(oReader.Rows[i]["reciept_option"]);

            arcs_restaurant.MenuSeparation = Convert.ToInt32(oReader.Rows[i]["menu_separation"]);
            arcs_restaurant.CollectionTime = Convert.ToString(oReader.Rows[i]["collection_time"]);
            arcs_restaurant.ServerCallButton = Convert.ToInt64(oReader.Rows[i]["server_call_button"]);
            arcs_restaurant.PreOrder = Convert.ToInt64(oReader.Rows[i]["pre_order"]);
            arcs_restaurant.ShowOptionInline = Convert.ToInt64(oReader.Rows[i]["show_option_inline"]);
            arcs_restaurant.ExcludeDiscount = Convert.ToString(oReader.Rows[i]["exclude_discount"]);

            arcs_restaurant.RecieptFont = Convert.ToString(oReader.Rows[i]["reciept_font"]);

            arcs_restaurant.ConfirmPayment = Convert.ToInt64(oReader.Rows[i]["confirm_payment"]);

            arcs_restaurant.UpdateRequired = Convert.ToInt32(oReader.Rows[i]["update_required"]);

            arcs_restaurant.CurrentVersion = Convert.ToString(oReader.Rows[i]["current_version"]);

            arcs_restaurant.ShowOrderNumber = Convert.ToInt64(oReader.Rows[i]["show_order_number"]);

            arcs_restaurant.DefaultOrderStatus = Convert.ToString(oReader.Rows[i]["default_order_status"]);

            arcs_restaurant.PrintCopy = Convert.ToInt32(oReader.Rows[i]["print_copy"]);

            arcs_restaurant.UseJava = Convert.ToInt64(oReader.Rows[i]["use_java"]);

            arcs_restaurant.LocalIp = Convert.ToString(oReader.Rows[i]["local_ip"]);

            arcs_restaurant.RecieptMinHeight = Convert.ToInt32(oReader.Rows[i]["reciept_min_height"]);

            arcs_restaurant.DelPrintCopy = Convert.ToInt32(oReader.Rows[i]["del_print_copy"]);

            arcs_restaurant.DineInPrintCopy = Convert.ToInt32(oReader.Rows[i]["in_print_copy"]);

            arcs_restaurant.MultiplePart = Convert.ToInt32(oReader.Rows[i]["multiple_part"]);

            //   require_served
            //try
            //{

            //    if (oReader["sync_type"] != DBNull.Value)
            //    {
            //        arcs_restaurant.SyncType = Convert.ToString(oReader["sync_type"]);  
            //    }
            //}
            //catch (Exception exception)
            //{
            //    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
            //    aErrorReportBll.SendErrorReport(exception.ToString());
            //}

            try
            {

                arcs_restaurant.RequireServed = Convert.ToInt32(oReader.Rows[i]["require_served"]);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            arcs_restaurant.IsServiceCharge = Convert.ToInt32(oReader.Rows[i]["is_service_charge"]);

            arcs_restaurant.IsSyncOrder = Convert.ToInt32(oReader.Rows[i]["Is_sync_order"]);

            arcs_restaurant.IsSyncCustomer = Convert.ToInt32(oReader.Rows[i]["Is_sync_customer"]);

            return arcs_restaurant;}

    }
}
