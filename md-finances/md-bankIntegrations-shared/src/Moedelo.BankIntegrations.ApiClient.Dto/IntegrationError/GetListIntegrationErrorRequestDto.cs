using System.Collections.Generic;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;

public class GetListIntegrationErrorRequestDto
{
    /// <summary> Идентификатор фирмы </summary>
    public int FirmId { get; set; }
    
    /// <summary> Интегрированный партнёра </summary>
    public IntegrationPartners Partner { get; set; }

    /// <summary>
    /// Идентификаторы integrationError
    /// </summary>
    public List<int> Ids { get; set; }
}