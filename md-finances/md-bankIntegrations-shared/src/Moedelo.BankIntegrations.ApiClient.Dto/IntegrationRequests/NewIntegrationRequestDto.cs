using System;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

/// <summary>
/// Заявка на создание нового интеграционного запроса
/// </summary>
public class NewIntegrationRequestDto
{
    /// <summary> Индификатор фирмы </summary>
    public int FirmId { get; set; }
    /// <summary> Интеграционный партнёр, для которого формируется запрос </summary>
    public IntegrationPartners IntegrationPartner { get; set; }
    /// <summary>
    /// Индентификатор запроса (уникальный в контексте <see cref="IntegrationPartner"/>)
    /// может быть равен null
    /// </summary>
    public string RequestId { get; set; }
    /// <summary> Статус запроса </summary>
    public RequestStatus Status { get; set; }
    /// <summary> Расчетный счет </summary>
    public string SettlementNumber { get; set; }
    /// <summary> Тип вызова </summary>
    public IntegrationCallType IntegrationCallType { get; set; }
    /// <summary> Фактическая дата запроса </summary>
    public string DateOfCall { get; set; }
    /// <summary> Запрос выполнен вручную </summary>
    public bool IsManual { get; set; }
    /// <summary> Дата начала периода </summary>
    public string StartDate { get; set; }
    /// <summary> Дата окончания периода </summary>
    public string EndDate { get; set; }
    /// <summary> Xml запроса </summary>
    public string RequestXml { get; set; }
    /// <summary> XML ответа </summary>
    public string ResponseXml { get; set; }
}
