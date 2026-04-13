using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Salary;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    /// <summary>
    /// Авансовый отчет. Таблица командировок
    /// </summary>
    public class NewAdvanceStatementBusinessTripItemClientData
    {
        public long? Id { get; set; }

        public BusinessTripExpensesType Type { get; set; }

        public string DocumentNumber { get; set; }

        public string DocumentDate { get; set; }

        public string ConsumptionName { get; set; }

        /// <summary>
        /// Сумма (отчет)
        /// </summary>
        public decimal ReportedSum { get; set; }

        /// <summary>
        /// Сумма (принято)
        /// </summary>
        public decimal AcceptedSum { get; set; }

        /// <summary>
        /// Учесть в (переключатель СНО)
        /// </summary>
        public TaxationSystemType TaxationSystemType { get; set; }

        /// <summary>
        /// Бух счет
        /// </summary>
        public int SyntheticAccount { get; set; }

        public decimal? NdsSum { get; set; }

        public AoInvoiceClientData Invoice { get; set; }

        /// <summary>
        /// Учесть в ОСНО/УСН
        /// </summary>
        public decimal TaxableSum { get; set; }

        public int? KontragentId { get; set; }

        public string KontragentName { get; set; }

        public int ByOrder
        {
            get
            {
                if (this.Type == BusinessTripExpensesType.DailySum)
                    return -1;
                return (int) this.Type;
            }
        }
    }
}