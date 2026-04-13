using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Enums;
using System.Collections.Generic;

namespace Moedelo.Money.Api.Models
{
    public class ChangeTaxationSystemDto
    {
        [RequiredValue]
        public IReadOnlyCollection<long> DocumentBaseIds { get; set; }

        [Values(TaxationSystemType.Usn, TaxationSystemType.Osno, TaxationSystemType.Envd, TaxationSystemType.Patent)]
        public TaxationSystemType TaxationSystemType { get; set; }
    }
}
