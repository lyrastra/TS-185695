namespace Moedelo.CommonApiV2.Dto.FirmFlag;

public class GetEnabledByFirmsDto
{
    /// <summary>
    /// Список фирм
    /// </summary>
    public int[] FirmIds { get; set; }

    /// <summary>
    /// Название флага
    /// </summary>
    public string Name { get; set; }
}