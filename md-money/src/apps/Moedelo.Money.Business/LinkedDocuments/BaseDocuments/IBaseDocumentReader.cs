using Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.LinkedDocuments.BaseDocuments
{
    internal interface IBaseDocumentReader
    {
        Task<BaseDocument[]> GetByIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}