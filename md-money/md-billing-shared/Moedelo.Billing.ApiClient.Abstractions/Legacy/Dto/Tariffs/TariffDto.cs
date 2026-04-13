namespace Moedelo.Billing.Abstractions.Legacy.Dto.Tariffs;

public class TariffDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Platform { get; set; }
    public string Group { get; set; }
    public bool IsOneTime { get; set; }
    public bool IsWithAccess { get; set; }
}