using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.RepeatHandler
{
    public class RepeatEventDeleteRequestDto
    {
        public IReadOnlyCollection<int> EventIds { get; set; }
    }
}
