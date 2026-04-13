using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Client.Banks;
using Moedelo.CatalogV2.Dto.Banks;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Surf;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.Finances.Domain.Models.Money.Surf;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Parsers.Klto1CParser.Business;
using Moedelo.Parsers.Klto1CParser.Models;
using Moedelo.PaymentImport.Client.MovementList.Storage;
using Moedelo.RequisitesV2.Client.FirmActivityCategory;
using Moedelo.RequisitesV2.Client.FirmRequisites;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.FirmRequisites;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;

namespace Moedelo.Finances.Business.Services.Money.Surf
{
    [InjectPerWebRequest]
    public class PaymentOrderTo1cDocumentConfirmer : IPaymentOrderTo1cDocumentConfirmer
    {
        private readonly ConcurrentDictionary<int, SettlementAccountDto> settlementAccountsCache = new ConcurrentDictionary<int, SettlementAccountDto>();
        private readonly ConcurrentDictionary<int, BankDto> banksCache = new ConcurrentDictionary<int, BankDto>();

        private readonly IFirmRequisitesClient firmRequisitesClient;
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly IBanksApiClient banksApiClient;
        private readonly IPaymentOrderOperationDao dao;
        private readonly IMovementListIntegrationStorageClient movementListIntegrationStorageClient;
        private readonly IFirmActivityCategoryClient activityCategoryClient;

        public PaymentOrderTo1cDocumentConfirmer(
            IFirmRequisitesClient firmRequisitesClient,
            ISettlementAccountClient settlementAccountClient,
            IBanksApiClient banksApiClient,
            IPaymentOrderOperationDao dao,
            IMovementListIntegrationStorageClient movementListIntegrationStorageClient, 
            IFirmActivityCategoryClient activityCategoryClient)
        {
            this.firmRequisitesClient = firmRequisitesClient;
            this.settlementAccountClient = settlementAccountClient;
            this.banksApiClient = banksApiClient;
            this.dao = dao;
            this.movementListIntegrationStorageClient = movementListIntegrationStorageClient;
            this.activityCategoryClient = activityCategoryClient;
        }

        public async Task<List<SurfObject>> ConfirmAsync(int userId)
        {
            var result = new List<SurfObject>();

            var paymentOrders = await dao.GetFor1cConfirmationAsync(new DateTime(DateTime.Today.Year-1, 10, 1), 
                                                                    new DateTime(DateTime.Today.Year, 5, 1)).ConfigureAwait(false);
            var paymentOrdersBySourceDict = paymentOrders.GroupBy(x => x.SourceFileId)
                .ToDictionary(x => x.Key, x => x.ToList());

            var firmIds = paymentOrders.Select(x => x.FirmId).Distinct().ToList();

            var firmRequisitesCache = (await firmRequisitesClient.GetFirmsByIdsAsync(firmIds).ConfigureAwait(false)).ToDictionary(x => x.Id);

            var countRec = 0;
            var skipRec = 0;
            Console.Clear();
            Console.WriteLine($@"Найдено записей: {paymentOrders.Count}");
            Console.WriteLine($@"Найдено фалов: {paymentOrdersBySourceDict.Count}");
            Console.WriteLine($@"----------------------------------------------------");

            foreach (var paymentOrdersBySource in paymentOrdersBySourceDict)
            {
                var sourceFileId = paymentOrdersBySource.Key;

                string file;
                try
                {
                    var firmId = paymentOrdersBySource.Value.FirstOrDefault().FirmId;
                    var date = paymentOrdersBySource.Value.FirstOrDefault().Date;


                    Console.WriteLine($@"Date: {date}  FirmId = {firmId} Get file: {sourceFileId}");
                    countRec++;
                    file = await movementListIntegrationStorageClient.GetTextAsync(sourceFileId).ConfigureAwait(false);
                }
                catch(Exception e)
                {
                    skipRec++;
                    Console.WriteLine($@"Error: {e.Message}");
                    continue;
                }

                var movementList = Klto1CParser.Parse(file);

                var confirmedOperations = new List<Tuple<PaymentOrderOperation, Document>>();
                foreach (var paymentOrder in paymentOrdersBySource.Value)
                {
                    foreach (var doc1cs in movementList.Documents)
                    {

                        if ((paymentOrder.Date == GetDate(doc1cs, movementList.SettlementAccount)) && 
                            (paymentOrder.Sum == doc1cs.Summa) && 
                            (paymentOrder.Description == doc1cs.PaymentPurpose))
                        {
                            var item = new Tuple<PaymentOrderOperation, Document>(paymentOrder, doc1cs);
                            confirmedOperations.Add(item);
                            break;
                        }
                    }
                }

                foreach (var operation in confirmedOperations)
                {
                    var firmId = operation.Item1.FirmId;
                    FirmDto firm = null;
                    if (firmRequisitesCache.Count > 0)
                    {
                        firm = firmRequisitesCache[firmId];
                    }
                    else
                    {
                        firm =  await firmRequisitesClient.GetFirmByIdAsync(firmId).ConfigureAwait(false);
                    }

                    var settlementAccount = await GetSettlementAccountAsync(firmId, userId, operation.Item1.SettlementAccountId ?? 0).ConfigureAwait(false);
                    if (settlementAccount == null || result.Where(x => x.DocumentBaseId == operation.Item1.DocumentBaseId).Any())
                    {
                        continue;
                    }
                    
                    var bank = await GetBankAsync(settlementAccount.BankId).ConfigureAwait(false);
                    var categoryClient = await activityCategoryClient.GetMainAsync(firm.Id, userId).ConfigureAwait(false);
                    

                    result.Add(Map(operation.Item1, operation.Item2, firm.Inn, bank.Inn, settlementAccount.Number, categoryClient.Code));
                }
            }
            return result;
        }

