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
            //DefaultPageSettings.PaperSize = new PaperSize("psize", 354, 512);

            // 9.6x13.6cm v palcoch = 3.78x5.35
            DefaultPageSettings.PaperSize = new PaperSize("psize", 378, 535);
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
            //r.Inflate(2, 2);
            e.Graphics.DrawImage(i, r);

            var format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            e.Graphics.DrawString(Item.Name, Times14Bold, Brushes.Black, new RectangleF(10, 260, r.Width - 20, 50), format);
            e.Graphics.DrawString(Item.Country, Times14, Brushes.Black, new RectangleF(10, 280, r.Width - 20, 50), format);
            e.Graphics.DrawString(string.Format("{0} x {1}", Item.Length, Item.Width), Arial14Bold, Brushes.Black, new RectangleF(10, 300, r.Width - 20, 50), format);
            e.Graphics.DrawString(string.Format("{0},- {1}", Item.VkNetto, (char)0x20AC), Arial14Bold, Brushes.Black, new RectangleF(10, 320, r.Width - 20, 50), format);
            // ciarovy kod
            //e.Graphics.DrawString(barCode(Item.GlobalNumber), Code128, Brushes.Black, new RectangleF(37, 377, r.Width - 20, 150));
            e.Graphics.DrawString(Item.GlobalNumber, Code128, Brushes.Black, new RectangleF(37, 377, r.Width - 20, 150));
            e.Graphics.DrawString(string.Format("NR. {0}", Item.GlobalNumber), Times18Bold, Brushes.Black, new RectangleF(53, 448, r.Width - 20, 150));
            e.Graphics.DrawString(string.Format("{2}, {0} x {1}", Item.Length, Item.Width, Item.SupplierNr), Arial11Bold, Brushes.Black, new RectangleF(47, 470, r.Width - 20, 50));
        }


        string barCode(string args)
        {
            string code128 = "";

            int i, z, mini, dummy, checksum = 0;
            i = 1;
            if (args.Length > 0)
            {
                z = 0;
                i = 1;
                while ((i <= args.Length) && (z == 0))
                {
                    if ((args[i - 1] >= (char)32 && args[i - 1] <= (char)125) || args[i - 1] == (char)198)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                        z = 1;
                    }
                }
            }

            bool tableB = true;
            if (i > 0)
            {
                i = 1;
                while (i <= args.Length)
                {
                    if (tableB)
                    {
                        if (i == 1 || (i + 3) == args.Length)
                        {
                            mini = 4;
                        }
                        else { mini = 6; }
                        mini = testNum(mini, args, i);
                        if (mini < 0)
                        {
                            if (i == 1)
                            {
                                char ch = (char)210;
                                code128 = ch.ToString();
                            }
                            else { char ch = (char)204; code128 = code128 + ch; }
                            tableB = false;
                        }
                        else
                        {
                            if (i == 1) { char ch = (char)209; code128 = ch.ToString(); }
                        }
                    }
                    if (!tableB)
                    {
                        mini = 2;
                        mini = testNum(mini, args, i);
                        if (mini < 0)
                        {
                            string tmp = args.Substring(i - 1, 2);
                            dummy = myVal(tmp);
                            if (dummy < 95)
                            {
                                dummy = dummy + 32;
                            }
                            else
                            {
                                dummy = dummy + 105;
                            }
                            char ch = (char)dummy;
                            code128 = code128 + ch;
                            i = i + 2;
                        }
                        else
                        {
                            char ch = (char)205;
                            code128 = code128 + ch;
                            tableB = true;
                        }
                    }
                    if (tableB)
                    {
                        code128 = code128 + args[i - 1];
                        i++;
                    }
                }
                for (i = 1; i <= code128.Length; i++)
                {
                    dummy = code128[i - 1];
                    if (dummy < 127) dummy = dummy - 32; else dummy -= 105;
                    if (i == 1)
                    {
                        checksum = dummy;
                    }
                    checksum = (checksum + (i - 1) * dummy) % 103;
                }
                if (checksum < 95)
                {
                    checksum += 32;
                }
                else
                    checksum += 100;
                char ch2 = (char)checksum;
                char ch1 = (char)211;
                code128 = code128 + ch2 + ch1;
            }
            return code128;
        }


        int testNum(int mini, string chaine, int i)
        {
            mini = mini - 1;
            int y;
            if ((i + mini) <= chaine.Length)
            {
                y = 0;
                while (mini >= 0 && y == 0)
                {
                    if (chaine[i + mini - 1] < (char)48 || chaine[i + mini - 1] > (char)5748)
                    {
                        y = 1;
                        mini = mini + 1;
                    }
                    mini = mini - 1;
                }
            }
            return mini;
        }


        int myVal(string chaine)
        {
            int j = 1;
            int chaine2 = 0;
            while (j <= chaine.Length)
            {
                if (char.IsDigit(chaine[j-1]))
                {
                    chaine2 = chaine2 * 10 + int.Parse(chaine[j - 1].ToString());
                    j++;
                }
                else break;
            }
            return chaine2;
        }
    }
}