using System.Collections.Generic;

namespace Moedelo.BPM.Recognition.Client
{
    public class OcrInvoice
    {
        public class Item
        {
            public string Descript { get; set; }

            public string UnitCode { get; set; }

            public string Unit { get; set; }

            public string Qty { get; set; }

            public string Price { get; set; }

            public string Cost { get; set; }

            public string Excise { get; set; }

            public string TaxRate { get; set; }

            public string TaxSum { get; set; }

            public string SumWithTax { get; set; }

            public string CountryCode { get; set; }

            public string Country { get; set; }

            public string Decl { get; set; }

            public string RightDecl { get; set; }
        }

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

        public string DocType { get; set; }

        public string BackOrder { get; set; }

        public string Consignor { get; set; }

        public string Consignee { get; set; }

        public string PaymentDoc { get; set; }

        public string Stamp { get; set; }

        public string CorrectionNum { get; set; }

        public string CorrectionDate { get; set; }
        
        public List<Item> Table { get; set; }
    }
}