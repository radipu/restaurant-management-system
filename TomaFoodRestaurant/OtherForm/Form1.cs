using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using TomaFoodRestaurant;
using TomaFoodRestaurant.BLL;

public struct AD101DEVICEPARAMETER
{
    public int nRingOn;
    public int nRingOff;
    public int nHookOn;
    public int nHookOff;
    public int nStopCID;
    public int nNoLine;			// Add this parameter in new AD101(MCU Version is 6.0)
}

namespace TomaFoodRestaurant.OtherForm
{
    public partial class Form1 : Form
    {
        public static string CustomerPhoneNumber { set; get; }
        [DllImport("AD101Device.dll", EntryPoint = "AD101_InitDevice")]
        public static extern int AD101_InitDevice(int hWnd);

        [DllImport("AD101Device.dll", EntryPoint = "AD101_GetDevice")]
        public static extern int AD101_GetDevice();

        [DllImport("AD101Device.dll", EntryPoint = "AD101_GetCPUVersion", CharSet = CharSet.Ansi)]
        public static extern int AD101_GetCPUVersion(int nLine, StringBuilder szCPUVersion);

        // Start reading cpu id of device 
        [DllImport("AD101Device.dll", EntryPoint = "AD101_ReadCPUID")]
        public static extern int AD101_ReadCPUID(int nLine);

        // Get readed cpu id of device 
        [DllImport("AD101Device.dll", EntryPoint = "AD101_GetCPUID", CharSet = CharSet.Ansi)]
        public static extern int AD101_GetCPUID(int nLine, StringBuilder szCPUID);



        // Get caller id number  
        [DllImport("AD101Device.dll", EntryPoint = "AD101_GetCallerID", CharSet = CharSet.Ansi)]
        public static extern int AD101_GetCallerID(int nLine, StringBuilder szCallerIDBuffer, StringBuilder szName, StringBuilder szTime);

        // Get dialed number 
        [DllImport("AD101Device.dll", EntryPoint = "AD101_GetDialDigit", CharSet = CharSet.Ansi)]
        public static extern int AD101_GetDialDigit(int nLine, StringBuilder szDialDigitBuffer);


        // Get collateral phone dialed number 
        [DllImport("AD101Device.dll", EntryPoint = "AD101_GetCollateralDialDigit", CharSet = CharSet.Ansi)]
        public static extern int AD101_GetCollateralDialDigit(int nLine, StringBuilder szDialDigitBuffer);



        // Get last line state 
        [DllImport("AD101Device.dll", EntryPoint = "AD101_GetState")]
        public static extern int AD101_GetState(int nLine);

        // Get ring count
        [DllImport("AD101Device.dll", EntryPoint = "AD101_GetRingIndex")]
        public static extern int AD101_GetRingIndex(int nLine);

        // Get talking time
        [DllImport("AD101Device.dll", EntryPoint = "AD101_GetTalkTime")]
        public static extern int AD101_GetTalkTime(int nLine);


        [DllImport("AD101Device.dll", EntryPoint = "AD101_GetParameter")]
        public static extern int AD101_GetParameter(int nLine, ref AD101DEVICEPARAMETER tagParameter);

        [DllImport("AD101Device.dll", EntryPoint = "AD101_ReadParameter")]
        public static extern int AD101_ReadParameter(int nLine);

        // Set systematic parameter  
        [DllImport("AD101Device.dll", EntryPoint = "AD101_SetParameter")]
        public static extern int AD101_SetParameter(int nLine, ref AD101DEVICEPARAMETER tagParameter);

        // Free devices 

        [DllImport("AD101Device.dll", EntryPoint = "AD101_FreeDevice")]
        public static extern void AD101_FreeDevice();

        // Get current AD101 device count
        [DllImport("AD101Device.dll", EntryPoint = "AD101_GetCurDevCount")]
        public static extern int AD101_GetCurDevCount();

