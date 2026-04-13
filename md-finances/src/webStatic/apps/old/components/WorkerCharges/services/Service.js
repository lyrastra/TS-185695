import { get } from '@moedelo/md-frontendcore/mdCore/HttpClient';
import ChargesStore from '../stores/chargesStore';
import typeConverter from '../helpers/typeConverter';

export default class Service {
    constructor({
        paymentType,
        documentBaseId,
        workerId,
        collection
    }) {
        this.workerPaymentType = typeConverter.getTypeByUnderContract(paymentType);
        this.documentBaseId = documentBaseId;
        this.workerId = workerId;
        this.collection = collection;
        this.store = new ChargesStore();
    }

    getCharges({ excluded }) {
        const storedList = this._getStoreVal();

        if (storedList) {
            return Promise.resolve(this._getChargesList(excluded));
        } else
        if (this.workerId) {
            return this._loadCharges(excluded);
        }
        
        return Promise.resolve(this.getDefaultCharger());
    }

    _loadCharges(excluded) {
        const service = this;

        return get(`/Payroll/ChargePayments/GetUnboundForWorker`, {
            workerId: this.workerId,
            workerPaymentType: this.workerPaymentType,
            documentBaseId: this.documentBaseId
        })
            .then(response => {
                return service._onLoadCharges(response, excluded);
            });
    }

    _onLoadCharges(response, excluded) {
        if (response) {
            const list = response.WorkerChargePayments[0] ? response.WorkerChargePayments[0].ChargePayments : [];

            this.store.set({
                workerId: this.workerId,
                paymentType: this.workerPaymentType,
                list
            });

            return this._getChargesList(excluded);
        }
        
        return this.getDefaultCharger();
    }

    getDefaultCharger(data) {
        let list = [this._getWithoutCharge()];

        if (data && data.Description) {
            list = [data, ...list];
        }

        return list.map(obj => {
            return { text: obj.Description, value: obj.ChargeId, sum: obj.Sum };
        });
    }

    _getWithoutCharge() {
        return { Description: `Без начисления`, ChargeId: 0 };
    }

    _getChargesList(excludedCharge) {
        const list = this._getStoreVal();
        const existingIds = this.collection.getChargesIds();
        let newList = [...list, ...[this._getWithoutCharge()]];

        newList = newList.filter(({ ChargeId }) => {
            return !existingIds.includes(ChargeId) || excludedCharge === ChargeId;
        });

        newList = newList.map(({ Description, ChargeId, Sum }) => {
            return {
                text: Description || ``,
                value: ChargeId,
                sum: Sum
            };
        });

        return newList;
    }

    isListLoaded() {
        return this._getStoreVal();
    }

    _getStoreVal() {
        return this.store && this.store.get({
            workerId: this.workerId,
            paymentType: this.workerPaymentType
        });
    }

    getWorkerId() {
        return this.workerId;
    }

    clear() {
        this.store.clear();
    }
}
