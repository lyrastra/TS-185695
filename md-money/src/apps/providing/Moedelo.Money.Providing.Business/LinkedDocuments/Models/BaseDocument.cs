using Moedelo.LinkedDocuments.Enums;
using System;

namespace Moedelo.Money.Providing.Business.LinkedDocuments.Models
{
    public class BaseDocument
    {
        public long Id { get; set; }

        public LinkedDocumentType Type { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        /// <summary>
        /// Статус НУ
        /// </summary>
        public TaxPostingStatus? TaxStatus { get; set; }
    }
}
