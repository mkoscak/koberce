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
    public partial class EditSold : Form
    {
        private DBProvider db;

        public EditSold(DBProvider db, string tableName, string code)
        {
            InitializeComponent();
            
            this.db = db;
            
            if (code == null)
            {
                MessageBox.Show(this, "Code is missing!", "Edit sold", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var ds = db.ExecuteQuery(string.Format("select B.CODE, B.ITEMTITLE, B.COUNTRY, B.SUPPLIER, B.LENGTH, B.WIDTH, B.VK_NETTO, B.DATE, A.SELLDATE, A.SELLPRICE from {0} A join {1} B on A.CODE = B.CODE where A.code = {2}", tableName, DBProvider.TableNames[(int)TABS.MAIN], code));
                if (ds != null && ds.Tables.Count > 0)
                {
                    var vals = ds.Tables[0].Rows[0].ItemArray;
                    txtCode.Text = vals[0].ToString();
                    txtName.Text = vals[1].ToString();
                    txtCountry.Text = vals[2].ToString();
                    txtSupplier.Text = vals[3].ToString();
                    txtLegnth.Text = vals[4].ToString();
                    txtWidth.Text = vals[5].ToString();
                    txtItemPrice.Text = vals[6].ToString();
                    txtDate.Text = vals[7].ToString();
                    txtSellDate.Text = vals[8].ToString();
                    txtSellPrice.Text = vals[9].ToString();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                db.UpdateSold(txtCode.Text,
                        txtSellDate.Text,
                        txtSellPrice.Text);

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

        private void EditSold_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                DialogResult = DialogResult.Cancel;
        }
    }
}
