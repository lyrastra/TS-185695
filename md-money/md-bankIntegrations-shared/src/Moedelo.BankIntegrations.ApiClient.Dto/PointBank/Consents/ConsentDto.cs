using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.PointBank.Consents;

public class ConsentDto
{
    /// <summary> Статус разрешения </summary>
    public string Status { get; set; }
    /// <summary> Уникальный идентификатор, предназначенный для идентификации разрешения </summary>
    public string ConsentId { get; set; }
    /// <summary> Дата и время создания статуса ресурса </summary>
    public DateTime? CreationDateTime { get; set; }
    /// <summary> Уникальный код клиента </summary>
    public string CustomerCode { get; set; }
    /// <summary> Указание типов данных доступа </summary>
    public List<string> Permissions { get; set; }
    /// <summary> Дата и время истечения срока действия разрешений. </summary>
    public DateTime? ExpirationDateTime { get; set; }
    /// <summary> Истёк ли срок разрешения или нет </summary>
    public bool IsValid { get; set; }
}