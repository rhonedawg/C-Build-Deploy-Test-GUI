using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using BroadcomBuild.Properties;


namespace BroadcomBuild
{
    //all the current states of the GUI
    enum Form2States
    {
        WaitingToCheckOut, WaitingToCheckOutBroadcom, WaitingToCheckOutFreescale, CheckedOutBroadcom, WaitingToBuildFreescale, CheckingOutBroadcom, CheckingOutFreescale, WaitingToCheckOutTools,
        CheckedOutTools, WaitingToMoveTools, MovingTools, WaitingToBuildBroadcom, BuildingBroadcom, BuildingFreescale, BinaryImageCheck, FailBuildBroadcom
    };

    public partial class Tutorial : Form
    {

        bool browseCancelled = false;
        bool Broadcom = true;

        private const string ECD_PATH = "C:\\Freescale\\CW MCU v10.4\\eclipse";
        int exitCode = 0;

        Form2States State;

        public Tutorial()
        {
            InitializeComponent();
            Browse2.Visible = false;
            buildButton.Visible = false;
            toolsLabel.Text = "Please enter in Virtual Box <Username>@ip ex(alpha@127.0.0.1)";
            toolsBox.Visible = true;
            StopButton.Enabled = false;
            ChangeState(0);
        }

        //Enabling ON/OFF switch for buttons
        private void lockButtons(bool buildButtonVisible, bool buildButtonEnabled, 
           bool CheckoutButtonVisible, bool CheckoutButtonEnabled, bool StopButtonEnabled)
        {
            buildButton.Visible = buildButtonVisible;
            buildButton.Enabled = buildButtonEnabled;
            CheckoutButton.Visible = CheckoutButtonVisible;
            CheckoutButton.Enabled = CheckoutButtonEnabled;
            StopButton.Enabled = StopButtonEnabled;
        }

        //MenuStrip Settings
        private void lockSettings(bool buildsToolStrip)
        {
            buildsToolStripMenuItem.Enabled = buildsToolStrip;

        }


