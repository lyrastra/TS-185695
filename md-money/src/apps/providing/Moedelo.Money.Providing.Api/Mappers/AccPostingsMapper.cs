using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Providing.Api.Models.AccPostings;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Providing.Api.Mappers
{
    static class AccPostingsMapper
    {
        public static AccPostingsResponseDto MapPostings(AccountingPostingsResponse response)
        {
            return new AccPostingsResponseDto
            {
                LinkedDocuments = response.LinkedDocuments?.Select(Map).ToArray() ?? Array.Empty<LinkedDocumentAccPostingsDto>(),
                Postings = response.Postings?.Select(Map).ToArray() ?? Array.Empty<AccPostingDto>()
            };
        }

        public static AccPostingsResponseDto EmptyPostings()
        {
            return new AccPostingsResponseDto
            {
                LinkedDocuments = Array.Empty<LinkedDocumentAccPostingsDto>(),
                Postings = Array.Empty<AccPostingDto>()
            };
        }

        private static LinkedDocumentAccPostingsDto Map(LinkedDocumentAccountingPostings linkedDocument)
        {
            return new LinkedDocumentAccPostingsDto
            {
                Date = linkedDocument.Date,
                Name = GetDocumentName(linkedDocument),
                Number = linkedDocument.Number,
                Type = linkedDocument.Type,
                Postings = linkedDocument.Postings?.Select(Map).ToArray() ?? Array.Empty<AccPostingDto>()
            };
        }

        private static AccPostingDto Map(AccountingPosting posting)
        {
            return new AccPostingDto
            {
                Date = posting.Date,
                Sum = posting.Sum,
                DebitCode = posting.DebitCode,
                DebitSubconto = Map(posting.DebitSubcontos),
                CreditCode = posting.CreditCode,
                CreditSubconto = Map(posting.CreditSubcontos),
                Description = posting.Description,
            };
        }

        private static SubcontoDto[] Map(IReadOnlyCollection<Subconto> subcontos)
        {
            return subcontos.Select(x =>
                new SubcontoDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type
                }).ToArray();
        }

        private static string GetDocumentName(LinkedDocumentAccountingPostings linkedDocument)
        {
            return $"{linkedDocument.Type.GetDescription()} №{linkedDocument.Number} от {linkedDocument.Date:dd.MM.yyyy}";
        }
    }
}

