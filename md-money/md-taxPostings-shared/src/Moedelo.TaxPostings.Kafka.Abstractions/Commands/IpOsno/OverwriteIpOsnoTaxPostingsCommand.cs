using System;
using System.Collections.Generic;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Commands.IpOsno
{
    public class OverwriteIpOsnoTaxPostingsCommand
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер платежа
        /// </summary>
        public string PaymentNumber { get; set; }

        /// <summary>
        /// Описание платежа
        /// </summary>
        public string PaymentDescription { get; set; }

        /// <summary>
        /// Дата проводки
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма проводки
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Направление движения денег (приход/расход)
        /// </summary>
        public TaxPostingDirection Direction { get; set; }
    }
}
