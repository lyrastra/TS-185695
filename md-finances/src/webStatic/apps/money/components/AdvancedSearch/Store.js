import {
    observable, computed, action, makeObservable
} from 'mobx';
import _ from 'underscore';
import dateHelper, { DateFormat } from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import CommonPeriodTypeEnum from '../../../../enums/newMoney/CommonPeriodTypeEnum';
import localDateHelper from '../../../../helpers/date';
import sumConditionEnum from '../../../../enums/newMoney/SumConditionEnum';
import { paymentOrderOperationResources } from '../../../../resources/MoneyOperationTypeResources';
import ProvideInTaxEnum from '../../../../enums/newMoney/ProvideInTaxEnum';
import {
    getIdFromValue,
    getTaxationSystemDropdownItems,
    getTaxationSystemDropdownValue, isPatentValue, isTaxationSystemValue
} from './helpers/taxationSystemFilterHelper';

const filterChangeEventId = `poisk_period_change`;

class Store {
    @observable startDate;

    @observable endDate;

    @observable minDate;

    @observable kontragentType;

    @observable kontragentId;

    @observable periodType;

    @observable direction;

    @observable operationType;

    @observable budgetaryType;

    @observable sumCondition;

    @observable sum;

    @observable sumFrom;

    @observable sumTo;

    @observable provideInTax;

    @observable closingDocumentsCondition;

    @observable approvedCondition;

    @observable isCurrencyOperationsAvailable;

    @observable taxationSystemType;

    @observable patentId;

    @observable taxationSystems = [];

    @observable patents = [];

    @observable userData = {};

    @observable isOutsourceUser;

    constructor({
        startDate,
        endDate,
        kontragentType,
        kontragentId,
        direction,
        operationType,
        budgetaryType,
        sumCondition,
        sum,
        sumFrom,
        sumTo,
        provideInTax,
        closingDocumentsCondition,
        taxationSystemType,
        patentId,
        approvedCondition
    }) {
        makeObservable(this);

        this.startDate = startDate;
        this.kontragentId = kontragentId;
        this.endDate = endDate;
        this.kontragentType = kontragentType;
        this.direction = direction || Direction.Default;
        this.operationType = operationType || [];
        this.budgetaryType = budgetaryType;
        this.sumCondition = sumCondition;
        this.sum = sum;
        this.sumFrom = sumFrom;
        this.sumTo = sumTo;
        this.provideInTax = provideInTax;
        this.closingDocumentsCondition = closingDocumentsCondition;
        this.taxationSystemType = taxationSystemType;
        this.patentId = patentId;
        this.approvedCondition = approvedCondition;
    }

    @action setDate({ startDate, endDate }) {
        this.startDate = startDate;
        this.endDate = endDate;
    }

    @action setKontragentValues({ kontragentType, kontragentId }) {
        this.kontragentType = kontragentType;
        this.kontragentId = kontragentId;
    }

    @action setPeriod({ periodType }) {
        this.periodType = periodType;
        this.startDate = null;
        this.endDate = null;

        mrkStatService.sendEventWithoutInternalUser({
            Event: filterChangeEventId,
            st5: `PeriodType - ${periodType.toString()}`
        });
    }

    @action setMinDate(...args) {
        const dateArr = args.map(item => item && localDateHelper.fromIsotoDate(item));
        this.minDate = _.max(dateArr);
    }

    @action setIsCurrencyOperationsAvailable({ isCurrencyAvailable }) {
        this.isCurrencyOperationsAvailable = isCurrencyAvailable;
    }

    @action setSum() {
        this.resetAllSum();

        switch (this.sumCondition) {
            case sumConditionEnum.Less:
            case sumConditionEnum.Great:

            // eslint-disable-next-line no-fallthrough
            case sumConditionEnum.Equal: {
                this.resetRangeSum();

                break;
            }

            case sumConditionEnum.Range: {
                this.setRangeSum();

                break;
            }

            default: {
                this.sum = null;
                this.resetRangeSum();

                break;
            }
        }
    }

    @action setOperationType({ value }) {
        this.operationType = value;

        if (value !== paymentOrderOperationResources.BudgetaryPayment.value) {
            this.budgetaryType = null;
        }
    }

    @action setDirection({ value }) {
        this.direction = value;
    }

    @action setBudgetaryType({ value }) {
        this.budgetaryType = value;
    }

    @action setTaxationSystemFilterValue(value) {
        const id = getIdFromValue(value);
        let taxationSystemType = null;
        let patentId = null;

        if (isTaxationSystemValue(value)) {
            taxationSystemType = id;
        }

        if (isPatentValue(value)) {
            patentId = id;
        }

        this.taxationSystemType = taxationSystemType;
        this.patentId = patentId;
    }

    @action setTaxationSystems(value) {
        this.taxationSystems = value;
    }

    @action setPatents(value) {
        this.patents = value;
    }

    @action setUserData(data) {
        this.userData = data;
    }

