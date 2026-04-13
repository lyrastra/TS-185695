using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Remains
{
    public class OpeningBalancesRepresentation
    {
        /// <summary>
        /// Дата ввода остатков
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Остатки по субсчетам
        /// </summary>
        public List<AccountOpeningBalancesModel> Accounts { get; set; }

        /// <summary>
        /// Сообщения об ошибках в не импортированных остатках
        /// </summary>
        public List<string> ErrorMessages { get; set; }
    }
}
