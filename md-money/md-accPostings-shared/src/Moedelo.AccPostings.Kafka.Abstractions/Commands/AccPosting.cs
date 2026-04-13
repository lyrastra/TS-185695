using Moedelo.AccPostings.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.AccPostings.Kafka.Abstractions.Commands
{
    public class AccPostingV2
    {
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
        public IReadOnlyCollection<Subconto> DebitSubcontos { get; set; }

        /// <summary>
        /// Список кредитовых субконто
        /// </summary>
        public IReadOnlyCollection<Subconto> CreditSubcontos { get; set; }

        /// <summary>
        /// Описание проводки
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// "создана и заполнена вручную"
        /// </summary>
        public bool IsManual { get; set; }
    }
}
