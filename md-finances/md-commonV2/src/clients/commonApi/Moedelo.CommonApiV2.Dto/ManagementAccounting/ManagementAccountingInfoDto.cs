namespace Moedelo.CommonApiV2.Dto.ManagementAccounting;

public class ManagementAccountingInfoDto
{
    /// <summary>
    /// Ссылка для перехода в кабинет партнера
    /// </summary>
    public string Link { get; set; }
        
    /// <summary>
    /// Признак, достаточно ли прав для пользования системой партнера
    /// </summary>
    public bool CanUseManagementAccounting { get; set; }
}