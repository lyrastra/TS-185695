namespace Moedelo.RptV2.Dto.ReportSettings
{
    public class RptSettingsDto
    {
        public bool IsPayLandTax { get; set; }
        public LandTaxSettingsDto LandTaxSettings { get; set; }

        public bool IsPayTransportTax { get; set; }
        public TransportTaxSettingsDto TransportTaxSettings { get; set; }

        public bool IsPayPropertyTax { get; set; }
        public PropertyTaxSettingsDto PropertyTaxSettings { get; set; }
    }
}
