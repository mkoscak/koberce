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
    public partial class FrmVKRecalculation : Form
    {
        string Condition;
        string CodesSelector;
        DBProvider db;

        public FrmVKRecalculation(string condition, DBProvider db)
        {
            InitializeComponent();

            this.txtCoef.Text = Properties.Settings.Default.PriceCoef.ToString();

            this.Condition = condition;
            this.CodesSelector = string.Format("select CODE from {0} where {1}", DBProvider.TableNames[0], Condition.Replace("A.", string.Empty));
            this.db = db;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DoRecalculation();
                Cursor.Current = Cursors.Default;

                MessageBox.Show(this, "Update successful!", "Price recalculation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(this, "Exception during update : " + ex, "Price recalculation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoRecalculation()
        {
            var eq = txtFormula.Text
                .Replace("Wx", "width")
                .Replace("Lx", "length")
                .Replace("Cx", Common.CleanPrice(txtCoef.Text).Replace(',', '.'))
                .Replace("EK", "CAST(ek_netto as INTEGER)")
                ;
            string update = string.Format("update {0} set vk_netto = ({1}) where code in ( {2} )", DBProvider.TableNames[0], eq, CodesSelector);

            db.ExecuteNonQuery(update);
        }
    }
}
