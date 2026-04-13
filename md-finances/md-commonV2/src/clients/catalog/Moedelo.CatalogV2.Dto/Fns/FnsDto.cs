using System;

namespace Moedelo.CatalogV2.Dto.Fns
{
    public class FnsDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public bool IsActive { get; set; }

        [Obsolete("Use FnsRequisitesDto.Inn")]
        public string Inn { get; set; }
        [Obsolete("Use FnsRequisitesDto.Kpp")]
        public string Kpp { get; set; }
        [Obsolete("Use FnsRequisitesDto.Recipient")]
        public string RecipientName { get; set; }
        [Obsolete("Use FnsRequisitesDto.SettlementAccount")]
        public string SettlementAccount { get; set; }
        [Obsolete("Use FnsRequisitesDto.UnifiedSettlementAccount")]
        public string UnifiedSettlementAccount { get; set; }
        [Obsolete("Use FnsRequisitesDto.Bik")]
        public string Bik { get; set; }
        [Obsolete]
        public string Okato { get; set; }
        [Obsolete]
        public string Oktmo { get; set; }
    }
}
