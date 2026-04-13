using System;

namespace Moedelo.Billing.Abstractions.Dto.PurchasedServices;

public struct ValidityDto
{
    /// <summary>
    /// Значение лимита
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// Дата начала действия лимита
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Дата окончания действия лимита (может быть null - бессрочный лимит)
    /// </summary>
    public DateTime? EndDate { get; set; }
}

public class PurchasedServiceDto
{
    public string Name { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ValidityDto[] Validities { get; set; }

    /// <summary>
    /// Флаг возможности продления услуги
    /// </summary>
    public bool CanBeProlongated { get; set; }

    /// <summary>
    /// Код услуги (для пакета имеется, для "старого" тарифа - нет)
    /// </summary>
    public string Code { get; set; }

    public string ProductCode { get; set; }
    
    /// <summary>
    /// код-идентификатор лимита, который был продан в рамках этой позиции
    /// </summary>
    public string FeatureLimitCode { get; set; }
    
    /// <summary>
    /// Идентификатор типа лимита
    /// </summary>
    public int? LimitTypeId { get; set; }

    /// <summary>
    /// Код типа модификатора
    /// </summary>
    public string ModifierTypeCode { get; set; }
}