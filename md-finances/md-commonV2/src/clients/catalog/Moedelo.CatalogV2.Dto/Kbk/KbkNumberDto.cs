using System;
using Moedelo.Common.Enums.Enums.KbkNumbers;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.CatalogV2.Dto.Kbk
{
    public class KbkNumberDto
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public KbkNumberType KbkType { get; set; }

        public KbkPaymentType KbkPaymentType { get; set; }

        public KbkUsingType KbkUsingType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public SyntheticAccountCode AccountCode { get; set; }

        public int PayerStatus { get; set; }

        public int PaymentPriority { get; set; }

        public int? Period { get; set; }

        public int PaymentBase { get; set; }

        public int PaymentType { get; set; }

        public string Purpose { get; set; }

        public BudgetaryFundType FundType { get; set; }

        public string DocNumber { get; set; }

        /// <summary>
        /// Дата, с которой КБК за период [StartDate, EndDate] действителен
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// Дата, до которой КБК за период [StartDate, EndDate] действителен 
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        public long SubcontoId { get; set; }

        public string SubcontoName { get; set; }
    }
}