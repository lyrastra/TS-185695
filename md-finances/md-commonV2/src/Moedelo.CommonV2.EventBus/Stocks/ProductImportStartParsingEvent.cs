using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Stocks
{
    public class ProductImportStartParsingEvent
    {
        public int FirmId { get; set; }

        public bool HasNdsInAccPolicy { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public List<byte> Data { get; set; }

        public bool HasFullOrPartialProductAccountingAccess { get; set; }
    }
}