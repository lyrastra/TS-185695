using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.PointBank.Consents;

public class GetConsentsDto
{
    /// <summary> Список разрешений </summary>
    public List<ConsentDto> Consents { get; set; }
}