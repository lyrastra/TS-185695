import {
    observable, computed, action, makeObservable
} from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import _ from 'underscore';
import localDateHelper from '../../../../../helpers/date';

class Store {
    @observable startDate;

    @observable endDate;

    @observable minDate;

    @observable errorMessage = {
        startDate: null,
        endDate: null,
        common: null
    };

    balanceDate;

    isBalanceDateError;

    constructor({ startDate, endDate }) {
        makeObservable(this);
        this.startDate = startDate;
        this.endDate = endDate;
    }

    @action setBalanceDate(balanceDate) {
        this.balanceDate = balanceDate;
    }

    @action setDate({ startDate, endDate }) {
        this.startDate = startDate;
        this.endDate = endDate;
    }

    @action setMinDate(...args) {
        const dateArr = args.map(item => item && dateHelper(item));

        this.minDate = _.max(dateArr);
    }

    @action setErrorMessage({ startDate, endDate, common }) {
        this.errorMessage = {
            startDate,
            endDate,
            common
        };
    }

    @action changeErrorMessage() {
        const value = this.valueByPeriod;
        const startVal = value.startDate;
        const endVal = value.endDate;
        const startDate = this.validateStartDate(startVal);
        const endDate = this.getDateValidationMessage(endVal);
        let common;
        this.isBalanceDateError = false;

        if (!startDate && !endDate && endVal && startVal) {
            if (dateHelper(endVal, `DD.MM.YYYY`).isBefore(dateHelper(startVal, `DD.MM.YYYY`))) {
                common = `Дата окончания раньше даты начала`;
            }

            if (this.balanceDate && dateHelper(startVal).isBefore(dateHelper(this.balanceDate, `DD.MM.YYYY`))) {
                common = `Не может быть ранее даты остатков`;
                this.isBalanceDateError = true;
            } else if (this.differentYears()) {
                common = `Укажите даты одного года`;
            }
        }

        this.setErrorMessage({ startDate, endDate, common });
    }

    @computed get isValidBalanceDate() {
        return this.isBalanceDateError;
    }

    @computed get valueByPeriod() {
        const { startDate, endDate } = this;
        const period = {};

        if (!localDateHelper.isEmpty(startDate)) {
            period.startDate = startDate;
        }

        if (!localDateHelper.isEmpty(endDate)) {
            period.endDate = endDate;
        }

        return period;
    }

    @computed get isValid() {
        const message = this.errorMessage;

        return !message.startDate && !message.endDate && !message.common;
    }

    getDateValidationMessage = value => {
        if (value) {
            if (!localDateHelper.isValid(value)) {
                return `Неверный формат даты`;
            }
        }

        return null;
    };

    differentYears = () => {
        const { startDate, endDate } = this;
        const startYear = dateHelper(startDate, `DD.MM.YYYY`).year();
        const endYear = dateHelper(endDate, `DD.MM.YYYY`).year();

        return startYear !== endYear;
    };

    validateStartDate = date => {
        if (!date) {
            return `Укажите дату начала`;
        }

        return this.getDateValidationMessage(date)
            || this.isFutureDate(date);
    };

    isFutureDate = date => {
        return dateHelper(date, `DD.MM.YYYY`).isAfter(dateHelper()) ? `Дата из будущего` : null;
    }
}

export default Store;
