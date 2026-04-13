using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class AdvanceAcceptanceReponseDto
    {
        /// <summary> Клиенты, которые подписывались И отписывались от наших платёжных требований в виде хронологического, упорядоченного списка </summary>
        public List<AdvanceAcceptanceDto> ResultList { get; set; }
    }
}
