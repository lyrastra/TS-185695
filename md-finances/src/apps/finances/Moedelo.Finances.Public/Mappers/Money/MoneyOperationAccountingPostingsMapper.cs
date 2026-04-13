using System.Collections.Generic;
using System.Linq;
using Moedelo.Finances.Domain.Models.Money.Operations.AccountingPostings;
using Moedelo.Finances.Public.ClientData.Money.Operations.AccountingPostings;

namespace Moedelo.Finances.Public.Mappers.Money
{
    public static class MoneyOperationAccountingPostingsMapper
    {
        public static AccountingPostingsResponseClientData MapAccountingPostingsResponse(List<AccountingPostingList> accountingPostings)
        {
            return new AccountingPostingsResponseClientData
            {
                Operations = accountingPostings.Select(MapAccountingPostings).ToList()
            };
        }

        private static AccountingPostingsClientData MapAccountingPostings(AccountingPostingList accountingPostings)
        {
            return new AccountingPostingsClientData
            {
                IsManualEdit = accountingPostings.IsManual,
                Postings = accountingPostings.Postings.Select(MapAccountingPostingDescription).ToList(),
                LinkedDocuments = accountingPostings.LinkedDocuments.Select(MapLinkedDocument).ToList()
            };
        }

        private static AccountingPostingLinkedDocumentClientData MapLinkedDocument(AccountingPostingLinkedDocument linkedDocument)
        {
            return new AccountingPostingLinkedDocumentClientData
            {
                DocumentName = linkedDocument.DocumentName,
                Postings = linkedDocument.Postings.Select(MapAccountingPostingDescription).ToList()
            };
        }

        private static AccountingPostingDescriptionClientData MapAccountingPostingDescription(AccountingPosting postingDescription)
        {
            return new AccountingPostingDescriptionClientData
            {
                Id = postingDescription.Id,
                Date = postingDescription.Date,
                Sum = postingDescription.Sum,
                Debit = (int)postingDescription.DebitCode,
                Credit = (int)postingDescription.CreditCode,
                DebitTypeId = postingDescription.DebitTypeId,
                CreditTypeId = postingDescription.CreditTypeId,
                DebitNumber = postingDescription.DebitNumber,
                CreditNumber = postingDescription.CreditNumber,
                Description = postingDescription.Description,
                SubcontoDebit = postingDescription.DebitSubcontos.Select(MapSubconto).ToList(),
                SubcontoCredit = postingDescription.CreditSubcontos.Select(MapSubconto).ToList(),
                DebitBalanceType = postingDescription.DebitBalanceType,
                CreditBalanceType = postingDescription.CreditBalanceType,
            };
        }

        private static SubcontoClientData MapSubconto(Subconto subconto)
        {
            return new SubcontoClientData
            {
                Id = subconto.Id,
                Name = subconto.Name,
                SubcontoType = subconto.Type,
            };
        }
    }
}
