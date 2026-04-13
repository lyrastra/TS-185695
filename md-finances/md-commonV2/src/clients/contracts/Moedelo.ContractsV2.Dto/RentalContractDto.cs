using System;

namespace Moedelo.ContractsV2.Dto
{
    public class RentalContractDto : ContractDto
    {
        public bool IsBuyout { get; set; }

        public decimal BuyoutSumWithoutNds { get; set; }

        public bool IsUseNds { get; set; }
        
        public bool? IsScheduledPayment { get; set; }
        
        /// <summary>
        /// Дата первого платежа, из графика платежей
        /// </summary>
        public DateTime? ScheduleFirstPaymentDate { get; set; }
    }
}
