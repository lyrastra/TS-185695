using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.ReceiptStatement.Enums;
using Newtonsoft.Json;
using System;

namespace Moedelo.ReceiptStatement.ApiClient.Abstractions.Dto
{
    public class ReceiptStatementDto
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        [JsonConverter(typeof(MdDateConverter))]
        public DateTime Date { get; set; }

        public long SubcontoId { get; set; }

        public int KontragentId { get; set; }

        public int ContractId { get; set; }

        public string Name { get; set; }

        public decimal SumWithNds { get; set; }

        public decimal NdsSum { get; set; }

        public NdsType NdsType { get; set; }

        public long FixedAssetDocumentBaseId { get; internal set; }
    }
}
