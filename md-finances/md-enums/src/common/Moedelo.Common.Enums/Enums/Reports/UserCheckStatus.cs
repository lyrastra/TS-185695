using System;
using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Reports
{
    [Flags]
    public enum UserCheckStatus : ulong
    {
        [Description("")]
        Success = 0,

        [Description("Нет оплаты")]
        NotPaid = 1 << 0,

        [Description("Нет интеграции")]
        NoActiveIntegration = 1 << 1,

        [Description("Фирма не на УСН")]
        NotUsn = 1 << 2,

        [Description("Фирма на аутсорсе")]
        Outsource = 1 << 3,

        [Description("Не заполнен ИНН")]
        MissedInn = 1 << 4,

        // Для декларации УСН
        [Description("Не заполнен КПП")]
        MissedKpp = 1 << 5,

        // Для декларации УСН
        [Description("Нет признака работодателя")]
        MissedPfrEmployerNumber = 1 << 6,

        // Для декларации УСН
        [Description("Не заполнен ОГРН")]
        MissedOgrn = 1 << 7,

        [Description("Не заполнен ОГРН ИП")]
        MissedOgrnip = 1 << 8,

        // Для декларации УСН
        [Description("Не указана основная деятельность")]
        MissedMainActivity = 1 << 9,

        // Для декларации УСН
        [Description("Не указано имя директора")]
        MissedDirector = 1 << 10,

        [Description("Не заполнена ФНС")]
        MissedFnsRequisites = 1 << 11,

        // Для декларации УСН
        [Description("Не указана фамилия директора")]
        MissedDirectorSurname = 1 << 12,

        [Description("Есть неопознанные платежи")]
        UnknownPayments = 1 << 13,

        [Description("Есть неопознанные платежи 2")]
        UnknownPaymentsInUse = 1 << 14,

        [Description("Есть пониженная ставка")]
        LowTaxRate = 1 << 15,

        [Description("Есть торговый объект")]
        TradeTax = 1 << 16,

        [Description("Есть целевые средства")]
        TargetFunds = 1 << 17,

        [Description("УСН не 6%")]
        IncorrectUSNType = 1 << 18,

        // Для декларации УСН
        [Description("Указан торговый объект")]
        WizardWithTradingTax = 1 << 19,

        [Description("Мастер завершен")]
        WizardUsnCompleted = 1 << 20,

        [Description("Не заполнен адрес")]
        MissedAddress = 1 << 21,

        [Description("Не заполнено наименование")]
        MissedIpName = 1 << 22,

        [Description("Не ИП")]
        NotIp = 1 << 23,

        [Description("У пользователя есть нераспознанные авансы по УСН")]
        UnaccountedUnknownPayments = 1 << 24,

        [Description("У пользователя нет вычетов за текущий период ")]
        ZeroSumFundPayments = 1 << 25,

        [Description("Мастер находится не на первом шаге")]
        WizardNotFirstStep = 1 << 26,

        [Description("Год меньше 2023")]
        YearLess2023 = 1 << 27,

        [Description("Ошибка при закрытии мастера")]
        CloseError = 1 << 28,

        [Description("Общая ошибка")]
        Failed = 1 << 29,

        [Description("Ошибка отправки в банк")]
        FailedToSendPayment = 1 << 30,

        // Для декларации УСН
        [Description("У пользователя рассчитан минимальный налог")]
        MinimalTax = 1UL << 31,

        [Description("Не завершена декларация за предыдущий год")]
        NotCompletedPreviousDeclaration = 1UL << 32,
    }
}