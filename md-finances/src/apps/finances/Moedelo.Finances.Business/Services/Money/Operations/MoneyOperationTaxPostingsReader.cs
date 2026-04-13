using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents.Extensions;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.Requisites;
using Moedelo.Common.Enums.Enums.TaxPostings;
using Moedelo.Common.Enums.Extensions.PostingEngine;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.TaxPostings;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.Postings.Client.LinkedDocument;
using Moedelo.Postings.Client.LinkOfDocuments;
using Moedelo.RequisitesV2.Client.FirmTaxationSystem;
using Moedelo.RequisitesV2.Dto.FirmTaxationSystem;
using Moedelo.TaxPostings.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.KudirOsno.Client.TaxPostings;
using Moedelo.KudirOsno.Client.TaxPostings.Dto;
using Moedelo.TaxPostings.Dto;

namespace Moedelo.Finances.Business.Services.Money.Operations
{
    [InjectAsSingleton]
    public class MoneyOperationTaxPostingsReader : IMoneyOperationTaxPostingsReader
    {
        private readonly ITaxPostingsUsnClient taxPostingsUsnClient;
        private readonly ITaxPostingsOsnoClient taxPostingsOsnoClient;
        private readonly ITaxPostingsPsnClient taxPostingsPsnClient;
        private readonly IIpOsnoTaxPostingsClient ipOsnoTaxPostingsClient;
        private readonly ILinkOfDocumentsClient linkOfDocumentsClient;
        private readonly ILinkedDocumentClient linkedDocumentClient;
        private readonly IFirmTaxationSystemClient taxationSystemClient;
        private readonly IOperationDao dao;

        public MoneyOperationTaxPostingsReader(
            ITaxPostingsUsnClient taxPostingsUsnClient,
            ITaxPostingsOsnoClient taxPostingsOsnoClient,
            ITaxPostingsPsnClient taxPostingsPsnClient,
            IIpOsnoTaxPostingsClient ipOsnoTaxPostingsClient,
            ILinkOfDocumentsClient linkOfDocumentsClient,
            ILinkedDocumentClient linkedDocumentClient,
            IFirmTaxationSystemClient taxationSystemClient,
            IOperationDao dao)
        {
            this.taxPostingsUsnClient = taxPostingsUsnClient;
            this.taxPostingsOsnoClient = taxPostingsOsnoClient;
            this.taxPostingsPsnClient = taxPostingsPsnClient;
            this.ipOsnoTaxPostingsClient = ipOsnoTaxPostingsClient;
            this.linkOfDocumentsClient = linkOfDocumentsClient;
            this.linkedDocumentClient = linkedDocumentClient;
            this.taxationSystemClient = taxationSystemClient;
            this.dao = dao;
        }

        public async Task<Dictionary<long, List<TaxSumRec>>> GetSumsByBaseIdsAsync(IUserContext userContext, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return new Dictionary<long, List<TaxSumRec>>();
            }

            var result = new Dictionary<long, List<TaxSumRec>>();

            var taxSumsUsn = await taxPostingsUsnClient.GetDocumentTaxSumsAsync(userContext.FirmId, userContext.UserId, baseIds)
                .ConfigureAwait(false);
            foreach (var taxSum in taxSumsUsn)
            {
                AddTaxSum(result, taxSum.DocumentId, TaxationSystemType.Usn, taxSum.Sum);
            }

            var taxSumsOsno = await taxPostingsOsnoClient.GetDocumentTaxSumsAsync(userContext.FirmId, userContext.UserId, baseIds)
                .ConfigureAwait(false);
            foreach (var taxSum in taxSumsOsno)
            {
                AddTaxSum(result, taxSum.DocumentId, TaxationSystemType.Osno, taxSum.Sum);
            }

            var taxSumsPsn = await taxPostingsPsnClient.GetDocumentTaxSumsAsync(userContext.FirmId, userContext.UserId, baseIds)
                .ConfigureAwait(false);
            foreach (var taxSum in taxSumsPsn)
            {
                AddTaxSum(result, taxSum.DocumentId, TaxationSystemType.Patent, taxSum.Sum);
            }

            var taxSumsIpOsno = await ipOsnoTaxPostingsClient.GetPaymentsTaxSumsAsync(userContext.FirmId, userContext.UserId, baseIds)
                .ConfigureAwait(false);
            foreach (var taxSum in taxSumsIpOsno)
            {
                AddTaxSum(result, taxSum.DocumentBaseId, TaxationSystemType.Osno, taxSum.Sum);
            }

