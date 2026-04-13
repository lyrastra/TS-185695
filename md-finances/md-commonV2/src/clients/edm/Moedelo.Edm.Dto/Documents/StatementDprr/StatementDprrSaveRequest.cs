using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Edm.Dto.Documents.StatementDprr
{
    public class StatementDprrSaveRequest
    {
        public int Id { get; set; }

        public long DocumentBaseId { get; set; }

        public List<StatementDprrItemDto> Items { get; set; }

        public List<StatementDprrReasonDto> Reasons { get; set; }

        public int KontragentId { get; set; }

        public NdsPositionType? NdsPositionType { get; set; }

        public DateTime DocumentDate { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string City { get; set; }
    }
}
