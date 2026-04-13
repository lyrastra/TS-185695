import {
    observable, action, computed, toJS, runInAction, makeObservable
} from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper, { DateFormat } from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Actions from './Actions';
import {
    getAccountCodesAsync,
    getUnifiedBudgetaryPaymentDescriptionBySubPayments
} from './services/unifiedBudgetaryPaymentService';
import { getIsFromImport } from '../../../../../../../../../services/newMoney/newMoneyOperationService';
import UnifiedBudgetaryPaymentRowStore from '../UnifiedBudgetaryPaymentRowStore/UnifiedBudgetaryPaymentRowStore';
import OpenOperationActions from '../../../../../../../../../enums/newMoney/OpenOperationActionsEnum';
import { isAvailableOperationStateToReloadDescription } from './helpers/descriptionReloadHelper';

export default class UnifiedBudgetaryPaymentStore extends Actions {
    @observable SubPayments = [];

    @observable taxDistributionRemain = 0;

    @observable validationState = {
        Sum: ``
    }

    isFromImport = false;

    canReloadDescription = false;

    TaxationSystem = {};

    Requisites = {};

    TradingObjectList = [];

    accountCodes = [];

    subPaymentOptions = {};

    constructor(props) {
        super(props);
        makeObservable(this);

        this.budgetaryPaymentModel = props.budgetaryPaymentModel;
        this.TaxationSystem = props.TaxationSystem;
        this.Requisites = props.Requisites;
        this.TradingObjectList = props.TradingObjectList;
        this.patents = props.patents;
        this.canEdit = props.canEdit;
        this.budgetaryPaymentStoreMethods = props.budgetaryPaymentStoreMethods;

        this.canReloadDescription = this.isNew || isAvailableOperationStateToReloadDescription(this.budgetaryPaymentModel.OperationState);
        this.reloadDescription();
    }

    onChangeSubPaymentSum = () => {
        if (this.isFromImport) {
            const diff = toFloat(`${this.budgetaryPaymentModel.Sum - this.subPaymentsTotalSum}`, 2);

            runInAction(() => {
                this.taxDistributionRemain = diff;
            });

            this.budgetaryPaymentModel.Sum === this.subPaymentsTotalSum && this.budgetaryPaymentStoreMethods.validateField(`Sum`);
        } else {
            this.budgetaryPaymentStoreMethods.setSum({ value: this.subPaymentsTotalSum });
        }
    }

    async init() {
        this.accountCodes = await getAccountCodesAsync();

        this.subPaymentOptions = {
            budgetaryPaymentModel: this.budgetaryPaymentModel,
            TaxationSystem: this.TaxationSystem,
            Requisites: this.Requisites,
            TradingObjectList: this.TradingObjectList,
            patents: this.patents,
            accountCodes: this.accountCodes,
            canEdit: this.canEdit,
            modelForSave: this.budgetaryPaymentStoreMethods.modelForSave,
            store: this
        };

        await this.checkIsFromImport();
        await this.initSubPayments(this.subPaymentOptions);
    }

    validateAll = () => {
        this.SubPayments.forEach(payment => payment.validateAll());
    }

    checkIsFromImport = async () => {
        const { DocumentBaseId } = this.budgetaryPaymentModel;

        if (!DocumentBaseId) {
            return;
        }

        this.isFromImport = await getIsFromImport(DocumentBaseId);
    }

    @action reloadDescription = async () => {
        if (!this.canReloadDescription) {
            return;
        }

        const result = await getUnifiedBudgetaryPaymentDescriptionBySubPayments({ params: this.subPaymentsForSave, date: dateHelper(this.budgetaryPaymentModel.Date).format(DateFormat.iso) });

        runInAction(() => {
            this.budgetaryPaymentStoreMethods.setDescription(result);
        });
    }

    @action addEmptySubPayment = () => {
        this.SubPayments.push(UnifiedBudgetaryPaymentRowStore.getDefaultFilled(this.subPaymentOptions));
    }

    @action deleteSubtax = subtaxStore => {
        this.SubPayments.remove(subtaxStore);
        this.onChangeSubPaymentSum();
        this.reloadDescription();
    }

    @action initSubPayments = async options => {
        const { SubPayments } = this.subPaymentOptions.budgetaryPaymentModel;

        if (!SubPayments.length) {
            this.SubPayments = [UnifiedBudgetaryPaymentRowStore.getDefaultFilled({ ...options })];

            return;
        }

        const subPaymentsFromModel = [];

        // eslint-disable-next-line no-restricted-syntax
        for await (const subPayment of SubPayments) {
            subPaymentsFromModel.push(await UnifiedBudgetaryPaymentRowStore.getFromModel({ subPayment, options }));
        }

        runInAction(() => {
            this.SubPayments = subPaymentsFromModel;
        });
    }

    @computed get subPaymentsForSave() {
        return this.SubPayments.filter(subPayment => !subPayment.isEmpty).map(subPayment => {
            return toJS(subPayment.getSubPaymentForSave);
        });
    }

    @computed get subPaymentsTotalSum() {
        return toFloat(this.SubPayments.reduce((acc, current) => {
            const { Sum } = current.model;

            if (Sum) {
                return acc + Sum;
            }

            return acc;
        }, 0), 2);
    }

    @computed get isSubPaymentsValid() {
        return !this.SubPayments.some(payment => !payment.isValid);
    }

    @computed get dirtySubPayments() {
        return this.SubPayments.filter(payment => payment.isSomeFieldDirty);
    }

    @computed get canShowAddSubPayment() {
        return this.SubPayments.length < 30;
    }

    @computed get isNew() {
        return this.budgetaryPaymentModel.Action === OpenOperationActions.new;
    }

    isValid() {
        this.validateAll();

        return !this.dirtySubPayments.some(payment => !payment.isValid);
    }
}
