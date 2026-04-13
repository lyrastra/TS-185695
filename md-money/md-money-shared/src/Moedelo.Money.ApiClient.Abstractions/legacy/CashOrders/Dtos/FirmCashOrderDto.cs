using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Dtos
{
    public class FirmCashOrderDto
    {
        /// <summary>
        /// Идентификатор кассового ордера (КО)
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// BaseId КО (предпочтительнее использовать вместо Id)
        /// </summary>
        public long DocumentBaseId { get; set; }

        public long CashId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public OperationType OperationType { get; set; }

        /// <summary>
        /// Направление платежа: 1 - исходящий, 2 - входящий
        /// </summary>
        public int Direction { get; set; }

        public decimal PaidCardSum { get; set; }

        public long KontragentId { get; set; }

        public int KontragentAccountCode { get; set; }

        public long? ContractBaseId { get; set; }

        public int? BudgetaryTaxesAndFees { get; set; }

        public int? KbkId { get; set; }
    }
}