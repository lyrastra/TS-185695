using Moedelo.Common.Enums.Enums.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Dto.Statements
{
    public class StatementDto
    {
        public int Id { get; set; }

        public long? DocumentBaseId { get; set; }

        public int KontragentId { get; set; }

        /// <summary>
        /// Идентификатор договора
        /// </summary>
        public int? ProjectId { get; set; }

        public NdsPositionType? NdsPositionType { get; set; }

        public string Number { get; set; }

        public DateTime? DocDate { get; set; }

        public IList<StatementItemDto> Items { get; set; }

        public PrimaryDocumentsTransferDirection Direction { get; set; }
    }
}
