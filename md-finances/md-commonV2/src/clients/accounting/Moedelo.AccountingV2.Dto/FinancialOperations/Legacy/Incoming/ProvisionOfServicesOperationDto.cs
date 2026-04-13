using System;
using System.Collections.Generic;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class ProvisionOfServicesOperationDto : IncomingOperationDto
    {
        public int StatementId { get; set; }
        public string StatementNumber { get; set; }
        public DateTime? StatementDate { get; set; }
        public string StatementType { get; set; }
        public double StatementSum { get; set; }
        public ServiceAndSaleType SaleType { get; set; }
        public decimal RecievedSum { get; set; }
        public decimal AgentSum { get; set; }
        public DateTime? AgentDate { get; set; }
        public int AgentOrderNumber { get; set; }
        public IList<PatentInMoneyDto> Patents { get; set; }

        public decimal AcquiringCommission { get; set; }
        public DateTime? AcquiringCommissionDate { get; set; }
        public decimal AcquiringUsnExpenseSum { get; set; }

        public override string Name => FinancialOperationNames.ProvisionOfServicesOperation;
    }
}
