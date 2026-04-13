using System;
using Moedelo.Payroll.Enums.Efs;
using Moedelo.Payroll.Enums.SalarySettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Employment
{
    public class EfsBaseWorkerEventDto
    {
        public int WorkerId { get; set; }

        public TerritorialConditionType TerritorialCondition { get; set; }

        public EfsEmploymentChangingType Type { get; set; }

        public string OrderNumber { get; set; }

        public string OrderName { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}