using System;
using Moedelo.Common.Enums.Enums.TradingObjects;

namespace Moedelo.RequisitesV2.Dto.TradingObjects
{
    public class TradingObjectV2Dto
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? StopDate { get; set; }

        public bool? StopFNSNotified { get; set; }

        public TradingObjectType TradingObjectType { get; set; }

        public string Name { get; set; }

        public TradingObjectAddressDto Address { get; set; }

        public string OKTMO { get; set; }

        public int IFNS { get; set; }

        public string Kpp { get; set; }

        public OwnType OwnType { get; set; }

        public KadastrType? KadastrType { get; set; }

        public string KadastrNumber { get; set; }

        public Decimal Square { get; set; }

        public BenefitType BenefitType { get; set; }

        public bool FNSNotified { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string AuthorizationNumber { get; set; }

        public Decimal Amount { get; set; }

        public int? UserCalendarEventId { get; set; }

        public bool IsSended { get; set; }

        public DateTime? SendDate { get; set; }

        public TradingObjectClass? TradingObjectClass { get; set; }
    }
}