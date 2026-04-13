using Moedelo.Common.Enums.Enums.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Dto.StockDebitOperation
{
    public class StockOperationDto
    {
        public long Id { get; set; }

        public long? SourceDocumentId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public bool ProvideInAccounting { get; set; }

        public StockOperationTypeEnum TypeCode { get; set; }

        public StockOperationParentTypeEnum TypeParentCode { get; set; }

        public IList<StockOperationOverProductDto> OperationsOverProducts { get; set; } = new List<StockOperationOverProductDto>();
    }
}
