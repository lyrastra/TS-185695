using System;

namespace Moedelo.BackofficeV2.Dto.Reports
{
    public class ReportNewActivityParamDto
    {
        public bool ForIp { get; set; }

        public bool ForOoo { get; set; }
		
		// куда отправляется письмо с отчётом
        public string Email { get; set; }

        public Guid QueryGuid { get; set; }
    }
}