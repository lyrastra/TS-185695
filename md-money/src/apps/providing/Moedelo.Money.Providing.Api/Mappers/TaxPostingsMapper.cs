using Moedelo.AccPostings.Enums;
using Moedelo.Money.Providing.Api.Models.TaxPostings;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.Money.Providing.Business.TaxPostings.Models;
using Moedelo.TaxPostings.Enums;
using System;
using System.Linq;

namespace Moedelo.Money.Providing.Api.Mappers
{
    static class TaxPostingsMapper
    {
        internal static TaxPostingsResponseDto MapPostings(ITaxPostingsResponse<ITaxPosting> response)
        {
            return new TaxPostingsResponseDto
            {
                LinkedDocuments = response.LinkedDocuments.Select(Map).ToArray(),
                Postings = response.Postings.Select(Map).ToArray(),
                TaxationSystemType = response.TaxationSystemType,
                TaxStatus = response.TaxStatus,
                ExplainingMessage = MapExplainingMessage(response)
            };
        }

        private static LinkedDocumentTaxPostingsDto<ITaxPostingDto> Map(ILinkedDocumentTaxPostings<ITaxPosting> linkedDocument)
        {
            return new LinkedDocumentTaxPostingsDto<ITaxPostingDto>
            {
                Date = linkedDocument.Date,
                Name = GetDocumentName(linkedDocument),
                Number = linkedDocument.Number,
                Type = linkedDocument.Type,
                Postings = linkedDocument.Postings.Select(Map).ToArray()
            };
        }

        private static ITaxPostingDto Map(ITaxPosting taxPosting)
        {
            return taxPosting switch
            {
                UsnTaxPosting usnTaxPosting => MapUsnPosting(usnTaxPosting),
                OsnoTaxPosting osnoTaxPosting => MapOsnoPosting(osnoTaxPosting),
                PatentTaxPosting psnTaxPosting => MapPatentPosting(psnTaxPosting),
                IpOsnoTaxPosting osnoTaxPosting => MapIpOsnoPosting(osnoTaxPosting),
                _ => throw new InvalidOperationException($"unknown posting type {taxPosting}"),
            };
        }

        private static UsnTaxPostingDto MapUsnPosting(UsnTaxPosting posting)
        {
            return new UsnTaxPostingDto
            {
                Direction = posting.Direction,
                Date = posting.Date,
                Sum = posting.Sum,
                Description = posting.Description,
            };
        }

        private static OsnoTaxPostingDto MapOsnoPosting(OsnoTaxPosting posting)
        {
            return new OsnoTaxPostingDto
            {
                Direction = posting.Direction,
                Date = posting.Date,
                Sum = posting.Sum,
                Kind = posting.Kind,
                Type = posting.Type,
                NormalizedCostType = posting.NormalizedCostType
            };
        }

        private static IpOsnoTaxPostingDto MapIpOsnoPosting(IpOsnoTaxPosting posting)
        {
            return new IpOsnoTaxPostingDto
            {
                Direction = posting.Direction,
                Date = posting.Date,
                Sum = posting.Sum
            };
        }
        
        private static PatentTaxPostingDto MapPatentPosting(PatentTaxPosting posting)
        {
            return new PatentTaxPostingDto
            {
                Direction = posting.Direction,
                Date = posting.Date,
                Sum = posting.Sum,
                Description = posting.Description,
            };
        }

        private static string MapExplainingMessage(ITaxPostingsResponse<ITaxPosting> response)
        {
            if (string.IsNullOrEmpty(response.Message) &&
                response.TaxStatus == TaxPostingStatus.NotTax)
            {
                return "Не учитывается при расчёте налога.";
            }
            return response.Message ?? string.Empty;
        }

        private static string GetDocumentName(ILinkedDocumentTaxPostings<ITaxPosting> linkedDocument)
        {
            return $"{linkedDocument.Type.GetDescription()} №{linkedDocument.Number} от {linkedDocument.Date:dd.MM.yyyy}";
        }
    }
}
