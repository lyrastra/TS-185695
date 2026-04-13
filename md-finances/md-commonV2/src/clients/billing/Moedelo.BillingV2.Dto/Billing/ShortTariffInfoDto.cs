namespace Moedelo.BillingV2.Dto.Billing
{
    public class ShortTariffInfoDto
    {
        public string TariffName { get; set; }

        public bool IsTrial { get; set; }

        public bool IsWithEmployee { get; set; }

        public bool IsPay { get; set; }
    }
}
