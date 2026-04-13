using System.Collections.Generic;
using Moedelo.BankIntegrations.Enums.UserIntegrationInfos;

namespace Moedelo.BankIntegrations.ApiClient.Dto.UserIntegrationInfos;

public class UserIntegrationInfoDto
{
    /// <summary> Подключенные банки-партнёры </summary>
    public List<IntegrationPartnerDto> TurnedOn { get; set; }
    /// <summary> Доступные для подключения банки-партнёры </summary>
    public List<IntegrationPartnerDto> Accessible { get; set; }
    /// <summary> Состояние счетчика доступных интеграций </summary>
    public UserIntegrationState UserIntegrationState { get; set; }
    /// <summary> Существует ограничение по количеству подключений </summary>
    public bool HasLimit { get; set; }
    /// <summary> HasUpsale зависит от HasLimit для клиентов на тарифе "Эконом" и "Мини" без активных интеграций
    /// показывается сообщение на frontend "Вам доступна 2 интеграции. Чтобы подключать больше банков смените тариф." </summary>
    public bool HasUpsale { get; set; }
}