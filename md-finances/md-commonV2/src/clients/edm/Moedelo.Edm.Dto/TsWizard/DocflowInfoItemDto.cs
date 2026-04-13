using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.TsWizard;
using Moedelo.Edm.Backend.Domain.Enums;

namespace Moedelo.Edm.Dto.TsWizard
{
    public class DocflowInfoItemDto
    {
        public int Id { get;  set; }

        public string Title { get;  set; }

        public EdmFolder Folder { get;  set; }

        public DocumentStatus Status { get;  set; }

        public List<DocflowDecisionDto> Decisions { get;  set; }

        public Guid WorkflowGuid { get;  set; }
    }
}
