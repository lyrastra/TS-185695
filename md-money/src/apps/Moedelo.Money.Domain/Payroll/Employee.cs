namespace Moedelo.Money.Domain.Payroll
{
    public class Employee : IEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long? SubcontoId { get; set; }
        public bool IsNotStaff { get; set; }
    }
}