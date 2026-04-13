using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moedelo.BankIntegrations.ApiClient.Dto.WbBank;

/// <summary>
/// Данные интеграции, сохраняемые в IntegratedUser.IntegrationData.
/// </summary>
public class IntegrationDataDto
{
    /// <summary>
    /// Список счетов клиента.
    /// </summary>
    [JsonProperty("accounts")]
    public List<AccountResponseDto> Accounts { get; set; }

    /// <summary>
    /// Согласие на ПДН (1 - есть, 0 - нет). Значение по умолчанию всегда 1.
    /// </summary>
    [JsonProperty("is_consent_to_pdn")]
    public int IsConsentToPdn { get; set; } = 1;

    /// <summary>
    /// Согласие на рекламу (1 - есть, 0 - нет).
    /// </summary>
    [JsonProperty("is_consent_to_ads")]
    public int IsConsentToAds { get; set; }

    /// <summary>
    /// История продлений тарифов, поступивших от банка.
    /// </summary>
    [JsonProperty("plan_renewals")]
    public List<PlanRenewalInfo> PlanRenewals { get; set; } = new();
}

public class PlanRenewalInfo
{
    /// <summary>
    /// Идентификатор тарифного плана.
    /// </summary>
    [JsonProperty("plan_id")]
    public string PlanId { get; set; }

    /// <summary>
    /// Дата создания события на стороне банка.
    /// </summary>
    [JsonProperty("create_at")]
    public string CreatedAt { get; set; }

    /// <summary>
    /// Дата начала продления.
    /// </summary>
    [JsonProperty("next_start_date")]
    public string NextStartDate { get; set; }

    /// <summary>
    /// Дата окончания продления.
    /// </summary>
    [JsonProperty("next_end_date")]
    public string NextEndDate { get; set; }

    /// <summary>
    /// Время фиксации события в адаптере.
    /// </summary>
    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }
}
