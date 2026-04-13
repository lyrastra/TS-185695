using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.RptV2.Dto.UsnDeclaration
{
    public class UsnTaxProfitDataDto
    {
        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Наличие сотрудников
        /// </summary>
        public bool? HasEmployess { get; set; }
        /// <summary>
        /// Доходы
        /// </summary>
        public decimal Profit { get; set; }
        /// <summary>
        /// Ставка
        /// </summary>
        public decimal TaxRate { get; set; }
        /// <summary>
        /// Взносы
        /// </summary>
        public decimal PaymentsForFunds { get; set; }
        /// <summary>
        /// Авансовые платежи
        /// </summary>
        public decimal Prepayments { get; set; }
        /// <summary>
        /// Торговый сбор
        /// </summary>
        public decimal PaymentsForTradingTax { get; set; }
    }
}
