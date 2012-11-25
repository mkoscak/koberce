namespace Koberce
{
    partial class Progress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Progress));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblCurrentOp = new System.Windows.Forms.Label();
            this.lblOp = new System.Windows.Forms.Label();
            this.lblProgrress = new System.Windows.Forms.Label();
            this.lblProg = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(15, 30);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(581, 26);
            this.progressBar.TabIndex = 0;
            // 
            // lblCurrentOp
            // 
            this.lblCurrentOp.AutoSize = true;
            this.lblCurrentOp.Location = new System.Drawing.Point(12, 14);
            this.lblCurrentOp.Name = "lblCurrentOp";
            this.lblCurrentOp.Size = new System.Drawing.Size(94, 13);
            this.lblCurrentOp.TabIndex = 1;
            this.lblCurrentOp.Text = "Current operation: ";
            // 
            // lblOp
            // 
            this.lblOp.AutoSize = true;
            this.lblOp.Location = new System.Drawing.Point(112, 14);
            this.lblOp.Name = "lblOp";
            this.lblOp.Size = new System.Drawing.Size(33, 13);
            this.lblOp.TabIndex = 2;
            this.lblOp.Text = "None";
            // 
            // lblProgrress
            // 
            this.lblProgrress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgrress.AutoSize = true;
            this.lblProgrress.Location = new System.Drawing.Point(12, 59);
            this.lblProgrress.Name = "lblProgrress";
            this.lblProgrress.Size = new System.Drawing.Size(54, 13);
            this.lblProgrress.TabIndex = 3;
            this.lblProgrress.Text = "Progress: ";
            // 
            // lblProg
            // 
            this.lblProg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProg.AutoSize = true;
            this.lblProg.Location = new System.Drawing.Point(112, 59);
            this.lblProg.Name = "lblProg";
            this.lblProg.Size = new System.Drawing.Size(33, 13);
            this.lblProg.TabIndex = 4;
            this.lblProg.Text = "None";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(496, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 26);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 110);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblProg);
            this.Controls.Add(this.lblProgrress);
            this.Controls.Add(this.lblOp);
            this.Controls.Add(this.lblCurrentOp);
            this.Controls.Add(this.progressBar);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Progress";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblCurrentOp;
        private System.Windows.Forms.Label lblOp;
        private System.Windows.Forms.Label lblProgrress;
        private System.Windows.Forms.Label lblProg;
        private System.Windows.Forms.Button btnCancel;
    }
}