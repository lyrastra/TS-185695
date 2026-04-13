using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    public class CreateCaseUpdateForEmailDto
    {
        public string Subject { get; set; }

        public string Description { get; set; }

        public string ContactName { get; set; }

        public string ContactAddres { get; set; }

        public bool DocsComplete { get; set; }
    }
}
