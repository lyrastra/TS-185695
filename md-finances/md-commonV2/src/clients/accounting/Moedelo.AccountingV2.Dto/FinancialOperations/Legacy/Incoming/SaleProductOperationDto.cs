using System;
using System.Collections.Generic;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class SaleProductOperationDto : IncomingOperationDto
    {
        public int WaybillId { get; set; }
        public string WaybillNumber { get; set; }
        public DateTime? WaybillDate { get; set; }
        public string WaybillType { get; set; }
        public double WaybillSum { get; set; }
        public ServiceAndSaleType SaleType { get; set; }
        public decimal RecievedSum { get; set; }
        public decimal AgentSum { get; set; }
        public DateTime? AgentDate { get; set; }
        public int AgentOrderNumber { get; set; }
        public IReadOnlyCollection<PatentInMoneyDto> Patents { get; set; }

        public decimal AcquiringCommission { get; set; }
        public DateTime? AcquiringCommissionDate { get; set; }
        public decimal AcquiringUsnExpenseSum { get; set; }

        public override string Name => FinancialOperationNames.SaleProductOperation;
    }
}
