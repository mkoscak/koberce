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
    public partial class InventoryResult : Form
    {
        public InventoryResult(DBProvider db, List<string> missingMain, List<string> missingInv)
        {
            InitializeComponent();

            var table = new DataTable("inventory");
            table.Columns.Add("Code");
            missingMain.ForEach(s => table.Rows.Add(s));
            gridMissingMain.DataSource = table;

            gridMissingInv.DataSource = db.ExecuteQuery(DBProvider.TableNames[0], string.Format(" where code in ({0})", string.Join(",", missingInv.ToArray())), " order by code desc ").Tables[0];
        }

        private void btnExportMain_Click(object sender, EventArgs e)
        {
            Common.ExportDataGrid(gridMissingMain.DataSource, string.Format("inventory_main_missing_{0}", DateTime.Now.ToString("yyyyMMdd")), false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.ExportDataGrid(gridMissingInv.DataSource, string.Format("inventory_inv_missing_{0}", DateTime.Now.ToString("yyyyMMdd")), false);
        }
    }
}
