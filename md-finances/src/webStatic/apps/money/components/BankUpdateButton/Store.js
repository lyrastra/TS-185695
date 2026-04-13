import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import _ from 'underscore';
import {
    observable, computed, action, makeObservable
} from 'mobx';
import localDateHelper from '../../../../helpers/date';

class Store {
    @observable startDate;

    @observable endDate;

    @observable minDate;

    @observable closedPeriodDate;

    @observable errorMessage = {
        startDate: null,
        endDate: null,
        common: null
    };

    constructor({ startDate, endDate }) {
        makeObservable(this);
        this.startDate = startDate;
        this.endDate = endDate;
    }

    @action setDate({ startDate, endDate }) {
        this.startDate = startDate;
        this.endDate = endDate;
    }

    @action setClosePeriodDate(closedPeriodDate) {
        this.closedPeriodDate = closedPeriodDate;
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
        let startDate;
        let endDate;
        let common;

        if (startVal) {
            startDate = this.getDateValidationMessage(startVal);
        }

        if (endVal) {
            endDate = this.getDateValidationMessage(endVal);

            if (!startVal) {
                startDate = `Укажите дату начала`;
            }
        }

        if (!startDate && !endDate && endVal && startVal) {
            if (dateHelper(endVal, `DD.MM.YYYY`).isBefore(dateHelper(startVal, `DD.MM.YYYY`))) {
                common = `Дата окончания раньше даты начала`;
            }
        }

        if (this.dateFromClosedPeriod(this.startDate)) {
            common = `Дата из закрытого периода`;
        }

        if (!this.dateFromClosedPeriod(this.startDate) && (this.dateBeforeMinDate(this.startDate) || this.dateBeforeMinDate(this.endDate))) {
            common = `Ранее даты регистрации или ввода остатков`;
        }

        return this.setErrorMessage({ startDate, endDate, common });
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
        const message = this.validationMessage;

        return !message.startDate && !message.endDate && !message.common;
    }

    getDateValidationMessage(value, closedPeriodDate) {
        if (value) {
            if (!localDateHelper.isValid(value)) {
                return `Неверный формат даты`;
            }

            if (dateHelper(value, `DD.MM.YYYY`).isAfter(dateHelper())) {
                return `Некорректная дата`;
            }

            if (closedPeriodDate && this.dateFromClosedPeriod(value)) {
                return `Дата из закрытого периода`;
            }
        }

        return null;
    }

    dateFromClosedPeriod = value => {
        const { closedPeriodDate } = this;
        const date = dateHelper(value, `DD.MM.YYYY`);
        const closedPeriodDateValue = dateHelper(closedPeriodDate);

        return date.isSameOrBefore(closedPeriodDateValue);
    };

    dateBeforeMinDate = value => {
        return dateHelper(value, `DD.MM.YYYY`).isBefore(dateHelper(this.minDate));
    }
}

export default Store;
