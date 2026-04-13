using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Operations
{
    public class ActualizeByImport
    {
        public IReadOnlyCollection<ActualizeByImportRec> Recs { get; set; }
    }
}