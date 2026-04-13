using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.RptV2.Dto.DownloadReport
{
    public class DownloadBillsRequestDto
    {
        public int FirmId{ get; set; }

        public string BillNumber { get; set; }

        public DateTime BillDate { get; set; }

        public int PriceListId { get; set; }

        public decimal Sum { get; set; }

        public string Note { get; set; }

        public string Payer { get; set; }

        public List<PaymentPositionDto> PaymentPositions { get; set; }
        
        public int RegionalPartnerInfoId { get; set; }
        
        /// <summary>
        /// реквизиты получателя платежа
        /// </summary>
        public BillRequisitesDto PayeeRequisites { get; set; }
        
        /// <summary>
        /// Услуга оказана с помощью платформы «Главучет 3.0»
        /// (если null, то заказчик не знает об этом признаке - его попробует угадать приёмная сторона)
        /// </summary>
        public bool? IsByGlavUchetPlatform30 { get; set; }

        /// список форматов, в котором желательно получить формы.
        /// Внимание: в ответе могут присутствовать файлы не всех заявленных форматов
        public DocumentFormat[] Formats { get; set; }

        /// <summary>
        /// Включить генерацию QR-кода
        /// </summary>
        public bool UseQrCode { get; set; }
    }
}