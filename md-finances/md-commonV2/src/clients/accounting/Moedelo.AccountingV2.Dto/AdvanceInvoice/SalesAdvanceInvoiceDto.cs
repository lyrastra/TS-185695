using System;
using Moedelo.AccountingV2.Dto.Invoices.Sales;
using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Nds;

namespace Moedelo.AccountingV2.Dto.AdvanceInvoice
{
    public class SalesAdvanceInvoiceDto
    {
        /// <summary>
        /// Id документа (Сквозная нумерация по всем типам документов)
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер документа (уникальный в пределах года)
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Тип начисления НДС 1 - не начислять, 2 - сверху, 3 - в том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Конрагент
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Признак документа "Подписан" (Да, Нет, Скан)
        /// </summary>
        public string OnHands { get; set; }
        
        /// <summary>
        /// Признак "Основной вид деятельности"
        /// </summary>
        public bool IsMainActivity { get; set; }

        /// <summary>
        /// Информация об изменениях документа
        /// </summary>
        public DocumentContextDto Context { get; set; }

        /// <summary>
        /// Идентификатор госконтракта (ИГК)
        /// </summary>
        public string GovernmentContractId { get; set; }

        /// <summary>
        /// Сумма налога(графа 8 авансового СФ)
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Тип НДС(графа 7 авансового СФ)
        /// </summary>
        public NdsTypes NdsType { get; set; }

        /// <summary>
        /// Данные из поля «Наименование» авансового счета-фактуры
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Первичный документ (документ основание)
        /// </summary>
        public SalesInvoiceLinkedDocumentDto ReasonDocument { get; set; }
    }
}