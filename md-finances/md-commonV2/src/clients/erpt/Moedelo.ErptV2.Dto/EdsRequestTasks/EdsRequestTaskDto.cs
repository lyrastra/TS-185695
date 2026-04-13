using System;
using Moedelo.Common.Enums.Enums.EdsRequestTasks;

namespace Moedelo.ErptV2.Dto.EdsRequestTasks
{
    public class EdsRequestTaskDto
    {
        public int Id { get; set; }
        
        public int FirmId { get; set; }
        
        public DateTime RequestDate { get; set; }
        
        public EdsRequestTaskType Type { get; set; }
        
        public EdsRequestTaskStatus Status { get; set; }
        
        public string Comment { get; set; }
    }
}