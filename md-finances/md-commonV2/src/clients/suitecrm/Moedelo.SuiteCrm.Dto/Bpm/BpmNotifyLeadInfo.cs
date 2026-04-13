using System.Collections.Generic;

namespace Moedelo.SuiteCrm.Dto.Bpm
{
    public class BpmNotifyLeadInfo
    {
        public int LeadId { get; set; }

        public List<string> Emails { get; set; }

        // нулевая активность
        public bool IsZeroActivity { get; set; }

        // включена интеграция по всем счетам
        public bool HasAllIntegrations { get; set; }
    }
}