namespace Moedelo.BankIntegrations.Enums
{
    public enum SalaryPaymentRegistryResultCode : byte
    {
        Unknow = 0,

        /// <summary> Регистр успешно создан </summary>
        Created = 1,

        /// <summary> Регистр отправлен в банковскую очередь </summary>
        Queued = 2,

        /// <summary> Во время создания сработала валидация </summary>
        Error = 3,

    }
}
