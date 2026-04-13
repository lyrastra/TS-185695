namespace Moedelo.Spam.ApiClient.Abastractions.Dto.PushToken;

public class PushTokenSaveRequestDto
{
    public int FirmId { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; }

    public string MobileAppName { get; set; }
}