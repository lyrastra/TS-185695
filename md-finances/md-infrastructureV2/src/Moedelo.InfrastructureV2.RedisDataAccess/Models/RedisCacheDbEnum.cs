namespace Moedelo.InfrastructureV2.RedisDataAccess.Models;

public enum RedisCacheDbEnum
{
    UserCommonContext = 0,

    TariffRole = 1,

    Stock = 2,

    EdsNotifications = 3,

    AuthorizationData = 4,

    ///??? = 5, //кем то используется Виталий Шундров (с)
    NdsRegisters = 6,

    Header = 7,

    PartnershipAuthorization = 11
}