            return result;
        }
        
        public async Task<Dictionary<long, List<TaxSumRec>>> GetSumsBySubBaseIdsAsync(IUserContext userContext, Dictionary<long,List<long>> subBaseIds)
        {
            if (subBaseIds.Count == 0)
            {
                return new Dictionary<long, List<TaxSumRec>>();
            }

            var subTaxSums = await GetSumsByBaseIdsAsync(userContext, GetParentIds(subBaseIds)).ConfigureAwait(false);

            if (subTaxSums.Count == 0)
            {
                return new Dictionary<long, List<TaxSumRec>>();
            }

            var result = new Dictionary<long, List<TaxSumRec>>();
            foreach (var record in subBaseIds)
            {
                var recordTaxSums = GetRecordTaxSums(record, subTaxSums);

                if (recordTaxSums.Count > 0)
                {
                    result.Add(record.Key, recordTaxSums);
                }
            }
            return result;
        }

        private static List<TaxSumRec> GetRecordTaxSums(KeyValuePair<long, List<long>> record, IReadOnlyDictionary<long, List<TaxSumRec>> subTaxSums)
        {
            var recordTaxSums = new List<TaxSumRec>();

            foreach (var id in record.Value)
            {
                if (subTaxSums.TryGetValue(id, out var taxSumRecs))
                {
                    recordTaxSums = recordTaxSums.Concat(taxSumRecs)
                        .GroupBy(x => x.TaxType)
                        .Select(x => new TaxSumRec()
                        {
                            TaxType = x.Key,
                            Sum = x.Sum(z => z.Sum)
                        })
                        .ToList();
                }
            }

            return recordTaxSums;
        }

        private static List<long> GetParentIds(Dictionary<long, List<long>> subBaseIds)
        {
            var parentIds = new List<long>();
            foreach (var ids in subBaseIds)
            {
                parentIds.AddRange(ids.Value);
            }
            return parentIds;
        }

        public async Task<TaxPostingList> GetByBaseIdAsync(IUserContext userContext, long baseId)
        {
            var operation = await dao.GetByBaseIdAsync(userContext.FirmId, baseId).ConfigureAwait(false);
            var taxSystem = operation == null
                ? null
                : await taxationSystemClient.GetByYearAsync(
                    userContext.FirmId, 
                    userContext.UserId, 
                    operation.Date.Year).ConfigureAwait(false);

            var (postings, message) = await GetTaxPostingsAndMessageAsync(userContext, taxSystem, baseId).ConfigureAwait(false);
            var linkedDocuments = await GetLinkedDocumentsTaxPostingsAsync(userContext, operation, taxSystem).ConfigureAwait(false);

            return new TaxPostingList
            {
                IsManual = IsPaymentTypeTaxByHand(operation),
                OperationType = operation?.OperationType ?? OperationType.Default,
                Postings = postings,
                LinkedDocuments = linkedDocuments,
                Message = message
            };
        }

