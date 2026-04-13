using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Models;
using LinkedDocumentType = Moedelo.LinkedDocuments.Enums.LinkedDocumentType;

namespace Moedelo.Money.Providing.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(PaymentLinksCreator))]
    class PaymentLinksCreator
    {
        private static readonly LinkedDocumentType[] ReasonDocumentTypes = new[]
        {
            LinkedDocumentType.Waybill,
            LinkedDocumentType.Statement,
            LinkedDocumentType.Upd,
            // TODO: т.к. типы общие для paymentToSupplier и paymentFromCustomer стоит ли разносить?
            LinkedDocumentType.SalesUpd,
            LinkedDocumentType.InventoryCard,
            LinkedDocumentType.ReceiptStatement
        };

        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ILinkOfDocumentsClient linkOfDocumentsClient;
        private readonly IPaymentReserveBaseDocumentsClient reserveDocumentClient;

        public PaymentLinksCreator(
            IExecutionInfoContextAccessor contextAccessor,
            ILinkOfDocumentsClient linkOfDocumentsClient,
            IPaymentReserveBaseDocumentsClient reserveDocumentClient)
        {
            this.contextAccessor = contextAccessor;
            this.linkOfDocumentsClient = linkOfDocumentsClient;
            this.reserveDocumentClient = reserveDocumentClient;
        }

        public Task<PaymentLinksCreationResponse> OverwriteAsync(PaymentLinksCreateRequest request)
        {
            return request.EventType == HandleEventType.ProvideRequested
                ? OverwriteWhenProvideRequestedAsync(request)
                : OverwriteNormalAsync(request);
        }

        private async Task<PaymentLinksCreationResponse> OverwriteNormalAsync(PaymentLinksCreateRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var previousBillBaseIds = GetPreviousBillBaseIds(request);

            await linkOfDocumentsClient.DeleteAllDocumentLinksAsync(
                context.FirmId, context.UserId, request.DocumentBaseId,
                new HttpQuerySetting(timeout: TimeSpan.FromSeconds(30)));

            // fixme: костыль для фикса проблемы, когда документы не существуют на момент фактического проведения
            request.BillLinks = request.CanHaveBills
                ? request.BillLinks.Where(x => request.BaseDocuments.ContainsKey(x.BillBaseId)).ToArray()
                : Array.Empty<BillLink>();
            request.DocumentLinks = request.DocumentLinks.Where(x => request.BaseDocuments.ContainsKey(x.DocumentBaseId)).ToArray();
            request.InvoiceLinks = request.InvoiceLinks.Where(x => request.BaseDocuments.ContainsKey(x.InvoiceBaseId)).ToArray();

            var oneWayLinks = new List<LinkOfDocumentsDto>();

            oneWayLinks.AddRange(request.BillLinks
                .SelectMany(x => GetBillLink(request.DocumentBaseId, request.Date, x, request.BaseDocuments)));

            oneWayLinks.AddRange(request.DocumentLinks.Where(x => ReasonDocumentTypes.Contains(x.Type))
                .SelectMany(x => GetReasonDocumentLinks(request.DocumentBaseId, request.Date, x, request.BaseDocuments)));

            oneWayLinks.AddRange(request.InvoiceLinks
                .SelectMany(x => GetInvoiceLinks(request.DocumentBaseId, request.Date, x)));

            oneWayLinks.AddRange(request.AccountingStatements
                .SelectMany(x => GetAccountingStatementLinks(request.DocumentBaseId, request.Date, x)));
            oneWayLinks.AddRange(request.AccountingStatements
                .SelectMany(x => GetAccountingStatementLinks(x.PrimaryDocBaseId, x.PrimaryDocDate, x)));

            if (request.ContractBaseId > 0)
            {
                oneWayLinks.Add(GetContractLink(request));
            }

            if (request.ReserveSum > 0)
            {
                var reserveDocument = await reserveDocumentClient.GetOrCreateAsync();
                oneWayLinks.Add(GetReserveLink(reserveDocument, request.DocumentBaseId, request.ReserveSum.Value));
            }

            await CreateLinksAsync(context, oneWayLinks);

            return new PaymentLinksCreationResponse
            {
                PreviousBillBaseIds = previousBillBaseIds
            };
        }

        private async Task<PaymentLinksCreationResponse> OverwriteWhenProvideRequestedAsync(PaymentLinksCreateRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;

            // При перепроведении связи не изменяются, поэтому их можно не пересоздавать.
            // Однако пересоздаются бух.справки => связи с ними.
            // При удалении старых бух.справок (выше по стеку) зачищаются и их связи (или по событию "бухсправка удалена" в случае ошибки).
            var oneWayLinks = new List<LinkOfDocumentsDto>();

            oneWayLinks.AddRange(request.AccountingStatements
                .SelectMany(x => GetAccountingStatementLinks(request.DocumentBaseId, request.Date, x)));
            oneWayLinks.AddRange(request.AccountingStatements
                .SelectMany(x => GetAccountingStatementLinks(x.PrimaryDocBaseId, x.PrimaryDocDate, x)));

            await CreateLinksAsync(context, oneWayLinks);

            return new PaymentLinksCreationResponse
            {
                PreviousBillBaseIds = request.BillLinks.Select(x => x.BillBaseId).ToArray()
            };
        }

        private static long[] GetPreviousBillBaseIds(PaymentLinksCreateRequest request)
        {
            if (!request.CanHaveBills)
            {
                return [];
            }

            if (request.EventType == HandleEventType.Created)
            {
                return [];
            }

            return request.ExistentLinks
                .Where(x => x.Document is { Type: LinkedDocumentType.Bill })
                .Select(x => x.Document.Id)
                .Distinct()
                .ToArray();
        }

        private Task CreateLinksAsync(ExecutionInfoContext context, List<LinkOfDocumentsDto> oneWayLinks)
        {
            return oneWayLinks.Count > 0
                ? linkOfDocumentsClient.CreateLinksAsync(context.FirmId, context.UserId, oneWayLinks, new HttpQuerySetting(timeout: TimeSpan.FromSeconds(30)))
                : Task.CompletedTask;
        }

        private static LinkOfDocumentsDto[] GetBillLink(long paymentBaseId, DateTime date, BillLink link, IDictionary<long, BaseDocument> baseDocumentsById)
        {
            baseDocumentsById.TryGetValue(link.BillBaseId, out var baseDocument);
            return new[]
            {
                new LinkOfDocumentsDto
                {
                    Date = baseDocument?.Date ?? date,
                    Sum = link.LinkSum,
                    LinkedFromId = paymentBaseId,
                    LinkedToId = link.BillBaseId,
                    LinkType = LinkType.Bill
                },
                new LinkOfDocumentsDto
                {
                    Date = date,
                    Sum = link.LinkSum,
                    LinkedFromId = link.BillBaseId,
                    LinkedToId = paymentBaseId,
                    LinkType = LinkType.Payment
                }
            };
        }

        private static LinkOfDocumentsDto[] GetReasonDocumentLinks(long paymentBaseId, DateTime date, DocumentLink link, IDictionary<long, BaseDocument> baseDocumentsById)
        {
            baseDocumentsById.TryGetValue(link.DocumentBaseId, out var baseDocument);
            return new[]
            {
                new LinkOfDocumentsDto
                {
                    Date = baseDocument?.Date ?? date,
                    Sum = link.LinkSum,
                    LinkedFromId = paymentBaseId,
                    LinkedToId = link.DocumentBaseId,
                    LinkType = LinkType.Reason
                },
                new LinkOfDocumentsDto
                {
                    Date = date,
                    Sum = link.LinkSum,
                    LinkedFromId = link.DocumentBaseId,
                    LinkedToId = paymentBaseId,
                    LinkType = LinkType.Payment
                }
            };
        }

        private LinkOfDocumentsDto GetReserveLink(
            BaseDocumentDto reserveDocument,
            long paymentBaseId,
            decimal reserveSum)
        {
            // связь должна быть идентична связи с первичным документом (только в 1 сторону)
            return new LinkOfDocumentsDto
            {
                Date = reserveDocument.Date,
                Sum = reserveSum,
                LinkedFromId = paymentBaseId,
                LinkedToId = reserveDocument.Id,
                LinkType = LinkType.Reason
            };
        }

        private static LinkOfDocumentsDto[] GetInvoiceLinks(long paymentBaseId, DateTime date, InvoiceLink link)
        {
            return new[]
            {
                new LinkOfDocumentsDto
                {
                    Date = date,
                    Sum = 0m,
                    LinkedFromId = link.InvoiceBaseId,
                    LinkedToId = paymentBaseId,
                    LinkType = LinkType.Reason
                },
                new LinkOfDocumentsDto
                {
                    Date = date,
                    Sum = 0m,
                    LinkedFromId = paymentBaseId,
                    LinkedToId = link.InvoiceBaseId,
                    LinkType = LinkType.Invoice
                }
            };
        }

        private static LinkOfDocumentsDto[] GetAccountingStatementLinks(long documentBaseId, DateTime date, PaymentForDocumentCreateResponse paymentForDocument)
        {
            return new[]
            {
                new LinkOfDocumentsDto
                {
                    Date = date,
                    Sum = 0,
                    LinkedFromId = documentBaseId,
                    LinkedToId = paymentForDocument.AccountingStatement.DocumentBaseId,
                    LinkType = LinkType.SystemAccountingStatment
                },
                new LinkOfDocumentsDto
                {
                    Date = date,
                    Sum = paymentForDocument.AccountingStatement.Sum,
                    LinkedFromId = paymentForDocument.AccountingStatement.DocumentBaseId,
                    LinkedToId = documentBaseId,
                    LinkType = LinkType.Reason
                }
            };
        }

        private static LinkOfDocumentsDto GetContractLink(PaymentLinksCreateRequest request)
        {
            return new LinkOfDocumentsDto
            {
                Date = request.Date,
                Sum = request.Sum,
                LinkedFromId = request.DocumentBaseId,
                LinkedToId = request.ContractBaseId.Value,
                LinkType = LinkType.ByContract
            };
        }

        public async Task UpdateReserveAsync(SetReserveRequest request)
        {
            await linkOfDocumentsClient.DeleteLinksWithDocOfTypeAsync(
                contextAccessor.ExecutionInfoContext.FirmId,
                contextAccessor.ExecutionInfoContext.UserId,
                new DeleteLinksWithDocOfTypeRequestDto
                {
                    DocumentBaseId = request.DocumentBaseId,
                    LinkedDocumentType = LinkedDocumentType.PaymentReserve
                });

            if (!request.ReserveSum.HasValue || request.ReserveSum <= 0)
            {
                return;
            }

            var reserveDocument = await reserveDocumentClient.GetOrCreateAsync();
            var linkToReserve = GetReserveLink(reserveDocument, request.DocumentBaseId, request.ReserveSum.Value);

            await linkOfDocumentsClient.CreateLinksAsync(
                contextAccessor.ExecutionInfoContext.FirmId,
                contextAccessor.ExecutionInfoContext.UserId,
                new[] { linkToReserve },
                new HttpQuerySetting(timeout: TimeSpan.FromSeconds(30)));
        }
    }
}
