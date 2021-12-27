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

namespace TomaFoodRestaurant.OtherForm
{
    public partial class FrmCidv2 : Form
    {

        delegate void SetTextCallback();

        Int32 tVendorId = 4660;
        Int32 tProductId = 1;
        CEHIDLibrary.CidEasyHIDLibrary CE_Library = new CEHIDLibrary.CidEasyHIDLibrary();

        string theCallerId;
        public mainForm SinglePage;

        public FrmCidv2(mainForm SinglePage)
        {
            InitializeComponent();
            this.SinglePage = SinglePage;

            try
            {
                CE_Library.ConstructModels(tVendorId, tProductId);

                CE_Library.enableCheckDigitControl = true;
                CE_Library.followLineEvents = true;

                CE_Library.onNewCall_v2 += new CidEasyHIDLibrary.HIDNewCallHandler_v2(NewCallHandler);
                CE_Library.onDeviceArrival += new CEHIDLibrary.CidEasyHIDLibrary.DeviceArrivalHandler(DeviceArrival);
                CE_Library.onDeviceRemove += new CEHIDLibrary.CidEasyHIDLibrary.DeviceRemoveHandler(DeviceRemove);
                CE_Library.onLineEvent += new CidEasyHIDLibrary.LineEventHandler(LineEvent);
            }
            catch (Exception ex)
            {
                File.AppendAllText("Config/log.txt", "CallerID v2 Init Exception : " + DateTime.Now + " " + ex.ToString() + "\n\n");
            }
            
        }


        public void NewCallHandler(string DeviceSerial, string Caller, string Callee, string CallId, char Port, string Year, string Month, string Day, string Hour, string Minute)
        {
            Debug.WriteLine("New Call Received on Device : " + DeviceSerial + " of Port : " + Port);
            Debug.WriteLine("                     Caller : " + Caller);
            Debug.WriteLine("                     Callee : " + Callee);
            Debug.WriteLine("                     CallId : " + CallId);
            Debug.WriteLine("                     Dt/Tm  : " + Year + "-" + Month + "-" + Day + " " + Hour + ":" + Minute);

            theCallerId = Caller;
            DisplayCallerID(); 
        }


        private void DisplayCallerID()
        {
            if (this.lblCID.InvokeRequired)
            {
                //If request comes from different thread, invoke it in user thread
                SetTextCallback d = new SetTextCallback(DisplayCallerID);
                this.Invoke(d, new object[] { });
            }
            else
            {
                try
                {
                    if (theCallerId != String.Empty)
                    {

                        if (SinglePage != null)
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


        private void DeviceArrival()
        {
            Debug.WriteLine("DEVICE ARRIVAL");
            List_Connected_Devices();
        }


        private void DeviceRemove()
        {
            Debug.WriteLine("DEVICE REMOVED");
            List_Connected_Devices();
        }


        public void LineEvent(string DeviceSerial, char Port, string Event)
        {
            Debug.WriteLine( "Live Event : " + "\"" + Event + "\"" +"   Detected on Port : " + Port + " of Device : " + DeviceSerial);
        }


        private void List_Connected_Devices()
        {
            if (CE_Library.numConnectedDevices > 0)
            {
                Debug.WriteLine("LIST OF CONNECTED DEVICES :");
                for (int i = 0; i < CE_Library.numConnectedDevices; i++)
                    Debug.WriteLine("#" + i + ": Serial Number : " + CE_Library.deviceInfoList[i].DeviceSerial + 
                                              ", FirmWare Version : " + CE_Library.deviceInfoList[i].FirmWareVersion + 
                                              ", Device Type : " + CE_Library.deviceInfoList[i].DeviceType + 
                                              ", Device Model :  " + CE_Library.deviceInfoList[i].DeviceModel + 
                                              ", Number of Input Ports :  " + CE_Library.deviceInfoList[i].numPorts);
                Debug.WriteLine("");
            }
            else
            {
                Debug.WriteLine("NO DEVICES CONNECTED...");
            }
        }

    }
}

