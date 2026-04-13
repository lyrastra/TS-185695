using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Registry.Domain.Models
{
    public class CreateMoneyOperationCommand
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
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public Contractor Contractor { get; set; }

        /// <summary>
        /// Тип: поступление/списание
        /// </summary>
        public MoneyDirection Direction { get; set; }

        /// <summary>
        /// Источник: банк/касса/платежные системы
        /// </summary>
        public Source Source { get; set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// Статус оплаты
        /// </summary>
        public bool IsPaid { get; set; }
	}
}
