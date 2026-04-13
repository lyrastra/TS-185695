using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.EdsRequestTasks;

namespace Moedelo.ErptV2.Dto.EdsRequestTasks
{
    public class EdsRequestTaskRequestDto
    {
        public List<EdsRequestTaskType> Types { get; set; }
        public string InnFilter { get; set; }
        public string LoginFilter { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public EdsRequestTaskSortColumn SortBy { get; set; }
        public bool AscOrder { get; set; }
        public bool? WithIdentification { get; set; }
        public bool? WithDocuments { get; set; }
        public bool? WithEdsIdentification { get; set; }
    }
}