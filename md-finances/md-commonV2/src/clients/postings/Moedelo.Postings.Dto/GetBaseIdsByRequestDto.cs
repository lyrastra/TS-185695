using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Postings.Dto
{
    public class GetBaseIdsByRequestDto
    {
        public int? CreateUserId { get; set; }
        
        public AccountingDocumentType? Type { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
    }
}