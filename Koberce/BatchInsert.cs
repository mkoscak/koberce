using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

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

            for (int i = 1; i < toDecode.Length; i++)
            {
                if (bw.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                bw.ReportProgress((int)(((double)(i + 1) / toDecode.Length) * 100.0));

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
                newItem.GlobalNumber = line[8];
                newItem.SupplierNr = line[9];
                newItem.RgNr = line[10];
                newItem.QmPrice = line[11];
                newItem.EuroStuck = line[12];

                data.Add(newItem);
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
                string sprice = item.QmPrice.Length == 0 ? item.EuroStuck : item.QmPrice;
                if (sprice.Trim().Length == 0)
                    sprice = "1";

                string qm = new string(sprice.ToCharArray().Where(c => "1234567890.,".Contains(c)).ToArray());
                qm = qm.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                qm = qm.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                double price = double.Parse(qm);

                double ek = price;
                double vk = Math.Round(w * l / 10000.0 * price * Properties.Settings.Default.PriceCoef);
                if (item.QmPrice.Length == 0)
                    vk = ek;

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
                        "",
                        item.Color,
                        item.Material,
                        item.Comment,
                        item.RgNr);

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
