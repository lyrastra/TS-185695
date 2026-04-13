namespace Moedelo.BankIntegrations.Dto.BssBanks
{
    public class BssIntegrationDataDto
    {
        public int FirmId { get; set; }
        public int SsoIntegrationPartner { get; set; }
        public BssBankIntegratedDataDto IntegratedData { get; set; }
    }
}
