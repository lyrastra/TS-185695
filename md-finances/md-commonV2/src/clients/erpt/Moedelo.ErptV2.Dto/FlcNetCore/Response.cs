using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto.FlcNetCore
{
    public class Response
    {
        public IReadOnlyList<ProblemResult> Problems { get; }

        public Response(IReadOnlyList<ProblemResult> problems)
        {
            Problems = problems;
        }
    }
}