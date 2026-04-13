using System;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class FakeDocumentDto
    {
        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int KontragentId { get; set; }

        public string KontragentName { get; set; }
    }
}