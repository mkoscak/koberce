using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Koberce
{
    static class BatchInsert
    {
        private static string[] toDecode;
        private static BindingList<DataItem> decodedDS;
        public static BindingList<DataItem> Decode(string[] toDecode)
        {
            BatchInsert.toDecode = toDecode;

            Progress p = new Progress(0, 100, "Decoding..", "starting..", Decoding, null, null, true, true);
            p.StartWorker();

            return decodedDS;
        }

        public static void Decoding(BackgroundWorker bw, DoWorkEventArgs e, object userData)
        {
            var data = new List<DataItem>();

            // zaciname od riadku 1 kvoli nadpisom
            for (int i = 1; i < toDecode.Length; i++)
            {
                if (bw.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                bw.ReportProgress((int)(((double)(i + 1) / toDecode.Length) * 100.0));

                try
                {
                    if (toDecode[i].Trim().Length == 0)
                        continue;

                    var l = toDecode[i];
                    if (!l.EndsWith("\t"))
                        l += "\t";
                    var line = l.Split('\t');

                    DataItem newItem = new DataItem();
                    newItem.Country = line[0];
                    newItem.Name = line[1];
                    newItem.Length = line[2];
                    newItem.Width = line[3];
                    newItem.Color = line[4];
                    newItem.Material = line[5];
                    newItem.Supplier = line[6];
                    newItem.Comment = line[7];
                    newItem.QmPrice = line[8];
                    newItem.SupplierNr = line[9];
                    newItem.Invoice = line[10];
                    newItem.EkNetto = line[11];
                    newItem.EuroStuck = line[12];
                    if (line.Length > 13)
                        newItem.Info = line[13];

                    data.Add(newItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(null, "Error while decoding in line " + i + Environment.NewLine + ex, "Error while decoding!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    decodedDS = new BindingList<DataItem>();
                    return;
                }
            }

            decodedDS = new BindingList<DataItem>(data);
        }

        private static string DateToString(DateTime datetime)
        {
            string day = datetime.Day.ToString();
            string month = datetime.Month.ToString();

            if (day.Length == 1)
                day = "0" + day;
            if (month.Length == 1)
                month = "0" + month;

            return datetime.Year.ToString() + "-" + month + "-" + day;
        }

        static BindingList<DataItem> DataSource;
        static DBProvider dbProvider;
        public static void InsertMultiple(BindingList<DataItem> itemsDs, DBProvider db, bool _5series)
        {
            if (itemsDs == null)
                return;

            DataSource = itemsDs;
            dbProvider = db;

            Progress p = new Progress(0, 100, "Batch inserting..", "Loading max code", Inserting, null, _5series, true, true);
            p.StartWorker();
        }

        public static void Inserting(BackgroundWorker bw, DoWorkEventArgs e, object userData)
        {
            bool _5series = (bool)userData;
            int maxCode = dbProvider.LoadMaxCode(_5series);

            //foreach (var item in DataSource)
            for (int i = 0; i < DataSource.Count; i++)
            {
                var item = DataSource[i];

                if (item.Process == false)
                    continue;

                int w = int.Parse(item.Width);
                int l = int.Parse(item.Length);
                var ekPrice = Common.GetPrice(item.EkNetto);

                double ek = ekPrice;
                double vk = Common.CalcPrice(w, l, ekPrice, Properties.Settings.Default.PriceCoef);

                maxCode++;

                dbProvider.Add(false, item.SupplierNr,
                        maxCode.ToString(),
                        item.Name,
                        item.Country,
                        item.Supplier,
                        int.Parse(item.Length),
                        int.Parse(item.Width),
                        ek.ToString(),
                        "1",
                        vk.ToString(),
                        DateToString(DateTime.Now),
                        "No",
                        DateToString(DateTime.Now),
                        item.Invoice,
                        item.Color,
                        item.Material,
                        item.Comment,
                        item.Info,
                        item.EuroStuck,
                        item.QmPrice);

                if (bw.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                bw.ReportProgress( (int)(((double)(i+1.0) / DataSource.Count) * 100.0));
            }

            if (!_5series)
                dbProvider.ExecuteNonQuery("update last set lastCode = " + maxCode.ToString());
        }
    }
}
