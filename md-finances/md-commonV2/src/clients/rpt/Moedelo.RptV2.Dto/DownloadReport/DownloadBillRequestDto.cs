using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.RptV2.Dto.DownloadReport
{
    public class DownloadBillRequestDto
    {
        public string BillNumber { get; set; }

        public DateTime BillDate { get; set; }

        public int PriceListId { get; set; }

        public decimal Sum { get; set; }

        public string Note { get; set; }

        public string Payer { get; set; }

        public List<PaymentPositionDto> PaymentPositions { get; set; }

        public int RegionalPartnerInfoId { get; set; }
        
        /// <summary>
        /// Услуга оказана с помощью платформы «Главучет 3.0»
        /// (если null, то заказчик не знает об этом признаке - его попробует угадать приёмная сторона)
        /// </summary>
        public bool? IsByGlavUchetPlatform30 { get; set; }

        /// <summary>
        /// реквизиты получателя платежа
        /// </summary>
        public BillRequisitesDto PayeeRequisites { get; set; }

        public DocumentFormat Format { get; set; }
    }
}
