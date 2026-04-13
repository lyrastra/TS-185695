using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Docs.Enums;
using Moedelo.Money.Providing.Business.NdsRatePeriods.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Requisites.Enums.NdsRatePeriods;
using Moedelo.Stock.Enums;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier.TaxPostings;

// Note: Внимание! Копия алгоритма лежит в md-bookkeeping (проведение рко) и md-docs частично (проведение УПД). Синхронизовать в случае изменений.
internal static class PurchasesDocTaxSumHelper
{
    public static TaxCoefficients GetTaxCoefficients(
        PurchasesWaybill document,
        IReadOnlyList<NdsRatePeriod> ndsRatePeriods,
        IDictionary<long, StockProductTypeEnum> stockProductsTypes)
    {
        var materials = new ItemSummary();
        var other = new ItemSummary();
        
        foreach (var item in document.Items)
        {
            stockProductsTypes.TryGetValue(item.StockProductId ?? -1, out var productType);
            var summary = productType == StockProductTypeEnum.Material
                ? materials
                : other;

            summary.SumWithNds += item.SumWithNds;
            summary.SumWithoutNds += item.SumWithoutNds;
        }

        var docDate = document.ForgottenDocumentDate ?? document.Date;
        var docSumWithNds = materials.SumWithNds + other.SumWithNds;

        return new TaxCoefficients
        {
            Materials = CalculateCoefficient(materials, ndsRatePeriods, docDate, docSumWithNds),
            Services = 0 // в накладных не может быть услуг
        };
    }

    public static TaxCoefficients GetTaxCoefficients(
        PurchasesUpd document,
        IReadOnlyList<NdsRatePeriod> ndsRatePeriods,
        IDictionary<long, StockProductTypeEnum> stockProductsTypes)
    {
        var materials = new ItemSummary();
        var services = new ItemSummary();
        var other = new ItemSummary();
        
        foreach (var item in document.Items)
        {
            var summary = item.Type switch
            {
                ItemType.Service => services,
                _ => stockProductsTypes.TryGetValue(item.StockProductId ?? -1, out var productType) && productType == StockProductTypeEnum.Material
                    ? materials
                    : other
            };

            summary.SumWithNds += item.SumWithNds;
            summary.SumWithoutNds += item.SumWithoutNds;
        }

        var docDate = document.ForgottenDocumentDate ?? document.Date;
        var docSumWithNds = materials.SumWithNds + services.SumWithNds + other.SumWithNds;

        return new TaxCoefficients
        {
            Services = CalculateCoefficient(services, ndsRatePeriods, document.Date, docSumWithNds),
            Materials = CalculateCoefficient(materials, ndsRatePeriods, docDate, docSumWithNds)
        };
    }

    public static TaxCoefficients GetTaxCoefficients(
        PurchasesStatement document,
        IReadOnlyList<NdsRatePeriod> ndsRatePeriods)
    {
        var services = new ItemSummary();
        
        foreach (var item in document.Items)
        {
            services.SumWithNds += item.SumWithNds;
            services.SumWithoutNds += item.SumWithoutNds;
        }

        var docSumWithNds = services.SumWithNds;
        return new TaxCoefficients
        {
            Services = CalculateCoefficient(services, ndsRatePeriods, document.Date, docSumWithNds),
            Materials = 0 // в актах не может быть материалов
        };
    }

    private static decimal CalculateCoefficient(
        ItemSummary itemsByType,
        IReadOnlyList<NdsRatePeriod> ndsRatePeriods,
        DateTime docDate,
        decimal docSumWithNds)
    {
        if (docSumWithNds == 0)
        {
            return 0;
        }
        
        var date = docDate.Date; // настройку НДС в УП определяем по документу (не нужно учитывать дату платежа) 
        var needExcludeNds = ndsRatePeriods
            ?.FirstOrDefault(x => x.StartDate <= date && date <= x.EndDate)
            ?.Rate is NdsRateType.Nds20 or NdsRateType.Nds22;

        var itemsSum = needExcludeNds && itemsByType.SumWithoutNds < itemsByType.SumWithNds
            ? itemsByType.SumWithoutNds
            : itemsByType.SumWithNds;

        return itemsSum / docSumWithNds; // округлять только при формировании проводки!
    }

    private class ItemSummary
    {
        public decimal SumWithNds { get; set; }
        public decimal SumWithoutNds { get; set; }
    }

    // коэффициент_очистки_от_НДС = сумма_без_НДС / сумма_с_НДС (если его нужно вычленять, иначе = 1)
    internal struct TaxCoefficients
    {
        /// <summary>
        /// Вес УСЛУГ в общей сумме документа * коэффициент_очистки_от_НДС 
        /// </summary>
        public decimal Services { get; set; }
        /// <summary>
        /// Вес МАТЕРИАЛОВ в общей сумме документа * коэффициент_очистки_от_НДС
        /// </summary>
        public decimal Materials { get; set; }
    }
}