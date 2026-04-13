namespace Moedelo.AccountV2.Dto.UserAccessControl
{
    public class TariffDto
    {
        public int Id { get; set; }

        public string TariffName { get; set; }

        public decimal Price { get; set; }

        public string ProductPlatform { get; set; }
    }
}