using System.Collections.Generic;

namespace Moedelo.PayrollV2.Dto.Transfer
{
    public class TransferWorkersRequestDto
    {
        public int BizUserId { get; set; }

        public int BizFirmId { get; set; }

        public int AccUserId { get; set; }

        public int AccFirmId { get; set; }

        public IReadOnlyDictionary<int, TransferKontragentDto> BizToAccKontragentMap { get; set; }
    }
}
