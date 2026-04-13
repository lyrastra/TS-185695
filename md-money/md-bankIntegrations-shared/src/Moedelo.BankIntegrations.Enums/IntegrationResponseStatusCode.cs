namespace Moedelo.BankIntegrations.Enums
{
    public enum IntegrationResponseStatusCode
    {
        Ok = 0,
        NeedSms = 5,
        Error = 6,
        BussinessError = 7,
        /// <summary> Для зарплатного проекта, если запрос отменён в банке по валидации </summary>
        BadRequest = 8,
        /// <summary> Для зарплатного проекта, если создание реестра истекло </summary>
        TimeoutOccurred = 9,
        /// <summary> Недостаточно прав </summary>
        AccessDenied = 10
    }
}
