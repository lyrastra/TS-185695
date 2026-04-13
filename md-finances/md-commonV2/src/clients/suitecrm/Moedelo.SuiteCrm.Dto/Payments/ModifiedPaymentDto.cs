using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.SuiteCrm.Dto.Payments
{
    public class ModifiedPaymentDto
    {
        public int PaymentId { get; set; }
        
        public int FirmId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public decimal? Sum { get; set; }

        public string Tariff { get; set; }

        public bool? IsReselling { get; set; }
    }
}
