using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.ContractsV2.Client;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.Postings.Client.LinkedDocument;
using Moedelo.Postings.Client.LinkOfDocuments;
using Moedelo.Postings.Dto;

namespace Moedelo.Finances.Business.Services.Money.Operations
{
    [InjectAsSingleton]
    public class MoneyOperationLinkedDocumentsReader : IMoneyOperationLinkedDocumentsReader
    {
        private static readonly ISet<LinkType> LinkedTypes = new HashSet<LinkType>
        {
            LinkType.Reason,
            LinkType.Bill,
            LinkType.Contract,
            LinkType.ByContract,
            LinkType.Invoice,
            LinkType.Move,
            LinkType.MiddlemanContract,
            LinkType.AdvanceStatement,
            LinkType.NdsDeduction
        };

        private static readonly ISet<AccountingDocumentType> LinkedDocumentTypes = new HashSet<AccountingDocumentType>
        {
            AccountingDocumentType.Bill,
            AccountingDocumentType.Statement,
            AccountingDocumentType.Waybill,
            AccountingDocumentType.Invoice,
            AccountingDocumentType.Project,
            AccountingDocumentType.AccountingAdvanceStatement,
            AccountingDocumentType.MiddlemanReport,
            AccountingDocumentType.Upd,
            AccountingDocumentType.SalesUpd,
            AccountingDocumentType.PaymentOrder,
            AccountingDocumentType.IncomingCashOrder,
            AccountingDocumentType.OutcomingCashOrder,
            AccountingDocumentType.InventoryCard,
            AccountingDocumentType.ReceiptStatement,
            AccountingDocumentType.SalesCurrencyInvoice,
            AccountingDocumentType.PurchasesCurrencyInvoice,
            AccountingDocumentType.Ukd,
            AccountingDocumentType.PaymentOrder,
            AccountingDocumentType.OutcomingCashOrder,
            AccountingDocumentType.PaymentReserve
        };

        // Документы, которые учитываются при покрытии суммы платежа

        private readonly ILinkedDocumentClient linkedDocumentClient;
        private readonly ILinkOfDocumentsClient linkOfDocumentsClient;
        private readonly IContractClient contractClient;

        public MoneyOperationLinkedDocumentsReader(
            ILinkedDocumentClient linkedDocumentClient,
            ILinkOfDocumentsClient linkOfDocumentsClient,
            IContractClient contractClient)
        {
            this.linkedDocumentClient = linkedDocumentClient;
            this.linkOfDocumentsClient = linkOfDocumentsClient;
            this.contractClient = contractClient;
        }

        public async Task<List<LinkedDocument>> GetByBaseIdAsync(IUserContext userContext, long baseId)
        {
            var baseIds = new[] { baseId };

            var baseDocuments = await GetMapByBaseIdsAsync(userContext, baseIds).ConfigureAwait(false);
            if (!baseDocuments.ContainsKey(baseId))
            {
                return new List<LinkedDocument>();
            }

            var linkedDocuments = baseDocuments[baseId];

            var linkedContracts = linkedDocuments.Where(x => x.Type == AccountingDocumentType.Project).ToList();
            if (linkedContracts.Count == 0)
            {
                return linkedDocuments;
            }

            // костыль, так как для договоров не реализовано открытие по ид базового документа на UI
            var linkedContractBaseIds = linkedContracts.Select(x => x.Id).Distinct().ToArray();
            var contractsByBaseId = (await contractClient.GetByBaseIdsAsync(userContext.FirmId, userContext.UserId, linkedContractBaseIds).ConfigureAwait(false))
                .ToDictionary(x => x.DocumentBaseId, x => x.Id);
            if (contractsByBaseId.Count == 0)
            {
                return linkedDocuments;
            }

            // подменяем ид базового документа на ид договора
            foreach (var linkedContract in linkedContracts)
            {
                if (contractsByBaseId.TryGetValue(linkedContract.Id, out var contractId))
                {
                    linkedContract.Id = contractId;
                }
            }

            return linkedDocuments;
        }

