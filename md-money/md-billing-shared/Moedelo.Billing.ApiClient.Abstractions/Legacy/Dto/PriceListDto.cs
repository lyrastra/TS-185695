namespace Moedelo.Billing.Abstractions.Legacy.Dto;

/// <summary> Прайс-лист </summary>
public class PriceListDto
{
    /// <summary> Идентификатор </summary>
    public int Id { get; set; }

    /// <summary> Идентификатор тарифа </summary>
    public int TariffId { get; set; }

    /// <summary> Наименование </summary>
    public string Name { get; set; }

    /// <summary> Актуальность </summary>
    public bool IsActual { get; set; }

    /// <summary> Количество месяцев </summary>
    public int MonthCount { get; set; }

    /// <summary> Стоимость </summary>
    public decimal Price { get; set; }

    /// <summary> Для биллинга </summary>
    public bool IsForBilling { get; set; }

    /// <summary> Для партнеров </summary>
    public bool IsForPartner { get; set; }

    /// <summary> Для банков-партнеров </summary>
    public bool IsForBankPartner { get; set; }

    /// <summary> Для покупки в личном кабинете </summary>
    public bool IsForBuy { get; set; }

    /// <summary> Для продления в личном кабинете </summary>
    public bool IsForProlongation { get; set; }

    /// <summary> Для фирм на ИП в личном кабинете </summary>
    public bool IsForIp { get; set; }

    /// <summary> Для фирм на ООО в личном кабинете </summary>
    public bool IsForOoo { get; set; }

    /// <summary> Сумма которая полагается партнёру </summary>
    public decimal PricePartner { get; set; }

    /// <summary> Стоимость дополнительных услуг (конс. услуг для Бюро, услуг сопровождения для аутсорсинга) </summary>
    public decimal AdditionalServicePrice { get; set; }
}