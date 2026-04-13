namespace Moedelo.InfrastructureV2.RedisDataAccess.Models
{
    public enum RedisDbEnum
    {
        DefaultDb = 0,
        TokenDb = 1,
        
        /// <summary>
        /// Partnership токены авторизации (использует ту же БД что и TokenDb)
        /// </summary>
        PartnershipTokenDb = 1,
        
        SilentLoginDb = 2,
        AuthCodeDb = 3,
        RefreshTokenDb = 4,
        ChatPlatformDb = 5,
        PaymentOrderImportDb = 6,

        AuditDb = 15,
    }
}