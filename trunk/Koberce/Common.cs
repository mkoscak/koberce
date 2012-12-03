using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Data;
using OfficeOpenXml;
using System.IO;

namespace Koberce
{
    class Common
    {
        static string[] SoldCols = new string[] { "selldate", "supplier_nr", "itemtitle", "supplier", "length", "width", "ek_netto", "area", "vk_netto" };
        static string ExportFileName;
        static string ExportTitle;
        static bool IsSold;
        static int SoldOffset;
        public static void ExportDataGrid(object ds, string fileName)
        {
            if (ds == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = fileName;
            sfd.Filter = "Excel 2007 files|*.xlsx";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            ExportFileName = sfd.FileName;
            ExportTitle = fileName;
            if (ExportFileName.EndsWith(".xlsx") == false)
                ExportFileName = ExportFileName + ".xlsx";

            IsSold = false;
            SoldOffset = 0;
            if (fileName.ToLower().Contains("sold"))
            {
                IsSold = true;
                SoldOffset = 1;
            }

            Progress p = new Progress(0, 100, "Export", "Preparing..", DoExport, null, ds, true, true);
            p.StartWorker();
        }

        public static void DoExport(BackgroundWorker bw, DoWorkEventArgs e, object userData)
        {
            if (!(userData is DataTable))
                return;

            var dt = userData as DataTable;
            List<int> indices = new List<int>();

            for (int i = 0; i < dt.Columns.Count; i++)
                if ((IsSold && SoldCols.Contains(dt.Columns[i].ColumnName.ToLower())) || (!IsSold))
                    indices.Add(i);

            ExcelPackage ep = new ExcelPackage();
            var ws = ep.Workbook.Worksheets.Add(ExportTitle);
            ws.DefaultColWidth = 11.5;
            ws.Cells[1, 1].Value = ExportTitle;
            ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
            ws.Cells[1, 1, 1, dt.Columns.Count].Merge = true;

            for (int i = 0; i < indices.Count; i++)
            {
                // poradove cislo
                if (IsSold && i == 0)
                {
                    ws.Cells[2, 1].Value = "Nr.";
                    ws.Cells[2, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[2, 1].Style.Fill.BackgroundColor.SetColor(Color.Green);
                    ws.Cells[2, 1].Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells[2, i + 1 + SoldOffset].Value = dt.Columns[indices[i]].ColumnName;
                ws.Cells[2, i + 1 + SoldOffset].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, i + 1 + SoldOffset].Style.Fill.BackgroundColor.SetColor(Color.Green);
                ws.Cells[2, i + 1 + SoldOffset].Style.Font.Color.SetColor(Color.White);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // poradove cislo
                if (IsSold)
                    ws.Cells[3 + i, 1].Value = i + 1;

                for (int j = 0; j < indices.Count; j++)
                {
                    ws.Cells[3 + i, j + 1 + SoldOffset].Value = dt.Rows[i].ItemArray[indices[j]];
                }

                if (bw.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                bw.ReportProgress((int)(((double)(i + 1.0) / dt.Rows.Count) * 100.0));
            }

            ep.SaveAs(new FileInfo(ExportFileName));
        }
    }
}
