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
        Font Times14Bold { get; set; }
        Font Times14 { get; set; }
        Font Arial { get; set; }
        Font Code128 { get; set; }

        public PrintDoc(DataItem item)
        {
            this.Item = item;

            Times14Bold = new Font("Times New Roman", 14, FontStyle.Bold);
            Times14 = new Font("Times New Roman", 14, FontStyle.Regular);
            Arial = new Font("Arial", 16);
            Code128 = new Font("Code 128", 16);
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

            e.Graphics.DrawString(Item.Name, Times14Bold, Brushes.Black, new RectangleF(10, 250, r.Width - 20, 50), format);
            e.Graphics.DrawString(Item.Country, Times14, Brushes.Black, new RectangleF(10, 270, r.Width - 20, 50), format);
            e.Graphics.DrawString(string.Format("{0} x {1}", Item.Length, Item.Width), Times14Bold, Brushes.Black, new RectangleF(10, 290, r.Width - 20, 50), format);
            e.Graphics.DrawString(string.Format("{0},- {1}", Item.VkNetto, (char)0x20AC), Times14Bold, Brushes.Black, new RectangleF(10, 310, r.Width - 20, 50), format);
        }
    }
}
