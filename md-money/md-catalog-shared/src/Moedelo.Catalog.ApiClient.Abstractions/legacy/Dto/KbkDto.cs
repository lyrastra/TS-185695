using Moedelo.Catalog.ApiClient.Enums;
using System;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto
{
    public class KbkDto
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public KbkType KbkType { get; set; }

        public KbkPaymentType KbkPaymentType { get; set; }

        public BudgetaryFundType FundType { get; set; }

        public KbkUsingType KbkUsingType { get; set; }

        public int PaymentPriority { get; set; }

        public BudgetaryPayerStatus PayerStatus { get; set; }

        public BudgetaryPaymentBase PaymentBase { get; set; }

        public long SubcontoId { get; set; }

        public string DocNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        public int AccountCode { get; set; }

        public string Purpose { get; set; }

        public string Description { get; set; }
    }
}
