
namespace Moedelo.AccountingV2.Client.AgencyContract.Dto
{
    public class AgencyContractDto
    {
        /// <summary>
        /// Номер документа
        /// </summary>

        public string Number { get; set; }

        /// <summary>
        /// Дата 
        /// </summary>


        public string Date { get; set; }

        /// <summary>
        /// Id кассы
        /// </summary>

        public long CashId { get; set; }

        /// <summary>
        /// Id контрагента
        /// </summary>

        public long? KontragentId { get; set; }

        /// <summary>
        /// Имя контрагента
        /// </summary>

        public string KontragentName { get; set; }

        /// <summary>
        /// Приложение
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>

        public decimal Sum { get; set; }

        /// <summary>
        /// Id договора
        /// </summary>

        public long Contract { get; set; }

        /// <summary>
        /// DocumentBaseId созданного документа
        /// </summary>
        public long? BaseDocumentId { get; set; }

        /// <summary>
        /// id созданного документа
        /// </summary>
        public long Id { get; set; }
    }
}
