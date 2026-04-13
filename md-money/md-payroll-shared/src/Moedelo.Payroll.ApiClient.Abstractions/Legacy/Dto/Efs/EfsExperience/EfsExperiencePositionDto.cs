using System;
using System.Collections.Generic;
using Moedelo.Payroll.Shared.Enums.Efs;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.EfsExperience
{
    public class EfsExperiencePositionDto
    {
        public int WorkerId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public int DivisionId { get; set; }
        
        public string Division { get; set; }
        
        public int PositionTypeId { get; set; }
        
        public string Position { get; set; }
        
        public EfsExperiencePositionType PositionType { get; set; }
    }
}