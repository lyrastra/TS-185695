using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.TaxPostings.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using ProvidePostingType = Moedelo.Money.Enums.ProvidePostingType;

namespace Moedelo.Money.Api.Mappers
{
    static class TaxPostingsMapper
    {
        public static TaxPostingsData MapTaxPostings(TaxPostingsSaveDto postings, TaxPostingDirection direction)
        {
            var result = new TaxPostingsData
            {
                ProvidePostingType = postings?.IsManual ?? false
                    ? ProvidePostingType.ByHand
                    : ProvidePostingType.Auto
            };

            if (result.ProvidePostingType == ProvidePostingType.ByHand)
            {
                result.UsnTaxPostings = MapUsnTaxPostings(postings?.Postings, direction);
                result.IpOsnoTaxPosting = MapIpOsnoTaxPosting(postings?.Postings, direction);
                result.OsnoTaxPostings = MapOsnoTaxPostings(postings?.Postings, direction);
                result.PatentTaxPostings = MapPatentTaxPostings(postings?.Postings);
            }
            return result;
        }

        public static TaxPostingsData MapTaxPostings(CustomTaxPostingsSaveDto postings, TaxPostingDirection direction)
        {
            return new TaxPostingsData
            {
                ProvidePostingType = ProvidePostingType.ByHand,
                UsnTaxPostings = MapUsnTaxPostings(postings?.Postings, direction),
                IpOsnoTaxPosting = MapIpOsnoTaxPosting(postings?.Postings, direction),
                OsnoTaxPostings = MapOsnoTaxPostings(postings?.Postings, direction),
                PatentTaxPostings = MapPatentTaxPostings(postings?.Postings)
            };
        }

        public static IReadOnlyCollection<UsnTaxPosting> MapUsnTaxPostings(IReadOnlyCollection<TaxPostingCommonDto> postings, TaxPostingDirection direction)
        {
            return postings?.Select(x =>
                new UsnTaxPosting
                {
                    Direction = direction,
                    Sum = x.Sum,
                    Description = x.Description
                }).ToArray() ?? Array.Empty<UsnTaxPosting>();
        }

        public static IpOsnoTaxPosting MapIpOsnoTaxPosting(IReadOnlyCollection<TaxPostingCommonDto> postings, TaxPostingDirection direction)
        {
            var posting = postings?.FirstOrDefault();
            if (posting == null)
            {
                return null;
            }

            return new IpOsnoTaxPosting
            {
                Direction = direction,
                Sum = posting.Sum
            };
        }

        public static IReadOnlyCollection<OsnoTaxPosting> MapOsnoTaxPostings(IReadOnlyCollection<TaxPostingCommonDto> postings, TaxPostingDirection direction)
        {
            return postings?.Select(x =>
                new OsnoTaxPosting
                {
                    Direction = direction,
                    Sum = x.Sum,
                    Type = x.Type ?? OsnoTransferType.Unknown,
                    Kind = x.Kind ?? (OsnoTransferKind) (-1),
                    NormalizedCostType = x.NormalizedCostType ?? (NormalizedCostType) (-1)
                }).ToArray() ?? Array.Empty<OsnoTaxPosting>();
        }

        public static IReadOnlyCollection<PatentTaxPosting> MapPatentTaxPostings(
            IReadOnlyCollection<TaxPostingCommonDto> postings)
        {
            return postings?.Select(x => new PatentTaxPosting
            {
                Sum = x.Sum,
                Description = x.Description
            }).ToArray() ?? Array.Empty<PatentTaxPosting>();
        }
    }
}