        // Change handle of window that uses to receive message
        [DllImport("AD101Device.dll", EntryPoint = "AD101_ChangeWindowHandle")]
        public static extern int AD101_ChangeWindowHandle(int hWnd);

        // Show or don't show collateral phone dialed number
        [DllImport("AD101Device.dll", EntryPoint = "AD101_ShowCollateralPhoneDialed")]
        public static extern void AD101_ShowCollateralPhoneDialed(bool bShow);

        // Control led 
        [DllImport("AD101Device.dll", EntryPoint = "AD101_SetLED")]
        public static extern int AD101_SetLED(int nLine, int enumLed);
        
        // Control line connected with ad101 device to busy or idel
        [DllImport("AD101Device.dll", EntryPoint = "AD101_SetBusy")]
        public static extern int AD101_SetBusy(int nLine, int enumLineBusy);

        // Set line to start talking than start timer
        [DllImport("AD101Device.dll", EntryPoint = "AD101_SetLineStartTalk")]
        public static extern int AD101_SetLineStartTalk(int nLine);


        // Set time to start talking after dialed number 
        [DllImport("AD101Device.dll", EntryPoint = "AD101_SetDialOutStartTalkingTime")]
        public static extern int AD101_SetDialOutStartTalkingTime(int nSecond);


        // Set ring end time
        [DllImport("AD101Device.dll", EntryPoint = "AD101_SetRingOffTime")]
        public static extern int AD101_SetRingOffTime(int nSecond);



        public const int MCU_BACKID = 0x07;	// Return Device ID
        public const int MCU_BACKSTATE = 0x08;	// Return Device State
        public const int MCU_BACKCID = 0x09;		// Return Device CallerID
        public const int MCU_BACKDIGIT = 0x0A;	// Return Device Dial Digit
        public const int MCU_BACKDEVICE = 0x0B;	// Return Device Back Device ID
        public const int MCU_BACKPARAM = 0x0C;	// Return Device Paramter
        public const int MCU_BACKCPUID = 0x0D;	// Return Device CPU ID
        public const int MCU_BACKCOLLATERAL = 0x0E;		// Return Collateral phone dialed
        public const int MCU_BACKDISABLE = 0xFF;		// Return Device Init
        public const int MCU_BACKENABLE = 0xEE;
        public const int MCU_BACKMISSED = 0xAA;		// Missed call 
        public const int MCU_BACKTALK = 0xBB;		// Start Talk

        // LED Status 
        enum LEDTYPE
        {
            LED_CLOSE = 1,
            LED_RED,
            LED_GREEN,
            LED_YELLOW,
            LED_REDSLOW,
            LED_GREENSLOW,
            LED_YELLOWSLOW,
            LED_REDQUICK,
            LED_GREENQUICK,
            LED_YELLOWQUICK,
        };
        //////////////////////////////////////////////////////////////////////////////////////////////

        // Line Status 
        enum ENUMLINEBUSY
        {
            LINEBUSY = 0,
            LINEFREE,
        };


        public const int HKONSTATEPRA = 0x01; // hook on pr+  HOOKON_PRA
        public const int HKONSTATEPRB = 0x02;  // hook on pr-  HOOKON_PRR
        public const int HKONSTATENOPR = 0x03;  // have pr  HAVE_PR
        public const int HKOFFSTATEPRA = 0x04;   // hook off pr+  HOOKOFF_PRA
        public const int HKOFFSTATEPRB = 0x05;  // hook off pr-  HOOKOFF_PRR
        public const int NO_LINE = 0x06; // no line  NULL_LINE
        public const int RINGONSTATE = 0x07;  // ring on  RING_ON
        public const int RINGOFFSTATE = 0x08;  // ring off RING_OFF
        public const int NOHKPRA = 0x09; // NOHOOKPRA= 0x09, // no hook pr+
        public const int NOHKPRB = 0x0a; // NOHOOKPRR= 0x0a, // no hook pr-
        public const int NOHKNOPR = 0x0b; // NOHOOKNPR= 0x0b, // no hook no pr