        private async Task<List<TaxPostingLinkedDocument>> GetLinkedDocumentsTaxPostingsAsync(
            IUserContext userContext,
            MoneyOperation operation, 
            TaxationSystemDto taxationSystem)
        {
            if (operation == null)
            {
                return new List<TaxPostingLinkedDocument>();
            }

            var contextData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            var isIpOsno = taxationSystem?.IsOsno == true && !contextData.IsOoo;
            if (isIpOsno && operation.OperationType == OperationType.CashOrderOutgoingIssuanceAccountablePerson)
            {
                var ipOsnoTaxPostings = await ipOsnoTaxPostingsClient.GetByPaymentBaseIdAsync(userContext.FirmId, userContext.UserId, operation.DocumentBaseId)
                    .ConfigureAwait(false);
                return MapIpOsnoTaxPostings(ipOsnoTaxPostings).ToList();
            }
            
            // Вероятно, проводки по связанным документам тянутся стоит ограничить типом "Поступление за товар оплаченный банковской картой" (MemorialWarrantReceiptGoodsPaidCreditCard)
            var accountingStatementLinks = await linkOfDocumentsClient.GetLinksFromAsync(userContext.FirmId, userContext.UserId, operation.DocumentBaseId, LinkType.SystemAccountingStatment)
                 .ConfigureAwait(false);
            var accountingStatementLinksBaseIds = accountingStatementLinks.Select(x => x.LinkedToId).ToList();

            var linkedDocuments = await linkedDocumentClient.GetByIdsAsync(userContext.FirmId, userContext.UserId, accountingStatementLinksBaseIds).ConfigureAwait(false);

            // проводки по актам/накладным только для "Оплата поставщику" (PaymentOrderOutgoingPaymentSuppliersForGoods
            // и CashOrderOutgoingPaymentSuppliersForGoods) на УСН(/ЕНВД) "доходы - расходы" если дата документа больше даты п/п
            if (operation.OperationType == OperationType.PaymentOrderOutgoingPaymentSuppliersForGoods
                || operation.OperationType == OperationType.CashOrderOutgoingPaymentSuppliersForGoods)
            {
                if (taxationSystem != null && taxationSystem.IsUsn && taxationSystem.UsnType == UsnTypes.ProfitAndOutgo)
                {
                    var reasonLinks = await linkOfDocumentsClient.GetLinksFromAsync(userContext.FirmId, userContext.UserId, operation.DocumentBaseId, LinkType.Reason)
                         .ConfigureAwait(false);
                    var reasonDocumentBaseIds = reasonLinks.Select(x => x.LinkedToId).ToList();
                    var reasonDocuments = await linkedDocumentClient.GetByIdsAsync(userContext.FirmId, userContext.UserId, reasonDocumentBaseIds).ConfigureAwait(false);
                    foreach (var reasonDocument in reasonDocuments)
                    {
                        if (operation.Date < reasonDocument.DocumentDate)
                        {
                            linkedDocuments.Add(reasonDocument);
                        }
                    }
                }
            }

            var result = new List<TaxPostingLinkedDocument>();
            foreach (var linkedDocument in linkedDocuments)
            {
                var postings = await GetLinkedTaxPostingsAsync(userContext, linkedDocument.Id, operation.DocumentBaseId).ConfigureAwait(false);
                if (postings.Any())
                {
                    result.Add(new TaxPostingLinkedDocument
                    {
                        DocumentName = $"{linkedDocument.DocumentType.GetDocumentTypeName()} №{linkedDocument.DocumentNumber} от {linkedDocument.DocumentDate:dd.MM.yyyy}",
                        DocumentNumber = linkedDocument.DocumentNumber,
                        DocumentDate = linkedDocument.DocumentDate,
                        Type = linkedDocument.DocumentType,
                        Postings = postings
                    });
                }
            }
            return result;
        }

        private  async Task<List<TaxPosting>> GetLinkedTaxPostingsAsync(IUserContext userContext, long linkedDocumentId, long documentId)
        {
            // желательно доработать метод подобно GetTaxPostingsAndMessageAsync
            var result = new List<TaxPosting>();

            var usnPostings = await taxPostingsUsnClient.GetByDocumentIdsAsync(userContext.FirmId, userContext.UserId,
                new[] { linkedDocumentId }).ConfigureAwait(false);
            var usnPostingsByDocument = usnPostings
                .Where(posting => posting.RelatedDocumentBaseIds.Any(id => id == documentId)).ToList();
            result.AddRange(MapUsnTaxPostings(usnPostingsByDocument));

            var osnoPostings = await taxPostingsOsnoClient.GetByDocumentIdsAsync(userContext.FirmId, userContext.UserId,
                new[] { linkedDocumentId }).ConfigureAwait(false);
            result.AddRange(MapOsnoTaxPostings(osnoPostings));

            var psnPostings = await taxPostingsPsnClient.GetByBaseIdAsync(userContext.FirmId, userContext.UserId,
                linkedDocumentId).ConfigureAwait(false);
            result.AddRange(MapPsnTaxPostings(psnPostings));

            return result;
        }

        private async Task<(List<TaxPosting> result, string message)> GetTaxPostingsAndMessageAsync(
            IUserContext userContext,
            TaxationSystemDto taxSystem, 
            long operationBaseId)
        {
            var result = new List<TaxPosting>();
            string message = null;
            
            var contextData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);

            if (taxSystem?.IsUsn == true)
            {
                var usnPostings = await taxPostingsUsnClient.GetByDocumentIdsAsync(userContext.FirmId, userContext.UserId,
                    new[] { operationBaseId }).ConfigureAwait(false);
                result.AddRange(MapUsnTaxPostings(usnPostings));
            }

