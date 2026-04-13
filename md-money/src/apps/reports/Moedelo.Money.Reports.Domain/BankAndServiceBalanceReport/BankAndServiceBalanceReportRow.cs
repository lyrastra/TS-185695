using System;

namespace Moedelo.Money.Reports.Domain.BankAndServiceBalanceReport
{
    public class BankAndServiceBalanceReportRow
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Тариф
        /// </summary>
        public string Tariff { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// ОПФ
        /// </summary>
        public bool IsOoo { get; set; }

        /// <summary>
        /// Расчетный счет
        /// </summary>
        public string SettlementAccount { get; set; }

        /// <summary>
        /// Банк
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Дата, на которую были получены остатки из банка
        /// </summary>
        public DateTime BankBalanceDate { get; set; }

        /// <summary>
        /// Баланс в банке
        /// </summary>
        public decimal BankBalance { get; set; }

        /// <summary>
        /// Баланс в сервисе
        /// </summary>
        public decimal ServiceBalance { get; set; }

        /// <summary>
        /// Остатки введены
        /// </summary>
        public bool RemainsFilled { get; set; }

        /// <summary>
        /// Дата ввода остатков
        /// </summary>
        public DateTime? RemainsFillDate { get; set; }

        /// <summary>
        /// Количество р\с, по которым заполнены остатки
        /// </summary>
        public int CountSettlementAccountWithRemainsFilled { get; set; }

        /// <summary>
        /// Текущие состояние автосверки по разделу деньги
        /// </summary>
        public string ReconciliationState { get; set; }

        /// <summary>
        /// Дата последний автосверки
        /// </summary>
        public DateTime? LastReconciliationStartDate { get; set; }

        /// <summary>
        /// Общее количество нераспознанных поступлений
        /// </summary>
        public int UnrecognizedIncomingCount { get; set; }

        /// <summary>
        /// Сумма нераспознанных поступлений
        /// </summary>
        public decimal UnrecognizedIncomingSum { get; set; }

        /// <summary>
        /// Общее количество нераспознанных списаний
        /// </summary>
        public int UnrecognizedOutgoingCount { get; set; }

        /// <summary>
        /// Сумма нераспознанных списаний
        /// </summary>
        public decimal UnrecognizedOutgoingSum { get; set; }
    }
}
