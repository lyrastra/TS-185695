using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Registry.Domain.Models
{
    public class DeleteMoneyOperationCommand
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата операции
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType OperationType { get; set; }
    }
}
