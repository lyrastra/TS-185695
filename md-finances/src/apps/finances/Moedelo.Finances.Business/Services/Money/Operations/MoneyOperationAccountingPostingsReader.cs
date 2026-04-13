using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.Client;
using Moedelo.AccPostings.Dto;
using Moedelo.Common.Enums.Enums.Documents.Extensions;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.Common.Enums.Extensions.PostingEngine;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.AccountingPostings;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.Postings.Client.LinkedDocument;
using Moedelo.Postings.Client.LinkOfDocuments;

namespace Moedelo.Finances.Business.Services.Money.Operations
{
    [InjectAsSingleton]
    public class MoneyOperationAccountingPostingsReader : IMoneyOperationAccountingPostingsReader
    {
        private readonly ILinkOfDocumentsClient linkOfDocumentsClient;
        private readonly ILinkedDocumentClient linkedDocumentClient;
        private readonly IAccountingPostingsClient accountingPostingsClient;
        private readonly ISyntheticAccountClient syntheticAccountClient;
        private readonly ISubcontoClient subcontoClient;
        private readonly IOperationDao dao;

        public MoneyOperationAccountingPostingsReader(
            ILinkOfDocumentsClient linkOfDocumentsClient,
            ILinkedDocumentClient linkedDocumentClient,
            IAccountingPostingsClient accountingPostingsClient,
            ISyntheticAccountClient syntheticAccountClient,
            ISubcontoClient subcontoClient,
            IOperationDao dao)
        {
            this.linkOfDocumentsClient = linkOfDocumentsClient;
            this.linkedDocumentClient = linkedDocumentClient;
            this.accountingPostingsClient = accountingPostingsClient;
            this.syntheticAccountClient = syntheticAccountClient;
            this.subcontoClient = subcontoClient;
            this.dao = dao;
        }

        public async Task<List<AccountingPostingList>> GetByBaseIdAsync(IUserContext userContext, long baseId)
        {
            var linksFrom = await linkOfDocumentsClient.GetLinksFromAsync(userContext.FirmId, userContext.UserId, baseId, LinkType.SystemAccountingStatment)
                .ConfigureAwait(false);

            var documentBaseIds = linksFrom.Select(x => x.LinkedToId)
                .Distinct()
                .ToList();
            documentBaseIds.Add(baseId);
            var postings = await accountingPostingsClient.GetByAsync(userContext.FirmId, userContext.UserId,
                new AccountingPostingsSearchCriteriaDto
                {
                    DocumentBaseIds = documentBaseIds
                }).ConfigureAwait(false);

            var accountCodes = postings.SelectMany(x => new[] { x.DebitCode, x.CreditCode }).Distinct().ToList();
            var syntheticAccountDict = (await syntheticAccountClient.GetByCodesAsync(accountCodes).ConfigureAwait(false))
                .ToDictionary(x => x.Code);

            var debitSubcontoIds = postings.SelectMany(x => x.DebitSubcontos);
            var creditSubcontoIds = postings.SelectMany(x => x.CreditSubcontos);
            var subcontoIds = debitSubcontoIds.Concat(creditSubcontoIds).Distinct().ToList();
            var subcontoDict = (await subcontoClient.GetAsync(userContext.FirmId, userContext.UserId, subcontoIds).ConfigureAwait(false))
                .ToDictionary(x => x.Id);

            var operation = await dao.GetByBaseIdAsync(userContext.FirmId, baseId).ConfigureAwait(false);

            return new List<AccountingPostingList>()
            {
                new AccountingPostingList
                {
                    IsManual = operation?.OperationType.IsOtherPaymentType() ?? false,
                    Postings = MapPostings(postings, syntheticAccountDict, subcontoDict),
                    LinkedDocuments = await GetLinkedDocumentsAccountingPostingsAsync(userContext, baseId).ConfigureAwait(false)
                }
            };
        }

