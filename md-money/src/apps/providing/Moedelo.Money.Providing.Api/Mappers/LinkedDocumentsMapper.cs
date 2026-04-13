using Moedelo.Money.Providing.Api.Models;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Providing.Api.Mappers
{
    public class LinkedDocumentsMapper
    {
        public static IReadOnlyCollection<DocumentLink> Map(IReadOnlyCollection<DocumentLinkDto> documents)
        {
            return documents?.Select(x => new DocumentLink
            {
                DocumentBaseId = x.DocumentBaseId,
                LinkSum = x.Sum
            }).ToArray();
        }
    }
}
