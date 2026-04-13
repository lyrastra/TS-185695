using System;
using System.Collections.Generic;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Statements.Dto
{
    public class PurchasesStatementResponseDto
    {
        /// <summary>
        /// Id документа
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
        /// Контрагент
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Тип начисления НДС 1 - Нет, 2 - Сверху, 3 - В том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Компенсируется ли документ заказчиком
        /// </summary>
        public bool IsCompensated { get; set; }

        /// <summary>
        /// Заказчик
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Посреднический договор
        /// </summary>
        public long? CustomerProjectId { get; set; }

        /// <summary>
        /// Позиции акта
        /// </summary>
        public List<PurchasesStatementItemResponseDto> Items { get; set; }

        /// <summary>
        /// Счет-фактура
        /// </summary>
        public LinkedInvoiceRepresentationDto Invoice { get; set; }

        /// <summary>
        /// Системная информация
        /// </summary>
        public DocumentContextDto Context { get; set; }
    }
}