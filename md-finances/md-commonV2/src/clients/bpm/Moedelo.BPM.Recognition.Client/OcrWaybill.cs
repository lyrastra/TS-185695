using System.Collections.Generic;

namespace Moedelo.BPM.Recognition.Client
{
    public class OcrWaybill
    {
        public class Item
        {
            public string Descript { get; set; }

            public string Unit { get; set; }

            public string Qty { get; set; }

            public string Price { get; set; }

            public string Cost { get; set; }

            public string TaxRate { get; set; }

            public string TaxSum { get; set; }

            public string SumWithTax { get; set; }
        }

        public string DocName { get; set; }

        public string DocNum { get; set; }

        public string DocDate { get; set; }

        public string Sum { get; set; }

        public string TaxSum { get; set; }

        public string DocCurrency { get; set; }

        public string IssCompany { get; set; }

        public string IssAddres { get; set; }

        public string IssINN { get; set; }

        public string IssKPP { get; set; }

        public string DesCompany { get; set; }

        public string DestAddres { get; set; }

        public string DestINN { get; set; }

        public string DestKPP { get; set; }

        public string BackOrder { get; set; }

        public string Stamp { get; set; }

        public List<Item> Table { get; set; }
    }
}