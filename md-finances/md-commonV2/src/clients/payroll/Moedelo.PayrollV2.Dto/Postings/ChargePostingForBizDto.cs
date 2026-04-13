using System;
using Moedelo.Common.Enums.Enums.Payroll;

namespace Moedelo.PayrollV2.Dto.Postings
{
    public class ChargePostingForBizDto
    {
        public int TypeId { get; set; }

        public ChargeTypeCode ParentType { get; set; }

        public int Type { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public long? SettingId { get; set; }

        public long? SpecialScheduleId { get; set; }

        public int? KontragentId { get; set; }
    }
}