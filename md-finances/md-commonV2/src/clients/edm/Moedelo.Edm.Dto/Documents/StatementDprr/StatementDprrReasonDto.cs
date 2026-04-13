using System;

namespace Moedelo.Edm.Dto.Documents.StatementDprr
{
    public class StatementDprrReasonDto
    {
        public bool IsBill { get; set; }

        public bool IsContract { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DocumentDate { get; set; }
    }
}