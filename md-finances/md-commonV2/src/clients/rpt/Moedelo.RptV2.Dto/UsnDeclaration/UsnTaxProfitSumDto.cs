using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.RptV2.Dto.UsnDeclaration
{
    public class UsnTaxProfitSumDto
    {
        /// <summary>
        /// Общая сумма налога	
        /// </summary>
        public decimal TaxSum { get; set; }
        /// <summary>
        /// Уплаченные взносы (больничные)
        /// </summary>
        public decimal FundPaymentsDecreaseSum { get; set; }
        /// <summary>
        /// Сумма налога к уплате
        /// </summary>
        public decimal TaxSumForPayment { get; set; }
        /// <summary>
        /// Торговый сбор
        /// </summary>
        public decimal TradingTaxSum { get; set; }

        public bool HasTradingTax { get; set; }
    }
}
