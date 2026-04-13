using System.Collections.Generic;

namespace Moedelo.Edm.Dto.Documents
{
    public class EdmChangeDocumentsStatusRequest
    {
        public IReadOnlyList<string> WorkflowGuids { get; set; }
        public byte EdmStatus { get; set; }
    }
}