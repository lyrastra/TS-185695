using System.Collections.Generic;

namespace Moedelo.Registration.Dto.RegistrationHistory
{
    public class FirmRegistrationHistoryDto
    {
        public int FirmId { get; set; }
        public IReadOnlyCollection<RegistrationHistoryDto> RegistrationHistoryData { get; set; }
    }
}