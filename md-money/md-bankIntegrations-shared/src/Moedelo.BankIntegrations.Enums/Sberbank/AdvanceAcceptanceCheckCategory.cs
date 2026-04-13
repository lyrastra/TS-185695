namespace Moedelo.BankIntegrations.Enums.Sberbank
{
    /// <summary>
    /// Категории для проверки клиентов по ЗДА
    /// </summary>
    public enum AdvanceAcceptanceCheckCategory
    {
        /// <summary> Клиент использует тариф, не предоставляемый Сбербанком. </summary>
        NotSberbankTariff = 0,
        /// <summary> Счёт клиента заблокирован и не может быть использован. </summary>
        AccountBlocked,
        /// <summary> Не удалось получить информацию о текущем остатке на счете клиента. </summary>
        FailedToGetAccountBalance,
        /// <summary> На счете клиента недостаточно средств для выполнения операции. </summary>
        InsufficientFunds,
        /// <summary> Успешная проверка, клиент может выставить ПТ. </summary>
        Success,
        /// <summary> Ошибка, клиент не может выставить ПТ. </summary>
        Error,
        /// <summary> Клиент пользуется промо-тарифом. </summary>
        PromoTariff
    }
}
