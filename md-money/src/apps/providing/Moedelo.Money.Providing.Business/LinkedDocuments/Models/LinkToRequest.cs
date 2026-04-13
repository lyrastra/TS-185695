using Moedelo.LinkedDocuments.Enums;
using System;

namespace Moedelo.Money.Providing.Business.LinkedDocuments.Models
{
    class LinkToRequest
    {
        /// <summary>
        /// Условно "второй" документ в связи (с ним связываем) 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Сумма связи
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Дата связи
        /// </summary>
        public DateTime Date { get; set; }

        public LinkedDocumentType Type { get; set; }
    }
}
