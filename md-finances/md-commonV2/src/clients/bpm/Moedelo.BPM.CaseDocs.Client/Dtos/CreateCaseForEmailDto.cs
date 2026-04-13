using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    public class CreateCaseForEmailDto
    {
        public string Subject { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public CaseSource Source { get; set; }

        public string EmailAddress { get; set; }

        public string ContactName { get; set; }

        public bool IsLost { get; set; }
    }
}