        public const int WM_USBLINEMSG = 1024 + 180;
        //  public static Form1 form1 = new Form1();

        public MainFormView ResMainFormView;
        public mainForm SinglePage;


        public Form1(MainFormView ResponsivePage, mainForm SinglePage)
        {
            InitializeComponent();
            this.ResMainFormView = ResponsivePage;
            this.SinglePage = SinglePage;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                // GetExactNumber("");
                listView1.Columns.Clear();
                listView1.Columns.Add("Channel No.", 90, HorizontalAlignment.Left);
                listView1.Columns.Add("Device State", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("CPU Version", 120, HorizontalAlignment.Left);
                listView1.Columns.Add("Line State", 120, HorizontalAlignment.Left);
                listView1.Columns.Add("Caller ID", 100, HorizontalAlignment.Left);
                listView1.Columns.Add("Dialed Number", 100, HorizontalAlignment.Left);
                listView1.Columns.Add("Collateral Dialed", 110, HorizontalAlignment.Left);
                listView1.Columns.Add("Talk Time", 100, HorizontalAlignment.Left);
                listView1.Columns.Add("CPU ID", 100, HorizontalAlignment.Left);


                listView1.Items.Add("Channel 1");
                listView1.Items.Add("Channel 2");
                listView1.Items.Add("Channel 3");
                listView1.Items.Add("Channel 4");

                comboBox1.SelectedIndex = 0;
                for (int i = 0; i < 4; i++)
                {
                    listView1.Items[i].SubItems.Add("");
                    listView1.Items[i].SubItems.Add("");
                    listView1.Items[i].SubItems.Add("");
                    listView1.Items[i].SubItems.Add("");
                    listView1.Items[i].SubItems.Add("");
                    listView1.Items[i].SubItems.Add("");
                    listView1.Items[i].SubItems.Add("");
                    listView1.Items[i].SubItems.Add("");
                    listView1.Items[i].SubItems[1].Text = "Disable";
                }

                if (AD101_InitDevice(Handle.ToInt32()) == 0)
                {
                    return;
                }

                AD101_GetDevice();

                textBox7.Text = "3";
                textBox8.Text = "7";


                AD101_SetDialOutStartTalkingTime(3);
                AD101_SetRingOffTime(7);

                checkBoxSHOWPHONENUMBER.Checked = true;
                checkBoxSHOWNUMBER.Checked = true;
            } catch(Exception ex)
            {
                return;
            }

            


        }

