using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BroadcomBuild
{
    public partial class UserSettings : Form
    {
        public UserSettings()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            int Checkout = 0;
            int Update = 0;
            int Build = 0;
            OKButton.Visible = false;
            // not runnable:
            //All 0's 
            //If they enter in just Build (checkout = 0 & update = 0) must ask for file URL.
            //Checkout option for Freescale & Broadcom require a URL for checkout as while... 
            if (FreescaleCheckout.Checked)
            {
                Checkout = 1;
                CheckoutURLF.Text = "Checkout Freescale URL:";
                SubmitButton.Visible = true;
                CheckoutBoxF.Visible = true;

                lockCheckboxs(FreescaleCheckout);
                //check if sumbit button is clicked. 
            }
            if (BroadcomCheckout.Checked)
            {
                Checkout = 2;
            }
            if (FreescaleCheckout.Checked && BroadcomCheckout.Checked)
            {
                Checkout = 3;
            }
            if (FreescaleUpdate.Checked)
            {
                Update = 1;
            }
            if (BroadcomUpdate.Checked)
            {
                Update = 2;
            }
            if (FreescaleUpdate.Checked && BroadcomUpdate.Checked)
            {
                Update = 3;
            }
            if (FreescaleBuild.Checked)
            {
                Build = 1;
            }
            if (BroadcomBuild.Checked)
            {
                Build = 2;
            }
            if (FreescaleBuild.Checked && BroadcomBuild.Checked)
            {
                Build = 3;
            }
        }

        private void lockCheckboxs(CheckBox checkbox)
        { 
            FreescaleCheckout.Enabled = false;
            BroadcomCheckout.Enabled = false;
            FreescaleUpdate.Enabled = false;
            BroadcomUpdate.Enabled = false;
            FreescaleBuild.Enabled = false;
            FreescaleBuild.Enabled = false;
            checkbox.Enabled = true;
        }

        private void unlockCheckboxs(CheckBox checkbox)
        {
            FreescaleCheckout.Enabled = true;
            BroadcomCheckout.Enabled = true;
            FreescaleUpdate.Enabled = true;
            BroadcomUpdate.Enabled = true;
            FreescaleBuild.Enabled = true;
            FreescaleBuild.Enabled = true;
            checkbox.Enabled = false;
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}