        public async Task<Dictionary<long, List<LinkedDocument>>> GetMapByBaseIdsAsync(IUserContext userContext, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return new Dictionary<long, List<LinkedDocument>>();
            }

            var requestIds = baseIds.Distinct().ToArray();

            // получаем прямые и косвенные связи по ид базовых документов
            var linksLevel1 = await linkOfDocumentsClient.GetLinksFromAsync(userContext.FirmId, userContext.UserId, requestIds).ConfigureAwait(false);
            if (linksLevel1.Count != 0)
            {
                var LinkedToIdlinksLevel1 = linksLevel1.Select(x => x.LinkedToId).ToList();
                var linksLevel2 = await linkOfDocumentsClient.GetLinksFromAsync(userContext.FirmId, userContext.UserId, LinkedToIdlinksLevel1).ConfigureAwait(false);
                var exclude = LinkedToIdlinksLevel1.Union(linksLevel1.Select(x => x.LinkedFromId)).ToArray();
                // исключаем из linksLevel2 связи которые уже присутствуют в linksLevel1
                linksLevel2 = linksLevel2.Where(p => p.LinkType == LinkType.Invoice && !exclude.Contains(p.LinkedToId)).ToList();
                // из-за обилия связей косвенные связи могут повторяться
                linksLevel2 = linksLevel2.GroupBy(x => x.LinkedToId).Select(group => group.First()).ToList();
                // т.к. ниже идет группировка по id базового документа то для linksLevel2 меняем LinkedFromId на Id базового документа
                var childLinks = linksLevel1.Join(linksLevel2, l1 => l1.LinkedToId, l2 => l2.LinkedFromId, (l1, l2) => new LinkOfDocumentsDto { LinkedFromId = l1.LinkedFromId, LinkedToId = l2.LinkedToId, LinkType = l2.LinkType }).ToList();
                linksLevel1.AddRange(childLinks);
            }

            // группируем: ид базового документа -> список связей
            var linkDict = (linksLevel1)
                .Where(x => LinkedTypes.Contains(x.LinkType))
                .GroupBy(x => x.LinkedFromId)
                .ToDictionary(x => x.Key, x => x.ToArray());

            // получаем все ид связанных базовых документов
            var linkedBaseIds = linkDict.SelectMany(x => x.Value).Select(x => x.LinkedToId).Distinct().ToArray();

            // получаем все базовые документы, кроме основных договоров
            // группируем: ид базового документа -> базовый документ
            var baseDocs = await linkedDocumentClient.GetByIdsAsync(
                userContext.FirmId, 
                userContext.UserId, 
                linkedBaseIds).ConfigureAwait(false);
            var baseDocumentDict = baseDocs
                .Where(x => LinkedDocumentTypes.Contains(x.DocumentType) &&
                            // todo: пофиксить костыль
                            !(x.DocumentType == AccountingDocumentType.Project && x.DocumentNumber == "Основной договор"))
                .ToDictionary(x => x.Id);

            // выбираем связанные базовые документы по связям и маппим в доменный объект
            return linkDict.ToDictionary(x => x.Key, x => Map(x.Value, baseDocumentDict));
        }

        private static List<LinkedDocument> Map(LinkOfDocumentsDto[] links, Dictionary<long, LinkedDocumentDto> baseDocumentDict)
        {
            var result = new List<LinkedDocument>();
            foreach (var link in links)
            {
                if (baseDocumentDict.TryGetValue(link.LinkedToId, out var linkedDocument))
                {
                    result.Add(Map(link, linkedDocument));
                }
            }
            return result;
        }

        private static LinkedDocument Map(LinkOfDocumentsDto link, LinkedDocumentDto linkedDocument)
        {
            return new LinkedDocument
            {
                Id = linkedDocument.Id,
                Type = linkedDocument.DocumentType,
                Number = linkedDocument.DocumentNumber,
                Date = linkedDocument.DocumentDate,
                PayedSum = link.Sum
            };
        }
    }
}