        private void ChangeState(Form2States newState)
        {
            if (newState == State)
            {
                Console.WriteLine("State transition back to original state");
                return;
            }

            State = newState;
            switch (newState)
            {
                case Form2States.WaitingToCheckOut:
                    {
                        _cancelTask = new TaskCompletionSource<bool>();
                        //BuildVis,BuildEnab,CheckoutVis,CheckoutEnab,StopEnab
                        lockButtons(false, false,true, true, false);
                        lockSettings(true);

                        break;
                    }
                case Form2States.WaitingToCheckOutFreescale:
                    {
                        _cancelTask = new TaskCompletionSource<bool>();
                        Browse2.Visible = false;
                        this.URLBox.Text = String.Empty;
                        this.URLBox.Text = Settings.Default["FSPath"].ToString();
                        Settings.Default["FSPath"] = URLBox.Text;
                        label5.Text = "Choose the folder in which to install Freescale.";
                        label2.Text = "Folder URL:";
                        buildsToolStripMenuItem.Text = "Freescale";
                        toolsBox.Visible = false;
                        toolsLabel.Visible = false;
                        break;
                    }
                case Form2States.WaitingToCheckOutBroadcom:
                    {
                        _cancelTask = new TaskCompletionSource<bool>();
                        lockButtons(false, false, true, true, false);
                        lockSettings(true);
                        label5.Text = "Choose the folder in which contains SSHKEY (Putty Gen)";
                        Settings.Default["SSHKEY"] = URLBox.Text;
                        buildsToolStripMenuItem.Text = "Broadcom";
                        Browse2.Visible = false;
                        toolsLabel.Text = "Please enter in Virtual Box <Username>@ip ex(alpha@127.0.0.1)";
                        toolsBox.Visible = true;

                        break;
                    }
                case Form2States.CheckedOutTools:
                    {
                        break;
                    }
                case Form2States.WaitingToMoveTools:
                    {
                        _cancelTask = new TaskCompletionSource<bool>();

                        MessageBox.Show("Please enter folder path of zOEMTools_eCos inside of the ex. /DM3/Broadcom directory:" + Environment.NewLine + "Please enter path manually in the textbox above");

                        this.URLBox.Text = Settings.Default["ShareTools"].ToString();
                        this.toolsBox.Text = Settings.Default["DM3"].ToString();

                        toolsBox.Visible = true;
                        toolsLabel.Visible = true;

                        label2.Text = "Shared Tools Path: (Shared linux file path ex. /media/sf_sharefolder/zOEMtools_eCos)";
                        toolsLabel.Text = "Folder that tools  will be moved to: (linux file path must contain ex. /home/'user'/DM3/Broadcom)";
                        browse.Enabled = false;
                        lockButtons(false, false, true, true, false);
                        break;
                    }
                case Form2States.CheckingOutFreescale:
                    {
                        _cancelTask = new TaskCompletionSource<bool>();
                        lockSettings(false);
                        lockButtons(false, false, true, true, true);
                        break;
                    }
                case Form2States.CheckingOutBroadcom:
                    {
                        _cancelTask = new TaskCompletionSource<bool>();
                        lockSettings(false);
                        lockButtons(false, false, true, true, true);
                        break;
                    }
                case Form2States.WaitingToBuildFreescale:
                    {
                        _cancelTask = new TaskCompletionSource<bool>();
                        buildsToolStripMenuItem.Text = "Build";
                        lockSettings(false);

                        lockButtons(true, true, false, false, false);
                        Browse2.Visible = true;

                        label2.Text = "Broadcom binary image:";
                        label5.Text = "Choose Broadcom binary image to merge with after build";
                        URLBox.Enabled = false;
                        toolsBox.Enabled = false;
                        toolsLabel.Visible = true;
                        toolsLabel.Text = "Choose Freescale source verison folder path ex(DSM33_FS_v02.02)";
                        toolsBox.Visible = true;
                        toolsBox.Text = string.Empty;
                        break;
                    }
                case Form2States.CheckedOutBroadcom:
                    {
                        _cancelTask = new TaskCompletionSource<bool>();
                        label2.Text = "Shared Folder URL:";
                        label5.Text = "Choose the shared folder between host and guest OS";
                        lockButtons(false, false, true, true, true);
                        toolsBox.Visible = false;
                        toolsLabel.Visible = false;
                        break;
                    }
                case Form2States.WaitingToBuildBroadcom:
                    {
                        label5.Text = "Please click Build for final phase of Broadcom";
                        _cancelTask = new TaskCompletionSource<bool>();
                        lockButtons(true, true, false, false, true);
                        break;
                    }
                case Form2States.BuildingBroadcom:
                    {
                        _cancelTask = new TaskCompletionSource<bool>();
                        lockButtons(true, false, false, false, true);
                        break;
                    }
                case Form2States.BuildingFreescale:
                    {
                        _cancelTask = new TaskCompletionSource<bool>();
                        break;
                    }
                case Form2States.MovingTools:
                    {
                        _cancelTask = new TaskCompletionSource<bool>();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        /*          SVN Process
         **************TOP****************************/


        // This task acts as a flag. It is browseCancelled when the cancel button is pressed.
        // When this happens, methods awaiting the completion of either a legitimate task OR this task
        // are made aware of the cancellation.
        // _cancelTask is initialized when build, checkout or update is pressed, so that
        // any of the resulting processes can be browseCancelled.
        private TaskCompletionSource<bool> _cancelTask;

        // This is the basic task we use to run all our subprocesses.
        // What it does depends on the value of the global ProcessArgs.
        // It has functionality to send the output and error streams of the subprocess
        // to the output box of Form2,
        // and to clean itself up if the subprocess is browseCancelled, based on the global ArgsExit.
        private async Task AsyncProcess(String processArgs, String exitArgs)
        {
            // Make an AsyncProcess whose
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = processArgs,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false,
                }
            };

            process.Start();

            await Task.Delay(250);

            Task readerTasks = Task.WhenAll(
                ConsumeReader(process.StandardError),
                ConsumeReader(process.StandardOutput));

            //if any of these tasks are completed then completedTask will complete.
            Task completedTask = await Task.WhenAny(readerTasks, _cancelTask.Task);

            if (completedTask == _cancelTask.Task)
            {

                Process exit = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = exitArgs,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = false,
                    }
                };
                exit.Start();
                await readerTasks;
                throw new TaskCanceledException(_cancelTask.Task);
            }
            process.WaitForExit();

