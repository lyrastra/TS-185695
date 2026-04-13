using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.StockV2.Dto.Operations;

namespace Moedelo.StockV2.Dto.Stocks
{
    public class RequisitionWaybillWithStockOperationDto
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public string Date { get; set; }

        public int? WorkerId { get; set; }

        public bool IsOtherOutgo { get; set; }

        public bool? IsNonOperatingOutgo { get; set; }

        public long StockId { get; set; }

        public StockOperationDto StockOperation { get; set; }

    }
}
