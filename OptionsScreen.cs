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
    public partial class OptionsScreen : Form
    {
        public OptionsScreen()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Tutorial frm2 = new Tutorial();
            frm2.Show();
            this.Owner = frm2;
            this.Hide();
        }
    }
}
