using System;

namespace Moedelo.Money.Reports.Domain.BankAndServiceBalanceReport
{
    public class BankAndServiceBalanceReportPrintRow
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
        public string Opf { get; set; }

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
        /// Баланс сходится
        /// </summary>
        public string BalancesIsEqual { get; set; }

        /// <summary>
        /// Разница балансов
        /// </summary>
        public decimal BalancesDiff { get; set; }

        /// <summary>
        /// Баланс в банке > Баланс в сервисе = 1
        /// Баланс в банке = Баланс в сервисе = 0
        /// Баланс в банке < Баланс в сервисе = -1
        /// </summary>
        public string BankBalanceGtServiceBalance { get; set; }

        /// <summary>
        /// Остатки введены
        /// </summary>
        public string RemainsFilled { get; set; }

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
        /// Если у пользователя есть операции в красной зоне
        /// </summary>
        public string HasUnrecognizedOperations { get; set; }

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

        /// <summary>
        /// "Баланс в сервисе" + "Нераспознанные поступления (сумма)" - "Нераспознанные списания (сумма)"
        /// </summary>
        public decimal ServiceBalanceWithUnrecognizedSum { get; set; }

        /// <summary>
        /// Баланс сходится с учетом красной зоны
        /// </summary>
        public string IsBalancesEqualWithUnrecognizedSum { get; set; }
    }
}
