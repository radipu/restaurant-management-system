using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraMap;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class DeliveryMaplocationview : Form
    {
        private List<DelOrderDataTable> DelOrders;

        public DeliveryMaplocationview(List<DelOrderDataTable> delList)
        {
            InitializeComponent();
            DelOrders = delList;
           
          //  gmap.MouseDown += GMap_MouseDown;
        }
        private void GMap_MouseDown(object sender, MouseEventArgs e)
        {
            gmap.MouseMove += GMap_MouseMove;
        }
        private void GMap_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //gMap.Position = new PointLatLng(X, Y);
        }
        private GMapMarker resLocation;
        GMapControl gmap = new GMapControl();
        private void CreateCircle(Double lat, Double lon, double radius)
        {
            GMapOverlay polyOverlay = new GMapOverlay("polygons");
            PointLatLng point = new PointLatLng(lat, lon);
            int segments = 1000;

            List<PointLatLng> gpollist = new List<PointLatLng>();

            for (int i = 0; i < segments; i++)
                gpollist.Add(FindPointAtDistanceFrom(point, i, radius / 1000));

            GMapPolygon gpol = new GMapPolygon(gpollist, "pol");

            gpol.Fill = new SolidBrush(Color.FromArgb(30, Color.GhostWhite));
            gmap.Overlays.Add(polyOverlay);
        }
        public static double DegreesToRadians(double degrees)
        {
            const double degToRadFactor = Math.PI / 180;
            return degrees * degToRadFactor;
        }
        public static double RadiansToDegrees(double radians)
        {
            const double radToDegFactor = 180 / Math.PI;
            return radians * radToDegFactor;
        }
        public static GMap.NET.PointLatLng FindPointAtDistanceFrom(GMap.NET.PointLatLng startPoint, double initialBearingRadians, double distanceKilometres)
        {
            const double radiusEarthKilometres = 6371.01;
            var distRatio = distanceKilometres / radiusEarthKilometres;
            var distRatioSine = Math.Sin(distRatio);
            var distRatioCosine = Math.Cos(distRatio);

            var startLatRad = DegreesToRadians(startPoint.Lat);
            var startLonRad = DegreesToRadians(startPoint.Lng);

            var startLatCos = Math.Cos(startLatRad);
            var startLatSin = Math.Sin(startLatRad);

            var endLatRads = Math.Asin((startLatSin * distRatioCosine) + (startLatCos * distRatioSine * Math.Cos(initialBearingRadians)));

            var endLonRads = startLonRad + Math.Atan2(
                          Math.Sin(initialBearingRadians) * distRatioSine * startLatCos,
                          distRatioCosine - startLatSin * Math.Sin(endLatRads));

            return new GMap.NET.PointLatLng(RadiansToDegrees(endLatRads), RadiansToDegrees(endLonRads));
        }
        private void DeliveryMaplocationview_Load(object sender, EventArgs e)
        {


            try
            {
              
                gmap.MapProvider = GMapProviders.OpenStreetMap;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;

                gmap.ShowCenter = true;
                gmap.CanDragMap = true;
                gmap.MinZoom = 1;
                gmap.MaxZoom = 25;
                gmap.Zoom = 12;
                gmap.CacheLocation = "@gmap/";
                List<PointLatLng> point = new List<PointLatLng>();
               // point.Add(new PointLatLng(54.906869, -1.383801));
               // point.Add(new PointLatLng(54.901671, -1.389269));

                GMapOverlay markers = new GMapOverlay("markers");
                 

                RestaurantInformation setting = GlobalSetting.RestaurantInformation;
                PostCodeBLL postcodeinfo = new PostCodeBLL();

                MySqlCustomerDAO aCustomerDao = new MySqlCustomerDAO();
             //   var resAddress = postcodeinfo.GetPostCodeInformation(setting.Postcode.Replace(" ", String.Empty));
                Postcode resAddress = postcodeinfo.GetAddressInformation(setting.House, setting.Address, setting.Postcode.Replace(" ", String.Empty));
                PointLatLng location = new PointLatLng(Convert.ToDouble(resAddress.Latitude), Convert.ToDouble(resAddress.Longitude));
                resLocation = new GMarkerGoogle(location, GMarkerGoogleType.green_pushpin);
               
                List<LocationInformation> shortpath = new List<LocationInformation>();

                foreach (DelOrderDataTable order in DelOrders.Where(a => a.Customer > 0)){

                    RestaurantUsers _aResUser = aCustomerDao.GetUserByUserId(order.Customer);
                    Postcode _orderPostcode = postcodeinfo.GetAddressInformation(_aResUser.House, _aResUser.Address,order.PostCode.Replace(" ", String.Empty));
                    if (_orderPostcode.Latitude != null)
                    {
                        PointLatLng posLatLng = new PointLatLng(Convert.ToDouble(_orderPostcode.Latitude), Convert.ToDouble(_orderPostcode.Longitude));

                        //double distance = GetDistance(pointLatLng, location);
                        GDirections ss;
                        var xx = GMapProviders.GoogleMap.GetDirections(out ss, location, posLatLng, false, false, false, false, false);
                        //GMapRoute r = new GMapRoute(ss.Route, "My route");
                        //GMapOverlay routesOverlay = new GMapOverlay("Myroutes");
                        //routesOverlay.Routes.Add(r);
                        //gmap.Overlays.Add(routesOverlay);
                        //r.Stroke.Width = 5;
                        //r.Stroke.DashStyle=DashStyle.Solid;
                        //r.Stroke.Color = Color.DodgerBlue;
                        shortpath.Add(new LocationInformation()
                        {
                            Distance = 4.89,//r.Distance,
                            PointLatLng = posLatLng,
                            orderData = order
                        });
                    }
                    //Thread.Sleep(1000);
                }
                int i = 0;
                foreach (LocationInformation locationInformation in shortpath.ToList())
                {
                    i++;
                    GMapMarker marker = new GMarkerGoogle(locationInformation.PointLatLng, GMarkerGoogleType.red);

                    gmap.Update();
                    marker.ToolTip = new GMapRoundedToolTip(marker);
                    string details = "";
                    if (locationInformation.orderData.FormatedAddress != null)
                    {                      
                     
                        if (locationInformation.orderData.FormatedAddress.Contains("@#@") && locationInformation.orderData.FormatedAddress != "")
                        {
                            String[] spearator = { "@#@" };
                            string[] address = locationInformation.orderData.FormatedAddress.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                            details = "\n" + address[0] + "\n" + address[1] + "\n" + "Total :" +
                                     locationInformation.orderData.Total + "\n" + "Distance :" + locationInformation.Distance.ToString("n2");
                        }
                        else
                        {
                            details = locationInformation.orderData.FormatedAddress;
                        }
                    } 
                    marker.ToolTipText = details;
                   
                    marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    gmap.Position = locationInformation.PointLatLng;
                    markers.Markers.Add(marker);
                    gmap.Overlays.Add(markers);

                }

                gmap.Position = location;
                markers.Markers.Add(resLocation);
                gmap.Overlays.Add(markers); 
                gmap.Dock = DockStyle.Fill;
                this.panel2.Controls.Add(gmap);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.GetBaseException().ToString());
                this.Activate();
    
            }

           
        }

        private void inputTextButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public class LocationInformation
    {
        public PointLatLng PointLatLng { get; set; }
        public double Distance { get; set; }
        public DelOrderDataTable orderData { get; set; }
    }

}
