namespace Moedelo.BankIntegrations.ApiClient.Dto.LogService;

public class SaveRequestResponseLogRequestDto
{
    public int MacroRequestId { get; set; }

    public int? MicroRequestId { get; set; }

    public string Sourse { get; set; }

    public BankAdapterLogDto Log { get; set; }
}