        private async Task<List<AccountingPostingLinkedDocument>> GetLinkedDocumentsAccountingPostingsAsync(IUserContext userContext, long baseId)
        {
            var linksFromMove = await linkOfDocumentsClient.GetLinksFromAsync(userContext.FirmId, userContext.UserId, baseId, LinkType.Move)
                 .ConfigureAwait(false);
            var baseIds = linksFromMove.Select(x => x.LinkedToId).ToList();

            var linkedDocuments = await linkedDocumentClient.GetByIdsAsync(userContext.FirmId, userContext.UserId, baseIds).ConfigureAwait(false);
            var result = new List<AccountingPostingLinkedDocument>();
            foreach (var linkedDocument in linkedDocuments)
            {
                var postings = await accountingPostingsClient.GetByAsync(userContext.FirmId, userContext.UserId,
                new AccountingPostingsSearchCriteriaDto
                {
                    DocumentBaseIds = baseIds
                }).ConfigureAwait(false);
                var accountCodes = postings.SelectMany(x => new[] { x.DebitCode, x.CreditCode }).Distinct().ToList();
                var syntheticAccountDict = (await syntheticAccountClient.GetByCodesAsync(accountCodes).ConfigureAwait(false))
                    .ToDictionary(x => x.Code);

                var debitSubcontoIds = postings.SelectMany(x => x.DebitSubcontos);
                var creditSubcontoIds = postings.SelectMany(x => x.CreditSubcontos);
                var subcontoIds = debitSubcontoIds.Concat(creditSubcontoIds).Distinct().ToList();
                var subcontoDict = (await subcontoClient.GetAsync(userContext.FirmId, userContext.UserId, subcontoIds).ConfigureAwait(false))
                .ToDictionary(x => x.Id);

                if (postings.Any())
                {
                    result.Add(new AccountingPostingLinkedDocument
                    {
                        DocumentName = $"{linkedDocument.DocumentType.GetDocumentTypeName()} №{linkedDocument.DocumentNumber}",
                        Postings = MapPostings(postings, syntheticAccountDict, subcontoDict),
                    });
                }
            }
            return result;
        }

        private List<AccountingPosting> MapPostings(IReadOnlyCollection<AccountingPostingDto> accountingPostingDtos,
            IDictionary<SyntheticAccountCode, SyntheticAccountTypeDto> syntheticAccountDict, IDictionary<long, SubcontoDto> subcontoDict)
        {
            return accountingPostingDtos.Select(x => MapPosting(x, syntheticAccountDict, subcontoDict)).ToList();
        }

        private AccountingPosting MapPosting(AccountingPostingDto accountingPostingDto,
            IDictionary<SyntheticAccountCode, SyntheticAccountTypeDto> syntheticAccountDict, IDictionary<long, SubcontoDto> subcontoDict)
        {
            var debitAccount = syntheticAccountDict[accountingPostingDto.DebitCode];
            var creditAccount = syntheticAccountDict[accountingPostingDto.CreditCode];
            var debitSubcontos = GetSubcontos(subcontoDict, accountingPostingDto.DebitSubcontos);
            var creditSubcontos = GetSubcontos(subcontoDict, accountingPostingDto.CreditSubcontos);
            return new AccountingPosting
            {
                Id = accountingPostingDto.Id,
                Date = accountingPostingDto.Date,
                Sum = accountingPostingDto.Sum,
                DebitCode = debitAccount.Code,
                CreditCode = creditAccount.Code,
                DebitTypeId = debitAccount.Id,
                CreditTypeId = creditAccount.Id,
                DebitBalanceType = debitAccount.BalanceType,
                CreditBalanceType = creditAccount.BalanceType,
                DebitNumber = debitAccount.Code.GetAccountDisplayName(),
                CreditNumber = creditAccount.Code.GetAccountDisplayName(),
                Description = accountingPostingDto.Description,
                DebitSubcontos = debitSubcontos,
                CreditSubcontos = creditSubcontos,
            };
        }

        private List<Subconto> GetSubcontos(IDictionary<long, SubcontoDto> subcontoDict, IReadOnlyCollection<long> subcontoIds)
        {
            return subcontoDict.Where(x => subcontoIds.Contains(x.Key))
                .Select(x => new Subconto
                {
                    Id = x.Value.Id,
                    Name = x.Value.Name,
                    Type = x.Value.Type,
                })
                .ToList();
        }
    }
}
