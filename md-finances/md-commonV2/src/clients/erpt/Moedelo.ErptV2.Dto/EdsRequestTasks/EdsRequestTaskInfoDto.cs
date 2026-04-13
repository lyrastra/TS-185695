using System;
using Moedelo.Common.Enums.Enums.EdsRequestTasks;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.EdsRequestTasks
{
    public class EdsRequestTaskInfoDto
    {
        public int Id { get; set; }
        
        public int FirmId { get; set; }

        public EdsProvider EdsProvider { get; set; }

        public DateTime RequestDate { get; set; }
        
        public EdsRequestTaskType Type { get; set; }

        public string Login { get; set; }
        
        public string OrgName { get; set; }
        
        public string Inn { get; set; }
        
        public DateTime EdsEndDate { get; set; }
        
        public DateTime? ConfirmDate { get; set; }
        
        public string PartnerLogin { get; set; }
        
        public string Comment { get; set; }
    }
}