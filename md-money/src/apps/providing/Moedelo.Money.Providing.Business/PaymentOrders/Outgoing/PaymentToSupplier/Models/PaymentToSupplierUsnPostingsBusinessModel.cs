using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Estate.Models;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Money.Providing.Business.Stock.Models;
using Moedelo.Requisites.Enums.TaxationSystems;
using System;
using System.Collections.Generic;
using Moedelo.Money.Providing.Business.NdsRatePeriods;
using Moedelo.Money.Providing.Business.NdsRatePeriods.Models;

namespace Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models
{
    class PaymentToSupplierUsnPostingsBusinessModel
    {
        public TaxationSystemType TaxationSystem { get; set; }

        public IReadOnlyList<NdsRatePeriod> NdsRatePeriods { get; set; } = Array.Empty<NdsRatePeriod>();

        public bool IsUsnProfitAndOutgo { get; set; }

        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public Kontragent Kontragent { get; set; }

        public bool IncludeNds { get; set; }

        public decimal? NdsSum { get; set; }

        public IReadOnlyCollection<BaseDocument> BaseDocuments { get; set; }

        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; } = Array.Empty<DocumentLink>();

        public IReadOnlyCollection<PurchasesStatement> Statements { get; set; } = Array.Empty<PurchasesStatement>();

        public IReadOnlyCollection<PurchasesWaybill> Waybills { get; set; } = Array.Empty<PurchasesWaybill>();

        public IReadOnlyCollection<PurchasesUpd> Upds { get; set; } = Array.Empty<PurchasesUpd>();

        public IReadOnlyCollection<InventoryCard> InventoryCards { get; set; } = Array.Empty<InventoryCard>();

        public IReadOnlyCollection<Estate.Models.ReceiptStatement> ReceiptStatements { get; set; } = Array.Empty<Estate.Models.ReceiptStatement>();

        public IReadOnlyCollection<StockProduct> StockProducts { get; set; } = Array.Empty<StockProduct>();

        public Dictionary<long, InventoryCard> InventoryCardsFromFixedAssetInvestment { get; set; } = new Dictionary<long, InventoryCard>();
    }
}
