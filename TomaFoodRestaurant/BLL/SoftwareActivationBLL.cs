using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.BLL
{
    public class SoftwareActivationBLL
    {
        string passphrase = "tpospass";
        public string ToCheckSoftwareActivation(string key, string data, string url)
        {
            string result = string.Empty;
            try
            {
                string postData = data;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Post the data to the right place.
                Uri target = new Uri(url);
                WebRequest request = WebRequest.Create(target);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

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
                //result = "OK";
            }

            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
                result = ex.ToString();
            }
            return result;
        }

        public HardwareInforamtion HardDriveInforamtion(HardwareInforamtion information, ManagementObjectSearcher mo)
        {
            foreach (ManagementObject mob in mo.Get())
            {
                try
                {

                    //information.HardDriveModel = mob.GetPropertyValue("Model").ToString(); // mob["Model"].ToString();
                    //information.HardDriveInterfaceType = mob.GetPropertyValue("InterfaceType").ToString();// mob["InterfaceType"].ToString();
                    //information.HardDriveCaption = mob.GetPropertyValue("Caption").ToString(); // mob["Caption"].ToString();
                    information.HardDriveSerialNo = mob.GetPropertyValue("SerialNumber").ToString();

                }
                catch (Exception ex)
                {
                    information.HardDriveModel = "";
                    information.HardDriveInterfaceType = "";
                    information.HardDriveCaption = "";
                    information.HardDriveSerialNo = "";

                    //MessageBox.Show("Need to restart your computer to start this application.Please restart and start TPOS again.", " SYSTEM WARNING ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    //aErrorReportBll.SendErrorReport(ex.ToString());
                }
            }
            return information;
        }

        public HardwareInforamtion GetProcessInformation(HardwareInforamtion information, ManagementObjectSearcher mo)
        {
            foreach (ManagementObject mob in mo.Get())
            {
                try
                {

                    information.ProcessorSerial = mob["ProcessorId"].ToString();

                }
                catch (Exception ex)
                {
                 //  information.ProcessorSerial = "UNKNOWNDEVICE";
                   return information;
                    // MessageBox.Show("Need to restart your computer to start this application.Please restart and start TPOS again.", " SYSTEM WARNING ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    //aErrorReportBll.SendErrorReport(ex.ToString());
                }
            }
            return information;
        }
        public HardwareInforamtion GetHardwareInforamtion()
        {
            HardwareInforamtion information = new HardwareInforamtion();

            try
            {
                ManagementObjectSearcher processor = new ManagementObjectSearcher("select * from Win32_Processor");
                ManagementObjectSearcher mo = new ManagementObjectSearcher("select * from Win32_DiskDrive where Index='0'");

                var processorformation = GetProcessInformation(information, processor);

                var hardirveinformation = HardDriveInforamtion(information, mo);

                information.HardDriveSerialNo = hardirveinformation.HardDriveSerialNo ?? "NULL".Trim();
                information.ProcessorSerial = processorformation.ProcessorSerial.Trim();
            }
            catch(Exception ex) {

                information.HardDriveSerialNo = "UNKNOWNHD";
                information.ProcessorSerial ="PROCESSORID";
            }
            return information;
        }


        public string EncryptData(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

        public string DecryptString(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }

        internal string onlineLogin(string username, string sha, Int32 restaurantID, string url_)
        {

            // return "1";
            string result = string.Empty;

            string postData = "username=" + username + "&password=" + sha + "&restaurantID=" + restaurantID;
            string url = url_ + "/restaurantcontrol/request/check_online_login";

            try
            {

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Post the data to the right place.
                Uri target = new Uri(url);
                WebRequest request = WebRequest.Create(target);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

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
            }

            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
                //  result = ex.ToString();
            }
            return result;

        }

        internal string updateUser(string result_, string password)
        {

            if (GlobalSetting.DbType == "SQLITE")
            {
                UserLoginDAO aUserLoginDao = new UserLoginDAO();
                return aUserLoginDao.UpdateUserInfo(result_, password);
            }
            else
            {
                MySqlUserLoginDAO aUserLoginDao = new MySqlUserLoginDAO();
                return aUserLoginDao.UpdateUserInfo(result_, password);
            }


        }
    }
}
