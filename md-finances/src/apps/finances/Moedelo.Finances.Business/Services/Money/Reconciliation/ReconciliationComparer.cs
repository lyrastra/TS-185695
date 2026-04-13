using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.Extensions.System;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Services.Money.Reconciliation.ForUser;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.KontragentsV2.Client.Kontragents;
using Moedelo.Parsers.Klto1CParser.Business;
using Moedelo.Parsers.Klto1CParser.Enums;
using Moedelo.Parsers.Klto1CParser.Exceptions;
using Moedelo.Parsers.Klto1CParser.Extensions;
using Moedelo.Parsers.Klto1CParser.Models;
using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using OperationType = Moedelo.Common.Enums.Enums.PostingEngine.OperationType;
using TransferType = Moedelo.Parsers.Klto1CParser.Enums.TransferType;

namespace Moedelo.Finances.Business.Services.Money.Reconciliation
{
    [InjectAsSingleton]
    public class ReconciliationComparer(
        IKontragentsClient kontragentsClient,
        ISettlementAccountClient settlementAccountClient,
        IPaymentOrderOperationDao paymentOrderOperationDao,
        IMoneyTransferOperationDao moneyTransferOperationDao,
        IDocumentTypeApiClient documentTypeApiClient,
        ILogger logger) : IReconciliationComparer
    {
        private const string Tag = nameof(ReconciliationComparer);

        public async Task<ReconciliationCompareResult> CompareWithBankStatementAsync(IUserContext userContext, string fileText, DateTime startDate, DateTime endDate)
        {
            MovementList movementList;
            try
            {
                movementList = Klto1CParser.Parse(fileText, ParseOptions.NoCheckStartBalance);
            }
            catch (BadFormatException)
            {
                logger.Error(Tag, $"Сверка с банком невозможна: формат файла выписки не корректный.",
                    extraData: new { userContext.FirmId, userContext.UserId });
                return new ReconciliationCompareResult
                {
                    ExcessOperations = [],
                    MissingOperations = []
                };
            }
            catch (UnknownEncodingException)
            {
                logger.Error(Tag, $"Сверка с банком невозможна: кодировка файла выписки не корректна.",
                    extraData: new { userContext.FirmId, userContext.UserId });
                return new ReconciliationCompareResult
                {
                    ExcessOperations = [],
                    MissingOperations = []
                };
            }

            // Костыль TS-101492,TS-103024, TS-107781, TS-117581 у текущего пользователя банк лажает и присылает операции за другой Р\С.
            // По мотивам этого кейса в интеграции заведене задача в тех долге.
            // Если ты читаешь это и сейчас на дворе 2025 напиши в интеграцию и спроси статус этой задачи.
            if (userContext.FirmId is 9282381 or 8274283 or 1035810 or 9537674)
            {
                movementList.Documents.RemoveAll(x => x.ContractorAccount != movementList.SettlementAccount && x.PayerAccount != movementList.SettlementAccount);
            }

            var existsOperations = await LoadOperationFromServiceAsync(userContext, movementList.SettlementAccount, startDate.AddDays(-3), endDate.AddDays(3)).ConfigureAwait(false);

            var missingOperations = new List<Document>();
            var excessOperations = new List<ReconciliationOperation>();

            foreach (var document in movementList.Documents)
            {
                await CompareAsync(userContext, document, movementList.SettlementAccount, existsOperations, missingOperations, excessOperations).ConfigureAwait(false);
            }
            excessOperations.AddRange(existsOperations.Where(x => x.Date >= startDate && x.Date <= endDate));

            var documentTypesChunks = new List<IDictionary<string, TransferType>>();
            var isAccounting = await userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff).ConfigureAwait(false);
            if (isAccounting)
            {
                var outgoingDocuments = missingOperations.Where(x => IsOutgoing(x, movementList.SettlementAccount)).ToList();
                outgoingDocuments.ForEach(x => x.ReservedString = Guid.NewGuid().ToString());
                foreach (var documents in outgoingDocuments.Chunk(1000))
                {
                    var documentTypes = await documentTypeApiClient.DetermineAsync(userContext.FirmId, userContext.UserId, movementList.SettlementAccount, documents.ToList()).ConfigureAwait(false);
                    documentTypesChunks.Add(documentTypes);
                }
            }

            return new ReconciliationCompareResult
            {
                ExcessOperations = excessOperations,
                MissingOperations = missingOperations.Select(x => Map(x, movementList.SettlementAccount, documentTypesChunks.SelectMany(dict => dict).ToDictionary(d => d.Key, d => d.Value))).ToList()
            };
        }

