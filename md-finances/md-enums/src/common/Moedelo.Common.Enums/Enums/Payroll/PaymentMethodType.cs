using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Payroll
{
    /// <summary>
    /// Способ выплаты денегъ сотрудникамъ.
    /// </summary>
    public enum PaymentMethodType
    {
        /// <summary>
        /// Наличными по расчетной ведомости
        /// </summary>
        [Description("Ведомость")]
        Paybill = 0,

        /// <summary>
        /// На карту зарплатного проекта.
        /// </summary>
        [Description("Зарплатный проект")]
        SalaryCard = 1,

        /// <summary>
        /// На персональную карту.
        /// </summary>
        [Description("На карту")]
        PersonalCard = 2,

        /// <summary>
        /// По кассовому ордеру
        /// </summary>
        [Description("По кассовому ордеру")]
        CashOrder = 3,

        /// <summary>
        /// Квитанция для бюджетных платежей
        /// </summary>
        [Description("Квитанция")]
        Receipt = 4
    }
}