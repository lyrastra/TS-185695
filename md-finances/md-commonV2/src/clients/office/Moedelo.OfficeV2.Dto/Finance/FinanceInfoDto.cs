namespace Moedelo.OfficeV2.Dto.Finance
{
    public class FinanceInfoDto
    {
        public int Year { get; set; }

        public FinanceItemInfoDto Revenue { get; set; }

        public FinanceItemInfoDto Profit { get; set; }

        public FinanceItemInfoDto Assets { get; set; }
    }
}
