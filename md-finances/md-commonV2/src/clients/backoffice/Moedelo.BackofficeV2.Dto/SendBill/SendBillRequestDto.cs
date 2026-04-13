using System.Collections.Generic;

namespace Moedelo.BackofficeV2.Dto.SendBill
{
    public class SendBillRequestDto
    {
        public string UserLogin { get; set; }

        public string UserFio { get; set; }

        public string OperatorLogin { get; set; }

        public string SendBillEmail { get; set; }

        public List<SendBillItemRequestDto> Items { get; set; }

        public List<BillPaymentInfoRequestDto> BillPayments { get; set; }

        public int PriceListId { get; set; }

        public string PriceListName { get; set; }

        public string BillExpirationDate { get; set; }

        public string Payer { get; set; }

        public OperationType OperationType { get; set; }

        public bool IsOutsource { get; set; }

        public string Note { get; set; }

        public string CoveringMessage { get; set; }

        public bool FromPartner { get; set; }

        public bool AutoCreate { get; set; }

        public bool OnlyForOperator { get; set; }

        public bool SendPayLinkOnly { get; set; }

        public int FirmId { get; set; }
    }
} 