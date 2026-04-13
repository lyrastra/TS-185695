using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto.Flc
{
    public class FlcResultDto
    {
        public bool HasProblems { get; set; }
        public string ProblemName { get; set; }
        public FlcProblemType? ProblemType { get; set; }
        public List<string> ProblemList { get; set; }
    }
}