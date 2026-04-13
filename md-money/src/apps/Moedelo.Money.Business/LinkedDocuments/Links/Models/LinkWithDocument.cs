using Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models;
using System;

namespace Moedelo.Money.Business.LinkedDocuments.Links.Models
{
    class LinkWithDocument
    {
        /// <summary>
        /// Условно "второй" документ в связи (с ним связан ) 
        /// </summary>
        public BaseDocument Document { get; set; }

        /// <summary>
        /// Сумма связи
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Дата связи
        /// </summary>
        public DateTime Date { get; set; }
    }
}
