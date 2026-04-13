using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Bills.Dto.Marketplace;

/// <summary>
/// Запрос получения информации для продления продуктовой услуги 
/// </summary>
public class ProlongationRequestDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }
    
    /// <summary>
    /// Код продлеваемого пакета/ПУ
    /// </summary>
    public string ConfigurationCode { get; set; }

    /// <summary>
    /// Промо-код
    /// </summary>
    public string PromoCode { get; set; }
        
    /// <summary>
    /// Явно заданная длительность действия
    /// </summary>
    public int? ExplicitDuration { get; set; }

    /// <summary>
    /// Скидка по бонусам "Пригласи друга"
    /// </summary>
    public int? FriendInviteDiscount { get; set; }

    /// <summary>
    /// Доступно продление месячных пакетов
    /// </summary>
    public bool IsOneMonthPackagesActive { get; set; }
    
    /// <summary>
    /// Источник выставления счета
    /// </summary>
    public BillCreationSource CreationSource { get; set; }
}