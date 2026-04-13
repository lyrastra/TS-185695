namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Dto
{
    public class EmployeeResponseDto
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; }
    }
}
