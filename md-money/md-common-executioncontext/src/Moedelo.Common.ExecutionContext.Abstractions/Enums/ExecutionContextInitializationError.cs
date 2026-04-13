namespace Moedelo.Common.ExecutionContext.Abstractions.Enums;

public enum ExecutionContextInitializationError
{
    NoError = 0,
    OtherError = 1,
    JwtDecodingFailed = 2,
    OutdatedClaims = 3
}
