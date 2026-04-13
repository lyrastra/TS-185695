namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class ChargePatentDto
{
    public long PatentId { get; set; }
    public decimal Sum { get; set; }
    public decimal FundSum { get; set; }
    public bool IsPrivilege { get; set; }
}