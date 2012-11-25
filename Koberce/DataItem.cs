using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koberce
{
    public class DataItem
    {
        public DataItem()
        {
            Process = true;
        }

        public bool Process { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public string Supplier { get; set; }
        public string Comment { get; set; }
        public string GlobalNumber { get; set; }
        public string SupplierNr { get; set; }
        public string RgNr { get; set; }
        public string QmPrice { get; set; }
        public string EuroStuck { get; set; }

        internal string EkNetto { get; set; }
        internal string Quantity { get; set; }
        internal string VkNetto { get; set; }
        internal string Date { get; set; }
        internal string MvDate { get; set; }
        internal string Paid { get; set; }
        internal string Invoice { get; set; }
    }
}