        public async Task<ReconciliationCompareResult> CompareWithEmptyBankStatementAsync(IUserContext userContext, string settlementAccountNumber, DateTime startDate, DateTime endDate)
        {
            var excessOperations = await LoadOperationFromServiceAsync(userContext, settlementAccountNumber, startDate, endDate).ConfigureAwait(false);
            return new ReconciliationCompareResult
            {
                ExcessOperations = excessOperations,
                MissingOperations = []
            };
        }

        private async Task<List<ReconciliationOperation>> LoadOperationFromServiceAsync(IUserContext userContext, string settlementAccountNumber, DateTime startDate, DateTime endDate)
        {
            var settlementAccounts = await settlementAccountClient.GetByNumbersAsync(userContext.FirmId, userContext.UserId, new[] { settlementAccountNumber })
                .ConfigureAwait(false);
            var settlementAccount = settlementAccounts.FirstOrDefault();
            if (settlementAccount == null)
            {
                return [];
            }
            return await GetForReconciliationAsync(userContext, settlementAccount.Id, startDate, endDate).ConfigureAwait(false);
        }

        private async Task<List<ReconciliationOperation>> GetForReconciliationAsync(IUserContext userContext, int settlementAccountId, DateTime startDate, DateTime endDate)
        {
            var isBiz = await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false);
            if (isBiz)
            {
                var moneyTransferOperations = await moneyTransferOperationDao.GetForReconciliationAsync(
                    userContext.FirmId, settlementAccountId, startDate, endDate).ConfigureAwait(false);
                return moneyTransferOperations.Select(x =>
                    new ReconciliationOperation
                    {
                        Id = x.DocumentBaseId,
                        Date = x.Date,
                        Number = x.Number,
                        IsOutgoing = x.Direction == MoneyDirection.Outgoing,
                        Sum = x.Sum,
                        KontragentId = x.KontragentId,
                        KontragentName = x.KontragentName,
                        Description = x.Description
                    }).ToList();
            }

