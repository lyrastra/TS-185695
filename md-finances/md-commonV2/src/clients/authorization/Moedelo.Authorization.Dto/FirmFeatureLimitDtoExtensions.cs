namespace Moedelo.Authorization.Dto;

public static class FirmFeatureLimitDtoExtensions
{
    /// <summary>
    /// лимит на данный функционал не установлен
    /// </summary>
    public static bool IsUnlimited(this FirmFeatureLimitDto limit)
    {
        return limit == null;
    }
}