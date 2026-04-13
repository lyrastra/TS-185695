namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalarySettings
{
    public class NdflSettingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FirmId { get; set; }
        public string Kpp { get; set; }
        public string Oktmo { get; set; }
        public int FnsId { get; set; }
        public string FnsName { get; set; }
        public string FnsCode { get; set; }
    }
}