        private static DateTime GetDate(Document doc, string settlementNumber)
        {
            var date = doc.PayerAccount == settlementNumber
                ? doc.OutgoingDate
                : doc.IncomingDate;
            return date ?? doc.DocDate ?? DateTime.MinValue;
        }

        private async Task<BankDto> GetBankAsync(int id)
        {
            if (banksCache.TryGetValue(id, out var bank))
            {
                return bank;
            }
            bank = (await banksApiClient.GetByIdsAsync(new[] { id }).ConfigureAwait(false)).FirstOrDefault();
            if (bank != null)
            {
                banksCache.TryAdd(id, bank);
            }
            return bank;
        }

        private async Task<SettlementAccountDto> GetSettlementAccountAsync(int firmId, int userId, int id)
        {
            if (settlementAccountsCache.TryGetValue(id, out var settlementAccount))
            {
                return settlementAccount;
            }
            settlementAccount = await settlementAccountClient.GetByIdAsync(firmId, userId, id).ConfigureAwait(false);
            if (settlementAccount != null)
            {
                settlementAccountsCache.TryAdd(id, settlementAccount);
            }
            return settlementAccount;
        }

        private static SurfObject Map(PaymentOrderOperation operation, Document document, string firmInn, string bankInn, string settlementAccount, string OKVED)
        {
            return new SurfObject
            {
                FirmId = operation.FirmId,
                DocumentBaseId = operation.DocumentBaseId,
                OpType = Map(operation.OperationType), 
                OpPurpose = document.PaymentPurpose.Replace(";", " "),
                KontragentInn = GetKontragentInn(operation.Direction, document),
                IsProprietaryInn = GetKontragentInn(operation.Direction, document) == firmInn,
                IsBankInn = GetKontragentInn(operation.Direction, document) == bankInn,
                RecvAcc = document.ContractorAccount,
                IsProprietaryRecvAcc = document.ContractorAccount == settlementAccount,
                SndrAcc = document.PayerAccount,
                IsProprietarySndrAcc = document.PayerAccount == settlementAccount,
                RecvBank = document.ContractorBankName,
                SndrBank = document.PayerBankName,
                OpSum = operation.Sum,
                OpDate = operation.Date,
                SourceFileId = operation.SourceFileId,
                Direction = (int)operation.Direction,
                OKVED = OKVED
            };
        }

