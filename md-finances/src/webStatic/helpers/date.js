import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/**
 * @deprecated используйте методы из dateHelper
 */
const utils = {
    /* @deprecated используйте методы из dateHelper */
    firstDayOfYear(year) {
        return new Date(year, 0, 1);
    },
    
    /* @deprecated используйте методы из dateHelper */
    lastDayOfYear(year) {
        return new Date(year, 11, 31);
    },

    /* @deprecated используйте методы из dateHelper */
    isYear(startDate, endDate) {
        if (!startDate || !endDate) {
            return false;
        }

        const momentStartDate = this.toDate(startDate);
        const momentEndDate = this.toDate(endDate);

        const year = momentStartDate.getFullYear();

        return dateHelper(momentStartDate).isSame(this.firstDayOfYear(year), `day`) && dateHelper(momentEndDate).isSame(this.lastDayOfYear(year), `day`);
    },

    /* @deprecated используйте методы из dateHelper */
    isQuarter(startDate, endDate) {
        if (!startDate || !endDate) {
            return false;
        }

        const momentStartDate = this.toDate(startDate);
        const momentEndDate = this.toDate(endDate);

        const quarter = this.quarter(momentStartDate);
        const year = momentStartDate.getFullYear();

        return dateHelper(momentStartDate).isSame(this.firstDayOfQuarter(year, quarter), `day`) && dateHelper(momentEndDate).isSame(this.lastDayOfQuarter(year, quarter), `day`);
    },

    /* @deprecated используйте методы из dateHelper */
    firstDayOfQuarter(year, quarter) {
        return new Date(year, (quarter - 1) * 3, 1);
    },

    /* @deprecated используйте методы из dateHelper */
    lastDayOfQuarter(year, quarter) {
        const start = this.firstDayOfQuarter(year, quarter);

        return dateHelper(start).add(3, `month`).add(-1, `day`).toDate();
    },

    /* @deprecated используйте методы из dateHelper */
    quarter(date) {
        const momentDate = this.toDate(date);

        return Math.ceil((dateHelper(momentDate).month() + 1) / 3);
    },

    /* @deprecated используйте методы из dateHelper */
    fisrtDayOfMonth(date) {
        const m = dateHelper(date);

        return new Date(m.year(), m.month(), 1);
    },

    /* @deprecated используйте методы из dateHelper */
    lastDayOfMonth(date) {
        return dateHelper(date).add(1, `month`).add(-1, `day`).toDate();
    },

    /* @deprecated используйте методы из dateHelper */
    isEmpty(date) {
        const minAvailableDate = dateHelper(undefined, `DD.MM.YYYY`);

        return !date || dateHelper(date).isSame(minAvailableDate, `day`);
    },

    /* @deprecated используйте методы из dateHelper */
    isMonth(startDate, endDate) {
        if (!startDate || !endDate) {
            return false;
        }

        const momentStartDate = this.toDate(startDate);
        const momentEndDate = this.toDate(endDate);

        return dateHelper(momentStartDate).isSame(this.fisrtDayOfMonth(momentStartDate), `day`) && dateHelper(momentEndDate).isSame(this.lastDayOfMonth(momentStartDate), `day`);
    },

    /* @deprecated используйте методы из dateHelper */
    toDate(date) {
        if (typeof date === `string`) {
            return dateHelper(date, `DD.MM.YYYY`).toDate();
        }

        return date;
    },

    /* @deprecated используйте методы из dateHelper */
    fromIsotoDate(date) {
        if (typeof date === `string`) {
            return dateHelper(date).toDate();
        }

        return date;
    },

    /* @deprecated используйте методы из dateHelper */
    toString(date) {
        return dateHelper(date).format(`DD.MM.YYYY`);
    },

    /* @deprecated используйте методы из dateHelper */
    isValid(date) {
        const formatDate = date.replace(`_`, ``);

        return formatDate.length === 10 && dateHelper(formatDate, `DD.MM.YYYY`).isValid();
    },

    /* @deprecated используйте методы из dateHelper */
    year(date) {
        return this.toDate(date).getFullYear();
    },

    /* @deprecated используйте методы из dateHelper */
    format(date, format) {
        if (typeof date === `string`) {
            return dateHelper(date, `DD.MM.YYYY`).format(format);
        }

        return dateHelper(date).format(format);
    }
};

export default utils;
