#nullable enable
namespace Moedelo.AccountV2.Dto.User;

public sealed class LoginChangeResponseDto
{
    public enum StatusCode : byte
    {
        Success = 0,
        UserNotFound = 1,
        NewLoginIsBusy = 2,
        UnknownError = 5
    }
    
    public StatusCode Status { get; set; }
}
