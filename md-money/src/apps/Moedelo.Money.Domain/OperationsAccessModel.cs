namespace Moedelo.Money.Domain
{
    public class OperationsAccessModel
    {
        /// <summary>
        /// Есть ли доступ на создание/редактирование валютных операций
        /// </summary>
        public bool CanEditCurrencyOperations { get; set; }
    }
}