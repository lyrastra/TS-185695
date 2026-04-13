import {
    observable, action, reaction, toJS, runInAction, computed, makeObservable
} from 'mobx';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import { getMappedWorkerListAsync, getWorkerChargePaymentsAsync } from '../../../../../../../../../services/newMoney/workerChargesService';
import ChargePaymentModel from './ChargePaymentModel';
import ContractTypesEnum from '../../../../../../../../../enums/newMoney/ContractTypesEnum';
import { validateWorker } from '../../../../../validation/workerChargesValidator';

const defaultChargeModel = {
    ChargeId: 0,
    Description: `Без начисления`,
    Sum: 0
};

class WorkerChargeModel {
    @observable Charges = [];

    @observable WorkerId = 0;

    @observable WorkerName = ``;

    @observable WorkerTaxationSystemType = 0;

    @observable chargesList = [{ ...defaultChargeModel }];

    @observable chargesListLoaded = false;

    @observable errorMessage = ``;

    @observable loading = true;

    index = getUniqueId();

    constructor(data = {}, operationStore) {
        const { ChargePayments = [], Employee = { Id: null, Name: `` }, TaxationSystem } = data;
        makeObservable(this);

        this.operationStore = operationStore;
        this.WorkerTaxationSystemType = TaxationSystem;
        this.WorkerId = Employee?.Id;
        this.WorkerName = Employee?.Name;

        this.initReactions();

        if (this.operationStore.canEdit) {
            this.loadChargesListAsync()
                .then(() => {
                    this.initCharges(ChargePayments);
                    this.setLoading(false);
                });

            return;
        }

        this.initCharges(ChargePayments);
        this.setLoading(false);
    }

    initReactions = () => {
        reaction(() => [this.WorkerId], this.loadChargesListAsync);

        reaction(() => [
            this.operationStore.model.Date,
            this.operationStore.model.UnderContract,
            this.WorkerId,
            ...(this.Charges.map(charge => charge.ChargeId)),
            ...(this.Charges.map(charge => charge.Sum))
        ], this.operationStore.handleDescriptionMessage);
        reaction(() => this.Charges.map(charge => charge.Sum), this.operationStore.setSum);
    }

    initCharges = ChargePayments => {
        if (ChargePayments.length > 0 && !this.operationStore.isCopy()) {
            ChargePayments.map(charge => this.addCharge(charge, true));
            this.closeAllChargesDropdownsForce();
        } else {
            this.addCharge();
        }
    }

    getDataForWorkerAutocomplete = async ({ query }) => {
        if (!query && this.WorkerId) {
            this.updateWorkerCharge();
        }

        const data = await getMappedWorkerListAsync(this.getDataForWorkerAutocompleteRequest(query));

        return {
            data,
            value: query
        };
    }

    getDataForWorkerAutocompleteRequest = query => {
        const { model } = this.operationStore;

        return {
            date: model.Date,
            includeNoStaff: model.UnderContract !== ContractTypesEnum.Employment.value,
            exclude: this.operationStore.getAddedWorkersIdsArray(this.WorkerId),
            onlyWorking: false,
            count: 5,
            query
        };
    }

    toJS = () => {
        const {
            Charges, WorkerId, WorkerName, WorkerTaxationSystemType
        } = this;

        return toJS({
            Employee: {
                Id: WorkerId,
                Name: WorkerName
            },
            EmployeeId: WorkerId,
            TaxationSystem: WorkerTaxationSystemType,
            ChargePayments: Charges.filter(chargeModel => chargeModel.Sum > 0).map(chargeModel => chargeModel.toJS())
        });
    }

    loadChargesListAsync = async () => {
        const { BaseDocumentId, UnderContract } = this.operationStore.model;
        const data = {
            documentBaseId: BaseDocumentId || null,
            workerId: this.WorkerId,
            workerPaymentType: UnderContract === ContractTypesEnum.Employment.value ? 0 : UnderContract // вот такой разлет в енамах, лучше бы пофиксить при склейке
        };
        const chargesList = await getWorkerChargePaymentsAsync(data);

        runInAction(() => {
            this.chargesList = [...chargesList, defaultChargeModel];
            this.chargesListLoaded = true;
        });
    }

    @action fullWorkerValidation = () => {
        this.errorMessage = validateWorker(this);

        this.WorkerId && this.Charges.forEach(chargeModel => chargeModel.validateChargeModel());
    }

    /** addingByBulk - флаг, указывающий, добавляется ли начисление из цикла
     * обнулять состояние дропдаунов в цикле накладно и смысла не имеет.
     * сброс лучше непосредственно после цикла
     * */
    @action addCharge = (data = defaultChargeModel, addingByBulk = false) => {
        !addingByBulk && this.closeAllChargesDropdownsForce();
        this.Charges.push(new ChargePaymentModel(data, this));
    }

    /** если открыли дропдаун, но ничего не выбрали, изменить isOpen не можем
     * т.к. у Dropdown нет коллбеков на onClickOutside и onBlur.
     * приходится у всех вручную закрывать */
    @action closeCurrentWorkerChargesDropdowns = () => {
        this.Charges.forEach(charge => charge.closeDropdownForce());
    }

    @action setLoading = value => {
        this.loading = !!value;
    }

    @action clearCharge = chargeModel => {
        this.closeAllChargesDropdownsForce();

        if (this.Charges.length === 1) {
            chargeModel.update();
            this.operationStore.handleWorkerRemove(this);
        }

        if (this.Charges.length > 1) {
            this.Charges.remove(chargeModel);
        }
    }

    @action clearAllCharges = () => {
        this.chargesList = [{ ...defaultChargeModel }];
        this.chargesListLoaded = false;
        this.Charges = [];
        this.addCharge();
    }

    @action updateWorkerCharge = ({ original = {} } = {}) => {
        this.WorkerId = original.Id || 0;
        this.WorkerName = original.Name || ``;
        this.WorkerTaxationSystemType = original.TaxationSystemType || 0;

        if (!this.WorkerId) {
            this.clearAllCharges();
        }

        if (this.errorMessage.length) {
            this.errorMessage = validateWorker(this);
        }
    }

    @action clearDefaultChargeSum = () => {
        const defaultCharge = this.chargesList.find(charge => charge.ChargeId === 0);
        defaultCharge.Sum = 0;
    }

    @action closeAllChargesDropdownsForce = () => {
        this.operationStore.closeAllWorkersChargesDropdownsForce();
    };

    @computed get hasChargesWithSum() {
        return this.Charges.some(payment => payment.Sum > 0);
    }
}

export default WorkerChargeModel;
