using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.AuthorizedCapital
{
    /// <summary>
    /// Уставный капитал (УК)
    /// </summary>
    public class AuthorizedCapitalDto
    {
        /// <summary>
        /// Дата УК
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Доли УК
        /// </summary>
        public List<AuthorizedCapitalShareDto> Shares { get; set; }
    }
}