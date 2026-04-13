namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class ChargeBonusDto
{
    public decimal Sum { get; set; }
    public int ChargeTypeId { get; set; }
    public decimal Rate { get; set; }
}