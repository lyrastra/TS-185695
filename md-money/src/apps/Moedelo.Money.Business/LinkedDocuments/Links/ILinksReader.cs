using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.LinkedDocuments.Links
{
    internal interface ILinksReader
    {
        Task<LinkWithDocument[]> GetLinksWithDocumentsAsync(long documentBaseId);

        Task<Dictionary<long, LinkWithDocument[]>> GetLinksWithDocumentsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}