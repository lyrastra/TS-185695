using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.AccountV2.Dto.UserContext
{
    public class UserPaymentExtendedDto
    {
        public bool? IsBiz { get; set; }
        public int? OutsourcingTariff { get; set; }
        public string Coupone { get; set; }
        public string PromoCode { get; set; }
        public int? PromoCodeId { get; set; }
        public string TariffFullName { get; set; }
        public int Tariff { get; set; }
        public bool? IsPro { get; set; }
        public int PriceListId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool Success { get; set; }
        public decimal Summ { get; set; }
        public string PaymentMethod { get; set; }
        public int FirmId { get; set; }
        public int Id { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsMobileTariff { get; set; }
        public string Note { get; set; }
        public bool ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }
    }

}
