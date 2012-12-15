using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Koberce
{
    public partial class WaitForm : Form
    {
        OperationType OpType = OperationType.Sold;
        DBProvider db;

        public WaitForm(DBProvider db, OperationType opType)
        {
            this.OpType = opType;
            this.db = db;

            InitializeComponent();

            this.Text += " "+opType.ToString();
        }

        void ImportSold()
        {
            var lines = File.ReadAllLines(Properties.Settings.Default.PtcommDir + @"\SOLD.TXT");
            if (lines != null && (lines.Length % 3) == 0)
            {
                for (int i = 0; i < lines.Length; i += 3)
                {
                    var code = lines[i + 0];
                    var sellDate = lines[i + 1];
                    var sellPrice = lines[i + 2];

                    var tmp = sellDate.Split('/');
                    if (tmp.Length == 3)
                        sellDate = string.Format("{0}-{1}-{2}", tmp[2], tmp[1], tmp[0]);

                    db.SoldItem(code, sellDate, sellPrice);
                }
            }
        }

        void ImportInventory()
        {
            var linesInv = File.ReadAllLines(Properties.Settings.Default.PtcommDir + @"\INVENTOR.TXT");
            if (linesInv != null)
            {
                for (int i = 0; i < linesInv.Length; i++)
                {
                    db.InventoryItem(linesInv[i]);
                }
            }
        }

        private void WaitForm_Shown(object sender, EventArgs e)
        {
            Update();

            switch (OpType)
            {
                case OperationType.Sold:
                    ImportSold();
                    break;

                case OperationType.Inventory:
                    ImportInventory();
                    break;

                case OperationType.fromSK:
                    ImportFromSK();
                    break;

                default:
                    break;
            }

            Close();
        }

        private void ImportFromSK()
        {
            var linesInv = File.ReadAllLines(Properties.Settings.Default.PtcommDir + @"\SK.TXT");
            
            if (linesInv != null)
            {
                for (int i = 0; i < linesInv.Length; i++)
                {
                    db.FromSKItem(linesInv[i]);
                }
            }
        }
    }

    public enum OperationType
    {
        Sold,
        Inventory,
        fromSK
    }
}
