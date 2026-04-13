namespace Moedelo.Common.Enums.Enums.PaymentImport
{
    public enum ImportResultStatus
    {
        Ok = 0,
        Archived = 1,
        NotExist = 2,
        CurrencyNotExist = 3,
        // проблемы с разбором выписки
        WrongFile = 4,
        // Операции из закрытого периода
        WrongOperations = 5,
        // несколько р/сч
        ManySettlementAccounts = 6,
        // Нет операций (пустая)
        NoOperations = 7,
        // публикация события для консоли
        InProcess = 8,
        // Нет доступа к валютным операциям
        CurrencyAccessDenied = 9,
        // Ограничение по размеру файла
        SizeLimit = 10
    }
}