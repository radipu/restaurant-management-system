using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using CEHIDLibrary;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class frmMain : Form
    {

        delegate void SetTextCallback();

        Int32 tVendorId = 0x1234;
        Int32 tProductId = 0x0001;
        CEHIDLibrary.CidEasyHIDLibrary CE_Library = new CEHIDLibrary.CidEasyHIDLibrary();

        string theCallerId;
        public mainForm SinglePage;

        public frmMain(mainForm SinglePage)
        {
            InitializeComponent();
            this.SinglePage = SinglePage;

            try
            {
                CE_Library.ConstructModels(tVendorId, tProductId);

                CE_Library.onNewCall += new CidEasyHIDLibrary.HIDNewCallHandler(NewCallOccured);
                CE_Library.onDeviceArrival += new CidEasyHIDLibrary.DeviceArrivalHandler(DeviceArrivalA);
                CE_Library.onDeviceRemove += new CidEasyHIDLibrary.DeviceRemoveHandler(DeviceRemoveA);
                CE_Library.onLineEvent += new CidEasyHIDLibrary.LineEventHandler(Line_Changed);
            } catch(Exception ex)
            {
                File.AppendAllText("Config/log.txt", "CallerID Init Exception : " + DateTime.Now + " " + ex.ToString() + "\n\n");
            }
            
            
        }

        protected override void WndProc(ref Message Mes)
        {
            //CE_Library.ParseWndMessage(Mes);
            base.WndProc(ref Mes);
        }


        /*public void NewCallOccured(string DeviceSerial, char Port, string Year, string Month, string Day, string Hour, string Minute, string Number)
        {
            Debug.WriteLine(DeviceSerial);
            Debug.WriteLine(Port);
            Debug.WriteLine(Year);
            Debug.WriteLine(Month);
            Debug.WriteLine(Day);
            Debug.WriteLine(Hour);
            Debug.WriteLine(Minute);
            Debug.WriteLine(Number);

            theCallerId = Number;
            DisplayCallerID(); 
        }*/

        public void NewCallOccured(string Callee, string Caller, string cCallId, char Port, string Year, string Month, string Day, string Hour, string Minute)
        {
            File.AppendAllText("Config/log.txt", "CallerID LOg : " + DateTime.Now + " " + Caller + "\n\n");
            Regex digitsOnly = new Regex(@"[^\d]");            
            theCallerId = digitsOnly.Replace(Caller, "");
            DisplayCallerID();
        }


            private void DisplayCallerID()
        {
            if (this.lblCID.InvokeRequired)
            {

                SetTextCallback d = new SetTextCallback(DisplayCallerID);
                this.Invoke(d, new object[] { });
            }
            else
            {

             try
            {
                    if (theCallerId != String.Empty)
                    {

                        if(SinglePage != null)
                        {
                            SinglePage.callToTpos(theCallerId);
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText("Config/log.txt", "CallerID LOg : " + DateTime.Now + " " + ex.GetBaseException() + "\n\n");
                }
                lblCID.Text = theCallerId + "\n" + lblCID.Text;
            }
        }


        private void DeviceArrivalA()
        {
            Debug.WriteLine("DEVICE ARRIVAL");
            List_Connected_Devices();
        }


        private void DeviceRemoveA()
        {
            Debug.WriteLine("DEVICE REMOVED");
            List_Connected_Devices();
        }


        public void Line_Changed(string DeviceSerial, char Port, string Status)
        {
            Debug.WriteLine("LINE EVENT : "+DeviceSerial+" "+Port+" "+Status);
        }


        private void List_Connected_Devices()
        {
            //for (byte i=0; i < CE_Library.deviceCounts;i++)
                //if (!CE_Library.devInfoList[i].devClosed) 
                    //Debug.WriteLine("#"+i+" : " + CE_Library.devInfoList[i].DeviceSerialNumber + " " + CE_Library.devInfoList[i].FirmWareVersion + " " + CE_Library.devInfoList[i].PathName);
        }


    }
}

