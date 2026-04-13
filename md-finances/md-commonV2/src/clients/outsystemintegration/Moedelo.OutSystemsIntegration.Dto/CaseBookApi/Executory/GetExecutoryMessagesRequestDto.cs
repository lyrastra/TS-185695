using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Bankruptcy;
using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Executory
{
    public class GetExecutoryMessagesRequestDto
    {
        public string Inn { get; set; }

        public string Ogrn { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int Size { get; set; }

        public int Page { get; set; }

        public string Status { get; set; }

        public string SearchText { get; set; }

        public string ArrearText { get; set; }

        public bool? IsFinished { get; set; }

        public bool? IsNotFinished { get; set; }

        public BankruptTypeEnum BankruptType { get; set; }
    }
}
