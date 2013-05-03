namespace Koberce
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chbAutobackup = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChooseBackupDir = new System.Windows.Forms.Button();
            this.txtBackupDir = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnChooseDBName = new System.Windows.Forms.Button();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtFtpPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFtpLogin = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWebParam = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWebServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtScannerServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtPriceCoef = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtPtcommCommand = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPtcommDir = new System.Windows.Forms.Button();
            this.txtPtcommDir = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnPtcomm = new System.Windows.Forms.Button();
            this.txtPtcomm = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(512, 533);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(431, 533);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chbAutobackup
            // 
            this.chbAutobackup.AutoSize = true;
            this.chbAutobackup.ForeColor = System.Drawing.SystemColors.Desktop;
            this.chbAutobackup.Location = new System.Drawing.Point(9, 53);
            this.chbAutobackup.Name = "chbAutobackup";
            this.chbAutobackup.Size = new System.Drawing.Size(171, 17);
            this.chbAutobackup.TabIndex = 2;
            this.chbAutobackup.Text = "Automatic backup after startup";
            this.chbAutobackup.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Backup directory";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnChooseBackupDir);
            this.groupBox1.Controls.Add(this.txtBackupDir);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chbAutobackup);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 77);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Backup";
            // 
            // btnChooseBackupDir
            // 
            this.btnChooseBackupDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseBackupDir.Location = new System.Drawing.Point(517, 24);
            this.btnChooseBackupDir.Name = "btnChooseBackupDir";
            this.btnChooseBackupDir.Size = new System.Drawing.Size(52, 23);
            this.btnChooseBackupDir.TabIndex = 5;
            this.btnChooseBackupDir.Text = "...";
            this.btnChooseBackupDir.UseVisualStyleBackColor = true;
            this.btnChooseBackupDir.Click += new System.EventHandler(this.btnChooseBackupDir_Click);
            // 
            // txtBackupDir
            // 
            this.txtBackupDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBackupDir.ForeColor = System.Drawing.Color.Maroon;
            this.txtBackupDir.Location = new System.Drawing.Point(112, 24);
            this.txtBackupDir.Name = "txtBackupDir";
            this.txtBackupDir.Size = new System.Drawing.Size(399, 20);
            this.txtBackupDir.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnChooseDBName);
            this.groupBox2.Controls.Add(this.txtDbName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(575, 64);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database";
            // 
            // btnChooseDBName
            // 
            this.btnChooseDBName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseDBName.Location = new System.Drawing.Point(517, 24);
            this.btnChooseDBName.Name = "btnChooseDBName";
            this.btnChooseDBName.Size = new System.Drawing.Size(52, 23);
            this.btnChooseDBName.TabIndex = 5;
            this.btnChooseDBName.Text = "...";
            this.btnChooseDBName.UseVisualStyleBackColor = true;
            this.btnChooseDBName.Click += new System.EventHandler(this.btnChooseDBName_Click);
            // 
            // txtDbName
            // 
            this.txtDbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDbName.ForeColor = System.Drawing.Color.Maroon;
            this.txtDbName.Location = new System.Drawing.Point(112, 24);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(399, 20);
            this.txtDbName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "DB name";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtFtpPassword);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtFtpLogin);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtWebParam);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtWebServer);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtScannerServer);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox3.Location = new System.Drawing.Point(12, 165);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(575, 155);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Servers";
            // 
            // txtFtpPassword
            // 
            this.txtFtpPassword.ForeColor = System.Drawing.Color.Maroon;
            this.txtFtpPassword.Location = new System.Drawing.Point(392, 50);
            this.txtFtpPassword.Name = "txtFtpPassword";
            this.txtFtpPassword.Size = new System.Drawing.Size(177, 20);
            this.txtFtpPassword.TabIndex = 12;
            this.txtFtpPassword.UseSystemPasswordChar = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(333, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Password";
            // 
            // txtFtpLogin
            // 
            this.txtFtpLogin.ForeColor = System.Drawing.Color.Maroon;
            this.txtFtpLogin.Location = new System.Drawing.Point(151, 50);
            this.txtFtpLogin.Name = "txtFtpLogin";
            this.txtFtpLogin.Size = new System.Drawing.Size(158, 20);
            this.txtFtpLogin.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(112, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Login";
            // 
            // txtWebParam
            // 
            this.txtWebParam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWebParam.ForeColor = System.Drawing.Color.Maroon;
            this.txtWebParam.Location = new System.Drawing.Point(196, 116);
            this.txtWebParam.Name = "txtWebParam";
            this.txtWebParam.Size = new System.Drawing.Size(373, 20);
            this.txtWebParam.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(6, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Web server parameter (XXX for code)";
            // 
            // txtWebServer
            // 
            this.txtWebServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWebServer.ForeColor = System.Drawing.Color.Maroon;
            this.txtWebServer.Location = new System.Drawing.Point(112, 90);
            this.txtWebServer.Name = "txtWebServer";
            this.txtWebServer.Size = new System.Drawing.Size(457, 20);
            this.txtWebServer.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(6, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Web server";
            // 
            // txtScannerServer
            // 
            this.txtScannerServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScannerServer.ForeColor = System.Drawing.Color.Maroon;
            this.txtScannerServer.Location = new System.Drawing.Point(112, 24);
            this.txtScannerServer.Name = "txtScannerServer";
            this.txtScannerServer.Size = new System.Drawing.Size(457, 20);
            this.txtScannerServer.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Scanner files server";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txtPriceCoef);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox4.Location = new System.Drawing.Point(12, 326);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(575, 56);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Miscellaneous";
            // 
            // txtPriceCoef
            // 
            this.txtPriceCoef.ForeColor = System.Drawing.Color.Maroon;
            this.txtPriceCoef.Location = new System.Drawing.Point(112, 24);
            this.txtPriceCoef.Name = "txtPriceCoef";
            this.txtPriceCoef.Size = new System.Drawing.Size(78, 20);
            this.txtPriceCoef.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(6, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Price coefficient";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.txtPtcommCommand);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.btnPtcommDir);
            this.groupBox5.Controls.Add(this.txtPtcommDir);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.btnPtcomm);
            this.groupBox5.Controls.Add(this.txtPtcomm);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox5.Location = new System.Drawing.Point(12, 388);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(575, 113);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Ptcomm - scanner communication program";
            // 
            // txtPtcommCommand
            // 
            this.txtPtcommCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPtcommCommand.ForeColor = System.Drawing.Color.Maroon;
            this.txtPtcommCommand.Location = new System.Drawing.Point(196, 76);
            this.txtPtcommCommand.Name = "txtPtcommCommand";
            this.txtPtcommCommand.Size = new System.Drawing.Size(373, 20);
            this.txtPtcommCommand.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(6, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(153, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Command (XXX for destination)";
            // 
            // btnPtcommDir
            // 
            this.btnPtcommDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPtcommDir.Location = new System.Drawing.Point(517, 50);
            this.btnPtcommDir.Name = "btnPtcommDir";
            this.btnPtcommDir.Size = new System.Drawing.Size(52, 23);
            this.btnPtcommDir.TabIndex = 8;
            this.btnPtcommDir.Text = "...";
            this.btnPtcommDir.UseVisualStyleBackColor = true;
            this.btnPtcommDir.Click += new System.EventHandler(this.btnPtcommDir_Click);
            // 
            // txtPtcommDir
            // 
            this.txtPtcommDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPtcommDir.ForeColor = System.Drawing.Color.Maroon;
            this.txtPtcommDir.Location = new System.Drawing.Point(112, 50);
            this.txtPtcommDir.Name = "txtPtcommDir";
            this.txtPtcommDir.Size = new System.Drawing.Size(399, 20);
            this.txtPtcommDir.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(6, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Destination directory";
            // 
            // btnPtcomm
            // 
            this.btnPtcomm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPtcomm.Location = new System.Drawing.Point(517, 24);
            this.btnPtcomm.Name = "btnPtcomm";
            this.btnPtcomm.Size = new System.Drawing.Size(52, 23);
            this.btnPtcomm.TabIndex = 5;
            this.btnPtcomm.Text = "...";
            this.btnPtcomm.UseVisualStyleBackColor = true;
            this.btnPtcomm.Click += new System.EventHandler(this.btnPtcomm_Click);
            // 
            // txtPtcomm
            // 
            this.txtPtcomm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPtcomm.ForeColor = System.Drawing.Color.Maroon;
            this.txtPtcomm.Location = new System.Drawing.Point(112, 24);
            this.txtPtcomm.Name = "txtPtcomm";
            this.txtPtcomm.Size = new System.Drawing.Size(399, 20);
            this.txtPtcomm.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(6, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Path to program";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 568);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SettingsForm_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chbAutobackup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnChooseBackupDir;
        private System.Windows.Forms.TextBox txtBackupDir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnChooseDBName;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtWebServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtScannerServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWebParam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtPriceCoef;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnPtcomm;
        private System.Windows.Forms.TextBox txtPtcomm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPtcommDir;
        private System.Windows.Forms.TextBox txtPtcommDir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPtcommCommand;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFtpPassword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFtpLogin;
        private System.Windows.Forms.Label label10;
    }
}