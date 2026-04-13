using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using NormalizedCostType = Moedelo.TaxPostings.Enums.NormalizedCostType;
using OsnoTransferKind = Moedelo.TaxPostings.Enums.OsnoTransferKind;
using OsnoTransferType = Moedelo.TaxPostings.Enums.OsnoTransferType;

namespace Moedelo.Money.Business.PaymentOrders.Import;

internal static class ImportCustomTaxPostingMapper
{
    internal static TaxPostingsData MapToSaveRequest(this ImportCustomTaxPosting posting)
    {
        if (posting == null)
        {
            return new TaxPostingsData();
        }

        return new TaxPostingsData
        {
            ProvidePostingType = ProvidePostingType.ByHand,
            // бизнес-слой выберет какую из проводок сохранять
            OsnoTaxPostings = new[] { MapOsno(posting) },
            UsnTaxPostings = new [] { MapUsnTaxPosting(posting) },
            PatentTaxPostings = new []{ MapPsnTaxPosting(posting) },
            IpOsnoTaxPosting = MapIpOsno(posting)
        };
    }

    private static IpOsnoTaxPosting MapIpOsno(ImportCustomTaxPosting posting)
    {
        return new IpOsnoTaxPosting 
        { 
            Sum = posting.Sum,
            Direction = posting.Direction
        };
    }

    private static PatentTaxPosting MapPsnTaxPosting(ImportCustomTaxPosting posting)
    {
        return new PatentTaxPosting
        {
            Sum = posting.Sum,
            Description = posting.Description
        };
    }

    private static UsnTaxPosting MapUsnTaxPosting(ImportCustomTaxPosting posting)
    {
        return new UsnTaxPosting
        {
            Sum = posting.Sum,
            Description = posting.Description,
            Direction = posting.Direction
        };
    }

    private static OsnoTaxPosting MapOsno(ImportCustomTaxPosting posting)
    {
        return new OsnoTaxPosting
        {
            Sum = posting.Sum,
            Direction = posting.Direction,
            Type = posting.Type.HasValue 
                ? (OsnoTransferType)posting.Type.Value 
                : OsnoTransferType.Unknown,
            Kind = posting.Kind.HasValue 
                ? (OsnoTransferKind)posting.Kind.Value 
                : OsnoTransferKind.None,
            NormalizedCostType = posting.NormalizedCostType.HasValue
                ? (NormalizedCostType)posting.NormalizedCostType.Value
                : NormalizedCostType.None
        };
    }
}