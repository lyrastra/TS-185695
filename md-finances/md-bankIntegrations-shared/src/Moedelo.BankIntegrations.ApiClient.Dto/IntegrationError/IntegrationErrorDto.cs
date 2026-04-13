using System;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;

/// <summary> Ошибка интеграции </summary>
public class IntegrationErrorDto
{
    /// <summary> Идентификатор ошибки </summary>
    public int Id { get; set; }

    /// <summary> Идентификатор фирмы </summary>
    public int FirmId { get; set; }

    /// <summary> Интегрированный банк </summary>
    public IntegrationPartners IntegrationPartnerId { get; set; }

    /// <summary> Номер расчетного счета </summary>
    public string SettlementNumber { get; set; }

    /// <summary> Тип ошибки интегрированного банка </summary>
    public IntegrationErrorType ErrorType { get; set; }

    /// <summary> Просмотрен </summary>
    public bool IsReaded { get; set; }

    /// <summary> Дата/время просмотра </summary>
    public DateTime? ReadDate { get; set; }

    /// <summary> Дата/время создания записи </summary>
    public DateTime CreateDate { get; set; }

    /// <summary> Дата/время последнего изменения записи </summary>
    public DateTime? ModifyDate { get; set; }

    /// <summary> Кастомное сообщение или null </summary>
    public string Message { get; set; }
}