using System;
using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto
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