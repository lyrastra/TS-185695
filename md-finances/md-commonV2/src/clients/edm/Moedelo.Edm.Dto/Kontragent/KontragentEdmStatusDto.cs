using Moedelo.Common.Enums.Enums.Kontragents;

namespace Moedelo.Edm.Dto.Kontragent
{
    public class KontragentEdmStatusDto
    {
        public int KontragentId { get; set; }

        public KontragentEdmInteractionStatus Status { get; set; }

        public string KontragentGuid { get; set; }
    }
}
