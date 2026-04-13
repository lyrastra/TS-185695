import {
    observable, action, computed, toJS, makeObservable
} from 'mobx';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import { toFinanceString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import { validateChargeSum } from '../../../../../validation/workerChargesValidator';

class ChargePaymentModel {
    @observable ChargeId = 0;

    @observable Description = `Без начисления`;

    @observable Sum = 0;

    @observable Salary = 0;

    @observable DeductionSum = 0;

    @observable loading = false;

    @observable isOpen = false;

    @observable errorMessage = ``;

    index = getUniqueId();

    ChargePaymentId = null;

    constructor(data, store) {
        makeObservable(this);
        this.store = store;
        this.initCharge(data);
        this.updateSalary();
    }

    toJS = () => {
        const {
            ChargeId, Description, Sum, ChargePaymentId
        } = this;

        return toJS({
            ChargeId: ChargeId || null,
            ChargePaymentId: ChargePaymentId || null,
            Description,
            Sum
        });
    }

    getSum = sumByRateOfSalary => {
        if (!this.Sum) {
            return sumByRateOfSalary;
        }

        return Math.min(this.Sum, sumByRateOfSalary);
    }

    findCurrentChargeInUnbinded = (id = this.ChargeId) => {
        return this.store.chargesList.find(payment => payment.ChargeId === id);
    }

    @action initCharge = data => {
        const isCurrentChargeBinded = !this.findCurrentChargeInUnbinded(data?.ChargeId);

        /** Не зарплатный проект и не закрытый период!
         *
         * Когда у нас есть несколько неоплаченных операций с одинаковым начислением
         * и одной из них присваевается статус "Оплачено", начисление пропадает из дропдауна и выставляется следующее по списку.
         * Принято решение сбрасывать в таком случае до "без начисления" */

        if (this.store.operationStore.canEdit && isCurrentChargeBinded) {
            this.update();

            return;
        }

        this.update(data);
    }

    @action toggleLoading = () => {
        this.loading = !this.loading;
    }

    @action loadChargePaymentsList = async ({ isOpen }) => {
        if (!isOpen && this.store.WorkerId && this.getChargePaymentsListForDropdown.length < 2) {
            this.toggleLoading();
            await this.store.loadChargesListAsync();
            this.toggleLoading();
            this.isOpen = true;
        }
    }

    @action update = ({
        ChargeId = 0, Description = `Без начисления`, Sum = 0, ChargePaymentId = null, DeductionSum = 0
    } = {}) => {
        this.ChargeId = ChargeId;
        this.Description = Description;
        this.ChargePaymentId = ChargePaymentId;
        this.DeductionSum = DeductionSum;
        this.Sum = this.getSum(Sum);

        this.store.closeAllChargesDropdownsForce();
    }

    @action setChargePayment = ({ value }) => {
        const selectedPayment = this.findCurrentChargeInUnbinded(value);

        if (!this.ChargeId && !selectedPayment.ChargeId) {
            Object.assign(selectedPayment, { Sum: this.Sum });
        } else {
            this.store.clearDefaultChargeSum();
        }

        this.update(selectedPayment);
        this.updateSalary();
        this.store.closeAllChargesDropdownsForce();
        this.validateChargeModel();
    }

    @action setSum = ({ value }) => {
        const Sum = toFloat(value);

        try {
            this.store.operationStore.closeAllWorkersChargesDropdownsForce();
        } catch (e) {
            throw new Error(e);
        }

        this.Sum = Sum || 0;
        this.validateChargeModel();
    }

    @action updateSalary = () => {
        if (!this.ChargeId) {
            return;
        }

        const currentCharge = this.findCurrentChargeInUnbinded();

        this.Salary = currentCharge ? currentCharge.Sum : 0;
    }

    @action validateChargeModel = () => {
        if (this.store.WorkerId) {
            this.errorMessage = validateChargeSum(this);
        } else {
            this.errorMessage = ``;
        }
    }

    @action closeDropdownForce = () => {
        this.isOpen = false;
    }

    @computed get isError() {
        return !!this.errorMessage;
    }

    @computed get formattedSum() {
        return this.Sum > 0 ? toFinanceString(this.Sum) : ``;
    }

    @computed get formattedSalary() {
        return this.Salary > 0 ? toFinanceString(this.Salary) : ``;
    }

    @computed get getChargePaymentsListForDropdown() {
        const {
            Charges, chargesList, chargesListLoaded, WorkerId
        } = this.store;
        const selectedPaymentsIdsArray = Charges
            .filter(payment => payment.ChargeId !== 0 && payment.ChargeId !== this.ChargeId)
            .map(payment => payment.ChargeId);

        if (chargesListLoaded && WorkerId) {
            return chargesList
                .filter(payment => !selectedPaymentsIdsArray.includes(payment.ChargeId))
                .map(payment => ({
                    value: payment.ChargeId,
                    text: payment.Description
                }));
        }

        return [
            {
                value: this.ChargeId,
                text: this.Description
            }
        ];
    }
}

export default ChargePaymentModel;
