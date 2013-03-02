using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;

namespace Koberce
{
    class PrintDoc : PrintDocument
    {
        DataItem Item { get; set; }
        static Font Times14Bold { get; set; }
        static Font Arial11Bold { get; set; }
        static Font Arial14Bold { get; set; }
        static Font Times14 { get; set; }
        static Font Times18Bold { get; set; }
        static Font Code128 { get; set; }

        static PrintDoc()
        {
            Times14Bold = new Font("Times New Roman", 14, FontStyle.Bold);
            Arial11Bold = new Font("Arial", 11, FontStyle.Bold);
            Arial14Bold = new Font("Arial", 14, FontStyle.Bold);
            Times14 = new Font("Times New Roman", 14, FontStyle.Regular);
            Times18Bold = new Font("Times New Roman", 16, FontStyle.Bold);
            Code128 = new Font("Code 128", 55);
        }

        public PrintDoc(DataItem item)
        {
            this.Item = item;
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            // 9x13cm v palcoch = 3.54x5.12
            DefaultPageSettings.PaperSize = new PaperSize("psize", 354, 512);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            e.Graphics.TranslateTransform(1, 1);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            // zobrazenie bitmapy
            var i = new Bitmap("global-label.png");
            Rectangle r = e.PageBounds;
            r.Inflate(2, 2);
            e.Graphics.DrawImage(i, r);

            var format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            e.Graphics.DrawString(Item.Name, Times14Bold, Brushes.Black, new RectangleF(10, 260, r.Width - 20, 50), format);
            e.Graphics.DrawString(Item.Country, Times14, Brushes.Black, new RectangleF(10, 280, r.Width - 20, 50), format);
            e.Graphics.DrawString(string.Format("{0} x {1}", Item.Length, Item.Width), Arial14Bold, Brushes.Black, new RectangleF(10, 300, r.Width - 20, 50), format);
            e.Graphics.DrawString(string.Format("{0},- {1}", Item.VkNetto, (char)0x20AC), Arial14Bold, Brushes.Black, new RectangleF(10, 320, r.Width - 20, 50), format);
            // ciarovy kod
            e.Graphics.DrawString(Item.GlobalNumber, Code128, Brushes.Black, new RectangleF(37, 377, r.Width - 20, 150));
            e.Graphics.DrawString(string.Format("NR. {0}", Item.GlobalNumber), Times18Bold, Brushes.Black, new RectangleF(53, 448, r.Width - 20, 150));
            e.Graphics.DrawString(string.Format("{2}, {0} x {1}", Item.Length, Item.Width, Item.SupplierNr), Arial11Bold, Brushes.Black, new RectangleF(47, 470, r.Width - 20, 50));
        }
    }
}