        // Конвертор из БИЗ в Учетку
        private static string Map(OperationType type)
        {
            var opType = "";
            switch (type)
            {
                case OperationType.PaymentOrderIncomingFromAnotherAccount:
                    opType = "MovementFromSettlementToSettlement";
                    break;
                case OperationType.PaymentOrderIncomingOther:
                    opType = "OtherIncoming";
                    break;
                case OperationType.PaymentOrderIncomingRetailRevenue:
                    opType = "ReceiptGoodsPaidCreditCard";
                    break;
                case OperationType.PaymentOrderIncomingPaymentForGoods:
                    opType = "SaleProduct";
                    break;
                case OperationType.PaymentOrderIncomingReturnFromAccountablePerson:
                    opType = "RefundFromEmployee";
                    break;
                case OperationType.PaymentOrderOutgoingReturnToBuyer:
                    opType = "RefundToCustomerOutgoing";
                    break;
                case OperationType.PaymentOrderOutgoingIssuanceAccountablePerson:
                    opType = "GetMoneyForEmployee";
                    break;
                case OperationType.PaymentOrderOutgoingOther:
                    opType = "ElectronicOutgoing";
                    break;
                case OperationType.PaymentOrderOutgoingPaymentSuppliersForGoods:
                    opType = "MainActivityOutgoing";
                    break;
                case OperationType.PaymentOrderOutgoingForTransferSalary:
                    opType = "PayDays";
                    break;
                case OperationType.PaymentOrderOutgoingTransferToOtherAccount:
                    opType = "MovementFromSettlementToSettlement";
                    break;
                case OperationType.PaymentOrderIncomingMediationFee:
                    opType = "ProvisionOfServices";
                    break;
                case OperationType.PaymentOrderIncomingFromPurse:
                    opType = "MovementFromCashToSettlement";
                    break;
                case OperationType.PaymentOrderIncomingLoanObtaining:
                    opType = "LoanParent";
                    break;
                case OperationType.PaymentOrderOutgoingLoanRepayment:
                    opType = "LoansThirdPartiesOutgoing";
                    break;
                case OperationType.PaymentOrderIncomingMaterialAid:
                    opType = "MaterialAid";
                    break;
                case OperationType.PaymentOrderIncomingContributionAuthorizedCapital:
                    opType = "UkInpayment";
                    break;
                case OperationType.PaymentOrderIncomingContributionOfOwnFunds:
                    opType = "ContributionOfOwnFunds";
                    break;
                case OperationType.PaymentOrderOutgoingProfitWithdrawing:
                    opType = "RemovingTheProfit";
                    break;
                case OperationType.PaymentOrderOutgoingtWithdrawalFromAccount:
                    opType = "MovementFromSettlementToCash";
                    break;
                case OperationType.PaymentOrderOutgoingBankFee:
                    opType = "BankFeeOutgoing";
                    break;
                case OperationType.BudgetaryPayment:
                    opType = "BudgetaryPayment";
                    break;
                case OperationType.PaymentOrderIncomingTransferFromCash:
                    opType = "MovementFromCashToSettlement";
                    break;
                case OperationType.PaymentOrderIncomingAccrualOfInterest:
                    opType = "OtherIncoming";
                    break;

                default:
                    opType = "Default";
                    break;
            }
            return opType;
        }

        public static string GetKontragentInn(MoneyDirection direction, Document document)
        {
            if (direction == MoneyDirection.Outgoing)
            {
                return document.ContractorInn;
            }
            if (direction == MoneyDirection.Incoming)
            {
                return document.PayerInn;
            }
            throw new NotSupportedException("unknown direction");
        }
    }

    [InjectAsSingleton]
    public class PaymentOrderTo1cDocumentConfirmerStateless : IPaymentOrderTo1cDocumentConfirmer
    {
        private readonly IDIResolver diResolver;

        private readonly AsyncLocal<IPaymentOrderTo1cDocumentConfirmer> paymentOrderTo1CDocumentConfirmer = new AsyncLocal<IPaymentOrderTo1cDocumentConfirmer>();
        private IPaymentOrderTo1cDocumentConfirmer PaymentOrderTo1CDocumentConfirmer => paymentOrderTo1CDocumentConfirmer.Value ?? 
                                                                                        (paymentOrderTo1CDocumentConfirmer.Value = diResolver.GetInstance<IPaymentOrderTo1cDocumentConfirmer>());

        public PaymentOrderTo1cDocumentConfirmerStateless(IDIResolver diResolver)
        {
            this.diResolver = diResolver;
        }

        public Task<List<SurfObject>> ConfirmAsync(int userId)
        {
            return PaymentOrderTo1CDocumentConfirmer.ConfirmAsync(userId);
        }
    }
}
