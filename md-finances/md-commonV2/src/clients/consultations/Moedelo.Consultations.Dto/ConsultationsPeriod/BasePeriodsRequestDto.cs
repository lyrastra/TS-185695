using System;

namespace Moedelo.Consultations.Dto.ConsultationsPeriod
{
    public class BasePeriodsRequestDto
    {
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public string UserMessage { get; set; }

        public bool IsOpen { get; set; }
    }
}