namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto
{
    public class OperationsAccessDto
    {
        /// <summary>
        /// Есть ли доступ на создание/редактирование валютных операций
        /// </summary>
        public bool CanEditCurrencyOperations { get; set; }
    }
}