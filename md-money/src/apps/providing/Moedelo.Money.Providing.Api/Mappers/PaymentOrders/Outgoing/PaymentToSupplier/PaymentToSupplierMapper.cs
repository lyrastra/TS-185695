using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Events;
using Moedelo.Money.Providing.Business.Abstractions.Enums;

namespace Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Outgoing.PaymentToSupplier
{
    public static class PaymentToSupplierMapper
    {
        public static PaymentToSupplierProvideRequest Map(PaymentToSupplierProvideRequired eventData)
        {
            return new PaymentToSupplierProvideRequest
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                KontragentId = eventData.Contractor?.Id ?? 0,
                ContractBaseId = eventData.ContractBaseId,
                Sum = eventData.Sum,
                IncludeNds = eventData.Nds != null,
                NdsSum = eventData.Nds?.NdsSum,
                IsMainKontragent = eventData.IsMainContractor,
                ProvideInAccounting = eventData.ProvideInAccounting,
                DocumentLinks = MapDocuments(eventData.DocumentLinks),
                InvoiceLinks = MapInvoices(eventData.InvoiceLinks),
                ReserveSum = eventData.ReserveSum,
                IsPaid = eventData.IsPaid,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvidingStateId = eventData.ProvidingStateId,
                EventType = HandleEventType.ProvideRequested
            };
        }

        public static PaymentToSupplierProvideRequest Map(PaymentToSupplierCreated eventData)
        {
            return new PaymentToSupplierProvideRequest
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                KontragentId = eventData.Contractor?.Id ?? 0,
                ContractBaseId = eventData.ContractBaseId,
                Sum = eventData.Sum,
                IncludeNds = eventData.Nds != null,
                NdsSum = eventData.Nds?.NdsSum,
                IsMainKontragent = eventData.IsMainContractor,
                ProvideInAccounting = eventData.ProvideInAccounting,
                DocumentLinks = MapDocuments(eventData.DocumentLinks),
                InvoiceLinks = Array.Empty<InvoiceLink>(),
                ReserveSum = eventData.ReserveSum,
                IsPaid = eventData.IsPaid,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                IsBadOperationState = eventData.OperationState.IsBadOperationState(),
                ProvidingStateId = eventData.ProvidingStateId,
                EventType = HandleEventType.Created
            };
        }

        public static PaymentToSupplierProvideRequest Map(PaymentToSupplierUpdated eventData)
        {
            return new PaymentToSupplierProvideRequest
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                KontragentId = eventData.Contractor?.Id ?? 0,
                ContractBaseId = eventData.ContractBaseId,
                Sum = eventData.Sum,
                IncludeNds = eventData.Nds != null,
                NdsSum = eventData.Nds?.NdsSum,
                IsMainKontragent = eventData.IsMainContractor,
                ProvideInAccounting = eventData.ProvideInAccounting,
                DocumentLinks = MapDocuments(eventData.DocumentLinks),
                InvoiceLinks = MapInvoices(eventData.InvoiceLinks),
                ReserveSum = eventData.ReserveSum,
                IsPaid = eventData.IsPaid,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvidingStateId = eventData.ProvidingStateId,
                IsBadOperationState = eventData.OperationState.IsBadOperationState(),
                EventType = HandleEventType.Updated
            };
        }

        private static DocumentLink[] MapDocuments(IReadOnlyCollection<Kafka.Abstractions.Models.DocumentLink> documents)
        {
            return documents?.Select(x => new DocumentLink
            {
                DocumentBaseId = x.DocumentBaseId,
                LinkSum = x.LinkSum
            }).ToArray() ?? Array.Empty<DocumentLink>();
        }

        private static InvoiceLink[] MapInvoices(
            IReadOnlyCollection<Kafka.Abstractions.Models.InvoiceLink> documents)
        {
            return documents?.Select(x => new InvoiceLink
            {
                InvoiceBaseId = x.DocumentBaseId,
            }).ToArray() ?? Array.Empty<InvoiceLink>();
        }

        public static SetReserveRequest Map(PaymentToSupplierSetReserve eventData)
        {
            return new SetReserveRequest
            {
                DocumentBaseId = eventData.DocumentBaseId,
                ReserveSum = eventData.ReserveSum
            };
        }
    }
}
