using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Moedelo.PaymentOrderImport.Enums
{
    public enum PaymentImportResultStatus
    {
        [Description("Успех")]
        Ok = 0,

        [Description("Архивный расчетный счет")]
        Archived = 1,

        [Description("Расчетный счет не существует")]
        NotExist = 2,

        [Description("Валюта не найдена")]
        CurrencyNotExist = 3,

        [Description("Неправильный формат")]
        WrongFile = 4,

        [Description("Операции из закрытого периода")]
        WrongOperations = 5,

        [Description("Несколько расчетных счетов")]
        ManySettlementAccounts = 6,

        [Description("Нет операций (пустая выписка)")]
        NoOperations = 7,

        [Description("Публикация события для консоли(устар.)")]
        InProcess = 8,

        [Description("Нет доступа к валютным операциям")]
        CurrencyAccessDenied = 9,

        [Description("Размер файла превышен")]
        SizeLimit = 10,

        [Description("Проблемы с кодировкой")]
        UnknownEncoding = 11,

        [Description("Расчетный счет не найден")]
        MissingSettlementAccount = 12,

        [Description("Расчетный счет не установлен")]
        SettlementAccountNotSet = 13,

        [Description("Неполные данные по расчетному счету / БИК")]
        SettlementDataNotIsFull = 14,

        [Description("В некоторых операциях не найден ИНН клиента")]
        InnNotFound = 15,

        [Description("Фирма с таким идентификатором не найдена")]
        FirmNotFound = 16,

        [Description("Идентификатор файла пуст")]
        EmptyFileId = 17,

        [Description("Идентификатор фирмы не указан")]
        EmptyFirmId = 18,

        [Description("Проблемы с периодом")]
        MissingPeriodStartDate = 19,

        [Description("Проблемы с периодом")]
        MissingPeriodEndDate = 20,

        [Description("Есть операции из закрытого периода")]
        OperationsInClosedPeriod = 21,

        [Description("Импорт невозможен в связи с включенной интеграцией по текущему счету")]
        SettlementIntegrationStatusEnabled = 22,

        [Description("Архивный расчетный счет")]
        SettlementAccountIsArchived = 23,

        [Description("Импорт не возможен, есть дубликаты")]
        DuplicateOperations = 24,


        [Description("Непредвиденная ошибка")]
        Error = 98,
        [Description("Не определён")]
        None = 99
    }

    public static class PaymentImportResultStatusExtention
    {
        public static string GeDescription(this PaymentImportResultStatus value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description;
        }
    }
}
