using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Docs.Dto.Ukd
{
    public class UkdDto
    {
        public long DocumentBaseId { get; set; }
        public int FirmId { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public int KontragentId { get; set; }
        public long SourceDocumentBaseId { get; set; }
        public long? AdditionalDocumentBaseId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool ProvideInAccounting { get; set; }
        public byte SignStatus { get; set; }
        public bool ReturnDefectiveGoods { get; set; }
        public bool BuyerNdsPayer { get; set; }
        public NdsPositionType NdsPositionType { get; set; }
        public UkdSourceType UkdSourceType { get; set; }
        public IList<UkdItemDto> Items { get; set; }
    }
}