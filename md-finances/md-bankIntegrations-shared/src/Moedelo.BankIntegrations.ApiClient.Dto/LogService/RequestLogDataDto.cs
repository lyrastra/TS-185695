namespace Moedelo.BankIntegrations.ApiClient.Dto.LogService;

public class RequestLogDataRequestDto
{
    public int MacroReqId { get; set; }
    public int? MicroReqId { get; set; }

    public string RequestLog { get; set; }
    public string ResponseLog { get; set; }

    public string Sourse { get; set; }
}
