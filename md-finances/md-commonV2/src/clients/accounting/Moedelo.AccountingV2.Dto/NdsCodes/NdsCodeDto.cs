using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.NdsCodes
{
    public class NdsCodeDto
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public PrimaryDocumentsTransferDirection Direction { get; set; }
    }
}
