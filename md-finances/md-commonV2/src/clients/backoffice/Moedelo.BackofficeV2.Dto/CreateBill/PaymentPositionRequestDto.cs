using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BackofficeV2.Dto.CreateBill
{
    public class PaymentPositionRequestDto
    {
        public PaymentPositionType Type { get; set; }

        public OneTimeServiceType? OneTimeType { get; set; }

        public decimal Price { get; set; }

        public decimal MinPrice { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Заблокировано изменение суммы при выставлении счёта
        /// </summary>
        public bool IsSumLocked { get; set; }

        public bool HasNds { get; set; }
    }
}
