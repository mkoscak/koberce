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

                    txtCode.Text = vals[0].ToString();
                    txtName.Text = vals[1].ToString();
                    txtCountry.Text = vals[2].ToString();
                    txtSupplier.Text = vals[3].ToString();
                    txtSupplierNr.Text = vals[4].ToString();
                    txtLength.Text = vals[5].ToString();
                    txtWidth.Text = vals[6].ToString();
                    txtEKNetto.Text = vals[7].ToString();
                    txtVKNetto.Text = vals[8].ToString();
                    txtQuantity.Text = vals[9].ToString();
                    txtQMPrice.Text = vals[10].ToString();
                    txtDate.Text = vals[11].ToString();
                    txtMVDate.Text = vals[12].ToString();
                    txtInvoice.Text = vals[13].ToString();
                    txtColor.Text = vals[14].ToString();
                    txtMaterial.Text = vals[15].ToString();
                    txtComment.Text = vals[16].ToString();
                    txtInfo.Text = vals[17].ToString();
                    txtEuroStuck.Text = vals[18].ToString();
                    txtPaid.Text = vals[19].ToString();
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
                            int.Parse(txtLength.Text),
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
                            txtInfo.Text,
                            txtEuroStuck.Text,
                            txtQMPrice.Text);
                }
                else
                {
                    db.Update(txtSupplierNr.Text,
                            txtCode.Text,
                            txtName.Text,
                            txtCountry.Text,
                            txtSupplier.Text,
                            int.Parse(txtLength.Text),
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
                            txtInfo.Text,
                            txtEuroStuck.Text,
                            txtQMPrice.Text);
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
