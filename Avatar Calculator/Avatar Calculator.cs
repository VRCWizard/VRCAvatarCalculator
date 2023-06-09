using CoreOSC;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace TTSVoiceWizard3._0
{
    public partial class VoiceWizardWindow : MaterialForm
    {
        private string currentCalculation = "";
        public CoreOSC.UDPSender OSCSender = new CoreOSC.UDPSender("127.0.0.1", 9000);//9000 = vrchat receiver
        public System.Threading.Timer VRCCounterTimer;
        public System.Threading.Timer KATTimer;
        public VoiceWizardWindow()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.DeepPurple800, Primary.DeepPurple900, Primary.DeepPurple800, Accent.DeepPurple700, TextShade.WHITE);

          //  materialButtonClearEntry.BackColor = Color.MediumPurple;
          //  materialButtonClear.BackColor = Color.MediumPurple;

            VRCCounterTimer = new System.Threading.Timer(VRCCountertimertick);
            VRCCounterTimer.Change(1600, 0);
            KATTimer = new System.Threading.Timer(KATtimertick);
            KATTimer.Change(500, 0);

        }

        MaterialSkinManager TManager = MaterialSkinManager.Instance;

        private void VoiceWizardWindow_Load(object sender, EventArgs e)
        {
            materialSwitch1.Checked = Settings1.Default.chatboxShow;
            try { OSCReciever(); }
            catch { MessageBox.Show("Another App is already listening on port 9001"); }

        }
        public void VRCCountertimertick(object sender) { Thread t = new Thread(doVRCCounterTimerTick); t.Start(); }
        private void doVRCCounterTimerTick()
        {
            if (materialSwitch1.Checked == true)
            {
                var text = "";
                text = currentCalculation;
                var messageSpeechBubble = new OscMessage("/chatbox/input", text, true, false);
                OSCSender.Send(messageSpeechBubble);
            }
            VRCCounterTimer.Change(1600, 0);
        }
        public void KATtimertick(object sender) { Thread t = new Thread(doKATTimerTick); t.Start(); }
        private void doKATTimerTick()
        {
          //  if (materialSwitch1.Checked == true)
          //  {
                var text = "";
            this.Invoke((MethodInvoker)delegate ()
            {
                text = textBoxOutput.Text;
            });
            // var messageSpeechBubble = new OscMessage("/chatbox/input", text, true, false);
            //  OSCSender.Send(messageSpeechBubble);
            Task.Run(() => outputVRChat(text));
          //  }
            KATTimer.Change(500, 0);
        }

        private void button_Click(object sender, EventArgs e)
        {
            // This adds the number or operator to the string calculation
            currentCalculation += (sender as Button).Text;

            // Display the current calculation back to the user
            textBoxOutput.Text = currentCalculation;

          //  Task.Run(() => outputVRChat(textBoxOutput.Text.ToString()));
        }
        private void button_Equals_Click(object sender, EventArgs e)
        {
            string formattedCalculation = currentCalculation.ToString().Replace("x", "*").ToString().Replace("�", "/");

            try
            {
                textBoxOutput.Text = new DataTable().Compute(formattedCalculation, null).ToString();
                currentCalculation = textBoxOutput.Text;
                var message1 = new OscMessage("/avatar/parameters/CALC_Pointer", 255);
                OSCSender.Send(message1);
              //  Thread.Sleep(100);
              //  Task.Run(() => outputVRChat(textBoxOutput.Text.ToString()));

            }
            catch (Exception ex)
            {
                textBoxOutput.Text = "0";
                currentCalculation = "";
               // var message1 = new OscMessage("/avatar/parameters/KAT_Pointer", 255);
               // OSCSender.Send(message1);
              //  Thread.Sleep(100);
              //  Task.Run(() => outputVRChat(textBoxOutput.Text.ToString()));
            }
        }
        private void button_Clear_Click(object sender, EventArgs e)
        {
           
            
            // Reset the calculation and empty the textbox
            textBoxOutput.Text = "0";
            currentCalculation = "";

            //  Task.Run(() => outputVRChat(textBoxOutput.Text.ToString()));
            //  var message1 = new OscMessage("/avatar/parameters/KAT_Pointer", 255);
            // OSCSender.Send(message1);
            var message1 = new OscMessage("/avatar/parameters/CALC_Pointer", 255);
            OSCSender.Send(message1);
        }
        private void button_ClearEntry_Click(object sender, EventArgs e)
        {
            
            
            // If the calculation is not empty, remove the last number/operator entered
            if (currentCalculation.Length > 0)
            {
                currentCalculation = currentCalculation.Remove(currentCalculation.Length - 1, 1);
            }

            // Re-display the calculation onto the screen
            textBoxOutput.Text = currentCalculation;
            //    Task.Run(() => outputVRChat(textBoxOutput.Text.ToString()));
            var message1 = new OscMessage("/avatar/parameters/CALC_Pointer", 255);
            OSCSender.Send(message1);
        }

        public void OSCReciever()
        {

          

         

            HandleOscPacket callback = delegate (OscPacket packet)
            {
 
              var messageReceived = (OscMessage)packet;
                





                if (messageReceived != null)
                {


                    try
                    {


                      //  System.Diagnostics.Debug.WriteLine("address: " + messageReceived.Address.ToString() + "argument: " + messageReceived.Arguments[0].ToString());
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            
                            if (messageReceived.Arguments[0].ToString() == "True")
                            {
                                switch (messageReceived.Address.ToString())
                                {
                                    case "/avatar/parameters/calculator/0": calc0.PerformClick(); Debug.WriteLine("it worked"); break;

                                    case "/avatar/parameters/calculator/1": calc1.PerformClick(); break;

                                    case "/avatar/parameters/calculator/2": calc2.PerformClick(); break;

                                    case "/avatar/parameters/calculator/3": calc3.PerformClick(); break;

                                    case "/avatar/parameters/calculator/4": calc4.PerformClick(); break;
                                    case "/avatar/parameters/calculator/5": calc5.PerformClick(); break;

                                    case "/avatar/parameters/calculator/6": calc6.PerformClick(); break;
                                    case "/avatar/parameters/calculator/7": calc7.PerformClick(); break;

                                    case "/avatar/parameters/calculator/8": calc8.PerformClick(); break;
                                    case "/avatar/parameters/calculator/9": calc9.PerformClick(); break;

                                    case "/avatar/parameters/calculator/(": materialButtonOpen.PerformClick(); break;
                                    case "/avatar/parameters/calculator/)": materialButtonClose.PerformClick(); break;

                                    case "/avatar/parameters/calculator/dot": materialButtonPoint.PerformClick(); break;

                                    case "/avatar/parameters/calculator/equals": materialButtonEquals.PerformClick(); break;

                                    case "/avatar/parameters/calculator/C": materialButtonClear.PerformClick(); break;

                                    case "/avatar/parameters/calculator/CE": materialButtonClearEntry.PerformClick(); break;

                                    case "/avatar/parameters/calculator/plus": materialButtonAdd.PerformClick(); break;
                                    case "/avatar/parameters/calculator/minus": materialButtonSub.PerformClick(); break;
                                    case "/avatar/parameters/calculator/multiply": materialButtonMult.PerformClick(); break;
                                    case "/avatar/parameters/calculator/divide": materialButtonDiv.PerformClick(); break;


                                    default: break;
                                }
                            }
                        });
                    







                    }
                    catch
                    {
                        System.Diagnostics.Debug.WriteLine("****-------*****--------Received a message! null address");

                    }
                }

            };

            var listener = new UDPListener(9001, callback);//9001 = vrchat sender

        }
    
        public async void outputVRChat(string textstringbefore)
        {


            string numKATSyncParameters = "16";
            DateTime lastDateTime = DateTime.Now;
            int debugDelayValue = 50;
            var message0 = new OscMessage("/avatar/parameters/CALC_Visible", true);


            OSCSender.Send(message0);







            //   string textstring = SplitToLines(textstringbefore, 32);


            string textstring = textstringbefore;


            int stringleng = 0;
            foreach (char h in textstring)
            {
                stringleng += 1;
            }
            //System.Diagnostics.Debug.WriteLine("textstring length =" + textstring.Length);

            int sentenceLength = stringleng % 16;

            switch (sentenceLength)
            {
                case 1:
                    textstring += "   ";
                    if (numKATSyncParameters == "8" || numKATSyncParameters == "16")
                    {
                        textstring += "    ";
                    };
                    if (numKATSyncParameters == "16")
                    {
                        textstring += "        ";
                    };
                    break;

                case 2:
                    textstring += "  ";
                    if (numKATSyncParameters == "8" || numKATSyncParameters == "16")
                    {
                        textstring += "    ";
                    }
                    if (numKATSyncParameters == "16")
                    {
                        textstring += "        ";
                    };
                    break;
                case 3:
                    textstring += " ";
                    if (numKATSyncParameters == "8" || numKATSyncParameters == "16")
                    {
                        textstring += "    ";
                    }
                    if (numKATSyncParameters == "16")
                    {
                        textstring += "        ";
                    }
                    break;
                case 4:
                    textstring += "";
                    if (numKATSyncParameters == "8" || numKATSyncParameters == "16")
                    {
                        textstring += "    ";
                    }
                    if (numKATSyncParameters == "16")
                    {
                        textstring += "        ";
                    }
                    break;
                case 5:
                    textstring += "   ";
                    if (numKATSyncParameters == "16")
                    {
                        textstring += "        ";
                    }; break;
                case 6:
                    textstring += "  ";
                    if (numKATSyncParameters == "16")
                    {
                        textstring += "        ";
                    }; break;
                case 7:
                    textstring += " ";
                    if (numKATSyncParameters == "16")
                    {
                        textstring += "        ";
                    }; break;
                case 8: textstring += "        "; break; //16 mode
                case 9: textstring += "       "; break; //16 mode
                case 10: textstring += "      "; break; //16 mode
                case 11: textstring += "     "; break; //16 mode
                case 12: textstring += "    "; break; //16 mode
                case 13: textstring += "   "; break; //16 mode
                case 14: textstring += "  "; break; //16 mode
                case 15: textstring += " "; break; //16 mode
                default:; break;
            }

            float letter = 0.0F;
            int charCounter = 0;
            int stringPoint = 1;


            float letterFloat0 = 0;
            float letterFloat1 = 0;
            float letterFloat2 = 0;
            float letterFloat3 = 0;
            float letterFloat4 = 0;
            float letterFloat5 = 0;
            float letterFloat6 = 0;
            float letterFloat7 = 0;

            float letterFloat8 = 0; //16 mode
            float letterFloat9 = 0;//16 mode
            float letterFloat10 = 0;//16 mode
            float letterFloat11 = 0;//16 mode
            float letterFloat12 = 0;//16 mode
            float letterFloat13 = 0;//16 mode
            float letterFloat14 = 0;//16 mode
            float letterFloat15 = 0;//16 mode

            var message1 = new OscMessage("/avatar/parameters/CALC_Pointer", 255);
            var message2 = new OscMessage("/avatar/parameters/CALC_CharSync0", letterFloat0);
            var message3 = new OscMessage("/avatar/parameters/CALC_CharSync1", letterFloat1);
            var message4 = new OscMessage("/avatar/parameters/CALC_CharSync2", letterFloat2);
            var message5 = new OscMessage("/avatar/parameters/CALC_CharSync3", letterFloat3);

            var message6 = new OscMessage("/avatar/parameters/CALC_CharSync4", letterFloat4);
            var message7 = new OscMessage("/avatar/parameters/CALC_CharSync5", letterFloat5);
            var message8 = new OscMessage("/avatar/parameters/CALC_CharSync6", letterFloat6);
            var message9 = new OscMessage("/avatar/parameters/CALC_CharSync7", letterFloat7);

            var message10 = new OscMessage("/avatar/parameters/CALC_CharSync0", letterFloat8);
            var message11 = new OscMessage("/avatar/parameters/CALC_CharSync1", letterFloat9);
            var message12 = new OscMessage("/avatar/parameters/CALC_CharSync2", letterFloat10);
            var message13 = new OscMessage("/avatar/parameters/CALC_CharSync3", letterFloat11);
            var message14 = new OscMessage("/avatar/parameters/CALC_CharSync4", letterFloat12);
            var message15 = new OscMessage("/avatar/parameters/CALC_CharSync5", letterFloat13);
            var message16 = new OscMessage("/avatar/parameters/CALC_CharSync6", letterFloat14);
            var message17 = new OscMessage("/avatar/parameters/CALC_CharSync7", letterFloat15);


            //  var message0 = new SharpOSC.OscMessage("/avatar/parameters/KAT_Visible", true);


            //   string testingthis = (DateTime.Now - lastDateTime).ToString("ss");

            //  ot.outputLog(MainForm, testingthis);


         //   if ((DateTime.Now - lastDateTime).Seconds <= 1)
          //  {
              

            //    Task.Delay(1555).Wait();
          //  }
         //   lastDateTime = DateTime.Now;

           // if (currentCalculation == "")
         //   {
         //       OSCSender.Send(message1);
        //    }






            // Task.Delay(50).Wait(); // this delay is to fix text box showing your previous message for a brief second (turned off for now because hide text replaced with clear text)    
            //  MainForm.sender3.Send(message0);


            message1 = new OscMessage("/avatar/parameters/CALC_Pointer", 1);

            foreach (char c in textstring)
            {
                switch (c)
                {
                    case ' ': letter = 0; break;

                    case '(': letter = 8; break;
                    case ')': letter = 9; break;
                    case '*': letter = 10; break;
                    case '+': letter = 11; break;
                    case '-': letter = 13; break;
                    case '.': letter = 14; break;
                    case '�': letter = 15; break;// slash
                    case '0': letter = 16; break;
                    case '1': letter = 17; break;
                    case '2': letter = 18; break;
                    case '3': letter = 19; break;
                    case '4': letter = 20; break;
                    case '5': letter = 21; break;
                    case '6': letter = 22; break;
                    case '7': letter = 23; break;
                    case '8': letter = 24; break;
                    case '9': letter = 25; break;
                    case '=': letter = 29; break;
                    case '?': letter = 31; break;
                    case 'X': letter = 56; break;
                    case 'x': letter = 88; break;




                    default: letter = 31; break;






                }




                if (letter > 127.5)
                {
                    letter = letter - 256;

                }
                letter = letter / 127;


                switch (charCounter)
                {
                    case 0:
                        letterFloat0 = letter;
                        break;
                    case 1:
                        letterFloat1 = letter;
                        break;
                    case 2:
                        letterFloat2 = letter;
                        break;
                    case 3:
                        if (numKATSyncParameters == "4")
                        {
                            Task.Delay(debugDelayValue).Wait();
                            letterFloat3 = letter;
                            message1 = new OscMessage("/avatar/parameters/CALC_Pointer", stringPoint);
                            message2 = new OscMessage("/avatar/parameters/CALC_CharSync0", letterFloat0);
                            message3 = new OscMessage("/avatar/parameters/CALC_CharSync1", letterFloat1);
                            message4 = new OscMessage("/avatar/parameters/CALC_CharSync2", letterFloat2);
                            message5 = new OscMessage("/avatar/parameters/CALC_CharSync3", letterFloat3);
                            message0 = new OscMessage("/avatar/parameters/CALC_Visible", true);

                            OSCSender.Send(message1);
                            OSCSender.Send(message2);
                            OSCSender.Send(message3);
                            OSCSender.Send(message4);
                            OSCSender.Send(message5);
                            OSCSender.Send(message0);


                            stringPoint += 1;
                            charCounter = -1;
                            letterFloat0 = 0;
                            letterFloat1 = 0;
                            letterFloat2 = 0;
                            letterFloat3 = 0;


                        }
                        if (numKATSyncParameters == "8" || numKATSyncParameters == "16")
                        {
                            letterFloat3 = letter;

                        }
                        break;
                    case 4:
                        letterFloat4 = letter;
                        break;
                    case 5:
                        letterFloat5 = letter;
                        break;
                    case 6:
                        letterFloat6 = letter;
                        break;
                    case 7:
                        if (numKATSyncParameters == "8")
                        {
                            Task.Delay(debugDelayValue).Wait();
                            letterFloat7 = letter;
                            message1 = new OscMessage("/avatar/parameters/CALC_Pointer", stringPoint);
                            message2 = new OscMessage("/avatar/parameters/CALC_CharSync0", letterFloat0);
                            message3 = new OscMessage("/avatar/parameters/CALC_CharSync1", letterFloat1);
                            message4 = new OscMessage("/avatar/parameters/CALC_CharSync2", letterFloat2);
                            message5 = new OscMessage("/avatar/parameters/CALC_CharSync3", letterFloat3);

                            message6 = new OscMessage("/avatar/parameters/CALC_CharSync4", letterFloat4);
                            message7 = new OscMessage("/avatar/parameters/CALC_CharSync5", letterFloat5);
                            message8 = new OscMessage("/avatar/parameters/CALC_CharSync6", letterFloat6);
                            message9 = new OscMessage("/avatar/parameters/CALC_CharSync7", letterFloat7);
                            message0 = new OscMessage("/avatar/parameters/CALC_Visible", true);


                            OSCSender.Send(message1);
                            OSCSender.Send(message2);
                            OSCSender.Send(message3);
                            OSCSender.Send(message4);
                            OSCSender.Send(message5);

                            OSCSender.Send(message6);
                            OSCSender.Send(message7);
                            OSCSender.Send(message8);
                            OSCSender.Send(message9);

                            OSCSender.Send(message0);


                            stringPoint += 1;
                            charCounter = -1;
                            letterFloat0 = 0;
                            letterFloat1 = 0;
                            letterFloat2 = 0;
                            letterFloat3 = 0;

                            letterFloat4 = 0;
                            letterFloat5 = 0;
                            letterFloat6 = 0;
                            letterFloat7 = 0;
                        }
                        if (numKATSyncParameters == "16")
                        {
                            letterFloat7 = letter;

                        }
                        break;

                    case 8:
                        letterFloat8 = letter;
                        break;
                    case 9:
                        letterFloat9 = letter;
                        break;
                    case 10:
                        letterFloat10 = letter;
                        break;
                    case 11:
                        letterFloat11 = letter;
                        break;
                    case 12:
                        letterFloat12 = letter;
                        break;
                    case 13:
                        letterFloat13 = letter;
                        break;
                    case 14:
                        letterFloat14 = letter;
                        break;
                    case 15:
                        Task.Delay(debugDelayValue).Wait();
                        letterFloat15 = letter;
                        message1 = new OscMessage("/avatar/parameters/CALC_Pointer", stringPoint);
                        message2 = new OscMessage("/avatar/parameters/CALC_CharSync0", letterFloat0);
                        message3 = new OscMessage("/avatar/parameters/CALC_CharSync1", letterFloat1);
                        message4 = new OscMessage("/avatar/parameters/CALC_CharSync2", letterFloat2);
                        message5 = new OscMessage("/avatar/parameters/CALC_CharSync3", letterFloat3);

                        message6 = new OscMessage("/avatar/parameters/CALC_CharSync4", letterFloat4);
                        message7 = new OscMessage("/avatar/parameters/CALC_CharSync5", letterFloat5);
                        message8 = new OscMessage("/avatar/parameters/CALC_CharSync6", letterFloat6);
                        message9 = new OscMessage("/avatar/parameters/CALC_CharSync7", letterFloat7);

                        message10 = new OscMessage("/avatar/parameters/CALC_CharSync8", letterFloat8);
                        message11 = new OscMessage("/avatar/parameters/CALC_CharSync9", letterFloat9);
                        message12 = new OscMessage("/avatar/parameters/CALC_CharSync10", letterFloat10);
                        message13 = new OscMessage("/avatar/parameters/CALC_CharSync11", letterFloat11);

                        message14 = new OscMessage("/avatar/parameters/CALC_CharSync12", letterFloat12);
                        message15 = new OscMessage("/avatar/parameters/CALC_CharSync13", letterFloat13);
                        message16 = new OscMessage("/avatar/parameters/CALC_CharSync14", letterFloat14);
                        message17 = new OscMessage("/avatar/parameters/KCALC_CharSync15", letterFloat15);
                        message0 = new OscMessage("/avatar/parameters/CALC_Visible", true);

                        OSCSender.Send(message1);

                        OSCSender.Send(message2);
                        OSCSender.Send(message3);
                        OSCSender.Send(message4);
                        OSCSender.Send(message5);

                        OSCSender.Send(message6);
                        OSCSender.Send(message7);
                        OSCSender.Send(message8);
                        OSCSender.Send(message9);

                        OSCSender.Send(message10);
                        OSCSender.Send(message11);
                        OSCSender.Send(message12);
                        OSCSender.Send(message13);
                        OSCSender.Send(message14);
                        OSCSender.Send(message15);
                        OSCSender.Send(message16);
                        OSCSender.Send(message17);


                        OSCSender.Send(message0);


                        stringPoint += 1;
                        charCounter = -1;
                        letterFloat0 = 0;
                        letterFloat1 = 0;
                        letterFloat2 = 0;
                        letterFloat3 = 0;

                        letterFloat4 = 0;
                        letterFloat5 = 0;
                        letterFloat6 = 0;
                        letterFloat7 = 0;


                        letterFloat8 = 0;
                        letterFloat9 = 0;
                        letterFloat10 = 0;
                        letterFloat11 = 0;
                        letterFloat12 = 0;
                        letterFloat13 = 0;
                        letterFloat14 = 0;
                        letterFloat15 = 0;
                        break;

                    default: break;
                }


                charCounter += 1;
                //  currentlyPrinting = true;


                if (stringPoint >= 33)
                {
                    break;

                }

            }
        }

        private void VoiceWizardWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings1.Default.chatboxShow = materialSwitch1.Checked;
            Settings1.Default.Save();
        }
    }
    }