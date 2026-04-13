namespace Moedelo.AccountV2.Dto.ProfOutsource
{
    public class ProfOutsourceTariffDto
    {
        public int Id { get; set; }

        public int ProfOutsourceId { get; set; }

        public int TariffId { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public string ProductPlatform { get; set; }
    }
}
