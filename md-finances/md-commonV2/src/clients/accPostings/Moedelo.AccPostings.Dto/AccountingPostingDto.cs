using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.AccPostings.Dto
{
    public class AccountingPostingDto
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        /// <summary>
        /// Код счета по дебету
        /// </summary>
        public SyntheticAccountCode DebitCode { get; set; }

        /// <summary>
        /// Код счета по кредиту
        /// </summary>
        public SyntheticAccountCode CreditCode { get; set; }

        /// <summary>
        /// Список дебетовых субконто
        /// </summary>
        public List<long> DebitSubcontos { get; set; }

        /// <summary>
        /// Список кредитовых субконто
        /// </summary>
        public List<long> CreditSubcontos { get; set; }

        /// <summary>
        /// Описание проводки
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// BaseId документа, по которому создана проводка
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Для поддержки старой схемы (из AccountingOperation)
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// "создана и заполнена вручную"
        /// </summary>
        public bool IsManual { get; set; }
    }
}