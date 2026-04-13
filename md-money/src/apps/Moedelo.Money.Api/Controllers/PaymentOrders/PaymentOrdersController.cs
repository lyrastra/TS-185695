using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.FinancialAssistance;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.Other;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Api.Controllers.PaymentOrders
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank)]
    public class PaymentOrdersController : ControllerBase
    {
        private readonly IPaymentOrderGetter getter;
        private readonly IPaymentOrderRemover remover;
        private readonly IAccrualOfInterestReader accrualOfInterestReader;
        private readonly IContributionToAuthorizedCapitalReader contributionToAuthorizedCapitalReader;
        private readonly ILoanObtainingReader loanObtainingReader;
        private readonly IPaymentFromCustomerReader paymentFromCustomerReader;
        private readonly ITransferFromCashReader transferFromCashReader;
        private readonly IRetailRevenueReader retailRevenueReader;
        private readonly IContributionOfOwnFundsReader contributionOfOwnFundsReader;
        private readonly IMediationFeeReader mediationFeeReader;
        private readonly ITransferFromPurseReader transferFromPurseReader;
        private readonly ITransferFromCashCollectionReader transferFromCashCollectionReader;
        private readonly IFinancialAssistanceReader financialAssistanceReader;
        private readonly IRefundFromAccountablePersonReader returnFromAccountablePersonReader;
        private readonly ITransferFromAccountReader transferFromAccountReader;
        private readonly IOtherIncomingReader otherIncomingReader;
        private readonly IRefundToCustomerReader refundToCustomerReader;
        private readonly IBankFeeReader bankFeeReader;
        private readonly IAgencyContractReader agencyContractReader;
        private readonly IWithdrawalOfProfitReader withdrawalOfProfitReader;
        private readonly IPaymentToSupplierReader paymentToSupplierReader;
        private readonly IPaymentToAccountablePersonReader paymentToAccountablePersonReader;
        private readonly ITransferToAccountReader transferToAccountReader;
        private readonly IWithdrawalFromAccountReader withdrawalFromAccountReader;
        private readonly ILoanRepaymentReader loanRepaymentReader;
        private readonly ILoanIssueReader loanIssueReader;
        private readonly IOtherOutgoingReader otherOutgoingReader;
        private readonly IOutgoingCurrencyPurchaseReader outgoingCurrencyPurchaseReader;
        private readonly IIncomingCurrencyPurchaseReader incomingCurrencyPurchaseReader;
        private readonly IOutgoingCurrencySaleReader outgoingCurrencySaleReader;
        private readonly IBudgetaryPaymentReader budgetaryPaymentReader;
        private readonly ICurrencyOtherIncomingReader currencyOtherIncomingReader;
        private readonly IIncomingCurrencySaleReader incomingCurrencySaleReader;
        private readonly ICurrencyPaymentToSupplierReader currencyPaymentToSupplierReader;
        private readonly ICurrencyBankFeeReader currencyBankFeeReader;
        private readonly ICurrencyOtherOutgoingReader currencyOtherOutgoingReader;
        private readonly ICurrencyPaymentFromCustomerReader currencyPaymentFromCustomerReader;
        private readonly ICurrencyTransferToAccountReader currencyTransferToAccountReader;
        private readonly ICurrencyTransferFromAccountReader currencyTransferFromAccountReader;
        private readonly IPaymentToNaturalPersonsReader paymentToNaturalPersonsReader;
        private readonly IRentPaymentReader rentPaymentReader;
        private readonly ILoanReturnReader loanReturnReader;
        private readonly IBudgetaryPaymentResaveService budgetaryResaveService;
        private readonly INumberService numberService;
        private readonly IDeductionReader deductionReader;
        private readonly IUnifiedBudgetaryPaymentReader unifiedBudgetaryPaymentReader;

        public PaymentOrdersController(
            IPaymentOrderGetter getter,
            IPaymentOrderRemover remover,
            IAccrualOfInterestReader accrualOfInterestReader,
            IContributionToAuthorizedCapitalReader contributionToAuthorizedCapitalReader,
            ILoanObtainingReader loanObtainingReader,
            IPaymentFromCustomerReader paymentFromCustomerReader,
            ITransferFromCashReader transferFromCashReader,
            IRetailRevenueReader retailRevenueReader,
            IContributionOfOwnFundsReader contributionOfOwnFundsReader,
            IMediationFeeReader mediationFeeReader,
            ITransferFromPurseReader transferFromPurseReader,
            ITransferFromCashCollectionReader transferFromCashCollectionReader,
            IFinancialAssistanceReader financialAssistanceReader,
            IRefundFromAccountablePersonReader returnFromAccountablePersonReader,
            ITransferFromAccountReader transferFromAccountReader,
            IOtherIncomingReader otherIncomingReader,
            IRefundToCustomerReader refundToCustomerReader,
            IBankFeeReader bankFeeReader,
            IAgencyContractReader agencyContractReader,
            IWithdrawalOfProfitReader withdrawalOfProfitReader,
            IPaymentToSupplierReader paymentToSupplierReader,
            IPaymentToAccountablePersonReader paymentToAccountablePersonReader,
            ITransferToAccountReader transferToAccountReader,
            IWithdrawalFromAccountReader withdrawalFromAccountReader,
            ILoanRepaymentReader loanRepaymentReader,
            IOtherOutgoingReader otherOutgoingReader,
            IOutgoingCurrencyPurchaseReader outgoingCurrencyPurchaseReader,
            IIncomingCurrencyPurchaseReader incomingCurrencyPurchaseReader,
            IOutgoingCurrencySaleReader outgoingCurrencySaleReader,
            IBudgetaryPaymentReader budgetaryPaymentReader,
            ICurrencyOtherIncomingReader currencyOtherIncomingReader,
            IIncomingCurrencySaleReader incomingCurrencySaleReader,
            ICurrencyPaymentToSupplierReader currencyPaymentToSupplierReader,
            ICurrencyBankFeeReader currencyBankFeeReader,
            ICurrencyOtherOutgoingReader currencyOtherOutgoingReader,
            ICurrencyPaymentFromCustomerReader currencyPaymentFromCustomerReader,
            ICurrencyTransferToAccountReader currencyTransferToAccountReader,
            ICurrencyTransferFromAccountReader currencyTransferFromAccountReader,
            IPaymentToNaturalPersonsReader paymentToNaturalPersonsReader,
            IRentPaymentReader rentPaymentReader,
            ILoanReturnReader loanReturnReader,
            ILoanIssueReader loanIssueReader,
            IBudgetaryPaymentResaveService budgetaryResaveService,
            INumberService numberService,
            IDeductionReader deductionReader,
            IUnifiedBudgetaryPaymentReader unifiedBudgetaryPaymentReader)

        {
            this.getter = getter;
            this.remover = remover;
            this.accrualOfInterestReader = accrualOfInterestReader;
            this.contributionToAuthorizedCapitalReader = contributionToAuthorizedCapitalReader;
            this.loanObtainingReader = loanObtainingReader;
            this.paymentFromCustomerReader = paymentFromCustomerReader;
            this.transferFromCashReader = transferFromCashReader;
            this.retailRevenueReader = retailRevenueReader;
            this.contributionOfOwnFundsReader = contributionOfOwnFundsReader;
            this.mediationFeeReader = mediationFeeReader;
            this.transferFromPurseReader = transferFromPurseReader;
            this.transferFromCashCollectionReader = transferFromCashCollectionReader;
            this.financialAssistanceReader = financialAssistanceReader;
            this.returnFromAccountablePersonReader = returnFromAccountablePersonReader;
            this.transferFromAccountReader = transferFromAccountReader;
            this.otherIncomingReader = otherIncomingReader;
            this.refundToCustomerReader = refundToCustomerReader;
            this.bankFeeReader = bankFeeReader;
            this.agencyContractReader = agencyContractReader;
            this.withdrawalOfProfitReader = withdrawalOfProfitReader;
            this.paymentToSupplierReader = paymentToSupplierReader;
            this.paymentToAccountablePersonReader = paymentToAccountablePersonReader;
            this.transferToAccountReader = transferToAccountReader;
            this.withdrawalFromAccountReader = withdrawalFromAccountReader;
            this.loanRepaymentReader = loanRepaymentReader;
            this.otherOutgoingReader = otherOutgoingReader;
            this.outgoingCurrencyPurchaseReader = outgoingCurrencyPurchaseReader;
            this.incomingCurrencyPurchaseReader = incomingCurrencyPurchaseReader;
            this.outgoingCurrencySaleReader = outgoingCurrencySaleReader;
            this.budgetaryPaymentReader = budgetaryPaymentReader;
            this.currencyOtherIncomingReader = currencyOtherIncomingReader;
            this.incomingCurrencySaleReader = incomingCurrencySaleReader;
            this.currencyPaymentToSupplierReader = currencyPaymentToSupplierReader;
            this.currencyBankFeeReader = currencyBankFeeReader;
            this.currencyOtherOutgoingReader = currencyOtherOutgoingReader;
            this.currencyPaymentFromCustomerReader = currencyPaymentFromCustomerReader;
            this.currencyTransferToAccountReader = currencyTransferToAccountReader;
            this.currencyTransferFromAccountReader = currencyTransferFromAccountReader;
            this.paymentToNaturalPersonsReader = paymentToNaturalPersonsReader;
            this.rentPaymentReader = rentPaymentReader;
            this.loanReturnReader = loanReturnReader;
            this.loanIssueReader = loanIssueReader;
            this.budgetaryResaveService = budgetaryResaveService;
            this.numberService = numberService;
            this.deductionReader = deductionReader;
            this.unifiedBudgetaryPaymentReader = unifiedBudgetaryPaymentReader;
        }

        /// <summary>
        /// Получение операции
        /// </summary>
        [HttpGet("{documentBaseId:long}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<object>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк" })]
        public async Task<IActionResult> GetAsync(long documentBaseId)
        {
            var operationType = await getter.GetOperationTypeAsync(documentBaseId);
            object responseDto;

            switch (operationType)
            {
                // Поступления

                //Начисление процентов от банка
                case OperationType.MemorialWarrantAccrualOfInterest:
                    var accrualOfInterestOperation = await accrualOfInterestReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, AccrualOfInterestMapper.Map(accrualOfInterestOperation));
                    break;
                //Взнос в уставный капитал
                case OperationType.PaymentOrderIncomingContributionToAuthorizedCapital:
                    var contributionToAuthorizedCapitalOperation = await contributionToAuthorizedCapitalReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, ContributionToAuthorizedCapitalMapper.Map(contributionToAuthorizedCapitalOperation));
                    break;
                //Получение займа
                case OperationType.PaymentOrderIncomingLoanObtaining:
                    var loanObtainingOperation = await loanObtainingReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, LoanObtainingMapper.Map(loanObtainingOperation));
                    break;
                //Возврат займа или процентов
                case OperationType.PaymentOrderIncomingLoanReturn:
                    var loanReturnOperation = await loanReturnReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, LoanReturnMapper.Map(loanReturnOperation));
                    break;
                //Оплата от покупателя
                case OperationType.PaymentOrderIncomingPaymentFromCustomer:
                    var paymentFromCustomerOperation = await paymentFromCustomerReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, PaymentFromCustomerMapper.Map(paymentFromCustomerOperation));
                    break;
                //Поступление из кассы
                case OperationType.MemorialWarrantTransferFromCash:
                    var transferFromCashOperation = await transferFromCashReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, TransferFromCashMapper.Map(transferFromCashOperation));
                    break;
                //Поступление за товар оплаченный банковской картой
                case OperationType.MemorialWarrantRetailRevenue:
                    var retailRevenueOperation = await retailRevenueReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, RetailRevenueMapper.Map(retailRevenueOperation));
                    break;
                //Взнос собственных средств
                case OperationType.PaymentOrderIncomingContributionOfOwnFunds:
                    var contributionOfOwnFundsOperation = await contributionOfOwnFundsReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, ContributionOfOwnFundsMapper.Map(contributionOfOwnFundsOperation));
                    break;
                //Посредническое вознаграждение
                case OperationType.PaymentOrderIncomingMediationFee:
                    var mediationFeeOperation = await mediationFeeReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, MediationFeeMapper.Map(mediationFeeOperation));
                    break;
                //Поступление с электронного кошелька
                case OperationType.PaymentOrderIncomingTransferFromPurse:
                    var transferFromPurseOperation = await transferFromPurseReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, TransferFromPurseMapper.Map(transferFromPurseOperation));
                    break;
                //Инкассированные денежные средства
                case OperationType.MemorialWarrantTransferFromCashCollection:
                    var transferFromCashCollectionOperation = await transferFromCashCollectionReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, TransferFromCashCollectionMapper.Map(transferFromCashCollectionOperation));
                    break;
                //Финансовая помощь от учредителя
                case OperationType.PaymentOrderIncomingFinancialAssistance:
                    var financialAssistanceOperation = await financialAssistanceReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, FinancialAssistanceMapper.Map(financialAssistanceOperation));
                    break;
                //Возврат от подотчетного лица
                case OperationType.PaymentOrderIncomingRefundFromAccountablePerson:
                    var returnFromAccountablePersonOperation = await returnFromAccountablePersonReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, RefundFromAccountablePersonMapper.Map(returnFromAccountablePersonOperation));
                    break;
                //Перевод со счета
                case OperationType.PaymentOrderIncomingTransferFromAccount:
                    var transferFromAccountOperation = await transferFromAccountReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, TransferFromAccountMapper.Map(transferFromAccountOperation));
                    break;
                //Прочее поступление
                case OperationType.PaymentOrderIncomingOther:
                    var otherIncomingOperation = await otherIncomingReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, OtherIncomingMapper.Map(otherIncomingOperation));
                    break;
                //Поступление от покупки валюты
                case OperationType.PaymentOrderIncomingCurrencyPurchase:
                    var incomingCurrencyPurchase = await incomingCurrencyPurchaseReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, IncomingCurrencyPurchaseMapper.Map(incomingCurrencyPurchase));
                    break;
                //Поступление от продажи валюты
                case OperationType.PaymentOrderIncomingCurrencySale:
                    var incomingCurrencySale = await incomingCurrencySaleReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, IncomingCurrencySaleMapper.Map(incomingCurrencySale));
                    break;
                //Прочее валютное поступление
                case OperationType.PaymentOrderIncomingCurrencyOther:
                    var incomingCurrencyOther = await currencyOtherIncomingReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, CurrencyOtherIncomingMapper.Map(incomingCurrencyOther));
                    break;
                // Оплата от покупателя в валюте
                case OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer:
                    var currencyPaymentFromCustomer = await currencyPaymentFromCustomerReader
                        .GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, CurrencyPaymentFromCustomerMapper.Map(currencyPaymentFromCustomer));
                    break;
                // Перевод с валютного счета
                case OperationType.PaymentOrderIncomingCurrencyTransferFromAccount:
                    var currencyTransferFromAccount = await currencyTransferFromAccountReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new IncomingPaymentOrderResponseDto(operationType, CurrencyTransferFromAccountMapper.Map(currencyTransferFromAccount));
                    break;

                // Списания

                //Возврат покупателю
                case OperationType.PaymentOrderOutgoingRefundToCustomer:
                    var returnToBuyerOperation = await refundToCustomerReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, RefundToCustomerMapper.Map(returnToBuyerOperation));
                    break;
                //Списана комиссия банка
                case OperationType.MemorialWarrantBankFee:
                    var bankFeeOperation = await bankFeeReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, BankFeeMapper.Map(bankFeeOperation));
                    break;
                //Выплата по агентскому договору
                case OperationType.PaymentOrderOutgoingAgencyContract:
                    var agencyContractOperation = await agencyContractReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, AgencyContractMapper.Map(agencyContractOperation));
                    break;
                //Снятие прибыли
                case OperationType.PaymentOrderOutgoingWithdrawalOfProfit:
                    var withdrawalOfProfitOperation = await withdrawalOfProfitReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, WithdrawalOfProfitMapper.Map(withdrawalOfProfitOperation));
                    break;
                //Оплата поставщику
                case OperationType.PaymentOrderOutgoingPaymentToSupplier:
                    var paymentToSupplierOperation = await paymentToSupplierReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, PaymentToSupplierMapper.Map(paymentToSupplierOperation));
                    break;
                //Выдача подотчетному лицу
                case OperationType.PaymentOrderOutgoingPaymentToAccountablePerson:
                    var paymentToAccountablePersonOperation = await paymentToAccountablePersonReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, PaymentToAccountablePersonMapper.Map(paymentToAccountablePersonOperation));
                    break;
                //Перевод на другой счет
                case OperationType.PaymentOrderOutgoingTransferToAccount:
                    var transferToAccountOperation = await transferToAccountReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, TransferToAccountMapper.Map(transferToAccountOperation));
                    break;
                //Снятие с р/сч
                case OperationType.MemorialWarrantWithdrawalFromAccount:
                    var withdrawalFromAccountOperation = await withdrawalFromAccountReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, WithdrawalFromAccountMapper.Map(withdrawalFromAccountOperation));
                    break;
                //Погашение займа или процентов
                case OperationType.PaymentOrderOutgoingLoanRepayment:
                    var loanRepaymentOperation = await loanRepaymentReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, LoanRepaymentMapper.Map(loanRepaymentOperation));
                    break;
                //Прочее списание
                case OperationType.PaymentOrderOutgoingOther:
                    var otherOutgoingOperation = await otherOutgoingReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, OtherOutgoingMapper.Map(otherOutgoingOperation));
                    break;
                //Списание покупка валюты
                case OperationType.PaymentOrderOutgoingCurrencyPurchase:
                    var outgoingCurrencyPurchase = await outgoingCurrencyPurchaseReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, OutgoingCurrencyPurchaseMapper.Map(outgoingCurrencyPurchase));
                    break;
                //Списание продажа валюты
                case OperationType.PaymentOrderOutgoingCurrencySale:
                    var outgoingCurrencySale = await outgoingCurrencySaleReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, OutgoingCurrencySaleMapper.Map(outgoingCurrencySale));
                    break;
                //Бюджетный платёж
                case OperationType.BudgetaryPayment:
                    var budgetaryPayment = await budgetaryPaymentReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, BudgetaryPaymentMapper.Map(budgetaryPayment));
                    break;
                case OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier:
                    var currencyPaymentToSupplier = await currencyPaymentToSupplierReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, CurrencyPaymentToSupplierMapper.Map(currencyPaymentToSupplier));
                    break;
                //Валютное списание комиссии банка
                case OperationType.CurrencyBankFee:
                    var currencyBankFee = await currencyBankFeeReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, CurrencyBankFeeMapper.Map(currencyBankFee));
                    break;
                //Прочее валютное списание
                case OperationType.PaymentOrderOutgoingCurrencyOther:
                    var currencyOtherOutgoing = await currencyOtherOutgoingReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, CurrencyOtherOutgoingMapper.Map(currencyOtherOutgoing));
                    break;
                //Перевод на другой валютный счет
                case OperationType.PaymentOrderOutgoingCurrencyTransferToAccount:
                    var currencyTransferOutgoing = await currencyTransferToAccountReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, CurrencyTransferToAccountMapper.Map(currencyTransferOutgoing));
                    break;
                //Арендный платёж
                case OperationType.PaymentOrderOutgoingRentPayment:
                    var rentPayment = await rentPaymentReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, RentPaymentMapper.Map(rentPayment));
                    break;
                //Выдача займа
                case OperationType.PaymentOrderOutgoingLoanIssue:
                    var loanIssueOperation = await loanIssueReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, LoanIssueMapper.Map(loanIssueOperation));
                    break;
                //Выплаты физ.лицам
                case OperationType.PaymentOrderOutgoingPaymentToNaturalPersons:
                    var paymentToNaturalPersonsOperation = await paymentToNaturalPersonsReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, PaymentToNaturalPersonsMapper.Map(paymentToNaturalPersonsOperation));
                    break;
                //Выплаты удержаний
                case OperationType.PaymentOrderOutgoingDeduction:
                    var deductionOperation = await deductionReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, DeductionMapper.Map(deductionOperation));
                    break;
                //ЕНП
                case OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment:
                    var unifiedBudgetaryPaymentOperation = await unifiedBudgetaryPaymentReader.GetByBaseIdAsync(documentBaseId);
                    responseDto = new OutgoingPaymentOrderResponseDto(operationType, UnifiedBudgetaryPaymentMapper.Map(unifiedBudgetaryPaymentOperation));
                    break;

                default:
                    throw new NotImplementedException($"Not found case for type: {operationType}");
            }

            return new ApiDataResult(responseDto);
        }

        /// <summary>
        /// Получение типа операции
        /// </summary>
        [HttpGet("{documentBaseId:long}/Type")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<OperationType>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк" })]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetTypeAsync(long documentBaseId)
        {
            var type = await getter.GetOperationTypeAsync(documentBaseId);
            return new ApiDataResult(type);
        }

        /// <summary>
        /// Получение типа операции по массиву базовых идентификаторов
        /// </summary>
        [HttpPost("GetTypeByBaseIds")]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<OperationTypeDto[]>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк" })]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Obsolete("Use PaymentOrdersPrivateController.GetTypeByBaseIds")]
        public async Task<IActionResult> GetTypeByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var response = await getter.GetOperationTypeByBaseIdsAsync(documentBaseIds);
            var result = response.Select(x => new OperationTypeDto { DocumentBaseId = x.DocumentBaseId, OperationType = x.OperationType }).ToArray();
            return new ApiDataResult(result);
        }

        /// <summary>
        /// Получение информации импортирована ли операция
        /// </summary>
        [HttpGet("{documentBaseId:long}/IsFromImport")]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<bool>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк" })]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetIsFromImportAsync(long documentBaseId)
        {
            var response = await getter.GetIsFromImportAsync(documentBaseId);

            return new ApiDataResult(response.IsFromImport);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        [HttpDelete("{documentBaseId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк" })]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> DeleteAsync(long documentBaseId)
        {
            await remover.DeleteAsync(documentBaseId);
            return Ok();
        }

        /// <summary>
        /// Пересохранение операции
        /// </summary>
        [HttpPost("{documentBaseId:long}/Resave")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Obsolete]
        public async Task<IActionResult> ResaveAsync(long documentBaseId)
        {
            var operationType = await getter.GetOperationTypeAsync(documentBaseId);

            switch (operationType)
            {
                //Бюджетный платёж
                case OperationType.BudgetaryPayment:
                    await budgetaryResaveService.ResaveAsync(documentBaseId);
                    break;

                default:
                    throw new NotImplementedException($"Not found case for type: {operationType}");
            }

            return Ok();
        }

        /// <summary>
        /// Получение Id операции
        /// </summary>
        [HttpGet("{documentBaseId:long}/Id")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<long>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк" })]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetIdAsync(long documentBaseId)
        {
            var id = await getter.GetOperationIdAsync(documentBaseId);
            return new ApiDataResult(id);
        }

        /// <summary>
        /// Получение следующего номера платежного поручения
        /// </summary>
        [HttpGet("Outgoing/NextNumber")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ApiDataResponseDto<string>))]
        [SwaggerOperation(Tags = new[] { "Деньги/Банк/Списания" })]
        public async Task<IActionResult> GetFindNextNumberByYearAsyncAsync()
        {
            var nextNumber = await numberService.GetNextNumberAsync();
            return new ApiDataResult(nextNumber);
        }

        /// <summary>
        /// Получение печатной формы платежного поручения
        /// </summary>
        [HttpGet("{documentBaseId:long}/Report")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReportAsync(long documentBaseId, [FromQuery] ReportFormat format)
        {
            var report = await getter.GetReportAsync(documentBaseId, format);
            return File(report.Content, report.ContentType, report.FileName);
        }
    }
}
