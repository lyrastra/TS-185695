using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation
{
    public class BackOfficeRequestDto
    {
        public int FirmId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IntegrationCallType CallType { get; set; }
    }
}