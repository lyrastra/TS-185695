using System;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

/// <summary>
/// Интеграционный запрос
/// ВНИМАНИЕ: не содержит полей RequestXml и ResponseXml
/// </summary>
public class IntegrationRequestDto
{
    /// <summary> Индентификатор объекта </summary>
    public int Id { get; set; }
    /// <summary> Статус запроса </summary>
    public RequestStatus RequestStatus { get; set; }
    /// <summary> Индификатор фирмы </summary>
    public int FirmId { get; set; }
    /// <summary> Какому банку отправили запрос </summary>
    public IntegrationPartners IntegrationPartner { get; set; }
    /// <summary> Дата начала периода </summary>
    public DateTime? StartDate { get; set; }
    /// <summary> Дата окончания периода </summary>
    public DateTime? EndDate { get; set; }
    /// <summary> Расчетный счет </summary>
    public string SettlementNumber { get; set; }
    /// <summary> Запрос выполнен вручную </summary>
    public bool IsManual { get; set; }
    /// <summary> Если банк не смог обработать запрос, пишем сюда вернувшуюся ошибку </summary>
    public string Error { get; set; }
    /// <summary> Фактическая дата запроса </summary>
    public DateTime? DateOfCall { get; set; }
    /// <summary> Тип вызова </summary>
    public IntegrationCallType IntegrationCallType { get; set; }
    /// <summary> Индентификатор запроса (уникальный в контексте <see cref="IntegrationPartner"/>). Часто это Guid в строковом виде </summary>
    public string RequestId { get; set; }
    /// <summary> Id файла с логами в Mongo </summary>
    public string LogFileId { get; set; }
    /// <summary> Файл с логами запроса в S3 </summary>
    public string S3LogFile { get; set; }
}

public class IntegrationRequestWithXmlHistoryDto : IntegrationRequestDto
{
    /// <summary> Xml запроса </summary>
    public string RequestXml { get; set; }
    /// <summary> XML ответа </summary>
    public string ResponseXml { get; set; }
}