namespace Moedelo.BankIntegrations.ApiClient.Dto.LogService;

public class AppendLogRequestDto
{
    public string ObjId { get; set; }

    public RequestLogDataRequestDto Log { get; set; }
}
