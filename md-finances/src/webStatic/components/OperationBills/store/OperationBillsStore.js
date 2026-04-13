import {
    observable, action, computed, reaction, makeObservable
} from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import BillModel from './BillModel';
import operationBillsService from '../operationBillsService';

const errors = {
    local: {
        moreThanBillSum: `Нельзя указать сумму более неоплаченного остатка по счету.`,
        wrongSumField: `Не правильный формат суммы(правильно 1234.56)`,
        noRemains: `У этого счета нет не оплаченного остатка`
    },
    common: {
        moreThanOperationSum: `Нельзя указать сумму более суммы операции.`
    },
    default: `Неизвестная ошибка`
};

export default class OperationBillsStore {
    contractBaseId;

    kontragentId = 0;

    errorMessages = errors;
    @observable operationSum = 0;
    @observable bills = [];
    @observable state = {
        commonError: {
            visible: false,
            message: ``
        }

    }

    constructor({
        handlers, operationSum = 0, billsArray, kontragentId = null, baseDocumentId, contractBaseId, isNew
    } = {}) {
        this.isNew = isNew;
        this.baseDocumentId = baseDocumentId;
        this.operationSum = operationSum;
        this.kontragentId = kontragentId;
        this.contractBaseId = contractBaseId;
        this.handlers = handlers;
        this.initBills(billsArray);

        makeObservable(this);

        [`Id`, `CurrentSum`].forEach((field => {
            reaction(() => this.bills.map(bill => bill[field]), () => {
                this.operationSum > 0 && this.bills.forEach(bill => {
                    this.validateBillSum(bill);
                });
            });
        }));

        reaction(() => this.operationSum, () => {
            this.operationSum > 0 && this.recalculateBills();
        });
    }

    @action updateContract(contractBaseId) {
        this.contractBaseId = contractBaseId;
    }

    @action updateKontragent(kontragentId = null) {
        if (!kontragentId || (this.kontragentId && this.kontragentId !== kontragentId)) {
            this.clearBills();
        }

        this.kontragentId = kontragentId;
    }

    @action clearBills() {
        this.bills.clear();
        this.addEmptyBill();
    }

    @action updateSum(value) {
        this.operationSum = value;
    }

    @action initBills(billsArray = []) {
        const store = this;

        !billsArray.length && billsArray.push({});

        this.bills = billsArray.map((bill, index) => {
            return BillModel.fromJS(store, index, bill);
        });
    }

    @action recalculateBills() {
        if (this.currentBillsSum >= this.operationSum && this.billsCount > 1) {
            this.reduceBills();
        } else {
            this.refillBills();
        }
    }

    @action reduceBills() {
        const { bills } = this;
        let { operationSum } = this;

        for (let i = 0; i < bills.length; i += 1) {
            const availableBillSum = bills[i].Sum - bills[i].PayedSum;
            const result = operationSum >= availableBillSum ? availableBillSum : operationSum;

            if (result > 0) {
                bills[i].CurrentSum = toFloat(result.toFixed(2), true);
                operationSum -= result;
            } else {
                bills[i].removeBill();
            }
        }
    }

    @action refillBills() {
        let availableTotal = this.operationSum;

        this.bills = this.bills.map(bill => {
            const currentBill = bill;
            const { Sum, PayedSum } = currentBill;
            const availableSum = Sum - PayedSum;

            if (availableSum > 0) {
                const refillSum = availableSum > availableTotal ? availableTotal : availableSum;

                currentBill.CurrentSum = refillSum;
                availableTotal -= refillSum;
            }

            return currentBill;
        });
    }

    @action addEmptyBill() {
        const index = this.billsCount;
        this.bills.push(new BillModel(this, index, {}));
    }

    @action setCommonError(message = this.errorMessages.default) {
        this.state.commonError = {
            visible: true,
            message
        };
    }

    @action unsetCommonError() {
        this.state.commonError = {
            visible: false,
            message: ``
        };
    }

    @computed get billsCount() {
        return this.bills.length;
    }

    @computed get readyBillsList() { // for setting bills to model
        return this.bills.filter(bill => bill.Id !== 0 && bill.CurrentSum !== 0);
    }

    @computed get cleanBillsList() { // for internal use
        return this.bills.filter(bill => bill.Id !== 0);
    }

    @computed get existingBillsArray() {
        return this.cleanBillsList.map(bill => bill.Id);
    }

    @computed get getTemporaryBillsList() {
        let BillsSum = 0;
        const Bills = this.cleanBillsList.map(({
            Date, DocumentBaseId, Id, Number, Sum, CurrentSum
        }) => {
            BillsSum += CurrentSum;

            return {
                PayedSum: toFloat(Sum >= CurrentSum ? CurrentSum : 0, true),
                Date,
                DocumentBaseId,
                Id,
                Number,
                Sum
            };
        });

        return {
            Bills,
            BillsSum
        };
    }

    @computed get getBillsList() {
        let BillsSum = 0;
        const Bills = this.readyBillsList.map(({
            Date, DocumentBaseId, Id, Number, Sum, KontragentId, CurrentSum
        }) => {
            BillsSum += CurrentSum;

            return {
                PayedSum: toFloat(CurrentSum, true),
                Date,
                DocumentBaseId,
                Id,
                Number,
                Sum,
                KontragentId
            };
        });

        return {
            Bills,
            BillsSum: toFloat(BillsSum, true)
        };
    }

    @computed get totalBillsSum() {
        return this.cleanBillsList.reduce((acc, current) => {
            return acc + current.PayedSum;
        }, 0);
    }

    @computed get currentBillsSum() {
        return this.cleanBillsList.reduce((acc, current) => {
            return acc + toFloat(current.CurrentSum);
        }, 0);
    }

    @action validateBillSum(bill = {}) {
        const { operationSum, currentBillsSum, errorMessages } = this;
        const {
            CurrentSum, maxPayedSum, Id
        } = bill;

        if (Id && (CurrentSum > maxPayedSum)) {
            bill.setError(errorMessages.local.moreThanBillSum);
        } else {
            bill.unsetError();
        }

        if (currentBillsSum > operationSum) {
            this.setCommonError(errorMessages.common.moreThanOperationSum);
        } else {
            this.unsetCommonError();
        }
    }

    async getBillsAutocomplete({ Name = ``, Id = 0 } = {}) {
        const {
            kontragentId, existingBillsArray, baseDocumentId, contractBaseId
        } = this;
        const data = {
            excludeIds: existingBillsArray.filter(bill => bill !== Id),
            baseDocumentId,
            kontragentId,
            contractBaseId,
            query: Name
        };
        const { List } = await operationBillsService.get(data);

        return List;
    }
}

