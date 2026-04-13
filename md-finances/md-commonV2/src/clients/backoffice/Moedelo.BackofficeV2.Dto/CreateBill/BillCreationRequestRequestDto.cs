using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BackofficeV2.Dto.CreateBill
{
    public class BillCreationRequestRequestDto
    {
        public int FirmId { get; set; }

        public string UserLogin { get; set; }

        public string UserFio { get; set; }

        public string BillExpirationDate { get; set; }

        public string SendBillEmail { get; set; }

        public BillingOperationType OperationType { get; set; }

        public int PromoCodeId { get; set; }

        public int MonthesAsBonus { get; set; }

        public DateTime ExpirationDateAsBonus { get; set; }

        public int PriceListId { get; set; }

        public int OutsourcePriceListId { get; set; }

        public decimal Sum { get; set; }

        public decimal NormativeSum { get; set; }

        public string Note { get; set; }

        public string CoveringMessage { get; set; }

        public string ProductGroup { get; set; }

        public int PayPartsCount { get; set; }

        public string Payer { get; set; }

        public DateTime TariffStartDate { get; set; }

        public bool Reselling { get; set; }

        public bool NotSendToUser { get; set; }

        public bool SendPayLinkOnly { get; set; }

        public List<PaymentPositionClientDataRequestDto> PaymentPositions { get; set; }

        public bool IsPhysicalPerson { get; set; }

        public bool IsPartner { get; set; }

        public bool PayOutsource { get; set; }
        
        public int PayRegionId { get; set; }
    }
}
