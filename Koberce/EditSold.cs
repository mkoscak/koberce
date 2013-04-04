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
        private bool Exhibition { get; set; }

        public EditSold(DBProvider db, string tableName, string code, bool exh)
        {
            InitializeComponent();
            
            this.db = db;
            this.Exhibition = exh;

            if (!Exhibition)
                txtExhibition.Enabled = false;
            else
            {
                txtSellDate.Enabled = false;
                txtSellPrice.Enabled = false;
            }

            if (code == null)
            {
                MessageBox.Show(this, "Code is missing!", "Edit sold", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sExh = string.Empty;
                if (Exhibition)
                    sExh = ", EXHIBITIONNAME";
                else
                    sExh = ", A.SELLDATE, A.SELLPRICE";

                var ds = db.ExecuteQuery(string.Format("select B.CODE, B.ITEMTITLE, B.COUNTRY, B.SUPPLIER, B.LENGTH, B.WIDTH, B.VK_NETTO, B.DATE {3} from {0} A join {1} B on A.CODE = B.CODE where A.code = {2}", tableName, DBProvider.TableNames[(int)TABS.MAIN], code, sExh));
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
                    if (Exhibition)
                        txtExhibition.Text = vals[8].ToString();
                    else
                    {
                        txtSellDate.Text = vals[8].ToString();
                        txtSellPrice.Text = vals[9].ToString();
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Exhibition)
                {
                    db.UpdateSold(txtCode.Text,
                            txtSellDate.Text,
                            txtSellPrice.Text);
                }
                else
                {
                    db.UpdateExh(txtCode.Text,
                            txtExhibition.Text);
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

        private void EditSold_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                DialogResult = DialogResult.Cancel;
        }
    }
}
