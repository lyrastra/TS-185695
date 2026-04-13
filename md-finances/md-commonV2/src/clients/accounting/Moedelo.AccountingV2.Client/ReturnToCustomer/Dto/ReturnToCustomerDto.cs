
namespace Moedelo.AccountingV2.Client.ReturnToCustomer.Dto
{
    public class ReturnToCustomerDto
    {
        /// <summary>
        /// id созданного документа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// DocumentBaseId созданного документа
        /// </summary>
        public long DocumentBaseId { get; set; }

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
        public int KontragentId { get; set; }

        /// <summary>
        /// Имя контрагента
        /// </summary>
        public string KontragentName { get; set; }

        /// <summary>
        /// Тип контрагента, основной "620200", прочий "760600"
        /// </summary>
        public int KontragentAccountCode { get; set; }

        /// <summary>
        /// Приложение
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Основание
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// DocumentBaseId договора
        /// </summary>
        public long ContractDocumentBaseId { get; set; }

        /// <summary>
        ///  Id договора
        /// </summary>
        public long ContractId { get; set; }
    }
}
