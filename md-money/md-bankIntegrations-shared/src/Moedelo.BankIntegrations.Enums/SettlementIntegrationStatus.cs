namespace Moedelo.BankIntegrations.Enums
{

    public enum SettlementIntegrationStatus
    {
        /// <summary> Интеграция не поддерживается </summary>
        NotSupported,

        /// <summary> Интеграция доступна, но не включена </summary>
        Available,

        /// <summary> Интеграция включена </summary>
        Enabled
    }
}
