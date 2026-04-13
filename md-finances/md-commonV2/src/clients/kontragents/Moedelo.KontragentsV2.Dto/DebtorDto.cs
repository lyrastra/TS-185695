namespace Moedelo.KontragentsV2.Dto
{
    public class DebtorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public decimal Debt { get; set; }
    }
}