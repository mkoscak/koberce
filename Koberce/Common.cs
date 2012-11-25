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
        static string ExportFileName;
        static string ExportTitle;
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

            Progress p = new Progress(0, 100, "Export", "Preparing..", DoExport, null, ds, true, true);
            p.StartWorker();
        }

        public static void DoExport(BackgroundWorker bw, DoWorkEventArgs e, object userData)
        {
            var dt = userData as DataTable;

            ExcelPackage ep = new ExcelPackage();
            var ws = ep.Workbook.Worksheets.Add(ExportTitle);
            ws.DefaultColWidth = 11.5;
            ws.Cells[1, 1].Value = ExportTitle;
            ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
            ws.Cells[1, 1, 1, dt.Columns.Count].Merge = true;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.Cells[2, i + 1].Value = dt.Columns[i].ColumnName;
                ws.Cells[2, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, i + 1].Style.Fill.BackgroundColor.SetColor(Color.Green);
                ws.Cells[2, i + 1].Style.Font.Color.SetColor(Color.White);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ws.Cells[3 + i, j + 1].Value = dt.Rows[i].ItemArray[j];
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
