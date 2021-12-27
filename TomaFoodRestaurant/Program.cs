using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.NewLoginForm;
using TomaFoodRestaurant.OtherForm; 

namespace TomaFoodRestaurant
{
    static class Program
    {

        private static string appGuid = "51a77807-8801-4fb7-9172-582cf754fb91";

        /// <summary>
        /// The main entry point for the application. </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

         

            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                try
                {
                    if (!mutex.WaitOne(0, false))
                    {
                        MessageBox.Show("TPOS already running.");
                        return;
                    }
                }
                catch (AbandonedMutexException ex)
                {
                    new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                    return;
                }                


                try
                {

                    if (Properties.Settings.Default.pcVersion == null)
                    {
                        if (GlobalSetting.DbType == null)
                        {
                            DBsetup dBsetup = new DBsetup(Form.ActiveForm);
                            dBsetup.ShowDialog();
                            return;
                        }
                    }

                    string backup = AppDomain.CurrentDomain.BaseDirectory + "user.config";
                    if (!File.Exists(backup))
                    {
                        Configuration objConfig;
                        objConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                        string sFolderPath = objConfig.FilePath;
                        FileInfo myFile = new FileInfo(sFolderPath);
                        myFile.Attributes = FileAttributes.Normal;
                        myFile.CopyTo(backup, true);
                    }

                    

                }
                catch (ConfigurationErrorsException ex)
                {
                    // MessageBox.Show();
                    string sFolderPath = ((ConfigurationErrorsException)ex.InnerException).Filename.ToString();
                    string backup = AppDomain.CurrentDomain.BaseDirectory + "user.config";
                    FileInfo myFile = new FileInfo(backup);
                    myFile.Attributes = FileAttributes.Normal;
                    myFile.CopyTo(sFolderPath, true);
                    Application.Restart();
                    return;
                }

                try
                {
                    if (ConnectionMannager.IsExistDatatabase(new ConnectionModelSave()) == false)
                    {
                        DBsetup dBsetup = new DBsetup(Form.ActiveForm);
                        dBsetup.ShowDialog();
                        return;
                    }

                    int count = new MySqlGatewayConnection().IsCheckTable();
                    if (count == 0)
                    {
                        Application.Run(new CheckSoftwareActivation());
                    }

                    string path = File.ReadAllText("Config/ppp.txt");
                    if (File.Exists(path) && GlobalSetting.DbType == "SQLITE")
                    {
                        GlobalSetting.ConnectionString = "Data Source=" + path + ";Version=3; busy timeout=10000; Pooling=True; Max Pool Size=100;datetimeformat=CurrentCulture;";
                        GlobalSetting.serverConnectionString = "Data Source=" + path + ";Version=3; busy timeout=10000; Pooling=True; Max Pool Size=100;datetimeformat=CurrentCulture;";
                    }

                    //  if (!GlobalSetting.IsOffline)


                    if (OthersMethod.CheckForInternetConnection())
                    {
                        RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                        RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

                        DateTime expireDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(restaurantInformation.Expire).ToLocalTime().Date;
                        DateTime currentDataTime = DateTime.Now.Date;
                        bool flag = false;

                        if (restaurantInformation != null && restaurantInformation.Id > 0)
                        {
                            if (currentDataTime >= expireDate)
                            {
                                RestaurantSync restaurantSync = aRestaurantInformationBll.GetRestaurantSyncInformation();
                                MySqlRestaurantInformationDAO aRestaurantInformationDao = new MySqlRestaurantInformationDAO();
                                aRestaurantInformationDao.UpdateRestaurantLicense(restaurantSync);
                                SoftActiveMsg msg = new SoftActiveMsg();
                                msg.ShowDialog();
                            }
                            else
                            {
                                Application.Run(new LoginForm());
                            }
                        }
                        else
                        {
                            if (restaurantInformation.Id <= 0)
                            {
                                Application.Run(new CheckSoftwareActivation());
                            }
                            else if (currentDataTime >= expireDate)
                            {
                                RestaurantSync restaurantSync = aRestaurantInformationBll.GetRestaurantSyncInformation();
                                MySqlRestaurantInformationDAO aRestaurantInformationDao = new MySqlRestaurantInformationDAO();
                                aRestaurantInformationDao.UpdateRestaurantLicense(restaurantSync);

                                SoftActiveMsg msg = new SoftActiveMsg();
                                msg.ShowDialog();
                            }

                        }
                    }
                    else
                    {
                        try
                        {
                            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                            RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

                            bool flag = false;
                            DateTime expireDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(restaurantInformation.Expire)
                                    .ToLocalTime().Date;
                            DateTime currentDataTime = DateTime.Now.Date;

                            if (restaurantInformation != null && restaurantInformation.Id > 0 && currentDataTime < expireDate)
                            {
                                if (restaurantInformation.UpdateRequired == 1)
                                {
                                    Application.Run(new Download());
                                }
                                else
                                {
                                    Application.Run(new LoginForm());
                                }

                            }
                            else
                            {

                                if (restaurantInformation.Id <= 0)
                                {
                                    Application.Run(new CheckSoftwareActivation());


                                }
                                else if (currentDataTime >= expireDate)
                                {

                                    RestaurantSync restaurantSync =
                                        aRestaurantInformationBll.GetRestaurantSyncInformation();
                                    MySqlRestaurantInformationDAO aRestaurantInformationDao = new MySqlRestaurantInformationDAO();
                                    aRestaurantInformationDao.UpdateRestaurantLicense(restaurantSync);
                                    SoftActiveMsg msg = new SoftActiveMsg();
                                    msg.ShowDialog();

                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            string sr = exception.Message;
                            // MessageBox.Show()
                            if (exception.Message == "SQL logic error or missing database\r\nno such table: rcs_restaurant")
                            {
                                Application.Run(new CheckSoftwareActivation());
                            }
                            else if (exception.Message == "Unable to connect to any of the specified MySQL hosts.")
                            {
                                Application.Run(new CheckSoftwareActivation());
                            }
                            else
                            {
                                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                aErrorReportBll.SendErrorReport(exception.ToString());
                                //MessageBox.Show(exception.ToString());
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());

                    if (ex.Message == "The type initializer for 'TomaFoodRestaurant.Model.GlobalSetting' threw an exception.")
                    {
                        DBsetup dBsetup = new DBsetup(Form.ActiveForm);
                        dBsetup.ShowDialog();
                        return;
                    }

                    else if (ex.Message == "Unknown database '" + Properties.Settings.Default.database + "'")
                    {
                        Application.Run(new DBsetup(Form.ActiveForm));
                    }
                    else
                    {
                        Application.Run(new CheckSoftwareActivation());
                    }
                }

            }







        }

        


    }
}
