using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class IntegrationRequestsListFilterDto
{
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string MinDateOfCall { get; set; }
    public string MaxDateOfCall { get; set; }
    public IReadOnlyCollection<IntegrationPartners> Partners { get; set; }
    public IReadOnlyCollection<RequestStatus> Statuses { get; set; }
    public IReadOnlyCollection<IntegrationCallType> Types { get; set; }
    [Range(0, int.MaxValue - 1), DefaultValue(0)]
    public int Offset { get; set; }
    [Range(1, 100000), Required]
    public int MaxCount { get; set; }
    public bool? IsManual { get; set; }
}
