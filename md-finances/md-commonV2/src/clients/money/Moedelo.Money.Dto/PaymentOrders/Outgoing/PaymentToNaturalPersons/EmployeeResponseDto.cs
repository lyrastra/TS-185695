namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    /// <summary>
    /// Сотрудник
    /// </summary>
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
