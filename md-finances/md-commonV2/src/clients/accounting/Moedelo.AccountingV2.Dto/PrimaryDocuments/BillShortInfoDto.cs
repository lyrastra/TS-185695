using Moedelo.Common.Enums.Enums.Documents;
using System;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class BillShortInfoDto
    {
        public int Id { get; set; }

        public long? DocumentBaseId { get; set; }

        public string Name { get; set; }

        public int KontragentId { get; set; }

        public string KontragentName { get; set; }

        public DateTime? BillDate { get; set; }

        public DocumentStatus Status { get; set; }

        public DateTime? DeadTimeForBill { get; set; }
    }
}
