using System;
using System.Collections.Generic;
using PaymentDirection = Moedelo.Common.Enums.Enums.Integration.PaymentDirection;

namespace Moedelo.BankIntegrationsV2.Dto.Mobile
{
    public class CachePaymentDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Идентификатор фирмы(FK in DB) </summary>
        public int FirmId { get; set; }

        /// <summary> Номер расчетного счета </summary>
        public string SettlementAccount { get; set; }

        /// <summary> Направление платежа </summary>
        public PaymentDirection PaymentDirection { get; set; }

        /// <summary> Дата операции </summary>
        public DateTime OrderDate { get; set; }

        /// <summary> Номер документа </summary>
        public string DocumentNumber { get; set; }

        /// <summary> Сумма </summary>
        public decimal Summa { get; set; }

        /// <summary> Имя контрагента </summary>
        public string Counterparty { get; set; }

        /// <summary> Назначение платежа </summary>
        public string PaymentPurpose { get; set; }

        /// <summary> Импортирован или нет </summary>
        public bool IsLoaded { get; set; }

        /// <summary> Был ли пропущен или нет </summary>
        public bool IsSkipped { get; set; }

        /// <summary> Хэш </summary>
        public int HashCode { get; set; }
    }

    public class CachePaymentListDto
    {
        public List<CachePaymentDto> CachePaymentList;

        public CachePaymentListDto()
        {
            CachePaymentList = new List<CachePaymentDto>();
        }
    }
}