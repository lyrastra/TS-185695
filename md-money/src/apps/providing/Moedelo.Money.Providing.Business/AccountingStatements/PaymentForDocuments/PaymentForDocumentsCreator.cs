using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccountingStatements.ApiClient.Abstractions;
using Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos;
using Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.PaymentForDocuments;
using Moedelo.AccountingStatements.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Incoming;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Outgoing;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;

namespace Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments
{
    [InjectAsSingleton(typeof(PaymentForDocumentsCreator))]
    class PaymentForDocumentsCreator
    {
        private readonly IPaymentForDocumentClient accountingStatementsClient;

        public PaymentForDocumentsCreator(IPaymentForDocumentClient accountingStatementsClient)
        {
            this.accountingStatementsClient = accountingStatementsClient;
        }

        public async Task<PaymentForDocumentCreateResponse[]> GenerateForIncomingPaymentAsync(IncomingPaymentForDocumentsCreateRequest request)
        {
            await DeleteByPaymentBaseIdAsync(request.ExistentLinks);

            if (request.IsBadOperationState)
            {
                return Array.Empty<PaymentForDocumentCreateResponse>();
            }

            var accountingStatements = Generate(request);
            return await CreateAccountStatements(accountingStatements);
        }

        public async Task<PaymentForDocumentCreateResponse[]> GenerateForOutgoingPaymentAsync(OutgoingPaymentForDocumentsCreateRequest request)
        {
            await DeleteByPaymentBaseIdAsync(request.ExistentLinks);

            if (request.IsBadOperationState || request.IsPaid == false)
            {
                return Array.Empty<PaymentForDocumentCreateResponse>();
            }

            var accountingStatements = Generate(request);
            return await CreateAccountStatements(accountingStatements);
        }

        private async Task DeleteByPaymentBaseIdAsync(IReadOnlyCollection<LinkWithDocument> existentLinks)
        {
            var accountingStatementBaseIds = existentLinks
                .Where(x => x.Document.Type == LinkedDocumentType.AccountingStatement)
                .Select(x => x.Document.Id)
                .Distinct()
                .ToArray();

            // Удаляем вместе со связями. Иначе вероятны deadlock-и при параллельном выполнении:
            // - удаление связей при обработке событий "удалена бухсправка"
            // - пересоздание связей п/п далее по стеку выполнения
            await accountingStatementsClient.DeleteAsync(
                accountingStatementBaseIds,
                deleteLinksImmediately: true,
                new HttpQuerySetting(timeout: TimeSpan.FromSeconds(30)));
        }

        private static PaymentForDocumentsGenerateResult[] Generate(IncomingPaymentForDocumentsCreateRequest request)
        {
            var generateRequest = new IncomingPaymentForDocumentsGenerateRequest
            {
                PaymentDate = request.PaymentDate,
                IsMainKontragent = request.IsMainKontragent,
                Links = request.Links,
                Waybills = request.Waybills,
                Statements = request.Statements,
                Upds = request.Upds,
                Kontragent = request.Kontragent,
                Contract = request.Contract
            };
            return IncomingPaymentForDocumentsGenerator.Generate(generateRequest);
        }

        private static PaymentForDocumentsGenerateResult[] Generate(OutgoingPaymentForDocumentsCreateRequest request)
        {
            var generateRequest = new OutgoingPaymentForDocumentsGenerateRequest
            {
                PaymentDate = request.PaymentDate,
                IsMainKontragent = request.IsMainKontragent,
                Links = request.Links,
                Waybills = request.Waybills,
                Statements = request.Statements,
                Upds = request.Upds,
                Kontragent = request.Kontragent,
                Contract = request.Contract
            };
            return OutgoingPaymentForDocumentsGenerator.Generate(generateRequest);
        }

        private async Task<PaymentForDocumentCreateResponse[]> CreateAccountStatements(PaymentForDocumentsGenerateResult[] accountingStatements)
        {
            if (accountingStatements.Length == 0)
            {
                return Array.Empty<PaymentForDocumentCreateResponse>();
            }

            var accountingStatementsCreateRequestDto = accountingStatements.Select(MapToDto).ToArray();
            var accountingStatementsCreateResponseDto =
                await accountingStatementsClient.CreateAsync(accountingStatementsCreateRequestDto);
            var responseDict = accountingStatementsCreateResponseDto.ToDictionary(x => x.TemporaryId);
            return accountingStatements.Select(x => MapToDomain(x, responseDict)).ToArray();
        }

        private static PaymentForDocumentCreateRequestDto MapToDto(PaymentForDocumentsGenerateResult result)
        {
            return new PaymentForDocumentCreateRequestDto
            {
                TemporaryId = result.TemporaryId,
                Date = result.AccountingStatement.Date,
                Description = result.AccountingStatement.Description,
                TaxationSystemType = (TaxationSystemType?)result.AccountingStatement.TaxationSystemType,
                Type = PaymentForDocumentType.OutgoingPaymentForIncomingDocument,
                AccountingPosting = new AccountingPostingDto
                {
                    Sum = result.AccoutingPosting.Sum,
                    Debit = (int)result.AccoutingPosting.DebitCode,
                    Credit = (int)result.AccoutingPosting.CreditCode,
                    DebitSubcontoIds = result.AccoutingPosting.DebitSubcontos.Select(x => x.Id).ToArray(),
                    CreditSubcontoIds = result.AccoutingPosting.CreditSubcontos.Select(x => x.Id).ToArray(),
                    Description = result.AccoutingPosting.Description
                }
            };
        }

        private static PaymentForDocumentCreateResponse MapToDomain(PaymentForDocumentsGenerateResult x, Dictionary<Guid, PaymentForDocumentCreateResponseDto> responseDict)
        {
            var accountingStatement = x.AccountingStatement;
            responseDict.TryGetValue(x.TemporaryId, out var responseDto);
            accountingStatement.DocumentBaseId = responseDto.DocumentBaseId;
            accountingStatement.Number = responseDto.Number;
            return new PaymentForDocumentCreateResponse
            {
                PrimaryDocBaseId = x.PrimaryDocBaseId,
                PrimaryDocDate = x.PrimaryDocDate,
                AccountingStatement = accountingStatement
            };
        }

        public Task UndoAsync(IReadOnlyCollection<PaymentForDocumentCreateResponse> accountingStatements)
        {
            var accountingStatementBaseIds = accountingStatements
                ?.Select(a => a.AccountingStatement.DocumentBaseId)
                .Distinct()
                .ToArray();
            return accountingStatementsClient.DeleteAsync(accountingStatementBaseIds);
        }
    }
}
