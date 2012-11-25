namespace Koberce
{
    partial class InventoryResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryResult));
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.btnExportMain = new System.Windows.Forms.Button();
            this.lblMissingMain = new System.Windows.Forms.Label();
            this.gridMissingMain = new Koberce.CustomDataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.gridMissingInv = new Koberce.CustomDataGridView();
            this.lblMissingInv = new System.Windows.Forms.Label();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMissingMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMissingInv)).BeginInit();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.btnExportMain);
            this.splitMain.Panel1.Controls.Add(this.lblMissingMain);
            this.splitMain.Panel1.Controls.Add(this.gridMissingMain);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.button1);
            this.splitMain.Panel2.Controls.Add(this.gridMissingInv);
            this.splitMain.Panel2.Controls.Add(this.lblMissingInv);
            this.splitMain.Size = new System.Drawing.Size(1123, 601);
            this.splitMain.SplitterDistance = 244;
            this.splitMain.TabIndex = 0;
            // 
            // btnExportMain
            // 
            this.btnExportMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportMain.Image = global::Koberce.Properties.Resources.xlsx_win_icon;
            this.btnExportMain.Location = new System.Drawing.Point(87, 535);
            this.btnExportMain.Name = "btnExportMain";
            this.btnExportMain.Size = new System.Drawing.Size(150, 59);
            this.btnExportMain.TabIndex = 2;
            this.btnExportMain.Text = "Export";
            this.btnExportMain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportMain.UseVisualStyleBackColor = true;
            this.btnExportMain.Click += new System.EventHandler(this.btnExportMain_Click);
            // 
            // lblMissingMain
            // 
            this.lblMissingMain.AutoSize = true;
            this.lblMissingMain.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblMissingMain.Location = new System.Drawing.Point(3, 9);
            this.lblMissingMain.Name = "lblMissingMain";
            this.lblMissingMain.Size = new System.Drawing.Size(131, 13);
            this.lblMissingMain.TabIndex = 1;
            this.lblMissingMain.Text = "Items missing in main table";
            // 
            // gridMissingMain
            // 
            this.gridMissingMain.AllowUserToAddRows = false;
            this.gridMissingMain.AllowUserToDeleteRows = false;
            this.gridMissingMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridMissingMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMissingMain.Location = new System.Drawing.Point(3, 25);
            this.gridMissingMain.Name = "gridMissingMain";
            this.gridMissingMain.Size = new System.Drawing.Size(234, 504);
            this.gridMissingMain.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Image = global::Koberce.Properties.Resources.xlsx_win_icon;
            this.button1.Location = new System.Drawing.Point(718, 535);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 59);
            this.button1.TabIndex = 3;
            this.button1.Text = "Export";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gridMissingInv
            // 
            this.gridMissingInv.AllowUserToAddRows = false;
            this.gridMissingInv.AllowUserToDeleteRows = false;
            this.gridMissingInv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridMissingInv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMissingInv.Location = new System.Drawing.Point(6, 25);
            this.gridMissingInv.Name = "gridMissingInv";
            this.gridMissingInv.Size = new System.Drawing.Size(862, 504);
            this.gridMissingInv.TabIndex = 1;
            // 
            // lblMissingInv
            // 
            this.lblMissingInv.AutoSize = true;
            this.lblMissingInv.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblMissingInv.Location = new System.Drawing.Point(3, 9);
            this.lblMissingInv.Name = "lblMissingInv";
            this.lblMissingInv.Size = new System.Drawing.Size(126, 13);
            this.lblMissingInv.TabIndex = 0;
            this.lblMissingInv.Text = "Items missing in inventory";
            // 
            // InventoryResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 601);
            this.Controls.Add(this.splitMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InventoryResult";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inventory result";
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel1.PerformLayout();
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.Panel2.PerformLayout();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMissingMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMissingInv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Label lblMissingMain;
        private CustomDataGridView gridMissingMain;
        private System.Windows.Forms.Label lblMissingInv;
        private CustomDataGridView gridMissingInv;
        private System.Windows.Forms.Button btnExportMain;
        private System.Windows.Forms.Button button1;
    }
}