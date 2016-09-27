namespace BroadcomBuild
{
    partial class Tutorial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.URLBox = new System.Windows.Forms.TextBox();
            this.CheckoutButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.StdoutBox = new System.Windows.Forms.TextBox();
            this.StopButton = new System.Windows.Forms.Button();
            this.browse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.buildsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freescaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.broadcomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buildButton = new System.Windows.Forms.Button();
            this.toolsBox = new System.Windows.Forms.TextBox();
            this.toolsLabel = new System.Windows.Forms.Label();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Browse2 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // URLBox
            // 
            this.URLBox.Location = new System.Drawing.Point(50, 134);
            this.URLBox.Name = "URLBox";
            this.URLBox.Size = new System.Drawing.Size(403, 20);
            this.URLBox.TabIndex = 0;
            // 
            // CheckoutButton
            // 
            this.CheckoutButton.Location = new System.Drawing.Point(459, 382);
            this.CheckoutButton.Name = "CheckoutButton";
            this.CheckoutButton.Size = new System.Drawing.Size(84, 24);
            this.CheckoutButton.TabIndex = 1;
            this.CheckoutButton.Text = "Checkout";
            this.CheckoutButton.UseVisualStyleBackColor = true;
            this.CheckoutButton.Click += new System.EventHandler(this.Checkout_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "KEY URL:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(213, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Choose Install Location";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(213, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(213, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Choose the folder in which to install Broadcom.";
            // 
            // StdoutBox
            // 
            this.StdoutBox.Location = new System.Drawing.Point(50, 228);
            this.StdoutBox.Multiline = true;
            this.StdoutBox.Name = "StdoutBox";
            this.StdoutBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StdoutBox.Size = new System.Drawing.Size(390, 237);
            this.StdoutBox.TabIndex = 6;
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(459, 289);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(84, 24);
            this.StopButton.TabIndex = 7;
            this.StopButton.Text = "Cancel";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // browse
            // 
            this.browse.Location = new System.Drawing.Point(459, 134);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(84, 24);
            this.browse.TabIndex = 8;
            this.browse.Text = "Browse";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Window;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(503, 53);
            this.menuStrip1.MdiWindowListItem = this.buildsToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(59, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // buildsToolStripMenuItem
            // 
            this.buildsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.freescaleToolStripMenuItem,
            this.broadcomToolStripMenuItem});
            this.buildsToolStripMenuItem.Name = "buildsToolStripMenuItem";
            this.buildsToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.buildsToolStripMenuItem.Text = "Builds";
            // 
            // freescaleToolStripMenuItem
            // 
            this.freescaleToolStripMenuItem.Name = "freescaleToolStripMenuItem";
            this.freescaleToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.freescaleToolStripMenuItem.Text = "Freescale";
            this.freescaleToolStripMenuItem.Click += new System.EventHandler(this.freescaleToolStripMenuItem_Click);
            // 
            // broadcomToolStripMenuItem
            // 
            this.broadcomToolStripMenuItem.Name = "broadcomToolStripMenuItem";
            this.broadcomToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.broadcomToolStripMenuItem.Text = "Broadcom";
            this.broadcomToolStripMenuItem.Click += new System.EventHandler(this.broadcomToolStripMenuItem_Click);
            // 
            // buildButton
            // 
            this.buildButton.Location = new System.Drawing.Point(459, 441);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(84, 24);
            this.buildButton.TabIndex = 11;
            this.buildButton.Text = "Build";
            this.buildButton.UseVisualStyleBackColor = true;
            this.buildButton.Click += new System.EventHandler(this.buildButton_Click);
            // 
            // toolsBox
            // 
            this.toolsBox.Location = new System.Drawing.Point(50, 181);
            this.toolsBox.Name = "toolsBox";
            this.toolsBox.Size = new System.Drawing.Size(303, 20);
            this.toolsBox.TabIndex = 12;
            // 
            // toolsLabel
            // 
            this.toolsLabel.AutoSize = true;
            this.toolsLabel.Location = new System.Drawing.Point(47, 165);
            this.toolsLabel.Name = "toolsLabel";
            this.toolsLabel.Size = new System.Drawing.Size(339, 13);
            this.toolsLabel.TabIndex = 13;
            this.toolsLabel.Text = "zOEMTools URL: (Must be placed in /DM3/Broadcom Linux File Path)";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "Freescale";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem3.Text = "Broadcom";
            // 
            // Browse2
            // 
            this.Browse2.Location = new System.Drawing.Point(459, 177);
            this.Browse2.Name = "Browse2";
            this.Browse2.Size = new System.Drawing.Size(84, 24);
            this.Browse2.TabIndex = 15;
            this.Browse2.Text = "Browse";
            this.Browse2.UseVisualStyleBackColor = true;
            this.Browse2.Click += new System.EventHandler(this.Browse2_Click);
            // 
            // Tutorial
            // 
            this.BackgroundImage = global::BroadcomBuild.Properties.Resources.alpha1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(571, 502);
            this.Controls.Add(this.Browse2);
            this.Controls.Add(this.toolsLabel);
            this.Controls.Add(this.toolsBox);
            this.Controls.Add(this.buildButton);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StdoutBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CheckoutButton);
            this.Controls.Add(this.URLBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Tutorial";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.Form2_Load_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox URLBox;
        private System.Windows.Forms.Button CheckoutButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox StdoutBox;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem buildsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem freescaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem broadcomToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buildButton;
        private System.Windows.Forms.TextBox toolsBox;
        private System.Windows.Forms.Label toolsLabel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.Button Browse2;
    }
}