        private void OnDeviceMsg(IntPtr wParam, IntPtr Lparam)
        {
            int nMsg = new int();
            int nLine = new int();

            nMsg = wParam.ToInt32() % 65536;
            nLine = wParam.ToInt32() / 65536;

            switch (nMsg)
            {
                case MCU_BACKDISABLE:
                    listView1.Items[nLine].SubItems[1].Text = "Disable";
                    listView1.Items[nLine].SubItems[2].Text = "";
                    listView1.Items[nLine].SubItems[3].Text = "";
                    listView1.Items[nLine].SubItems[4].Text = "";
                    listView1.Items[nLine].SubItems[5].Text = "";
                    listView1.Items[nLine].SubItems[6].Text = "";
                    listView1.Items[nLine].SubItems[7].Text = "";
                    listView1.Items[nLine].SubItems[8].Text = "";

                    break;

                case MCU_BACKENABLE:
                    listView1.Items[nLine].SubItems[1].Text = "Enable";
                    break;

                case MCU_BACKID:
                    {
                        StringBuilder szCPUVersion = new StringBuilder(32);

                        listView1.Items[nLine].SubItems[1].Text = "Enable";

                        AD101_GetCPUVersion(nLine, szCPUVersion);

                        listView1.Items[nLine].SubItems[2].Text = szCPUVersion.ToString();

                    }
                    break;

                case MCU_BACKCID:
                    {
                        StringBuilder szCallerID = new StringBuilder(128);
                        StringBuilder szName = new StringBuilder(128);
                        StringBuilder szTime = new StringBuilder(128);

                        int nLen = AD101_GetCallerID(nLine, szCallerID, szName, szTime);
                        listView1.Items[nLine].SubItems[4].Text = szCallerID.ToString();
                        string cellNumber = GetExactNumber(szCallerID.ToString());
                        try
                        {
                            if (cellNumber != String.Empty)
                            {
                                if (ResMainFormView != null)
                                {
                                    ResMainFormView.callToTpos(cellNumber);
                                }
                                else if (SinglePage != null)
                                {
                                    SinglePage.callToTpos(cellNumber);
                                }
                            }
                            //    Refresh();



                        }
                        catch (Exception ex)
                        {

                            File.AppendAllText("Config/log.txt", "CallerID LOg : " +DateTime.Now+" "+ ex.GetBaseException() + "\n\n");
                        }


                    }
                    break;

                case MCU_BACKSTATE:
                    {
                        switch (Lparam.ToInt32())
                        {
                            case HKONSTATEPRA:
                                listView1.Items[nLine].SubItems[3].Text = "HOOK ON PR+";
                                break;

                            case HKONSTATEPRB:
                                listView1.Items[nLine].SubItems[3].Text = "HOOK ON PR-";
                                break;

                            case HKONSTATENOPR:
                                listView1.Items[nLine].SubItems[3].Text = "HOOK ON NOPR";
                                break;

                            case HKOFFSTATEPRA:
                                {
                                    listView1.Items[nLine].SubItems[3].Text = "HOOK OFF PR+";

                                    StringBuilder szCallerID = new StringBuilder(128);
                                    StringBuilder szName = new StringBuilder(128);
                                    StringBuilder szTime = new StringBuilder(128);

                                    if (AD101_GetCallerID(nLine, szCallerID, szName, szTime) < 1 || AD101_GetRingIndex(nLine) < 1)
                                    {
                                        listView1.Items[nLine].SubItems[4].Text = "";
                                    }

                                    listView1.Items[nLine].SubItems[5].Text = "";
                                    listView1.Items[nLine].SubItems[6].Text = "";
                                    listView1.Items[nLine].SubItems[7].Text = "";

                                }
                                break;

                            case HKOFFSTATEPRB:
                                {
                                    listView1.Items[nLine].SubItems[3].Text = "HOOK OFF PR-";

                                    StringBuilder szCallerID = new StringBuilder(128);
                                    StringBuilder szName = new StringBuilder(128);
                                    StringBuilder szTime = new StringBuilder(128);

                                    if (AD101_GetCallerID(nLine, szCallerID, szName, szTime) < 1 || AD101_GetRingIndex(nLine) < 1)
                                    {
                                        listView1.Items[nLine].SubItems[4].Text = "";
                                    }

                                    listView1.Items[nLine].SubItems[5].Text = "";
                                    listView1.Items[nLine].SubItems[6].Text = "";
                                    listView1.Items[nLine].SubItems[7].Text = "";
                                }
                                break;

                            case NO_LINE:
                                {
                                    listView1.Items[nLine].SubItems[3].Text = "No Line";

                                    StringBuilder szCallerID = new StringBuilder(128);
                                    StringBuilder szName = new StringBuilder(128);
                                    StringBuilder szTime = new StringBuilder(128);

                                    if (AD101_GetCallerID(nLine, szCallerID, szName, szTime) < 1 || AD101_GetRingIndex(nLine) < 1)
                                    {
                                        listView1.Items[nLine].SubItems[4].Text = "";
                                    }

                                    listView1.Items[nLine].SubItems[5].Text = "";
                                    listView1.Items[nLine].SubItems[6].Text = "";
                                    listView1.Items[nLine].SubItems[7].Text = "";
                                }
                                break;

                            case RINGONSTATE:
                                {
                                    string szRing = "Ring:" + string.Format("{0:D2}", AD101_GetRingIndex(nLine));

                                    listView1.Items[nLine].SubItems[3].Text = szRing;
                                }
                                break;

                            case RINGOFFSTATE:
                                listView1.Items[nLine].SubItems[3].Text = "Ring Off";
                                break;

                            case NOHKPRA:
                                listView1.Items[nLine].SubItems[3].Text = "NO HOOK PR+";

                                break;

                            case NOHKPRB:
                                listView1.Items[nLine].SubItems[3].Text = "NO HOOK PR-";

                                break;

                            case NOHKNOPR:
                                {
                                    listView1.Items[nLine].SubItems[3].Text = "NO HOOK NOPR";

                                    StringBuilder szCallerID = new StringBuilder(128);
                                    StringBuilder szName = new StringBuilder(128);
                                    StringBuilder szTime = new StringBuilder(128);

                                    if (AD101_GetCallerID(nLine, szCallerID, szName, szTime) < 1 || AD101_GetRingIndex(nLine) < 1)
                                    {
                                        listView1.Items[nLine].SubItems[4].Text = "";
                                    }

                                    listView1.Items[nLine].SubItems[5].Text = "";
                                    listView1.Items[nLine].SubItems[6].Text = "";
                                    listView1.Items[nLine].SubItems[7].Text = "";
                                }
                                break;


                            default:
                                break;
                        }
                    }
                    break;

                case MCU_BACKDIGIT:
                    {
                        if (checkBoxSHOWPHONENUMBER.Checked == true)
                        {
                            StringBuilder szDialDigit = new StringBuilder(128);

                            AD101_GetDialDigit(nLine, szDialDigit);

                            listView1.Items[nLine].SubItems[5].Text = szDialDigit.ToString();

                        }

                    }
                    break;


                case MCU_BACKCOLLATERAL:
                    {
                        StringBuilder szDialDigit = new StringBuilder(128);

                        AD101_GetCollateralDialDigit(nLine, szDialDigit);

                        listView1.Items[nLine].SubItems[6].Text = szDialDigit.ToString();
                    }
                    break;

                case MCU_BACKDEVICE:
                    {
                        StringBuilder szCPUVersion = new StringBuilder(32);

                        listView1.Items[nLine].SubItems[1].Text = "Enable";

                        AD101_GetCPUVersion(nLine, szCPUVersion);

                        listView1.Items[nLine].SubItems[2].Text = szCPUVersion.ToString();

                    }
                    break;

                case MCU_BACKPARAM:
                    {
                        AD101DEVICEPARAMETER tagParameter = new AD101DEVICEPARAMETER();

                        AD101_GetParameter(nLine, ref tagParameter);
                        textBox1.Text = tagParameter.nRingOn.ToString();
                        textBox2.Text = tagParameter.nRingOff.ToString();
                        textBox3.Text = tagParameter.nHookOn.ToString();
                        textBox4.Text = tagParameter.nHookOff.ToString();
                        textBox5.Text = tagParameter.nStopCID.ToString();
                        textBox6.Text = tagParameter.nNoLine.ToString();
                    }
                    break;

                case MCU_BACKCPUID:
                    {
                        StringBuilder szCPUID = new StringBuilder(4);

                        AD101_GetCPUID(nLine, szCPUID);

                        listView1.Items[nLine].SubItems[8].Text = szCPUID.ToString();

                    }
                    break;

                case MCU_BACKMISSED:
                    {
                        listView1.Items[nLine].SubItems[3].Text = "Missed Call";
                    }
                    break;

                case MCU_BACKTALK:
                    {
                        string strTalk;
                        strTalk = string.Format("{0:D2}", Lparam) + "S";

                        listView1.Items[nLine].SubItems[7].Text = strTalk;

                    }
                    break;

                default:
                    break;
            }
        }

