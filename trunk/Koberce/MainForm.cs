﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace Koberce
{
    public enum TABS
    {
        MAIN,
        SOLD,
        FROM_SK,
        INVENTORY,
        BATCH,
        CUSTOMQUERY
    }

    public partial class MainForm : Form
    {
        private DBProvider db;  // databazovy provider
        private string DateTimeFormat = "yyyy-MM-dd";

        private CustomDataGridView DataGrid
        {
            get
            {
                if (tabControl1.SelectedIndex == (int)TABS.MAIN)
                    return Grid;
                else if (tabControl1.SelectedIndex == (int)TABS.SOLD)
                    return gridSell;
                else if (tabControl1.SelectedIndex == (int)TABS.FROM_SK)
                    return gridFromSK;
                else if (tabControl1.SelectedIndex == (int)TABS.INVENTORY)
                    return gridInventory;
                else if (tabControl1.SelectedIndex == (int)TABS.BATCH)
                    return gridBatch;
                else if (tabControl1.SelectedIndex == (int)TABS.CUSTOMQUERY)
                    return gridQueryRes;

                return null;
            }

            set
            {
                Grid = value;
            }
        }

        private Label LabelAll
        {
            get
            {
                if (tabControl1.SelectedIndex == (int)TABS.MAIN)
                    return lblAllCount;
                else if (tabControl1.SelectedIndex == (int)TABS.SOLD)
                    return lblSoldAll;
                else if (tabControl1.SelectedIndex == (int)TABS.FROM_SK)
                    return lblFromSKAll;
                else if (tabControl1.SelectedIndex == (int)TABS.INVENTORY)
                    return lblInvAllCount;
                else if (tabControl1.SelectedIndex == (int)TABS.BATCH)
                    return lblBatchItemCount;
                else if (tabControl1.SelectedIndex == (int)TABS.CUSTOMQUERY)
                    return lblQueryResCount;

                return lblAllCount;
            }

            set
            {
                lblAllCount = value;
            }
        }

        private Label LabelSelected
        {
            get
            {
                if (tabControl1.SelectedIndex == (int)TABS.MAIN)
                    return lblSelCount;
                else if (tabControl1.SelectedIndex == (int)TABS.SOLD)
                    return lblSoldSel;
                else if (tabControl1.SelectedIndex == (int)TABS.FROM_SK)
                    return lblFromSKSel;
                else if (tabControl1.SelectedIndex == (int)TABS.INVENTORY)
                    return lblInvSelCount;
                else if (tabControl1.SelectedIndex == (int)TABS.BATCH)
                    return lblBatchSelCount;
                else if (tabControl1.SelectedIndex == (int)TABS.CUSTOMQUERY)
                    return lblQuerySelCount;

                return lblSelCount;
            }

            set
            {
                lblSelCount = value;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            db = new DBProvider(Properties.Settings.Default.DbName);

            Visible = true;

            try
            {
                if (Properties.Settings.Default.AutoBackup && MessageBox.Show(this, "Do you want to backup database before any changes?", "Database backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!Directory.Exists(Properties.Settings.Default.BackupDirectory))
                        Directory.CreateDirectory(Properties.Settings.Default.BackupDirectory);

                    string destFile = Properties.Settings.Default.BackupDirectory + "\\backup_" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Ticks + ".db";
                    File.Copy(Properties.Settings.Default.DbName, destFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error while making backup: "+ex.ToString(), "Backup error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RefreshItems();
            ReadQueries();
        }

        private string[] Queries = new string[5];
        private void ReadQueries()
        {
            string fname = "Queries\\queryX.sql";
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    var name = fname.Replace('X', char.Parse(i.ToString()));
                    StreamReader sr = new StreamReader(name);
                    var content = sr.ReadToEnd().Trim();

                    Queries[i-1] = content;

                    sr.Close();

                    Button actualButton = null;
                    switch (i)
                    {
                        case 1:
                            actualButton = btnQuery1;
                            break;
                        case 2:
                            actualButton = btnQuery2;
                            break;
                        case 3:
                            actualButton = btnQuery3;
                            break;
                        case 4:
                            actualButton = btnQuery4;
                            break;
                        case 5:
                            actualButton = btnQuery5;
                            break;
                        default:
                            break;
                    }
                    if (actualButton != null)
                    {
                        actualButton.Text = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0].Replace("--", "").Trim();
                        actualButton.Enabled = true;
                    }
                }
                catch (Exception)
                {
                    // vynimku ignorujeme, ak neexistuje query, nevadi
                }
            }            
        }

        // refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void RefreshItems()
        {
            if (DataGrid == null)
                return;

            string condition = GetFilter();
            if (tabControl1.SelectedIndex > (int)TABS.INVENTORY)
                return;

            DataSet ds = null;

            if (tabControl1.SelectedIndex == (int)TABS.INVENTORY)
                // inventar ma len kody - join na main a vratime len tieto produkty
                ds = db.ExecuteQuery(string.Format("select A.CODE, B.* from {0} A left join {1} B on A.CODE = B.CODE where {2} order by B.CODE desc ", DBProvider.TableNames[(int)TABS.INVENTORY], DBProvider.TableNames[(int)TABS.MAIN], condition));
            else if (tabControl1.SelectedIndex == (int)TABS.SOLD)
                // sold ma len kody, datum a cenu predaja - join na main a vratime len tieto produkty
                ds = db.ExecuteQuery(string.Format("select A.CODE, A.SELLDATE, A.SELLPRICE, B.*, cast(B.length as real )* cast(B.width as real)/10000 as Area from {0} A left join {1} B on A.CODE = B.CODE where {2} order by B.CODE desc ", DBProvider.TableNames[(int)TABS.SOLD], DBProvider.TableNames[(int)TABS.MAIN], condition));
            else if (tabControl1.SelectedIndex == (int)TABS.FROM_SK)
                // fromsk ma len kody, datum a cenu predaja - join na main a vratime len tieto produkty
                ds = db.ExecuteQuery(string.Format("select A.CODE, B.* from {0} A left join {1} B on A.CODE = B.CODE where {2} order by B.CODE desc ", DBProvider.TableNames[(int)TABS.FROM_SK], DBProvider.TableNames[(int)TABS.MAIN], condition));
            else
                ds = db.ExecuteQuery(DBProvider.TableNames[tabControl1.SelectedIndex], " where " + condition, " order by code desc");

            if (ds == null || ds.Tables == null || ds.Tables.Count == 0)
                return;

            DataGrid.DataSource = ds.Tables[0];

            LabelAll.Text = ds.Tables[0].Rows.Count.ToString();
            LabelSelected.Text = GetSelectedRowCount().ToString();
            if (tabControl1.SelectedIndex == (int)TABS.SOLD)
                lblSellPriceSum.Text = GetSellSum(ds.Tables[0]).ToString();
        }

        double GetSellSum(DataTable table)
        {
            double ret = 0;

            int index = -1;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].ColumnName.ToLower() == "sellprice")
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                return ret;

            for (int j = 0; j < table.Rows.Count; j++)
            {
                try
                {
                    double val = double.Parse(table.Rows[j][index].ToString());

                    ret += val;
                }
                catch (Exception)
                {
                }
            }

            return ret;
        }

        // decode text data
        private void btnDecode_Click(object sender, EventArgs e)
        {
            try
            {
                var ds = BatchInsert.Decode(txtBatch.Lines);
                gridBatch.DataSource = ds;
                lblBatchItemCount.Text = ds.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error while decoding!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // insert multiple data
        private void btnStartBatch_Click(object sender, EventArgs e)
        {
            var ds = gridBatch.DataSource as BindingList<DataItem>;
            try
            {
                BatchInsert.InsertMultiple(ds, db);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error while inserting!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddEditMain frm = new AddEditMain(db, DBProvider.TableNames[0], null, EditMode.Add);

            frm.ShowDialog();

            btnRefresh.PerformClick();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateItem();
        }

        private void UpdateItem()
        {
            var sel = DataGrid.SelectedCells;
            if (sel.Count == 0)
            {
                MessageBox.Show(this, "Select item to edit first!", "Edit item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var code = DataGrid["Code", sel[0].RowIndex].Value.ToString();

            if (tabControl1.SelectedIndex == (int)TABS.MAIN)
            {
                AddEditMain frm = new AddEditMain(db, DBProvider.TableNames[0], code, EditMode.Update);
                frm.Text = "Edit item";
                if (frm.ShowDialog() == DialogResult.OK)
                    btnToolRefresh.PerformClick();
            }
            else if (tabControl1.SelectedIndex == (int)TABS.SOLD)
            {
                EditSold frm = new EditSold(db, DBProvider.TableNames[1], code);
                if (frm.ShowDialog() == DialogResult.OK)
                    btnToolRefresh.PerformClick();
            }
        }

        private void Grid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            UpdateItem();
        }

        private void AddFilter(StringBuilder sb, string text, string columnName)
        {
            if (text != null && text.Trim().Length > 0)
            {
                string op = " like ";
                string neg = "";
                string perc =  @"%";
                string quotes = "\"";

                if (text.Contains('!'))
                {
                    neg = " not ";
                    text = text.Replace("!", "");
                }
                else if (text.Contains('>'))
                {
                    op = " > ";
                    text = text.Replace(">", "");
                    if (text.Contains('='))
                        op = " >= ";
                    text = text.Replace("=", "");
                    perc = "";
                    quotes = "";

                    int ival;
                    if (!int.TryParse(text.Trim(), out ival))
                        return;

                    columnName = string.Format("CAST({0} as integer)", columnName);
                }
                else if (text.Contains('<'))
                {
                    op = " < ";
                    text = text.Replace("<", "");
                    if (text.Contains('='))
                        op = " <= ";
                    text = text.Replace("=", "");
                    perc = "";
                    quotes = "";

                    int ival;
                    if (!int.TryParse(text.Trim(), out ival))
                        return;

                    columnName = string.Format("CAST({0} as integer)", columnName);
                }

                if (text.Trim().Length == 0)
                    return;

                if (sb.Length > 0)
                    sb.AppendFormat(" AND ");

                sb.AppendFormat(" {0} {1} {2} {5}{4}{3}{4}{5} ", columnName, neg, op, text, perc, quotes);
            }
        }

        private void AddFilterDate(StringBuilder sb, string text, string columnName, string op)
        {
            if (text != null && text.Trim().Length > 0)
            {
                if (text.Trim().Length == 0)
                    return;

                if (sb.Length > 0)
                    sb.AppendFormat(" AND ");

                sb.AppendFormat(" {0} {1} \"{2}\" ", columnName, op, text);
            }
        }

        private string GetFilter()
        {
            StringBuilder sb = new StringBuilder();

            if (tabControl1.SelectedIndex == (int)TABS.MAIN)
            {
                AddFilter(sb, txtFilCode.Text, "A.CODE");
                AddFilter(sb, txtFilName.Text, "ITEMTITLE");
                AddFilter(sb, txtFilCountry.Text, "COUNTRY");
                AddFilter(sb, txtFilSupplier.Text, "SUPPLIER");
                AddFilter(sb, txtFilSupplierNr.Text, "SUPPLIER_NR");
                if (dtDateFrom.Checked)
                    AddFilterDate(sb, dtDateFrom.Value.ToString(DateTimeFormat), "DATE", ">=");
                if (dtDateTo.Checked)
                    AddFilterDate(sb, dtDateTo.Value.ToString(DateTimeFormat), "DATE", "<=");
                AddFilter(sb, txtFilMvDate.Text, "MVDATE");
                AddFilter(sb, txtQuantity.Text, "QUANTITY");
                AddFilter(sb, txtFilInv.Text, "INVOICE");
                AddFilter(sb, txtFilMat.Text, "MATERIAL");
                AddFilter(sb, txtFilLength.Text, "LENGTH");
                AddFilter(sb, txtFilWidth.Text, "WIDTH");
            }
            else
                if (tabControl1.SelectedIndex == (int)TABS.SOLD)
            {
                AddFilter(sb, textFilCodeSold.Text, "A.CODE");
                AddFilter(sb, textFilNameSold.Text, "ITEMTITLE");
                AddFilter(sb, textFilCountrySold.Text, "COUNTRY");
                AddFilter(sb, textFilSupSold.Text, "SUPPLIER");
                AddFilter(sb, txtFilSupNrSold.Text, "SUPPLIER_NR");
                AddFilter(sb, textFilPriceSold.Text, "VK_NETTO");
                if (dtSellDateFrom.Checked)
                    AddFilterDate(sb, dtSellDateFrom.Value.ToString(DateTimeFormat), "SELLDATE", ">=");
                if (dtSellDateTo.Checked)
                    AddFilterDate(sb, dtSellDateTo.Value.ToString(DateTimeFormat), "SELLDATE", "<=");
                AddFilter(sb, txtFilSellPrice.Text, "SELLPRICE");
                AddFilter(sb, txtFilSoldLength.Text, "LENGTH");
                AddFilter(sb, txtFilSoldWidth.Text, "WIDTH");
            }
            else
                if (tabControl1.SelectedIndex == (int)TABS.FROM_SK)
            {
                AddFilter(sb, txtFilSKCode.Text, "A.CODE");
                AddFilter(sb, txtFilSKTitle.Text, "ITEMTITLE");
                AddFilter(sb, txtFilSKCountry.Text, "COUNTRY");
                AddFilter(sb, txtFilSKSupplier.Text, "SUPPLIER");
                AddFilter(sb, txtFilSKSupplierNr.Text, "SUPPLIER_NR");
                AddFilter(sb, txtFilSKVK.Text, "VK_NETTO");
                /*if (dtpFilSKSellFrom.Checked)
                    AddFilterDate(sb, dtpFilSKSellFrom.Value.ToString(DateTimeFormat), "SELLDATE", ">=");
                if (dtpFilSKSellTo.Checked)
                    AddFilterDate(sb, dtpFilSKSellTo.Value.ToString(DateTimeFormat), "SELLDATE", "<=");
                AddFilter(sb, txtFilSKSellPrice.Text, "SELLPRICE");*/
                AddFilter(sb, txtFilSKLength.Text, "LENGTH");
                AddFilter(sb, txtFilSKWidth.Text, "WIDTH");
            }
            else
                if (tabControl1.SelectedIndex == (int)TABS.INVENTORY)
                {
                    AddFilter(sb, txtFilInvCode.Text, "A.CODE");
                    AddFilter(sb, txtFilInvName.Text, "ITEMTITLE");
                    AddFilter(sb, txtFilInvCountry.Text, "COUNTRY");
                    AddFilter(sb, txtFilInvSup.Text, "SUPPLIER");
                    AddFilter(sb, txtFilInvSupNr.Text, "SUPPLIER_NR");
                    if (dtInvDateFrom.Checked)
                        AddFilterDate(sb, dtInvDateFrom.Value.ToString(DateTimeFormat), "DATE", ">=");
                    if (dtInvDateTo.Checked)
                        AddFilterDate(sb, dtInvDateTo.Value.ToString(DateTimeFormat), "DATE", "<=");
                    AddFilter(sb, txtFilInvmvDate.Text, "MVDATE");
                    AddFilter(sb, txtFilInvQuantity.Text, "QUANTITY");
                    AddFilter(sb, txtFilInvInvoice.Text, "INVOICE");
                    AddFilter(sb, txtFilInvMaterial.Text, "MATERIAL");
                    AddFilter(sb, txtFilInvLength.Text, "LENGTH");
                    AddFilter(sb, txtFilInvWidth.Text, "WIDTH");
                }

            if (sb.Length == 0)
                sb.AppendFormat("1 = 1");

            sb.AppendFormat(" AND A.VALID = 1 ");

            return sb.ToString();
        }

        private void RefreshFilter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox tb = sender as TextBox;
                if (tb.Text == null || tb.Text.Length == 0)
                    tb.BackColor = Color.White;
                else
                {
                    if (tb.Text.Contains('!'))
                        tb.BackColor = Color.OrangeRed;
                    else
                        tb.BackColor = Color.YellowGreen;
                }
            }

            RefreshItems();
        }

        string[] GetSelectedCodes()
        {
            var sel = DataGrid.SelectedCells;
            if (sel.Count == 0)
                return null;

            List<string> codes = new List<string>(sel.Count);
            for (int i = 0; i < sel.Count; i++)
            {
                codes.Add(DataGrid["Code", sel[i].RowIndex].Value.ToString());
            }
            codes = codes.Distinct().ToList();

            return codes.Where(c => c.Trim().Length > 0).ToArray();
        }

        // remove selected items
        private void btnRemoveSel_Click(object sender, EventArgs e)
        {
            var codes = GetSelectedCodes();
            if (codes == null)
            {
                MessageBox.Show(this, "Select some item(s) first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(this, string.Format("Do you really want to delete {0} selected items?", codes.Length), "Delete item(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                string command = string.Format("update {0} set VALID = 0 WHERE code in ( {1} ) ", DBProvider.TableNames[tabControl1.SelectedIndex], string.Join(",", codes));
                db.ExecuteNonQuery(command);

                command = string.Format("update {0} set QUANTITY = 1 WHERE code in ( {1} ) ", DBProvider.TableNames[(int)TABS.MAIN], string.Join(",", codes));
                db.ExecuteNonQuery(command);

                MessageBox.Show(this, "Data deleted succesfully!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error while deleting!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnPreviousTab_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex > 0)
                tabControl1.SelectedIndex = tabControl1.SelectedIndex - 1;
            else
                tabControl1.SelectedIndex = tabControl1.TabCount - 1;
        }

        private void btnNextTab_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < tabControl1.TabCount - 1)
                tabControl1.SelectedIndex = tabControl1.SelectedIndex + 1;
            else
                tabControl1.SelectedIndex = 0;
        }

        private void btnToolRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh_Click(sender, e);
        }

        private void btnToolNew_Click(object sender, EventArgs e)
        {
            btnAddNew_Click(sender, e);
        }

        private void btnToolEdit_Click(object sender, EventArgs e)
        {
            UpdateItem();
        }

        private void btnToolRemove_Click(object sender, EventArgs e)
        {
            btnRemoveSel_Click(sender, e);
        }

        private void uploadFile(string FTPAddress, string filePath, string username, string password)
        {
            //Create FTP request
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(filePath));

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(username, password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            FileStream stream = File.OpenRead(filePath);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();
        }

        private void downloadFile(string ftpAddress, string fileName, string login, string password, string destDirectory)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpAddress + "/" + fileName);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(login, password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            File.WriteAllText(destDirectory + "\\" + fileName, reader.ReadToEnd());

            reader.Close();
            response.Close();
        }

        public void doUpload(BackgroundWorker bw, DoWorkEventArgs e, object userData)
        {
            var soldName = Properties.Settings.Default.PtcommDir + "\\"+userData.ToString();
            var server = Properties.Settings.Default.ScannerServer;
            var login = Properties.Settings.Default.FtpLogin;
            var passwd = Properties.Settings.Default.FtpPassword;

            try
            {
                bw.ReportProgress(0, "Uploading " + userData.ToString() + "..");
                uploadFile(server, soldName, login, passwd);
                bw.ReportProgress(100, "Done.");
            }
            catch (Exception ex)
            {
                Invoke(new Action(() => MessageBox.Show(this, "Error while uploading from " + server + ": " + ex.ToString(), "Download from server", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                e.Cancel = true;
            }
        }

        public void doDownload(BackgroundWorker bw, DoWorkEventArgs e, object userData)
        {
            var server = Properties.Settings.Default.ScannerServer;
            var login = Properties.Settings.Default.FtpLogin;
            var passwd = Properties.Settings.Default.FtpPassword;

            try
            {
                bw.ReportProgress(0, "Downloading " + userData.ToString() + "..");
                downloadFile(server, userData.ToString(), login, passwd, Properties.Settings.Default.PtcommDir);
            }
            catch (Exception ex)
            {
                Invoke(new Action(() => MessageBox.Show(this, "Error while downloading from " + server + ": " + ex.ToString(), "Download from server", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                e.Cancel = true;
            }
        }

        private void btnSetSold_Click(object sender, EventArgs e)
        {
            var codes = GetSelectedCodes();

            if (MessageBox.Show(this, string.Format("Do you really want to sold {0} selected item(s)?", codes.Length), "Sold item(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                Progress p = new Progress(0, 100, "Selling items..", "checking..", SoldItems, RefreshItems, codes, true, true);
                p.StartWorker();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error while updating!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SoldItems(BackgroundWorker bw, DoWorkEventArgs e, object userData)
        {
            var codes = userData as string[];

            //odstranime uz predane 
            List<string> sold = new List<string>();
            foreach (var code in codes)
            {
                var item = db.GetItem(code);
                if (item.Quantity == "0")
                    sold.Add(code);
            }
            codes = codes.Where(c => !sold.Contains(c)).ToArray();

            bw.ReportProgress(0, "Setting quantity to 0..");
            string command = string.Format("update {0} set quantity = 0 where code in ({1})", DBProvider.TableNames[0], string.Join(",", codes));
            db.ExecuteNonQuery(command);

            //foreach (var code in codes)
            for (int i = 0; i < codes.Length; i++ )
            {
                var code = codes[i];
                var item = db.GetItem(code);

                db.SoldItem(item.GlobalNumber, string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), item.VkNetto);

                var web = Properties.Settings.Default.WebServer;
                if (!web.EndsWith("/"))
                    web += "/";
                var param = "pages/stiahni-z-predaja.html?id=XXX";
                if (param.StartsWith("/"))
                    param = param.TrimStart('/');
                if (param.Contains("XXX"))
                    param = param.Replace("XXX", codes[0]);
                try
                {
                    Process.Start(web+param);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Exception occured: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (bw.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                bw.ReportProgress((int)(((double)(i + 1.0) / codes.Length) * 100.0));
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnToolNew.Enabled = false;
            btnToolEdit.Enabled = false;
            btnToolRemove.Enabled = false;
            btnToolExport.Enabled = false;
            bool refresh = false;

            switch (tabControl1.SelectedIndex)
            {
                case (int)TABS.MAIN: // main
                    btnToolNew.Enabled = true;
                    btnToolEdit.Enabled = true;
                    btnToolRemove.Enabled = true;
                    btnToolExport.Enabled = true;
                    refresh = true;
                    break;

                case (int)TABS.SOLD: // sold
                    btnToolEdit.Enabled = true;
                    btnToolRemove.Enabled = true;
                    btnToolExport.Enabled = true;
                    refresh = true;
                    break;

                case (int)TABS.FROM_SK: // fromSK
                    btnToolEdit.Enabled = true;
                    btnToolRemove.Enabled = true;
                    btnToolExport.Enabled = true;
                    refresh = true;
                    break;

                case (int)TABS.INVENTORY: // inventory
                    btnToolRemove.Enabled = true;
                    btnToolExport.Enabled = true;
                    refresh = true;
                    break;

                case (int)TABS.CUSTOMQUERY: // query
                    btnToolExport.Enabled = true;
                    break;

                default:
                    break;
            }

            if (refresh)
                RefreshItems();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (txtQuery.SelectedText.Trim().Length == 0)
            {
                txtQuery.AppendText(Environment.NewLine + "No text selected!" + Environment.NewLine);
                return;
            }

            try
            {
                var ds = db.ExecuteQuery(txtQuery.SelectedText);
                gridQueryRes.DataSource = ds.Tables[0];

                txtQuery.AppendText(string.Format("{0}Result: {1} items{2}", Environment.NewLine, ds.Tables[0].Rows.Count, Environment.NewLine));
                lblQueryResCount.Text = ds.Tables[0].Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                txtQuery.AppendText(System.Environment.NewLine + ex.ToString() + System.Environment.NewLine);
            }
        }

        private void btnExecNonQuery_Click(object sender, EventArgs e)
        {
            if (txtQuery.SelectedText.Trim().Length == 0)
            {
                txtQuery.AppendText(Environment.NewLine + "No text selected!" + Environment.NewLine);
                return;
            }

            try
            {
                db.ExecuteNonQuery(txtQuery.SelectedText);

                txtQuery.AppendText(System.Environment.NewLine + "Command executed successfully!" + System.Environment.NewLine);
            }
            catch (Exception ex)
            {
                txtQuery.AppendText(System.Environment.NewLine + ex.ToString() + System.Environment.NewLine);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Do you really want to clear query window?", "Clear query window", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                txtQuery.Clear();
        }

        int GetSelectedRowCount()
        {
            var sel = DataGrid.SelectedCells;
            List<int> ri = new List<int>();

            for (int i = 0; i < sel.Count; i++)
            {
                if (!ri.Contains(sel[i].RowIndex))
                    ri.Add(sel[i].RowIndex);
            }

            return ri.Count;
        }

        private void Grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            LabelSelected.Text = GetSelectedRowCount().ToString();
        }

        private void LoadQuery(object sender, EventArgs e)
        {
            Button button = sender as Button;
            var number = button.Name.ToCharArray().Where(c => "12345".Contains(c)).First();
            int i = int.Parse(new string(new char[] { number })) -1;
            if (i >= 0 && i < Queries.Length && Queries[i] != null)
            {
                var t = Environment.NewLine + Queries[i] + Environment.NewLine;
                txtQuery.AppendText(t);

                txtQuery.Focus();
                txtQuery.SelectionStart = txtQuery.Text.Length - t.Length;
                txtQuery.SelectionLength = t.Length;
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            var codes = GetSelectedCodes();
            if (codes == null || codes.Length == 0)
            {
                MessageBox.Show(this, "Select some item first!", "Item info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var web = Properties.Settings.Default.WebServer;
            if (!web.EndsWith("/"))
                web = web + "/";
            var param = Properties.Settings.Default.WebParam;
            if (param.StartsWith("/"))
                param = param.TrimStart('/');
            if (param.Contains("XXX"))
                param = param.Replace("XXX", codes[0]);
            
            Process.Start(web + param);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(this, "Do you really want to quit?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }

        private void btnToolSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.ShowDialog();
        }

        private void btnToolExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEditSold_Click(object sender, EventArgs e)
        {
            UpdateItem();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case (int)TABS.MAIN:
                    Common.ExportDataGrid(DataGrid.DataSource, string.Format("global_main{0}", DateTime.Now.ToString("yyyyMMdd")));
                    break;

                case (int)TABS.SOLD:
                    Common.ExportDataGrid(DataGrid.DataSource, string.Format("global_sold{0}", DateTime.Now.ToString("yyyyMMdd")));
                    break;

                case (int)TABS.FROM_SK:
                    Common.ExportDataGrid(DataGrid.DataSource, string.Format("global_fromSK{0}", DateTime.Now.ToString("yyyyMMdd")));
                    break;

                case (int)TABS.INVENTORY:
                    Common.ExportDataGrid(DataGrid.DataSource, string.Format("global_inventory{0}", DateTime.Now.ToString("yyyyMMdd")));
                    break;

                case (int)TABS.CUSTOMQUERY:
                    Common.ExportDataGrid(DataGrid.DataSource, string.Format("global_query{0}", DateTime.Now.ToString("yyyyMMdd")));
                    break;

                default:
                    break;
            }
        }

        private void btnInvEdit_Click(object sender, EventArgs e)
        {
            UpdateItem();
        }

        private void btnAddInventory_Click(object sender, EventArgs e)
        {
            var codes = GetSelectedCodes();

            if (MessageBox.Show(this, string.Format("Do you really want to add to inventory {0} selected item(s)?", codes.Length), "Add to inventory", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                Progress p = new Progress(0, 100, "Adding items to inventory..", "checking..", InventoryItems, RefreshItems, codes, true, true);
                p.StartWorker();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error while updating!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InventoryItems(BackgroundWorker bw, DoWorkEventArgs e, object userData)
        {
            var codes = userData as string[];

            //foreach (var code in codes)
            for (int i = 0; i < codes.Length; i++)
            {
                if (bw.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                bw.ReportProgress((int)(((double)(i + 1.0) / codes.Length) * 100.0));

                var code = codes[i];
                if (db.ExistsItem(code, DBProvider.TableNames[(int)TABS.INVENTORY]))    // kontrola na existujuci kod
                    continue;

                var command = string.Format("insert into {0} (code) values (\"{1}\")", DBProvider.TableNames[(int)TABS.INVENTORY], code);
                db.ExecuteNonQuery(command);
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == (int)TABS.MAIN)
            {
                txtFilCode.Text = string.Empty;
                txtFilName.Text = string.Empty;
                txtFilCountry.Text = string.Empty;
                txtFilSupplier.Text = string.Empty;
                dtDateFrom.Checked = false;
                dtDateTo.Checked = false;
                txtFilMvDate.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtFilInv.Text = string.Empty;
                txtFilMat.Text = string.Empty;
            }
            else
            if (tabControl1.SelectedIndex == (int)TABS.SOLD)
            {
                textFilCodeSold.Text = string.Empty;
                textFilNameSold.Text = string.Empty;
                textFilCountrySold.Text = string.Empty;
                textFilSupSold.Text = string.Empty;
                textFilPriceSold.Text = string.Empty;
                dtSellDateFrom.Checked = false;
                dtSellDateTo.Checked = false;
                txtFilSellPrice.Text = string.Empty;
            }
            else 
            if (tabControl1.SelectedIndex == (int)TABS.FROM_SK)
            {
                txtFilSKCode.Text = string.Empty;
                txtFilSKCountry.Text = string.Empty;
                txtFilSKLength.Text = string.Empty;
                //txtFilSKSellPrice.Text = string.Empty;
                txtFilSKSupplier.Text = string.Empty;
                txtFilSKSupplierNr.Text = string.Empty;
                txtFilSKTitle.Text = string.Empty;
                txtFilSKVK.Text = string.Empty;
                txtFilSKWidth.Text = string.Empty;
                //dtpFilSKSellFrom.Checked = false;
                //dtpFilSKSellTo.Checked = false;
            }
            else
            if (tabControl1.SelectedIndex == (int)TABS.INVENTORY)
            {
                txtFilInvCode.Text = string.Empty;
                txtFilInvName.Text = string.Empty;
                txtFilInvCountry.Text = string.Empty;
                txtFilInvSup.Text = string.Empty;
                dtInvDateFrom.Checked = false;
                dtInvDateTo.Checked = false;
                txtFilInvmvDate.Text = string.Empty;
                txtFilInvQuantity.Text = string.Empty;
                txtFilInvInvoice.Text = string.Empty;
                txtFilInvMaterial.Text = string.Empty;
            }

            RefreshItems();
        }

        List<string> MissingInMain = new List<string>();
        List<string> MissingInInventory = new List<string>();
        private void btnStartInventory_Click(object sender, EventArgs e)
        {
            Progress p = new Progress(0, 100, "Inventory checking", "Selecting non sold items", DoInventory, InventoryResult, null, true, false);
            p.StartWorker();
        }

        public void InventoryResult()
        {
            InventoryResult frmRes = new InventoryResult(db, MissingInMain, MissingInInventory);
            frmRes.ShowDialog(this);
        }

        public void DoInventory(BackgroundWorker bw, DoWorkEventArgs e, object userData)
        {
            var CODE = "CODE";
            var ds = db.ExecuteQuery(string.Format("select * from arena where quantity = 1 and valid = 1"));
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0)
            {
                MessageBox.Show(this, "All items are sold! No items found in main table.", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var mainItemsTable = ds.Tables[0];
            var invItemsTable = gridInventory.DataSource as DataTable;
            var mainQueryable = mainItemsTable.AsEnumerable();
            var invQueryable = invItemsTable.AsEnumerable();

            bw.ReportProgress(0, "Searching missing items in inventory table..");
            MissingInInventory.Clear();
            for (int i = 0; i < mainItemsTable.Rows.Count; i++)
            {
                // najdeme kody, ktore su v hlavnej, ale chybaju v inventari - malo by byt empty
                if (invQueryable.Where(r => r.Field<string>(CODE).Contains(mainItemsTable.Rows[i][CODE].ToString())).Count() == 0)
                    MissingInInventory.Add(mainItemsTable.Rows[i][CODE].ToString());

                if (bw.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                bw.ReportProgress((int)(((double)(i + 1.0) / mainItemsTable.Rows.Count) * 100.0));
            }

            bw.ReportProgress(0, "Searching missing items in main table..");
            MissingInMain.Clear();
            for (int i = 0; i < invItemsTable.Rows.Count; i++)
            {
                // najdeme kody, ktore su v invetari ale chybaju v hlavnej tabulke - hlavny problem?
                if (mainQueryable.Where(r => r.Field<string>(CODE).Contains(invItemsTable.Rows[i][CODE].ToString())).Count() == 0)
                    MissingInMain.Add(invItemsTable.Rows[i][CODE].ToString());

                if (bw.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                bw.ReportProgress((int)(((double)(i + 1.0) / invItemsTable.Rows.Count) * 100.0));
            }
        }

        private void btnToolScanner_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(Properties.Settings.Default.PtcommDir))
                    Directory.CreateDirectory(Properties.Settings.Default.PtcommDir);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Ptcomm destination directory is not correct! Change it in Settings dialog.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Process proc = new Process();
            proc.StartInfo.FileName = Properties.Settings.Default.Ptcomm;
            var args = Properties.Settings.Default.PtcommCommand;
            if (args.Contains("XXX"))
                args = Properties.Settings.Default.PtcommCommand.Replace("XXX", Properties.Settings.Default.PtcommDir);
            proc.StartInfo.Arguments = args;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            try
            {
                proc.Start();

                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Exception occured: "+ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if (MessageBox.Show(this, "Do you want to import SOLD and INVENTORY data?", "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //ImportScannerData();

            RefreshItems();
        }

        private OperationType CurrOpType = OperationType.Sold;
        private void ImportScannerData()
        {
            if (InvokeRequired)
                Invoke(new Action(ImportScannerData));

            //if (MessageBox.Show(this, "Do you want to import " + CurrOpType.ToString() + " data?", "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                WaitForm wf = new WaitForm(db, CurrOpType);
                wf.ShowDialog(this);
            }

            RefreshItems();
            this.Focus();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripItem))
            {
                MessageBox.Show(this, "Upload must be started from toolbar by selecting subitem of 'Upload' button!", "Upload files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            
            try
            {
                Progress p = new Progress(0, 100, "Upload scanner files..", "Connecting..", doUpload, RefreshItems, (sender as ToolStripItem).Tag, false, true);
                p.StartWorker();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Upload failed: " + ex.ToString(), "Upload files", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripItem))
            {
                MessageBox.Show(this, "Download must be started from toolbar by selecting subitem of 'Download' button!", "Download files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Progress p = new Progress(0, 100, "Downloading scanner files..", "Connecting..", doDownload, null, (sender as ToolStripItem).Tag, false, true);
                p.StartWorker();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Download failed: " + ex.ToString(), "Download files", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportSold_Click(object sender, EventArgs e)
        {
            try
            {
                CurrOpType = OperationType.Sold;
                ImportScannerData();

                MessageBox.Show(this, "Import successfull!", "Import file(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Import failed: " + ex.ToString(), "Import file(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportInventory_Click(object sender, EventArgs e)
        {
            try
            {
                CurrOpType = OperationType.Inventory;
                ImportScannerData();

                MessageBox.Show(this, "Import successfull!", "Import file(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Import failed: " + ex.ToString(), "Import file(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportFromSK_Click(object sender, EventArgs e)
        {
            try
            {
                CurrOpType = OperationType.fromSK;
                ImportScannerData();

                MessageBox.Show(this, "Import successfull!", "Import file(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Import failed: " + ex.ToString(), "Import file(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    class CustomDataGridView : DataGridView
    {
        public CustomDataGridView()
        {
            DoubleBuffered = true;
        }
    }
}
