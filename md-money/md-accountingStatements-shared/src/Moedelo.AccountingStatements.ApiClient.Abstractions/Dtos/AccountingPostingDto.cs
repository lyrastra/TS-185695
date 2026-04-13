using System;
using System.Collections.Generic;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos
{
    public class AccountingPostingDto
    {
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Номер счета по дебету
        /// </summary>
        public int Debit { get; set; }

        /// <summary>
        /// Номер счета по кредиту
        /// </summary>
        public int Credit { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификаторы объектов учета (субконто) по дебету. Для заполнения необходимо пользоваться справочником субконто
        /// </summary>
        public IReadOnlyCollection<long> DebitSubcontoIds { get; set; } = Array.Empty<long>();

        /// <summary>
        /// Идентификаторы объектов учета (субконто) по кредиту. Для заполнения необходимо пользоваться справочником субконто
        /// </summary>
        public IReadOnlyCollection<long> CreditSubcontoIds { get; set; } = Array.Empty<long>();
    }
}
