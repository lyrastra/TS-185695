using System;
using System.Collections.Generic;

namespace Moedelo.PayrollV2.Dto.Szv
{
    public class SzvWorkerModelDto
    {
        public int WorkerId { get; set; }

        public string Surname { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public string Patronymic { get; set; } = string.Empty;
        
        public string Snils { get; set; } = string.Empty;
        
        public DateTime? TerminationDate { get; set; }
        
        public List<SzvWorkerPeriodDto> Periods { get; set; }
        
        public bool IsValid { get; set; }
        
        public DateTime? DateOfStartWork { get; set; }
        
        public bool IsFounderWithoutContract { get; set; }
    }
}