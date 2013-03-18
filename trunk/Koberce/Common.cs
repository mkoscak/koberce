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
using System.Globalization;

namespace Koberce
{
    class Common
    {
        static string[] SoldCols = new string[] { "selldate", "supplier_nr", "itemtitle", "supplier", "length", "width", "ek_netto", "area", "vk_netto" };
        static string ExportFileName;
        static string ExportTitle;
        static bool IsSold;
        static int SoldOffset;
        public static void ExportDataGrid(object ds, string fileName, bool sold)
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
            if (sold)
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
                    try
                    {
                        if (IsSold && dt.Columns[indices[j]].ColumnName.ToLower() == "vk_netto")
                        {
                            ws.Cells[3 + i, j + 1 + SoldOffset].Formula= string.Format("F{0}*G{0}/10000*H{0}", i+3);
                            continue;
                        }

                        double val = GetPrice(dt.Rows[i].ItemArray[indices[j]].ToString());
                        if (double.IsNaN(val))
                            ws.Cells[3 + i, j + 1 + SoldOffset].Value = dt.Rows[i].ItemArray[indices[j]];
                        else
                            ws.Cells[3 + i, j + 1 + SoldOffset].Value = val;
                    }
                    catch (Exception)
                    {
                        ws.Cells[3 + i, j + 1 + SoldOffset].Value = dt.Rows[i].ItemArray[indices[j]];
                    }
                }

                if (bw.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                bw.ReportProgress((int)(((double)(i + 1.0) / dt.Rows.Count) * 100.0));
            }

            if (IsSold)
            {
                int row = 3 + dt.Rows.Count;
                ws.Cells[row, 8].Value = "Sum:";
                ws.Cells[row, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[row, 8].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                ws.Cells[row, 8].Style.Font.Color.SetColor(Color.DarkBlue);

                ws.Cells[row, 9].Formula = "SUM(I3:I"+(row-1)+")";
                ws.Cells[row, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[row, 9].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                ws.Cells[row, 9].Style.Font.Color.SetColor(Color.DarkBlue);

                ws.Cells[row, 10].Formula = "SUM(J3:J" + (row - 1) + ")";
                ws.Cells[row, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[row, 10].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                ws.Cells[row, 10].Style.Font.Color.SetColor(Color.DarkBlue);
            }

            ep.SaveAs(new FileInfo(ExportFileName));
        }

        public static double GetPrice(string strPrice)
        {
            if (strPrice == null)
                return double.NaN;

            var tmp = CleanPrice(strPrice);

            double ret = double.NaN;
            try
            {
                ret = double.Parse(tmp);
            }
            catch (Exception)
            {
            }

            return ret;
        }

        public static string CleanPrice(string strPrice)
        {
            // cena obsahuje aj bodku aj ciarku, napr 1,000.25.. prvy znak vyhodime
            if (strPrice.Contains(',') && strPrice.Contains('.'))
            {
                int pointIndex = strPrice.LastIndexOf('.');
                int commaIndex = strPrice.LastIndexOf(',');

                if (pointIndex > commaIndex)
                    strPrice = strPrice.Replace(",", "");   // odstranime vsetky ciarky
                else
                    strPrice = strPrice.Replace(".", "");   // odstranime vsetky bodky
            }

            strPrice = new string(strPrice.ToCharArray().Where(c => Char.IsDigit(c) || c == ',' || c == '.').ToArray()).Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            return strPrice;
        }
    }
}