        private string GetExactNumber(string szCallerID)
        {

            string mobileNumber = "";
            try{
                string str = "";
                File.AppendAllText("Config/log.txt", "Call Log : " + szCallerID + " " + DateTime.Now + "\n\n");
                string mystr = Regex.Replace(szCallerID, @"[^0-9]", "");// Regex.Replace(szCallerID, @"[^\w\d\s]", "");
                string result = string.Concat(Enumerable.Reverse(mystr));

                if (result.Count() >= 10){
                    for (int i = 0; i < 10; i++)
                    {
                        str += result[i];
                    }
                }
                str += "0";
                mobileNumber = string.Concat(Enumerable.Reverse(str));

            }
            catch (Exception ex)
            {
               // MessageBox.Show("The number cannot be recognised ");
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
            }

            return mobileNumber;

        }

        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_USBLINEMSG: //处理消息　
                    OnDeviceMsg(m.WParam, m.LParam);
                    break;
                default:
                    base.DefWndProc(ref m);//调用基类函数处理非自定义消息。　　　
                    break;
            }
        }

        private void Read_Click(object sender, EventArgs e)
        {
            AD101_ReadParameter(comboBox1.SelectedIndex);

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            AD101_SetLED(comboBox1.SelectedIndex, 1);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            AD101_SetLED(comboBox1.SelectedIndex, 2);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            AD101_SetLED(comboBox1.SelectedIndex, 5);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            AD101_SetLED(comboBox1.SelectedIndex, 8);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            AD101_SetLED(comboBox1.SelectedIndex, 3);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            AD101_SetLED(comboBox1.SelectedIndex, 6);
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            AD101_SetLED(comboBox1.SelectedIndex, 9);
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            AD101_SetLED(comboBox1.SelectedIndex, 4);
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            AD101_SetLED(comboBox1.SelectedIndex, 7);
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            AD101_SetLED(comboBox1.SelectedIndex, 10);
        }

        private void BtnLineBusy_Click(object sender, EventArgs e)
        {
            AD101_SetBusy(comboBox1.SelectedIndex, 0);
        }
        private void BtnLineFree_Click(object sender, EventArgs e)
        {
            AD101_SetBusy(comboBox1.SelectedIndex, 1);
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            string cellNumber = GetExactNumber("07595-043050");
            MessageBox.Show(cellNumber);        
            ////TopFrm.callToTpos("07599299719");
            //if (ResMainFormView != null)
            //{
            //    ResMainFormView.callToTpos("07964019459");
            //}
            //else if (SinglePage != null)
            //{
            //    SinglePage.callToTpos("07599299719");
            //    }//AD101_SetDialOutStartTalkingTime(Convert.ToInt32(textBox7.Text));
            //AD101_SetRingOffTime(Convert.ToInt32(textBox8.Text));

        }
        private void checkBoxSHOWNUMBER_CheckedChanged(object sender, EventArgs e)
        {
            AD101_ShowCollateralPhoneDialed(checkBoxSHOWPHONENUMBER.Checked);
        }

        private void Write_Click(object sender, EventArgs e)
        {
            AD101DEVICEPARAMETER tagParameter = new AD101DEVICEPARAMETER();

            tagParameter.nRingOn = Convert.ToInt32(textBox1.Text);
            tagParameter.nRingOff = Convert.ToInt32(textBox2.Text);
            tagParameter.nHookOn = Convert.ToInt32(textBox3.Text);
            tagParameter.nHookOff = Convert.ToInt32(textBox4.Text);
            tagParameter.nStopCID = Convert.ToInt32(textBox5.Text);
            tagParameter.nNoLine = Convert.ToInt32(textBox6.Text);

            if (AD101_SetParameter(comboBox1.SelectedIndex, ref tagParameter) == 0)
            {
                MessageBox.Show("Set parameters failed!");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                AD101_FreeDevice();
            }
            catch(Exception ex)
            {

            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}