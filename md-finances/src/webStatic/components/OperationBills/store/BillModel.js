import * as mobx from 'mobx';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';

const {
    observable, action, computed, makeObservable
} = mobx;

class BillModel {
    store;

    index;

    Sum;

    Date;

    Number;

    DocumentBaseId;

    PayedSum;

    @observable state = {
        loading: false,
        autocompleteOpen: false,
        error: false,
        errorMsg: ``
    };
    @observable Id;
    @observable Name;
    @observable CurrentSum;
    @observable autocompleteList = [];

    constructor(
        store,
        index,
        {
            Date = ``, DocumentBaseId = 0, Id = 0, Number = ``, PayedSum = 0, Sum = 0, CurrentPayedSum = 0, Name = ``
        } = {}
    ) {
        makeObservable(this);
        this.store = store;
        this.index = index;
        this.Id = Id;
        this.Sum = Sum;
        this.Date = Date;
        this.initialName = Name || (Number ? `№ ${Number} от ${Date}` : ``);
        this.Name = this.initialName;
        this.Number = Number;
        this.maxPayedSum = Sum - PayedSum;
        this.PayedSum = PayedSum;
        this.CurrentSum = CurrentPayedSum;
        this.DocumentBaseId = DocumentBaseId;
    }

    @action checkBill() {
        if (this.Name.length > 5) {
            this.Name = this.initialName;
        } else {
            this.clearBill();
        }
    }

    @action async getBillsAutocomplete() {
        this.autocompleteList = await this.store.getBillsAutocomplete(this);
        this.state.loading = false;
        this.state.autocompleteOpen = true;
    }

    @action clearAutocomplete() {
        this.state.autocompleteOpen = false;
        this.autocompleteList = [];
    }

    @action removeBill() {
        if (this.store.bills.length > 1) {
            this.store.bills.remove(this);
        } else {
            this.clearBill();
        }

        this.unsetError();
    }

    @action clearBill() {
        this.Id = 0;
        this.Sum = 0;
        this.Date = ``;
        this.Name = ``;
        this.Number = 0;
        this.CurrentSum = 0;
        this.maxPayedSum = 0;
        this.unsetError();
    }

    @action setBill({
        Date, DocumentBaseId, Id, Number, PayedSum, Sum, KontragentId
    }) {
        this.Id = Id;
        this.Sum = Sum;
        this.Date = Date;
        this.Number = Number;
        this.initialName = Number ? `№ ${Number} от ${Date}` : ``;
        this.Name = this.initialName;
        this.DocumentBaseId = DocumentBaseId;
        this.CurrentSum = 0;
        this.makeSumForNewBill({ PayedSum, Sum });

        KontragentId && this.store.handlers.setKontragentFromBill(KontragentId);
    }

    @action makeSumForNewBill({ PayedSum, Sum } = {}) {
        const { operationSum, currentBillsSum } = this.store;
        const availableSum = operationSum - currentBillsSum;
        const maxPayedSum = Sum - PayedSum;
        let resultSum = 0;

        if (availableSum > 0) {
            resultSum = availableSum >= maxPayedSum ? maxPayedSum : availableSum;
        }

        this.PayedSum = PayedSum;
        this.CurrentSum = resultSum;
        this.maxPayedSum = maxPayedSum;
    }

    @action onBlurSumField(value = 0) {
        const validSum = toFloat(value === `` ? 0 : value);

        if (validSum !== false) {
            this.CurrentSum = validSum;
        }
    }

    @action onChangeSumField(value = ``) {
        if (!value.length) {
            this.unsetError();

            return;
        }

        if (toFloat(value) !== false) {
            this.unsetError();
        } else {
            this.setError(this.store.errorMessages.local.wrongSumField);
        }
    }

    @action setError(msg = this.store.errorMessages.default) {
        if (!this.state.error) {
            this.state.error = true;
            this.state.errorMsg = msg;
        }
    }

    @action unsetError() {
        if (this.state.error) {
            this.state.error = false;
            this.state.errorMsg = ``;
        }
    }

    @computed get getSum() {
        const { CurrentSum } = this;

        return CurrentSum ? toAmountString(CurrentSum) : ``;
    }

    static fromJS(store, index, bill) {
        return new BillModel(store, index, bill);
    }
}

export default BillModel;
