using System;

namespace Moedelo.Accounting.Domain.Shared.Config
{
    public static class AccountingConfig
    {
        /// <summary>
        /// Минимально допустимое значение для даты документа в учётке
        /// </summary>
        public static readonly DateTime MinDocDate = new DateTime(2013, 01, 01);
    }
}
