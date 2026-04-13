using System;
using Moedelo.Payroll.Enums.SalarySettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.EfsExperience
{
    public class EfsExperienceTerritoryCodeItemDto
    {
        public int WorkerId { get; set; }
        
        public decimal RegionRate { get; set; }
        
        public TerritorialConditionType Code { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}