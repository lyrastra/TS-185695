using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Dto.Statements
{
    public class SavedStatementDto
    {
        public int Id { get; set; }

        public long? DocumentBaseId { get; set; }
    }
}
