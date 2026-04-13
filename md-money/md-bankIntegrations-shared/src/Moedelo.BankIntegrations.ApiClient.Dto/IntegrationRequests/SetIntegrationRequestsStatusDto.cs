using System;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

/// <summary>
/// Заявка на выставление статуса для интеграционных запросов, удовлетворяющих заданным условиям выборки
/// </summary>
public class SetIntegrationRequestsStatusDto
{
    /// <summary>
    /// Значение целевого статуса для запросов (тот, что будет выставлен)
    /// </summary>
    public RequestStatus TargetStatus { get; set; }
    /// <summary>
    /// Значение целевого статуса для подзапросов
    /// Если не указан, статусы подзапросов меняться не будут
    /// </summary>
    public IntegrationRequestPartStatusEnum? TargetPartsStatus { get; set; }
    /// <summary>
    /// Только для запросов фирмы с указанным идентификатором
    /// Если не указан - для запросов любых фирм
    /// </summary>
    public int? FirmId { get; set; }
    /// <summary>
    /// Только для запросов указанного партнёра
    /// Если не указан - для запросов любых партнёров
    /// </summary>
    public IntegrationPartners? Partner { get; set; }
    /// <summary>
    /// Только для запросов со значением DateOfCall не ранее указанного
    /// </summary>
    public string MinDateOfCall { get; set; }
    /// <summary>
    /// Только для запросов со значением DateOfCall не позднее указанного
    /// </summary>
    public string MaxDateOfCall { get; set; }
    /// <summary>
    /// Только для запросов с указанными статусами
    /// Если не указано - для запросов с любыми статусами
    /// </summary>
    public RequestStatus[] SourceStatuses { get; set; } = null;
    /// <summary>
    /// Только для запросов с указанными типами
    /// Если не указано - для запросов с любыми типами
    /// </summary>
    public IntegrationCallType[] Types { get; set; } = null;
}
