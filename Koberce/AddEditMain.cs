using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Koberce
{
    public enum EditMode
    {
        Add,
        Update
    }

    public partial class AddEditMain : Form
    {
        private DBProvider db;
        private EditMode mode;

        public AddEditMain(DBProvider db, string tableName, string code, EditMode mode)
        {
            InitializeComponent();
            
            this.mode = mode;
            this.db = db;
            
            if (code == null)
            {
                txtCode.Text = (db.LoadMaxCode(false) + 1).ToString();
            }
            else
            {
                var ds = db.ExecuteQuery(tableName, " where code = " + code, "");
                if (ds != null && ds.Tables.Count > 0)
                {
                    var vals = ds.Tables[0].Rows[0].ItemArray;
                    txtSupplierNr.Text = vals[0].ToString();
                    txtCode.Text = vals[1].ToString();
                    txtName.Text = vals[2].ToString();
                    txtCountry.Text = vals[3].ToString();
                    txtSupplier.Text = vals[4].ToString();
                    txtLegnth.Text = vals[5].ToString();
                    txtWidth.Text = vals[6].ToString();
                    txtEKNetto.Text = vals[7].ToString();
                    txtQuantity.Text = vals[8].ToString();
                    txtVKNetto.Text = vals[9].ToString();
                    txtDate.Text = vals[10].ToString();
                    txtPaid.Text = vals[11].ToString();
                    txtMVDate.Text = vals[12].ToString();
                    txtInvoice.Text = vals[13].ToString();
                    txtColor.Text = vals[14].ToString();
                    txtMaterial.Text = vals[15].ToString();
                    txtComment.Text = vals[16].ToString();
                    txtRgNr.Text = vals[18].ToString();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (mode == EditMode.Add)
                {
                    db.Add(true, txtSupplierNr.Text,
                            txtCode.Text,
                            txtName.Text,
                            txtCountry.Text,
                            txtSupplier.Text,
                            int.Parse(txtLegnth.Text),
                            int.Parse(txtWidth.Text),
                            txtEKNetto.Text,
                            txtQuantity.Text,
                            txtVKNetto.Text,
                            txtDate.Text,
                            txtPaid.Text,
                            txtMVDate.Text,
                            txtInvoice.Text,
                            txtColor.Text,
                            txtMaterial.Text,
                            txtComment.Text,
                            txtRgNr.Text,
                            txtEuroStuck.Text);
                }
                else
                {
                    db.Update(txtSupplierNr.Text,
                            txtCode.Text,
                            txtName.Text,
                            txtCountry.Text,
                            txtSupplier.Text,
                            int.Parse(txtLegnth.Text),
                            int.Parse(txtWidth.Text),
                            txtEKNetto.Text,
                            txtQuantity.Text,
                            txtVKNetto.Text,
                            txtDate.Text,
                            txtPaid.Text,
                            txtMVDate.Text,
                            txtInvoice.Text,
                            txtColor.Text,
                            txtMaterial.Text,
                            txtComment.Text,
                            txtRgNr.Text,
                            txtEuroStuck.Text);
                }

                MessageBox.Show(this, "Data written succesfully!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void RecalcPrice()
        {
            if (txtWidth.Text.Length == 0 || txtLegnth.Text.Length == 0 || txtQMPrice.Text.Length == 0)
                return;
            int w = int.Parse(txtWidth.Text);
            int l = int.Parse(txtLegnth.Text);
            double price = double.Parse(Common.CleanPrice(txtQMPrice.Text));

            txtEKNetto.Text = txtQMPrice.Text;
            txtVKNetto.Text = Common.CalcPrice(w, l, price).ToString();
        }

        private void PriceChanged(object sender, EventArgs e)
        {
            RecalcPrice();
        }

        private void LengthChanged(object sender, EventArgs e)
        {
            RecalcPrice();
        }

        private void WidthChanged(object sender, EventArgs e)
        {
            RecalcPrice();
        }

        private void AddEditMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                DialogResult = DialogResult.Cancel;
        }

        private void AddEditMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
