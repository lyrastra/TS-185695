/* eslint-disable */

import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { cashOrderOperationResources } from '../../../../../../../resources/MoneyOperationTypeResources';

const cash = window.Cash;
const common = window.Common;
cash.Models.OutgoingCashOrder = cash.Models.BaseCashOrder.extend({
    documentName: 'Списание',

    initialize() {
        this.on('change:OperationType', this.cleanModel);
    },

    defaults() {
        const today = dateHelper().format('DD.MM.YYYY');
        return {
            Title: 'Списание',
            Date: today,
            KontragentAccountCode: '600200',
            OperationType: cashOrderOperationResources.CashOrderOutgoingPaymentSuppliersForGoods.value,
            PaybillDate: today,
            PaybillNumber: 12,
            NdsType: common.Data.BankAndCashNdsTypes.Nds20
        };
    },

    url() {
        if (this.get('isCopy')) {
            return cash.Data.CopyCashOrder;
        }

        if (this.get('isFromContract')) {
            const contractId = this.get('ProjectId');
            return `/Accounting/FirmCash/OutgoingOrderFromContract?contractId=${contractId}`;
        }

        return cash.Data.GetOutgoingCashOrder;
    },


    isOsno() {
        try {
            const date = Converter.toDate(this.get('Date'));
            const taxObject = Common.Utils.CommonDataLoader.TaxationSystems.Current(date);
            return taxObject.get('IsOsno');
        } catch(e) {
            console.error(e);
        }
    },


    isKontragentPayment() {
        const operationType = parseInt(this.get('OperationType'), 10);
        const isPaymentSuppliers = operationType === cashOrderOperationResources.CashOrderOutgoingPaymentSuppliersForGoods.value;
        const isReturnToBuyer = operationType === cashOrderOperationResources.CashOrderOutgoingReturnToBuyer.value;
        const isPaymentAgencyContract = operationType === cashOrderOperationResources.CashOrderOutgoingPaymentAgencyContract.value;


        return isPaymentSuppliers || isReturnToBuyer || isPaymentAgencyContract;
    },

    isWorkerPayment() {
        return this.get('OperationType') == cashOrderOperationResources.CashOrderOutgoingIssuanceAccountablePerson.value;
    },

    isOtherPayment() {
        return this.get('OperationType') == cashOrderOperationResources.CashOrderOutgoingOther.value;
    },

    isSalaryPayment() {
        return this.get('OperationType') == cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value;
    },

    cleanModel() {
        this.set('Documents',[]);
        if (this.get('Sum') === 0) {
            this.unset('Sum');
        }
    }
});
