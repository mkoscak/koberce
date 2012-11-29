using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Koberce
{
    public partial class Progress : Form
    {
        private Action<BackgroundWorker, DoWorkEventArgs, object> action;
        private Action PostAction;
        private BackgroundWorker bw;
        private object UserData;
        private bool ShowOkMessage;

        public Progress(int min, int max, string opName, string startProgress, Action<BackgroundWorker, DoWorkEventArgs, object> action, Action postProcess, object userData, bool cancelEnabled, bool showOkMsg)
        {
            InitializeComponent();

            progressBar.Minimum = min;
            progressBar.Maximum = max;
            lblOp.Text = opName;
            lblProg.Text = startProgress;

            this.action = action;
            this.UserData = userData;
            this.PostAction = postProcess;
            this.ShowOkMessage = showOkMsg;

            btnCancel.Enabled = cancelEnabled;
        }

        public void StartWorker()
        {
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;

            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            bw.RunWorkerAsync();
            ShowDialog();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool doPostProcess = true;

            if (e.Cancelled == true)
            {
                MessageBox.Show(this, "Operation canceled!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Visible = false;
                Close();
                doPostProcess = false;
            }
            else if (e.Error != null)
            {
                MessageBox.Show(this, "Error occured!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Visible = false;
                Close();
                doPostProcess = false;
            }
            else
            {
                if (ShowOkMessage)
                    MessageBox.Show(this, "Operation done!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Visible = false;
                Close();
            }

            if (PostAction != null && doPostProcess)
                PostAction();
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
                lblOp.Text = e.UserState.ToString();
            
            lblProg.Text = e.ProgressPercentage.ToString() + @"%";
            progressBar.Value = e.ProgressPercentage;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            action(worker, e, UserData);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (bw != null)
                bw.CancelAsync();
        }
    }
}
