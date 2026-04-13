namespace Moedelo.CommonV2.Auth.Domain;

// ReSharper disable once ClassNeverInstantiated.Global
public record AuthenticationInfo(int UserId, int FirmId, int? ClientId = null);