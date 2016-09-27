using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using BroadcomBuild.Properties;
using System.IO;

namespace BroadcomBuild
{
    public partial class StartScreen : Form
    {
        Process VMProcess;
        String BoxName = "";
        public StartScreen()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Process proc = null;
            try
            {
                string batDir = string.Format(@"D:\");

                //System.Diagnostics.Process proc = new System.Diagnostics.Process();

                BoxName = VMname.Text;


                if (BoxName == "") //|| !IsValidLinuxFile(URL)) 
                {
                    MessageBox.Show("Unable to open VM Machine. Please enter in name of Machine", "Virtual Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Settings.Default["BoxName"] = VMname.Text;
                    Settings.Default.Save();
                    DialogResult result = MessageBox.Show("Would you like to checkout working copy of Broadcom from SVN? Estimated time 35-40 minutes",
                            "Broadcom Build", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.OK)
                    {
                        MessageBox.Show("Please have Virtual Box running", "Broadcom Build", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        VMProcess = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                //FileName = "C:\\Users\\peterssx\\Alpha\\Putty\\plink.exe",
                                FileName = "VBoxManage.exe",
                                //need to be able to send over sudo's password so it doesn't ask for it everytime.
                                Arguments = "startvm " + BoxName,
                                UseShellExecute = false,
                                RedirectStandardOutput = false,
                                RedirectStandardError = false,
                                CreateNoWindow = false,
                            }
                        };
                        VMProcess.Start();
                        VMProcess.WaitForExit();

                        Console.WriteLine(VMProcess.ExitCode);


                        // if (VMProcess.ExitCode != 0)
                        if (!IsVMRunning(BoxName)) { 
                            
                            MessageBox.Show("Unable to open VM Machine. Please enter in name of Machine", "Virtual Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            OptionsScreen frm3 = new OptionsScreen();
                            frm3.Show();
                            this.Owner = frm3;
                            this.Hide();
                            //Form1 is the owner of form2 there they will both be closed at the same time. 
                            //This method only works with two forms at a time. 
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        private Boolean IsVMRunning(String boxName)
        {
            VMProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    //FileName = "C:\\Users\\peterssx\\Alpha\\Putty\\plink.exe",
                    FileName = "VBoxManage.exe",
                    //need to be able to send over sudo's password so it doesn't ask for it everytime.
                    Arguments = "list runningvms",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = false,
                    CreateNoWindow = false,
                }
            };
            VMProcess.Start();

            //VMProcess.BeginOutputReadLine();

            StreamReader reader = VMProcess.StandardOutput;
            String listOfVMS = reader.ReadToEnd();

            //string listOfVMS = VMProcess.StandardOutput.ReadToEnd();

            VMProcess.WaitForExit();

            Console.WriteLine(listOfVMS);

            Boolean result = listOfVMS.Contains(boxName);
            Console.WriteLine(result);
            return result;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            VMname.Text = Settings.Default["BoxName"].ToString();
        }
    }
}
