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
            e.Graphics.DrawString(barCode(Item.GlobalNumber), Code128, Brushes.Black, new RectangleF(17, 377, r.Width - 20, 150));
            e.Graphics.DrawString(string.Format("NR. {0}", Item.GlobalNumber), Times18Bold, Brushes.Black, new RectangleF(53, 448, r.Width - 20, 150));
            e.Graphics.DrawString(string.Format("{2}, {0} x {1}", Item.Length, Item.Width, Item.SupplierNr), Arial11Bold, Brushes.Black, new RectangleF(47, 470, r.Width - 20, 50));
        }

        /*
         *	Auteur	:	Joffrey VERDIER
         *	Date	:	08/2006
         *	Légal	:	OpenSource © 2007 AVRANCHES
         * 
         */
        string barCode(string chaine)
        {
            int ind = 1;
            int checksum = 0;
            int mini;
            int dummy;
            bool tableB;
            String code128;
            int longueur;
            code128 = "";
            longueur = chaine.Length;

            if (longueur == 0)
                Console.WriteLine("\n chaine vide");
            else
                for (ind = 0; ind < longueur; ind++)
                {
                    if ((chaine[ind] < 32) || (chaine[ind] > 126))
                    {
                        Console.WriteLine("\n chaine invalide");
                    }
                }

            tableB = true;
            ind = 0;

            while (ind < longueur)
            {
                if (tableB == true)
                {
                    if ((ind == 0) || (ind + 3 == longueur - 1))
                        mini = 4;
                    else
                        mini = 6;

                    mini = mini - 1;

                    if ((ind + mini) <= longueur - 1)
                        while (mini >= 0)
                        {
                            if ((chaine[ind + mini] < 48) || (chaine[ind + mini] > 57))
                            {
                                Console.WriteLine("\n exit");
                                break;
                            }
                            mini = mini - 1;
                        }


                    if (mini < 0)
                    {
                        if (ind == 0)
                            code128 = Char.ToString((char)205);
                        else
                            code128 = code128 + Char.ToString((char)199);
                        tableB = false;
                    }
                    else
                    {
                        if (ind == 0)
                            code128 = Char.ToString((char)204);
                    }
                }

                if (tableB == false)
                {
                    mini = 2;
                    mini = mini - 1;
                    if (ind + mini < longueur)
                    {
                        while (mini >= 0)
                        {
                            if (((chaine[ind + mini]) < 48) || ((chaine[ind]) > 57))
                                break;
                            mini = mini - 1;
                        }
                    }
                    if (mini < 0)
                    {
                        dummy = Int32.Parse(chaine.Substring(ind, 2));
                        Console.WriteLine("\n  dummy ici : " + dummy);

                        if (dummy < 95)
                            dummy = dummy + 32;
                        else
                            dummy = dummy + 100;
                        code128 = code128 + (char)(dummy);

                        ind = ind + 2;
                    }
                    else
                    {
                        code128 = code128 + Char.ToString((char)200);
                        tableB = true;
                    }
                }
                if (tableB == true)
                {
                    code128 = code128 + chaine[ind];
                    ind = ind + 1;
                }
            }

            for (ind = 0; ind <= code128.Length - 1; ind++)
            {
                dummy = code128[ind];
                Console.WriteLine("\n  et voila dummy : " + dummy);
                if (dummy < 127)
                    dummy = dummy - 32;
                else
                    dummy = dummy - 100;
                if (ind == 0)
                    checksum = dummy;
                checksum = (checksum + (ind) * dummy) % 103;
            }

            if (checksum < 95)
                checksum = checksum + 32;
            else
                checksum = checksum + 100;
            code128 = code128 + Char.ToString((char)checksum)
                    + Char.ToString((char)206);

            return code128;
        }
    }
}