            // ОСНО-проводки для ООО
            if (taxSystem?.IsOsno == true && contextData.IsOoo)
            {
                var osnoPostings = await taxPostingsOsnoClient.GetByDocumentIdsAsync(userContext.FirmId, userContext.UserId,
                    new[] { operationBaseId }).ConfigureAwait(false);
                result.AddRange(MapOsnoTaxPostings(osnoPostings));
            }

            // ОСНО-проводки для ИП
            if (taxSystem?.IsOsno == true && !contextData.IsOoo)
            {
                var ipOsnoPostings = await ipOsnoTaxPostingsClient.GetByPaymentBaseIdAsync(userContext.FirmId, userContext.UserId, operationBaseId)
                    .ConfigureAwait(false);
                result.AddRange(MapIpOsnoPostings(ipOsnoPostings));
                message = ipOsnoPostings.Message;
            }

            // патент может быть только у ИП
            if (!contextData.IsOoo)
            {
                var psnPostings = await taxPostingsPsnClient.GetByBaseIdAsync(userContext.FirmId, userContext.UserId, operationBaseId)
                    .ConfigureAwait(false);
                result.AddRange(MapPsnTaxPostings(psnPostings));
            }

            return (result, message);
        }

        private static void AddTaxSum(IDictionary<long, List<TaxSumRec>> result, long baseId, TaxationSystemType taxSystemType, decimal sum)
        {
            if (!result.ContainsKey(baseId))
            {
                result.Add(baseId, new List<TaxSumRec>());
            }
            result[baseId].Add(new TaxSumRec { TaxType = taxSystemType, Sum = sum });
        }

        private static bool IsPaymentTypeTaxByHand(MoneyOperation operation)
        {
            if (operation == null)
            {
                return false;
            }
            return operation.OperationType.IsPaymentTypeTaxbleByHand();
            // проверить тип и узнать, что значит ChildDocuments
            //|| (operation.OperationType == OperationType.PaymentOrderOutgoingIssuanceAccountablePerson && !ChildDocuments.Any());
        }

        private static List<TaxPosting> MapUsnTaxPostings(IReadOnlyCollection<TaxPostingUsnDto> taxPostings)
        {
            return taxPostings.Select(x => new TaxPosting
            {
                Date = x.PostingDate,
                Sum = x.Sum,
                Direction = x.Direction,
                Description = x.Destination
            }).ToList();
        }

        private static List<TaxPosting> MapOsnoTaxPostings(IReadOnlyCollection<TaxPostingOsnoDto> taxPostings)
        {
            return taxPostings.Select(x => new TaxPosting
            {
                Date = x.Date,
                Sum = x.Sum,
                Direction = x.Direction,
                Type = x.Type,
                Kind = x.Kind,
                NormalizedCostType = x.NormalizedCostType
            }).ToList();
        }

        private IEnumerable<TaxPostingLinkedDocument> MapIpOsnoTaxPostings(PaymentTaxPostingsResponseDto ipOsnoTaxPostings)
        {
            return ipOsnoTaxPostings.LinkedDocuments.Select(x => new TaxPostingLinkedDocument
            {
                Type = x.Type,
                DocumentDate = x.Date,
                DocumentName = $"{x.Type.GetDocumentTypeName()} №{x.Number} от {x.Date:dd.MM.yyyy}",
                DocumentNumber = x.Number,
                Postings = x.Postings.Select(p => new TaxPosting
                {
                    Date = p.Date,
                    Sum = p.Sum,
                    Direction = p.Direction
                }).ToList()
            });
        }

        private static List<TaxPosting> MapPsnTaxPostings(IReadOnlyCollection<TaxPostingPsnDto> taxPostings)
        {
            return taxPostings.Select(x => new TaxPosting
            {
                Date = x.PostingDate,
                Sum = x.Sum,
                Direction = TaxPostingsDirection.Incoming,
                Description = x.Destination
            }).ToList();
        }

        private static List<TaxPosting> MapIpOsnoPostings(PaymentTaxPostingsResponseDto postingsResponse)
        {
            return postingsResponse.Postings.Select(x => new TaxPosting
            {
                // Для НУ-проводки ИП ОСНО нет даты
                Sum = x.Sum,
                Direction = x.Direction
            }).ToList();
        }
    }
}
