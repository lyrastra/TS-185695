using System;

namespace Moedelo.Money.Dto.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    /// <summary>
    /// Модель для сохранения операции "Возврат от подотчетного лица"
    /// </summary>
    public class RefundFromAccountablePersonSaveDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта
        /// </summary>
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Сотрудник
        /// </summary>
        public EmployeeSaveDto Employee { get; set; }

        /// <summary>
        /// Авансовый отчет
        /// </summary>
        public AdvanceStatementLinkSaveDto AdvanceStatement { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }
    }
}
