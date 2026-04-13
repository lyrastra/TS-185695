/* eslint-disable */

import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import { cashOrderOperationResources } from '../../../../../../../resources/MoneyOperationTypeResources';
import operationBillsHelper from '../../../../../../../components/OperationBills/operationBillsHelper';
import SyntheticAccountCodesEnum from '../../../../../../../enums/SyntheticAccountCodesEnum';

const cash = window.Cash;
const common = window.Common;

cash.Models.IncomingCashOrder = cash.Models.BaseCashOrder.extend({

    documentName: 'Поступление',
    yearExecludeNds: 2016,

    initialize() {
        this.calculateBillsSum();
        this.handleBillsStore();

        this.on('change:OperationType', () => {
            this.operationBillsStore && this.operationBillsStore.updateContract(operationBillsHelper.getContractBaseId(this));
            this.cleanModel();
            this.handleBillsStore();
        });
        this.on('change:BillNumber', this.setBill);
        this.on('change:ZReportNumber', this.setComment);
        this.on('change:Comments', this.setIsCommentsChange);
        this.on('change:Date change:IncludeNds change:Sum change:NdsSum', this.calculateTaxSum);
        this.on('change:ContractBaseId change:MiddlemanContract', () => {
            this.operationBillsStore && this.operationBillsStore.updateContract(operationBillsHelper.getContractBaseId(this));
        });
        this.on('change:KontragentId', (self, value) => {
            this.operationBillsStore && this.operationBillsStore.updateKontragent(value);
        });
        this.on('change:Sum', (self, value) => {
            this.operationBillsStore && this.operationBillsStore.updateSum(value);
        });
        this.on('change:BillsSum', () => {
            this.validate && this.validate(['Sum']);
        });
    },

    calculateBillsSum() {
        const bills = this.get('Bills') || [];

        if (_.isArray(bills)) {
            const BillsSum = bills.reduce((acc, bill) => {
                return acc + bill.CurrentPayedSum;
            }, 0);

            this.set({ BillsSum: toFloat(BillsSum, true) });
        }
    },

    defaults() {
        return {
            Title: 'Поступление',
            Date: dateHelper().format('DD.MM.YYYY'),
            KontragentAccountCode: SyntheticAccountCodesEnum._62_02,
            OperationType: cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value,
            // NdsType: common.Data.BankAndCashNdsTypes.Nds20
        };
    },

    url() {
        if (this.get('isCopy')) {
            return Cash.Data.CopyCashOrder;
        }

        if (this.get('isFromContract')) {
            const contractId = this.get('ProjectId');
            return `/Accounting/FirmCash/IncomingOrderFromContract?contractId=${contractId}`;
        }

        return Cash.Data.GetIncomingCashOrder;
    },

    calculateTaxSum() {
        const selectedDate = Converter.toDate(this.get('Date'));
        const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
        const taxSystem = ts.Current(selectedDate);

        let taxSum = Converter.toFloat(this.get('Sum'));
        if (taxSystem.get('IsUsn') && this.get('IncludeNds') && selectedDate.getFullYear() >= this.yearExecludeNds) {
            taxSum -= this.get('NdsSum');
        }
        this.set('TaxSum', taxSum);
    },

    handleBillsStore() {
        operationBillsHelper.initBillsStore(this);
    },

    setKontragent(KontragentId) {
        KontragentId && !this.get('KontragentId') && this.set({ KontragentId });
    },

    isKontragentPayment() {
        const type = this.get('OperationType');

        return type === cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value;
    },

    isCashContributing() {
        const type = this.get('OperationType');
        return type === cashOrderOperationResources.CashOrderIncomingContributionAuthorizedCapital.value;
    },

    isWorkerPayment() {
        return this.get('OperationType') == cashOrderOperationResources.CashOrderIncomingReturnFromAccountablePerson.value;
    },

    isOtherPayment() {
        return this.get('OperationType') == cashOrderOperationResources.CashOrderIncomingOther.value;
    },

    isCheckingAccountPayment() {
        return this.get('OperationType') == cashOrderOperationResources.CashOrderIncomingFromSettlementAccount.value;
    },

    isSalaryPayment() {
        return false;
    },

    isRetailRevenue() {
        const operationType = parseInt(this.get('OperationType'), 10);
        return operationType === cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.value ||
               operationType === cashOrderOperationResources.CashOrderIncomingMiddlemanRetailRevenue.value;
    },

    isRetailRevenueOnly() {
        const operationType = parseInt(this.get('OperationType'), 10);
        return operationType === cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.value;
    },

    cleanModel() {
        if (![
            cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.value,
            cashOrderOperationResources.CashOrderIncomingMiddlemanRetailRevenue.value
        ].includes(parseInt(this.get('OperationType'), 10))) {
            this.unset('PaidCardSum');
        }

        if (this.isRetailRevenue() || this.isCheckingAccountPayment()) {
            this.unset('KontragentId');
            this.unset('KontragentName');
        }
    },

    setBill() {
        if (!this.get('BillNumber')) {
            this.unset('BillDocumentBaseId');
        }
    },

    setIsCommentsChange() {
        if (this.isRetailRevenue() && this.get('Comments') === '') {
            this.set('isCommentsChange', true);
        }
    },

    setComment() {
        const zReport = this.get('ZReportNumber');
        if (this.get('Id') == 0 && this.isRetailRevenue() && zReport && !this.get('isCommentsChange')) {
            this.set('Comments', `Z-отчет (БСО) № ${zReport}`);
        }
    }
});
