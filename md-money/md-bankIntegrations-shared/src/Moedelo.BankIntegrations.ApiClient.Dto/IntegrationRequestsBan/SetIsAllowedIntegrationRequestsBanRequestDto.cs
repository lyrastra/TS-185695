using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequestsBan;

public class SetIsAllowedIntegrationRequestsBanRequestDto
{
    public IReadOnlyCollection<SetIsAllowedIntegrationRequestsBanRequestUnitDto> Dtos { get; set; }
    
    public bool Value { get; set;}
}