namespace Moedelo.Payroll.ApiClient.Abstractions.NdflPayments.Dto;

public class NdflPaymentsNdflSettingDto
{
    public int Id { get; set; }
    public string Kpp { get; set; }
    public string Oktmo { get; set; }
    public int TaxAdministrationId { get; set; }
    public string TaxAdministrationName { get; set; }
    public string TaxAdministrationCode { get; set; }
}