            exitCode = process.ExitCode;

            Console.Write("ExitCode is " + exitCode);  // debugging
        }

        // Reads from its reader in real time
        // and sends the results to StdoutBox
        private async Task ConsumeReader(TextReader reader)
        {
            char[] text = new char[512];
            int cch;

            while ((cch = await reader.ReadAsync(text, 0, text.Length)) > 0)
            {
                StdoutBox.AppendText(new string(text, 0, cch));
            }
        }
        /*          AsyncProcess
         ************BOT***************************************/

        async void Checkout_Click(object sender, EventArgs e)
        {
            Console.WriteLine((int)State);
            if (URLBox.Text == "" || toolsBox.Text == "")
            {
                MessageBox.Show("No file found. Please select a valid file path", "Download Folder error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                URLBox.Text = "Empty String Error";
            }
            if (State == Form2States.WaitingToCheckOutFreescale)
            {
                await CheckOutFreescale();
            }

            if (State == Form2States.WaitingToCheckOutBroadcom || State == Form2States.WaitingToCheckOut)
            {

                await CheckOutBroadcom();
            }
            if (State == Form2States.CheckedOutBroadcom)
            {
                CheckOutGNUTools();
            }
            if (State == Form2States.WaitingToMoveTools)
            {
                await MoveGNUTools();
            }
            if (State == Form2States.WaitingToBuildFreescale)
            {
                StdoutBox.AppendText("Checked out already!");
            }

        }

        private void freescaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (State == Form2States.WaitingToCheckOut || State == Form2States.WaitingToCheckOutBroadcom)
            {
                ChangeState(Form2States.WaitingToCheckOutFreescale);
            }
        }

