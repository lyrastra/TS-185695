using Moedelo.Common.Enums.Enums.OpeningBalance;

namespace Moedelo.AccountingV2.Dto.Remains
{
    public class OpeningBalancesDocumentModel
    {
        /// <summary>
        /// Название документа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Не оплаченная сумма документа
        /// </summary>
        public decimal NotPayedSum { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public OpeningBalanceDocumentType DocumentType { get; set; }
    }
}
