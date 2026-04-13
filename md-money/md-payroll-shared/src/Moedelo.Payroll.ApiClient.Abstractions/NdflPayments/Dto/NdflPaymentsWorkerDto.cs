namespace Moedelo.Payroll.ApiClient.Abstractions.NdflPayments.Dto;

public class NdflPaymentsWorkerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string FullName { get; set; }
    public int NdflSettingId { get; set; }
}