        // Changes global variables to indicate that Broadcom is selected
        private void broadcomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (State == Form2States.WaitingToCheckOut || State == Form2States.WaitingToCheckOutFreescale)
            {
                ChangeState(Form2States.WaitingToCheckOutBroadcom);
            }

        }
        private async void buildButton_Click(object sender, EventArgs e)
        {

            if (State == Form2States.WaitingToBuildBroadcom)
            {
                StdoutBox.Text = string.Empty;
                await BuildBroadcom();
     
            }
            if (State == Form2States.WaitingToBuildFreescale)
            {

                await BuildFreescale();
            }
        }

        private async Task CheckOutFreescale()
        {

            String processArgs;
            String exitArgs;
            String option = "co"; //Future: will take in an argument so it can be checkout or update that changes with the click of a button. 


            Settings.Default["FSPath"] = URLBox.Text;
            Settings.Default.Save();
            String URL = URLBox.Text;

            processArgs = "/C cd " + URL + " & svn " + option + " --username ATI_Testing --password alpha https://103.56.175.45/svn/Engineering/CBU-331-ALPHA-DSM33-FS/WorkProducts/Code/trunk Freescale";
            exitArgs = "/C taskkill /f /im svn.exe & taskkill /f /im/ cmd.exe";
            try
            {
                //Future: need to learn how to check if a folder is a shared folder or not in windows.
                if (System.IO.Directory.Exists(URL))
                {
                    StdoutBox.Text += Environment.NewLine + "Starting Download ..." + Environment.NewLine;
                    ChangeState(Form2States.CheckingOutFreescale);
                    await AsyncProcess(processArgs, exitArgs);
                    ChangeState(Form2States.WaitingToBuildFreescale);
                }
                else
                {
                    ChangeState(Form2States.WaitingToCheckOutFreescale);
                }

            }
            catch (OperationCanceledException)
            {
                this.StdoutBox.Text += "Canceled!";
                buildButton.Visible = false;
                CheckoutButton.Visible = true;

                /*used to cleanup svn download if browseCancelled*/

                String ArgsClean = "";

                ArgsClean = "/C rmdir /Q /S " + URL + "\\Freescale";

                Process SVNProcess1 = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = ArgsClean,
                        UseShellExecute = false,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false,
                        CreateNoWindow = true,
                    }
                };
                SVNProcess1.Start();
                SVNProcess1.WaitForExit();
                ChangeState(Form2States.WaitingToCheckOutFreescale);
            }
        }
        private async Task MoveGNUTools()
        {
            String shareURL = "";
            String toolsURL = "";
            String KEY1 = Settings.Default["SSHKEY"].ToString();
            String VB = Settings.Default["VB"].ToString();
            Settings.Default["ShareTools"] = URLBox.Text;
            Settings.Default.Save();
            shareURL = Settings.Default["ShareTools"].ToString();


            Settings.Default["DM3"] = toolsBox.Text;
            Settings.Default.Save();

            toolsURL = Settings.Default["DM3"].ToString();

            Console.WriteLine(shareURL + " " + toolsURL + " MoveTools location");

            if (!NotValidLinuxFile(toolsURL, shareURL))
            {
                MessageBox.Show("No file found. Please select a valid file path", "GNU Tools/ Share Folder error", MessageBoxButtons.OK);
                CheckoutButton.Enabled = true;
                shareURL = Settings.Default["ShareTools"].ToString();

            }
            else
            {
                try
                {
                    //Copying "zOEMtools_Ecos" to Broadcom source files "Ver_5.7.1mp1_02.14". 
                    StdoutBox.Text += "Setting up DM3 folder for Linux ...";
                    String Args = "/C plink -P 2222 -i " + KEY1 + " " + VB + " cp -r " + shareURL + " " + toolsURL;
                    String ArgsExit = "/C taskkill /f /im plink.exe & taskkill /f /im/ cmd.exe";
                    await AsyncProcess(Args, ArgsExit);
                    StdoutBox.Text += Environment.NewLine + "zOEMtools_Ecos copied over 50%";

                    //Copying "Permissions file/DSM33_Build" to "Ver_5.7.1mp1_02.14"
                    String Args2 = "/C plink -P 2222 -i " + KEY1 + " " + VB + " cp " + toolsURL + "/zOEMtools_eCos/DSM33_Build " + toolsURL + "/Ver_5.7.1mp1_02.14 ";
                    String ArgsExit2 = "/C taskkill /f /im plink.exe & taskkill /f /im/ cmd.exe";
                    await AsyncProcess(Args2, ArgsExit2);
                    StdoutBox.Text += Environment.NewLine + "DSM33_Build copied over 100%";

                    ChangeState(Form2States.WaitingToBuildBroadcom);
                }
                catch (OperationCanceledException)
                {
                    this.StdoutBox.Text = "Canceled!";
                    buildButton.Visible = false;
                    CheckoutButton.Visible = true;

                    /*used to cleanup svn download if browseCancelled
                    ****************TOP***********************/
                    String CDclean;
                    String ArgsClean = "";

                    ArgsClean = "/C plink -P 2222 -i " + KEY1 + " -m CleanUpTools.txt " + VB;
                    CDclean = null;

                    Process SVNProcess1 = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = CDclean + ArgsClean,
                            UseShellExecute = false,
                            RedirectStandardOutput = false,
                            RedirectStandardError = false,
                            CreateNoWindow = true,
                        }
                    };
                    SVNProcess1.Start();
                    SVNProcess1.WaitForExit();
                    ChangeState(Form2States.WaitingToMoveTools);
                }
            }
        }

        private async void CheckOutGNUTools()
        {
            String KEY = Settings.Default["SSHKEY"].ToString();
            MessageBox.Show("Please select shared folder between host and guest OS");
            browse.PerformClick();
            if (this.browseCancelled)
            {
                browse.Visible = true;
                URLBox.Visible = true;
                browse.ForeColor = Color.Green;
                CheckoutButton.ForeColor = Color.Green;
            }
            else
            {
                browse.ForeColor = DefaultForeColor;
                CheckoutButton.ForeColor = DefaultForeColor;

                String shareURL = URLBox.Text;
                Settings.Default["ShareURL"] = shareURL;
                String option = "co";
                String ArgsSVN = "/C cd " + shareURL + " & svn " + option + " https://blisvn.alpha-bli.com/svn/Comms_Auto_Build_Deploy_Test/zOEMtools_eCos";
                String ArgsExit = "/C taskkill /f /im svn.exe & taskkill /f /im/ cmd.exe";

                try
                {
                    await AsyncProcess(ArgsSVN, ArgsExit);
                    Console.WriteLine("in state checkouttools");
                    ChangeState(Form2States.WaitingToMoveTools);
                }
                catch (OperationCanceledException)
                {
                    this.StdoutBox.Text = "Canceled!";
                    buildButton.Visible = false;
                    CheckoutButton.Visible = true;

                    //used to cleanup svn download if browseCancelled
                    String CDclean;
                    String ArgsClean = "";
                    Console.WriteLine(shareURL + "\\zOEMtools_eCos");
                    ArgsClean = "/C rmdir /Q /S " + shareURL + "\\zOEMtools_eCos";
                    CDclean = null;

                    Process SVNProcess1 = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = CDclean + ArgsClean,
                            UseShellExecute = false,
                            RedirectStandardOutput = false,
                            RedirectStandardError = false,
                            CreateNoWindow = true,
                        }
                    };
                    SVNProcess1.Start();
                    SVNProcess1.WaitForExit();
                    ChangeState(Form2States.CheckedOutBroadcom);
                }
            }
        }

        private async Task BuildBroadcom()
        {

            String toolsURL = Settings.Default["DM3"].ToString();
            String KEY1 = Settings.Default["SSHKEY"].ToString();
            String VB = Settings.Default["VB"].ToString();
            Console.WriteLine(KEY1 + "Key");
            String Args = "/C plink -P 2222 -i " + KEY1 + " " + VB + " cd " + toolsURL + "/Ver_5.7.1mp1_02.14; sudo env PATH=$PATH bash " + toolsURL + "/Ver_5.7.1mp1_02.14/DSM33_Build";
            String ArgsExit = "/C taskkill /f /im svn.exe & taskkill /f /im/ cmd.exe & taskkill /f /im plink.exe";

            try
            {
                ChangeState(Form2States.BuildingBroadcom);
                await AsyncProcess(Args, ArgsExit);
                //In the future address the adjusting the exit code of the AsyncProcess
                if (StdoutBox.Text.Contains("Build FAILED"))
                {
                    buildButton.Enabled = true;
                }
                else
                {
                    //ChangeState(Form2States.BuiltBroadcom);
                    //Brings up message saying the Build has been complete and the image has been placed in the sharedFolder
                    MoveImage();
                    MessageBox.Show("Finished Build For Broadcom. Image has been placed in zOEMTools_eCos directory within the shareFolder");
                    Application.Exit();
                    //also send this output to a log file somewhere!
                    //else send the binary image over and say buil succeeded!
                }
            }
            catch (OperationCanceledException)
            {
                this.StdoutBox.Text = "Build Canceled! Please restart build";
                buildButton.Visible = false;
                CheckoutButton.Visible = true;
                ChangeState(Form2States.WaitingToBuildBroadcom);
            }
        }

        private async Task BuildFreescale()
        {
            _cancelTask = new TaskCompletionSource<bool>();

            URLBox.Enabled = false;
            Console.WriteLine("BuildFreescale called");
            String binaryImageLocation = @URLBox.Text;
            Console.WriteLine("binary image input = " + binaryImageLocation);

            String FreescaleSource = toolsBox.Text;


            Console.WriteLine(FreescaleSource + " source");
            string FileType = Path.GetExtension(binaryImageLocation);
            Console.WriteLine(FileType + " file type");

            bool accept = true;

            if(!FreescaleSource.Contains("DSM33_FS"))
            {
                StdoutBox.Text += Environment.NewLine + "Source file must contain DSM33_FS";
                accept = false;
            }

            if (FileType != ".bin")
            {
                StdoutBox.Text += Environment.NewLine + "Not a Binary Image File";
                Console.WriteLine("inside if statement");
                accept = false;
            }

            if(accept == true)
            {
                if (binaryImageLocation.Contains("DSM33_CM_IMG"))
                {
                    //Will copy the binaryimage over and build freescale
                    BinaryImageCopy(binaryImageLocation);

                    //workspace... where would you like for the freescale to be created in... the sharedfolder
                    //try creating the workspace with where the user downloaded Freescale to.
                    String freescaleWorkspace = Settings.Default.FSPath;

                    // Future: add defensive assertion that this is not empty string/null
                    // Future: make DSM33_<version number> string robust against changes.
                    String Args = "/C cd " + ECD_PATH + " & ecd -build -data " + freescaleWorkspace
                         + " –project \"" + FreescaleSource + "\\AlphaBoot\""
                        + " -project \"" + FreescaleSource + "\\MQX41\\mqx\\build\\cw10\\bsp_DSM33KM\""
                        + " -project \"" + FreescaleSource + "\\MQX41\\mqx\\build\\cw10\\psp_DSM33KM\""
                        + " -project \"" + FreescaleSource + "\\MQX41\\usb\\host\\build\\cw10\\usbh_DSM33KM\""
                        + " -project \"" + FreescaleSource + "\\Firmware\"";

                    Console.Write("args " + Args);
                    String ArgsExit = "/C taskkill /f /im ecd.exe & taskkill /f /im/ cmd.exe";

                    try
                    {
                        //Future: Adjust exit code of AsynProcess to know when build has succeed
                        ChangeState(Form2States.BuildingFreescale);
                        Console.Write("About to build Freescale");
                        await AsyncProcess(Args, ArgsExit);
                        Console.WriteLine(Environment.NewLine + " Finished");
                    }
                    catch (OperationCanceledException)
                    {
                        this.StdoutBox.Text = "Build Canceled! Please restart build";
                        ChangeState(Form2States.WaitingToBuildBroadcom);
                    }
                }
                else
                {
                    StdoutBox.Text += Environment.NewLine + "Valid binary file must be named DSM33_CM_IMG.bin. Please browse for another.";
                }
            }
        }

        private async Task CheckOutBroadcom()
        {
            _cancelTask = new TaskCompletionSource<bool>();

            String processArgs;
            String exitArgs;

            Settings.Default["SSHKEY"] = URLBox.Text;
            Settings.Default.Save();

            Settings.Default["VB"] = toolsBox.Text;
            Settings.Default.Save();

            String VB = Settings.Default["VB"].ToString();
            String KEY = Settings.Default["SSHKEY"].ToString();
            Console.WriteLine(KEY + " here is the key");
            processArgs = "/C plink -P 2222 -batch -i " + KEY + " -m BroadSVN.txt " + VB;
            exitArgs = "/C taskkill /f /im plink.exe & taskkill /f /im svn.exe & taskkill /f /im cmd.exe & tasskill /f /im BroadSVN.txt";
            try
            {
                if (ValidFile(KEY))
                {
                    StdoutBox.Text += Environment.NewLine + "Starting Download ..." + Environment.NewLine;
                    ChangeState(Form2States.CheckingOutBroadcom);
                    await AsyncProcess(processArgs, exitArgs);
                    ChangeState(Form2States.CheckedOutBroadcom);
                }
                else
                {
                    ChangeState(Form2States.WaitingToCheckOut);
                    Console.WriteLine(" key not accepted");
                }
            }
            catch (OperationCanceledException)
            {
                this.StdoutBox.Text += "Canceled!";
                buildButton.Visible = false;
                CheckoutButton.Visible = true;

                /*used to cleanup svn download if browseCancelled
                ****************TOP************************/
                String CDclean;
                String ArgsClean = "";

                ArgsClean = "/C plink -P 2222 -i " + KEY + " -m CleanUpBrod.txt " + VB;
                CDclean = null;

                Process SVNProcess1 = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = CDclean + ArgsClean,
                        UseShellExecute = false,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false,
                        CreateNoWindow = true,
                    }
                };
                SVNProcess1.Start();
                SVNProcess1.WaitForExit();
                ChangeState(Form2States.WaitingToCheckOut);
            }
        }
        //Move the newly created Broadcom Binary Image over to Freescale (Development/Testing Method)
        //the binary image file should be place over through the shared folder. 
        private async Task MoveImage()
        {
            string BroadcomPath = Settings.Default["DM3"].ToString();
            string VB = Settings.Default["VB"].ToString();
            string toolsURL = Settings.Default["DM3"].ToString();
            string sharedFolder = Settings.Default["ShareTools"].ToString();
            string KEY1 = Settings.Default["SSHKEY"].ToString();

            Console.WriteLine("Move Image ShareURL " + sharedFolder);

            //moves the signed image over to the zOEMtools_eCos in the shared folder
            String Args = "/C plink -P 2222 -i " + KEY1 + " " + VB + " cd " + toolsURL + "/zOEMtools_eCos; sudo bash " + toolsURL + "/zOEMtools_eCos/MoveImage.bash " + 
                toolsURL + "/Ver_5.7.1mp1_02.14/Binaries/Images/Signed/ "+ sharedFolder;
           
            String ArgsExit = "/C taskkill /f /im svn.exe & taskkill /f /im/ cmd.exe & taskkill /f /im plink.exe";

            try
            {
                await AsyncProcess(Args, ArgsExit);
            }
            catch (OperationCanceledException)
            {
                this.StdoutBox.Text = "Build Canceled! Please restart build";
                buildButton.Visible = false;
                CheckoutButton.Visible = true;
                ChangeState(Form2States.WaitingToBuildBroadcom);
            }
        }
        
        //this method will place the inputed BinaryImage file from Broadcom into the 
        //write command to copy the image ove into the tools directory of the Freescale build folder and rename/overwrite the file DSM33_CM_IMG.bin
        private async Task BinaryImageCopy(String BinaryURL)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C cd " + Settings.Default["FSPath"].ToString() + " " + "& dir /s DSM33_CM_IMG.bin",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };
            proc.Start();

            var stdout = proc.StandardOutput;
            string line = proc.StandardOutput.ReadToEnd();

            proc.WaitForExit();

            char delimiterChars = '\n';

            string startOfURL = "C:";
            int firstCharacter = line.IndexOf(startOfURL);
            string freescaleBinaryFolder = line.Substring(firstCharacter);

            Console.WriteLine(freescaleBinaryFolder + " testing LINE");

            string[] dirWords = freescaleBinaryFolder.Split(delimiterChars);

            //extracting the binary image file path from windows cmd dir.
            Console.WriteLine(dirWords[0] + "binary image location");
            Console.WriteLine("finished image " + BinaryURL);
            
            //this is the process of copying/overwriting the binary image in the freescale
            //with the newly built broadcom image in the shared folder between OS's.
            String Args = "/C COPY / Y / B " + BinaryURL + " " + dirWords[0] + "\\DSM33_CM_IMG.bin / B";
            String ArgsExit = "/C taskkill / f / im / cmd.exe";
            await AsyncProcess(Args, ArgsExit);
        }

        bool ValidFile(String URL)
        {
            return System.IO.File.Exists(URL);
        }

        bool NotValidLinuxFile(string toolsURL, string shareURL)
        {
            String KEY1 = Settings.Default["SSHKEY"].ToString();
            String VB = Settings.Default["VB"].ToString();
            Console.WriteLine(shareURL + " shareURL");
            Console.WriteLine(toolsURL + " toolsURL");

            if (shareURL == null || toolsURL == null)
            {
                return true;
            }
            else
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "plink.exe",
                        Arguments = "-P 2222 -i " + KEY1 + " " + VB + " test -d " + shareURL,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = false,
                    }
                };

                proc.Start();

                proc.WaitForExit();
                int exitCodeFile = proc.ExitCode;
                Console.WriteLine(exitCodeFile + "shared Folder ");

                if (exitCodeFile == 1)
                {
                    StdoutBox.Text += Environment.NewLine + "Shared Folder URL is not valid";
                    return exitCodeFile == 0;
                }

                var proc2 = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "plink.exe",
                        Arguments = "-P 2222 -i " + KEY1 + " " + VB + " test -d " + toolsURL,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = false,
                    }
                };
                proc2.Start();

                proc2.WaitForExit();

                exitCodeFile = proc2.ExitCode;
                Console.WriteLine(exitCodeFile + "Tools URL ");
                if (exitCodeFile == 1)
                {
                    StdoutBox.Text += Environment.NewLine + "Tools URL is not valid";
                    return exitCodeFile == 0;
                }

                return exitCodeFile == 0;
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _cancelTask.SetCanceled();
            StopButton.Enabled = false;
        }

        static void output(object sender, DataReceivedEventArgs e)
        {
            Console.Write("Output from other process");
            Console.Write(e.Data);
        }
        static void error(object sender, DataReceivedEventArgs e)
        {
            string errorStr = e.ToString();
            if (errorStr != null)
            {
                MessageBox.Show(e.Data, "Download Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Download Complete", "Download Complete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

        }

        private void Form2_Load_1(object sender, EventArgs e)
        {
            this.URLBox.Text = Settings.Default["SSHKEY"].ToString();
            this.toolsBox.Text = Settings.Default["VB"].ToString();

        }

        private void browse_Click(object sender, EventArgs e)
        {

            if (State == Form2States.WaitingToBuildFreescale)
            {
                FileBrowser();
            }
            if (State == Form2States.WaitingToCheckOut)
            {
                FileBrowser();
            }
            if (State == Form2States.WaitingToCheckOutBroadcom)
            {
                FileBrowser();
            }
            if (State == Form2States.WaitingToCheckOutFreescale)
            {
                FolderBrowser();
            }
            if (State == Form2States.CheckedOutBroadcom)
            {
                FolderBrowser();
            }

        }

        private void FileBrowser()
        {
            DialogResult result;
            result = this.openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                URLBox.Text = this.openFileDialog1.FileName;
                String KEY = this.openFileDialog1.FileName;
            }
            else
            {
                browseCancelled = result == DialogResult.OK;
            }
        }

        private void FolderBrowser()
        {
            DialogResult result;
            result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                URLBox.Text = this.folderBrowserDialog1.SelectedPath;
                String URL = this.folderBrowserDialog1.SelectedPath;
            }
            else
            {
                browseCancelled = result == DialogResult.OK;
            }
        }
        //2nd Browse Button
        private void Browse2_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                toolsBox.Text = this.folderBrowserDialog1.SelectedPath;
                String FreescaleVer = this.folderBrowserDialog1.SelectedPath;
            }
            else
            {
                browseCancelled = result == DialogResult.OK;
            }
        }
    }
}

