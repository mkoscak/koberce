using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Koberce
{
    public partial class SettingsForm : Form
    {

        public SettingsForm()
        {
            InitializeComponent();

            chbAutobackup.Checked = Properties.Settings.Default.AutoBackup;
            txtBackupDir.Text = Properties.Settings.Default.BackupDirectory;
            txtDbName.Text = Properties.Settings.Default.DbName;
            txtScannerServer.Text = Properties.Settings.Default.ScannerServer;
            txtWebServer.Text = Properties.Settings.Default.WebServer;
            txtWebParam.Text = Properties.Settings.Default.WebParam;
            txtPriceCoef.Text = Properties.Settings.Default.PriceCoef.ToString();
            txtPtcomm.Text = Properties.Settings.Default.Ptcomm;
            txtPtcommDir.Text = Properties.Settings.Default.PtcommDir;
            txtPtcommCommand.Text = Properties.Settings.Default.PtcommCommand;
            txtFtpLogin.Text = Properties.Settings.Default.FtpLogin;
            txtFtpPassword.Text = Properties.Settings.Default.FtpPassword;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoBackup = chbAutobackup.Checked;
            Properties.Settings.Default.BackupDirectory = txtBackupDir.Text;
            Properties.Settings.Default.DbName = txtDbName.Text;
            Properties.Settings.Default.ScannerServer = txtScannerServer.Text;
            Properties.Settings.Default.WebServer = txtWebServer.Text;
            Properties.Settings.Default.WebParam = txtWebParam.Text;
            Properties.Settings.Default.Ptcomm = txtPtcomm.Text;
            Properties.Settings.Default.PtcommDir = txtPtcommDir.Text;
            Properties.Settings.Default.PtcommCommand = txtPtcommCommand.Text;
            Properties.Settings.Default.FtpLogin = txtFtpLogin.Text;
            Properties.Settings.Default.FtpPassword = txtFtpPassword.Text;

            double coef;
            if (double.TryParse(txtPriceCoef.Text, out coef))
            {
                Properties.Settings.Default.PriceCoef = coef;
            }
            else
                MessageBox.Show(this, "Price coefficient not valid! Try change . to , or vice versa. Coefficient change will be discarded!", "Coefficient format error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Properties.Settings.Default.Save();

            Close();
        }

        private void btnChooseBackupDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdd = new FolderBrowserDialog();
            fdd.SelectedPath = txtBackupDir.Text;
            fdd.ShowNewFolderButton = true;
            fdd.Description = "Choose backup directory";
            
            if (fdd.ShowDialog() == DialogResult.OK)
            {
                txtBackupDir.Text = fdd.SelectedPath;
            }
        }

        private void btnChooseDBName_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = "db";
            ofd.Filter = "DB files|*.db";
            ofd.Multiselect = false;
            ofd.InitialDirectory = Environment.CurrentDirectory;
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtDbName.Text = ofd.FileName;
            }
        }

        private void btnPtcomm_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = "exe";
            ofd.Filter = "Executable files|*.exe";
            ofd.Multiselect = false;
            ofd.InitialDirectory = Environment.CurrentDirectory;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtPtcomm.Text = ofd.FileName;
            }
        }

        private void btnPtcommDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdd = new FolderBrowserDialog();
            fdd.SelectedPath = txtPtcommDir.Text;
            fdd.ShowNewFolderButton = true;
            fdd.Description = "Choose destination directory";

            if (fdd.ShowDialog() == DialogResult.OK)
            {
                txtPtcommDir.Text = fdd.SelectedPath;
            }
        }

        private void SettingsForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                Close();
        }
    }
}
