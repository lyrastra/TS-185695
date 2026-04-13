(function(mainModule, md) {
    'use strict';

    var complexOperations = [
        'MaterialAidOperation',
        'LoanParentOperation',
        'UkInpamentOperation',
        'LoansThirdPartiesOperation',
        'ReturnFalseMeans',
        'ReturnFromSupplier'
    ];

    if (md && md.Core) {
        mainModule = md.Core;
    }

    window.MoneyTools = mainModule.MoneyTools = {
        moneyTypeSwitcher: moneyTypeSwitcher,
        getOperationUrl: getOperationUrl
    };

    /** @access public */
    function moneyTypeSwitcher(operationType) {
        var operation = {}, opType, opName;
        switch (operationType) {
            case 'AgentIncomingOperation':
                opName = 'underTheAgency';
                opType = 'incoming';
                break;
            case 'RefundFromEmployeeIncomingOperation':
                opName = 'fromAccountablePerson';
                opType = 'incoming';
                break;
            case 'CurrencyPurchaseAndSaleIncomingOperation':
                opName = 'currencySellBuy';
                opType = 'incoming';
                break;
            case 'ContributionOfOwnFundsOperation':
                opName = 'away';
                opType = 'incoming';
                break;
            case 'LoanParentOperation':
                opName = 'fromFounder';
                opType = 'incoming';
                break;
            case 'UkInpamentOperation':
                opName = 'fromFounder';
                opType = 'incoming';
                break;
            case 'ReturnFalseMeansIncomingOperation':
                opName = 'fromVendor';
                opType = 'incoming';
                break;
            case 'MaterialAidOperation':
                opName = 'fromFounder';
                opType = 'incoming';
                break;
            case 'LoansThirdPartiesOperation':
                opName = 'fromVendor';
                opType = 'incoming';
                break;
            case 'ReturnFromSupplierIncomingOperation':
                opName = 'fromVendor';
                opType = 'incoming';
                break;
            case 'OtherIncomingOperation':
                opName = 'otherIncome';
                opType = 'incoming';
                break;
            case 'ProvisionOfServicesOperation':
                opName = 'provisionOfServices';
                opType = 'incoming';
                break;
            case 'SaleProductOperation':
                opName = 'saleOfProducts';
                opType = 'incoming';
                break;
            case 'CashIncomingOperation':
                opName = 'revenueOffice';
                opType = 'incoming';
                break;
            case 'PurseIncomingOperation':
                opName = 'personalAccount';
                opType = 'incoming';
                break;
            case 'BudgetaryPaymentOperation':
                opName = 'budgetaryPayment';
                opType = 'outgoing';
                break;
            case 'DividendPaymentOperation':
                break;
            case 'LoanParentOutgoingOperation':
                opName = 'loanParent';
                opType = 'outgoing';
                break;
            case 'LoansThirdPartiesOutgoingOperation':
                opName = 'loansThirdParties';
                opType = 'outgoing';
                break;
            case 'OperatingExpensesOperation':
                opName = 'operatingExpenses';
                opType = 'outgoing';
                break;
            case 'OtherOutgoingOperation':
                opName = 'other';
                opType = 'outgoing';
                break;
            case 'CurrencyPurchaseAndSaleOutgoingOperation':
                opName = 'currencySellBuy';
                opType = 'outgoing';
                break;
            case 'PayDaysOperation':
                opName = 'workerPayment';
                opType = 'outgoing';
                break;
            case 'PurchaseOfFixedAssetsOperation':
                opName = 'purchaseOfFixedAssets';
                opType = 'outgoing';
                break;
            case 'RemovingTheProfitOperation':
                opName = 'removingTheProfit';
                opType = 'outgoing';
                break;
            case 'MainActivityOutgoingOperation':
                opName = 'mainActivity';
                opType = 'outgoing';
                break;
            case 'PurseOutgoingOperation':
                opName = 'purse';
                opType = 'outgoing';
                break;
            case 'MovementFromSettlementToCashMoneyTransferOperation':
                opName = 'fromSettlementToCash';
                opType = 'movement';
                break;
            case 'MovementFromCashToSettlementMoneyTransferOperation':
                opName = 'fromCashToSettlement';
                opType = 'movement';
                break;
            case 'MovementFromSettlementToSettlementMoneyTransferOperation':
                opName = 'fromSettlementToSettlement';
                opType = 'movement';
                break;
            case 'MovementFromPurseToSettlementMoneyTransferOperation':
                opName = 'fromPurseToSettlement';
                opType = 'movement';
                break;
            case 'WorkerPaymentOperation':
                break;
            case 'GetMoneyForEmployeeOutgoingOperation':
                opName = 'getMoneyForEmployee';
                opType = 'outgoing';
                break;
            case 'RefundToCustomerOutgoingOperation':
                opName = 'refundToCustomer';
                opType = 'outgoing';
                break;
            case 'BankFeeOutgoingOperation':
                opName = 'bankFee';
                opType = 'outgoing';
                break;
            case 'ElectronicOutgoingOperation':
                opName = 'electronic';
                opType = 'outgoing';
                break;
            case 'RefundFromBudgetIncomingOperation':
                opName = 'refundFromBudget';
                opType = 'incoming';
                break;
        }

        operation.type = opType;
        operation.name = opName;
        return operation;
    }

    /** @access private */
    function getOperationUrl(transferId, transferType, type) {
        var switcherResult = window.MoneyTools.moneyTypeSwitcher(transferType);
        var url = '#moneyDialog/' + switcherResult.type + '/' + switcherResult.name + '/';

        if (complexOperations.indexOf(transferType) > 0) {
            url += transferType + '/' + type + '/' + transferId;
        } else {
            url += type + '/' + transferId;
        }

        return url;
    }
})(window, Md);

