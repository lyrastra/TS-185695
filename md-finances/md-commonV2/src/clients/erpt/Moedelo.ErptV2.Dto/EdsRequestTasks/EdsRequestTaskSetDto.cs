using Moedelo.Common.Enums.Enums.EdsRequestTasks;

namespace Moedelo.ErptV2.Dto.EdsRequestTasks
{
    public class EdsRequestTaskSetDto
    {
        public int Id { get; set; }
        public EdsRequestTaskStatus? Status { get; set; }
        public string Comment { get; set; }
    }
}