using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.SettlementAccounts;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models
{
    class PaymentToSupplierAccPostingGenerateRequest
    {
        public long PaymentBaseId { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal PaymentSum { get; set; }

        public bool IsMainKontragent { get; set; }

        public IReadOnlyCollection<BaseDocument> BaseDocuments { get; set; }

        public SettlementAccount SettlementAccount { get; set; }

        public Kontragent Kontragent { get; set; }

        public Contract Contract { get; set; }

        public bool IsStockInvisible { get; set; }

        // IsStockInvisible only
        public Subconto CostItemsSubconto { get; set; }

        // IsStockInvisible only
        public Subconto DivisionSubconto { get; set; }
    }
}
