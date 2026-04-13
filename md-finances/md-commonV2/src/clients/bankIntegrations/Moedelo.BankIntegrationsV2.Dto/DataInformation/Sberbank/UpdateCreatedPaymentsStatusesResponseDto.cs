using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation.Sberbank
{
    public class UpdateCreatedPaymentsStatusesResponseDto
    {
        public List<SberbankPaymentStatusResponseDto> Statuses { get; set; }

        public int LastPayId { get; set; }
    }
}
