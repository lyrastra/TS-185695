using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Docs.Dto.SalesUpd
{
    public class SalesUpdBaseInfoDto
    {
        public int Id { get; set; }

        public long DocumentBaseId { get; set; }

        public string DocumentNumber { get; set; }

        public decimal DocumentSum { get; set; }

        public DateTime DocumentDate { get; set; }

        public ProvidePostingType TaxPostingType { get; set; }
    }
}
