namespace Moedelo.BankIntegrations.ApiClient.Dto.Rshb;

public class SendConfirmRequestDto
{
    public string Id { get; set; }
    
    public bool IsSuccess { get; set; }

    public string Comment { get; set; }
}