using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Stock.Enums
{
    /// <summary>
    /// Типы складов (Оптовый, розничный)
    /// </summary>
    public enum StockType
    {
        /// <summary>
        ///  Оптовый склад
        /// </summary>
        Wholesale = 1,

        /// <summary>
        /// Розничный склад
        /// </summary>
        Retail = 2,
    }
}
