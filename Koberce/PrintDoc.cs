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
        public PrintDoc(DataItem item)
        {
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            //DefaultPageSettings.PaperSize = new PaperSize("ms", 142, 354);
            DefaultPageSettings.PaperSize = new PaperSize("ms", 460, 661);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            e.Graphics.TranslateTransform(1, 1);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            var i = new Bitmap("global-label.png");
            Rectangle r = e.PageBounds;
            r.Inflate(2, 2);
            e.Graphics.DrawImage(i, r);

            e.Graphics.DrawString("Michal Koščák", new Font("Times New Roman", 10), Brushes.Red, 10, 10);
        }
    }
}
