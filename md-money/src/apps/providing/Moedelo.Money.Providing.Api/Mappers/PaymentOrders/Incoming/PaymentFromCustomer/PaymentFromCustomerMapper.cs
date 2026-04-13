using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Events;
using Moedelo.Money.Providing.Business.Abstractions.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;

namespace Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Incoming.PaymentFromCustomer
{
    public class PaymentFromCustomerMapper
    {
        public static PaymentFromCustomerProvideRequest Map(PaymentFromCustomerProvideRequired eventData)
        {
            return new PaymentFromCustomerProvideRequest
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                Sum = eventData.Sum,
                SettlementAccountId = eventData.SettlementAccountId,
                KontragentId = eventData.Contractor?.Id ?? 0,
                KontragentName = eventData.Contractor?.Name ?? string.Empty,
                ContractBaseId = eventData.ContractBaseId,
                IncludeNds = eventData.Nds != null,
                NdsSum = eventData.Nds?.NdsSum,
                MediationNdsSum = eventData.MediationNds?.NdsSum,
                IsMediation = eventData.IsMediation,
                MediationCommissionSum = eventData.MediationCommissionSum,
                BillLinks = MapBills(eventData.BillLinks),
                DocumentLinks = MapDocuments(eventData.DocumentLinks),
                InvoiceLinks = MapInvoices(eventData.InvoiceLinks),
                ReserveSum = eventData.ReserveSum,
                TaxationSystemType = eventData.TaxationSystemType,
                IsMainKontragent = eventData.IsMainContractor,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvidingStateId = eventData.ProvidingStateId,
                EventType = HandleEventType.ProvideRequested
            };
        }

        public static PaymentFromCustomerProvideRequest Map(PaymentFromCustomerCreated eventData)
        {
            return new PaymentFromCustomerProvideRequest
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                Sum = eventData.Sum,
                SettlementAccountId = eventData.SettlementAccountId,
                KontragentId = eventData.Contractor?.Id ?? 0,
                KontragentName = eventData.Contractor?.Name ?? string.Empty,
                ContractBaseId = eventData.ContractBaseId,
                IncludeNds = eventData.Nds != null,
                NdsSum = eventData.Nds?.NdsSum,
                MediationNdsSum = eventData.MediationNds?.NdsSum,
                IsMediation = eventData.IsMediation,
                MediationCommissionSum = eventData.MediationCommissionSum,
                BillLinks = MapBills(eventData.BillLinks),
                DocumentLinks = MapDocuments(eventData.DocumentLinks),
                InvoiceLinks = Array.Empty<InvoiceLink>(),
                ReserveSum = eventData.ReserveSum,
                TaxationSystemType = eventData.TaxationSystemType,
                IsMainKontragent = eventData.IsMainContractor,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                IsBadOperationState = eventData.OperationState.IsBadOperationState(),
                ProvidingStateId = eventData.ProvidingStateId,
                EventType = HandleEventType.Created
            };
        }

        public static PaymentFromCustomerProvideRequest Map(PaymentFromCustomerUpdated eventData)
        {
            return new PaymentFromCustomerProvideRequest
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                Sum = eventData.Sum,
                SettlementAccountId = eventData.SettlementAccountId,
                KontragentId = eventData.Contractor?.Id ?? 0,
                KontragentName = eventData.Contractor?.Name ?? string.Empty,
                ContractBaseId = eventData.ContractBaseId,
                IncludeNds = eventData.Nds != null,
                NdsSum = eventData.Nds?.NdsSum,
                MediationNdsSum = eventData.MediationNds?.NdsSum,
                IsMediation = eventData.IsMediation,
                MediationCommissionSum = eventData.MediationCommissionSum,
                BillLinks = MapBills(eventData.BillLinks),
                DocumentLinks = MapDocuments(eventData.DocumentLinks),
                InvoiceLinks = MapInvoices(eventData.InvoiceLinks),
                ReserveSum = eventData.ReserveSum,
                TaxationSystemType = eventData.TaxationSystemType,
                IsMainKontragent = eventData.IsMainContractor,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvidingStateId = eventData.ProvidingStateId,
                EventType = HandleEventType.Updated,
                IsBadOperationState = eventData.OperationState.IsBadOperationState()
            };
        }

        public static SetReserveRequest Map(PaymentFromCustomerSetReserve eventData)
        {
            return new SetReserveRequest
            {
                DocumentBaseId = eventData.DocumentBaseId,
                ReserveSum = eventData.ReserveSum
            };
        }

        private static BillLink[] MapBills(IReadOnlyCollection<Kafka.Abstractions.Models.BillLink> bills)
        {
            return bills?.Select(x => new BillLink
            {
                BillBaseId = x.BillBaseId,
                LinkSum = x.LinkSum
            }).ToArray() ?? Array.Empty<BillLink>();
        }

        private static DocumentLink[] MapDocuments(
            IReadOnlyCollection<Kafka.Abstractions.Models.DocumentLink> documents)
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
    }
}