    @action setIsOutsourceUser(data) {
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = data;

        this.isOutsourceUser = (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;
    }

    @computed get valueByPeriod() {
        const { startDate, endDate, periodType } = this;

        switch (periodType) {
            case CommonPeriodTypeEnum.Year:
                return localDateHelper.isYear(startDate, endDate) ? localDateHelper.year(startDate) : null;
            case CommonPeriodTypeEnum.Month:
                return getMonthOrQuarter(startDate, endDate);

            case CommonPeriodTypeEnum.Period: {
                const period = {};

                if (!localDateHelper.isEmpty(startDate)) {
                    period.startDate = startDate;
                }

                if (!localDateHelper.isEmpty(endDate)) {
                    period.endDate = endDate;
                }

                return period;
            }

            case CommonPeriodTypeEnum.Day:
                return startDate;
            default:
                return null;
        }
    }

    @computed get isBudgetaryType() {
        return this.operationType.filter(item => {
            return item === paymentOrderOperationResources.BudgetaryPayment.value;
        }).length > 0;
    }

    @computed get validationMessage() {
        const value = this.valueByPeriod;

        switch (this.periodType) {
            case CommonPeriodTypeEnum.Day:
                return this.getDateValidationMessage(value);
            case CommonPeriodTypeEnum.Period:
                return this.getPeriodValidationMessage(value);
            default:
                return null;
        }
    }

    @computed get isValid() {
        const message = this.validationMessage;

        switch (this.periodType) {
            case CommonPeriodTypeEnum.Day:
                return !message;
            case CommonPeriodTypeEnum.Period:
                return !message.startDate && !message.endDate && !message.common;
            default:
                return true;
        }
    }

    @computed get taxationSystemTypeFilterData() {
        const { taxationSystems, patents } = this;
        const taxationSystemItems = getTaxationSystemDropdownItems({
            taxationSystems,
            patents
        });

        return taxationSystemItems;
    }

    @computed get taxationSystemTypeFilterValue() {
        const { taxationSystemType, patentId } = this;

        return getTaxationSystemDropdownValue({ taxationSystemType, patentId });
    }

    @computed get taxationSystemFilterVisible() {
        return this.provideInTax === ProvideInTaxEnum.Taken;
    }

    @computed get isFilterForSourceFirm() {
        const { TransferDate } = this.userData;
        const { endDate, isValid } = this;

        if (!endDate || !isValid) {
            return false;
        }

        if (!this.needCheckFilterForSourceFirm) {
            return false;
        }

        const transferYear = dateHelper(TransferDate, DateFormat.iso).year();

        if (dateHelper(endDate, DateFormat.ru).isBefore(dateHelper(`${transferYear}-01-01`, DateFormat.iso))) {
            return true;
        }

        return false;
    }

    @computed get needCheckFilterForSourceFirm() {
        const { SourceFirmId, TransferDate } = this.userData;

        if (!SourceFirmId || !TransferDate) {
            return false;
        }

        if (![2022, 2023].includes(dateHelper(TransferDate, DateFormat.iso).year())) {
            return false;
        }

        return true;
    }

    @computed get oldDateMessage() {
        if (!this.isFilterForSourceFirm) {
            return null;
        }

        return `Данные будут показаны в старом кабинете`;
    }

    getDateValidationMessage(value) {
        if (value) {
            if (!localDateHelper.isValid(value)) {
                return `Неверный формат даты`;
            }

            if (dateHelper(value, `DD.MM.YYYY`).isBefore(dateHelper(this.minDate))) {
                return `Слишком рано`;
            }
        }

        return null;
    }

    getPeriodValidationMessage(value) {
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

        return { startDate, endDate, common };
    }

    resetRangeSum() {
        this.sumFrom = null;
        this.sumTo = null;
    }

    setRangeSum() {
        if (this.sumFrom === this.sumTo) {
            this.sumCondition = sumConditionEnum.Equal;
            this.sum = this.sumFrom || this.sumTo;
            this.resetRangeSum();
        } else if (this.sumFrom && !this.sumTo) {
            this.sumCondition = sumConditionEnum.Great;
            this.sum = this.sumFrom;
            this.resetRangeSum();
        } else if (!this.sumFrom && this.sumTo) {
            this.sumCondition = sumConditionEnum.Less;
            this.sum = this.sumTo;
            this.resetRangeSum();
        } else {
            this.sumCondition = sumConditionEnum.Range;
            this.sum = null;
        }
    }

    resetAllSum() {
        if (!this.sum && this.sumCondition !== sumConditionEnum.Range) {
            this.sum = null;
            this.sumCondition = sumConditionEnum.Any;
        }

        if (this.sumCondition === sumConditionEnum.Range && !this.sumFrom && !this.sumTo) {
            this.sumCondition = sumConditionEnum.Any;
            this.resetRangeSum();
        }
    }
}

function getMonthOrQuarter(startDate, endDate) {
    if (startDate && endDate) {
        const formatedStartDate = localDateHelper.toDate(startDate);
        const formatedEndDate = localDateHelper.toDate(endDate);

        const year = formatedStartDate.getFullYear();

        if (localDateHelper.isQuarter(formatedStartDate, formatedEndDate)) {
            return { year, quarter: localDateHelper.quarter(formatedStartDate) };
        }

        if (localDateHelper.isMonth(formatedStartDate, formatedEndDate)) {
            return { year, month: dateHelper(formatedStartDate).month() + 1 };
        }
    }

    return { year: dateHelper().year() };
}

export default Store;