            var paymentOrderOperations = await paymentOrderOperationDao.GetForReconciliationAsync(
                userContext.FirmId, settlementAccountId, startDate, endDate).ConfigureAwait(false);
            return paymentOrderOperations.Select(x =>
                new ReconciliationOperation
                {
                    Id = x.DocumentBaseId,
                    Date = x.Date,
                    Number = x.Number,
                    IsOutgoing = x.Direction == MoneyDirection.Outgoing,
                    Sum = x.Sum,
                    KontragentId = x.KontragentId,
                    KontragentName = x.KontragentName,
                    Description = x.Description,
                    IsSalary = x.OperationType == OperationType.PaymentOrderOutgoingForTransferSalary
                }).ToList();
        }

        private async Task CompareAsync(IUserContext userContext, Document document, string settlementAccountNumber,
            List<ReconciliationOperation> existsOperations, List<Document> missingOperations, List<ReconciliationOperation> excessOperations)
        {
            var startDate = document.GetDate().AddDays(-3);
            var endDate = document.GetDate().AddDays(3);

            var matchedOperations = existsOperations.Where(x =>
                x.IsOutgoing == IsOutgoing(document, settlementAccountNumber) &&
                x.Sum == document.Summa &&
                x.Date >= startDate && x.Date <= endDate)
                .ToList();
            
            switch (matchedOperations.Count)
            {
                case 0:
                    missingOperations.Add(document);
                    return;
                case 1:
                    Remove(existsOperations, matchedOperations);
                    return;
            }

            //Compare purpose
            var pursposeMatchedOperations = matchedOperations.Where(x => x.Description == document.PaymentPurpose).ToList();
            switch (pursposeMatchedOperations.Count)
            {
                case 1:
                    Remove(existsOperations, pursposeMatchedOperations);
                    return;
                case 0:
                    pursposeMatchedOperations = matchedOperations;
                    break;
            }

            // Compare number
            var numberMatchedOperations = pursposeMatchedOperations.Where(x => x.Number == document.DocumentNumber).ToList();
            if (numberMatchedOperations.Count == 1)
            {
                Remove(existsOperations, numberMatchedOperations);
                return;
            }
            
            var hasRepeatedNumbers = pursposeMatchedOperations
                .GroupBy(x => x.Number)
                .Any(x => x.Count() > 1);
            
            // Если в pursposeMatchedOperations, все операции уникальны по Number, и document не найден по DocumentNumber - это Новая операция
            if (!hasRepeatedNumbers && numberMatchedOperations.Count == 0)
            {
                missingOperations.Add(document);
                return;
            }
            
            if (numberMatchedOperations.Count == 0)
            {
                numberMatchedOperations = matchedOperations;
            }

            // Compare kontragent
            if (IsKontragentInnEmptyOrZero(document, settlementAccountNumber))
            {
                missingOperations.Add(document);
                return;
            }
            
            var kontragents = await kontragentsClient.GetByInnAsync(userContext.FirmId, userContext.UserId, GetKontragentInn(document, settlementAccountNumber)).ConfigureAwait(false);
            var kontragentsIds = kontragents.Select(x => x.Id).ToList();

            var kontragentMatchedOperations = numberMatchedOperations.Where(x => !x.KontragentId.HasValue || x.KontragentId.HasValue &&
                kontragentsIds.Contains(x.KontragentId.Value)).ToList();
            if (kontragentMatchedOperations.Count == 0)
            {
                missingOperations.Add(document);
                return;
            }
            
            Remove(existsOperations, kontragentMatchedOperations);
            excessOperations.AddRange(kontragentMatchedOperations.OrderBy(x => Math.Abs((x.Date - document.GetDate()).TotalSeconds)).Skip(1));
        }

        private static void Remove(List<ReconciliationOperation> existsOperations, IReadOnlyCollection<ReconciliationOperation> matchedOperations)
        {
            if (matchedOperations == null || matchedOperations.Count == 0)
            {
                return;
            }
            
            var ids = matchedOperations.Select(x => x.Id).ToList();
            existsOperations.RemoveAll(x => ids.Contains(x.Id));
        }

        private static bool IsOutgoing(Document document, string settlementAccount)
        {
            if (document.ContractorAccount != settlementAccount && document.PayerAccount != settlementAccount)
                throw new NotSupportedException("Current settlement account does not match contractor and payer settlement account");

            if (document.ContractorAccount == settlementAccount)
            {
                return false;
            }
            if (document.PayerAccount == settlementAccount)
            {
                return true;
            }
            throw new NotSupportedException("unknown direction");
        }

        private static string GetKontragentName(Document document, string settlementAccount)
        {
            return IsOutgoing(document, settlementAccount)
                ? document.Contractor
                : document.Payer;
        }

        private static string GetKontragentInn(Document document, string settlementAccount)
        {
            return IsOutgoing(document, settlementAccount)
                ? document.ContractorInn
                : document.PayerInn;
        }

        private static bool IsKontragentInnEmptyOrZero(Document document, string settlementAccount)
        {
            var inn = GetKontragentInn(document, settlementAccount);
            return string.IsNullOrEmpty(inn) || inn == "0000000000" || inn == "000000000000" || inn == "0";
        }

        private static ReconciliationOperation Map(Document document, string settlementAccount, IDictionary<string, TransferType> types)
        {
            var operation = new ReconciliationOperation
            {
                Id = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0) / 1000,
                IsOutgoing = IsOutgoing(document, settlementAccount),
                Sum = document.Summa,
                Date = document.GetDate(),
                Number = document.DocumentNumber,
                Description = document.PaymentPurpose,
                KontragentName = GetKontragentName(document, settlementAccount),
                DocumentSection = document.RawSection
            };

            if (operation.IsOutgoing && types.TryGetValue(document.ReservedString, out var type))
            {
                operation.IsSalary = type is TransferType.PayDays or TransferType.SalaryProject or TransferType.DividendPayment;
            }

            return operation;
        }
    }
}
