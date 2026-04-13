using Moedelo.Common.Enums.Enums.Integration;
using System;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class AdvanceAcceptanceDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public string SberbankClientId { get; set; }

        /// <summary> Признак активности </summary>
        public bool IsAllowed { get; set; }

        /// <summary> Назначение платежа </summary>
        public string Purpose { get; set; }

        public string Inn { get; set; }

        public string SettlementNumber { get; set; }

        public string BankBik { get; set; }

        /// <summary> Дата начала действия заранее данного акцепта </summary>
        public string StartDate { get; set; }

        /// <summary> Дата окончания действия заранее данного акцепта </summary>
        public string EndDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string PayerName { get; set; }

        /// <summary>
        /// Тип ЗДА который был вадан клиентом: Basic - старый, Lite - новый
        /// </summary>
        public AcceptanceType AcceptanceType { get; set; }
    }
}