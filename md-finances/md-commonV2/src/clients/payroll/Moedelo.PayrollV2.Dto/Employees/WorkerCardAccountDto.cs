namespace Moedelo.PayrollV2.Dto.Employees
{
    public class WorkerCardAccountDto
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public string Number { get; set; }

        public bool IsCurrent { get; set; }

        public string ReasonPay { get; set; }

        public int? BankId { get; set; }

        /// <summary>Получатель, указывается в платежке </summary>
        public string Recipient { get; set; }

        /// <summary>ИНН получателя для платежек  </summary>
        public string InnRecipient { get; set; }
    }
}
