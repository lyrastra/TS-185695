using System;
using System.Collections.Generic;

namespace Moedelo.PaymentImport.Dto
{
    public class AppliedImportRulesRequestDto
    {
        public IReadOnlyCollection<long> DocumentBaseIds = Array.Empty<long>();
    }
}
