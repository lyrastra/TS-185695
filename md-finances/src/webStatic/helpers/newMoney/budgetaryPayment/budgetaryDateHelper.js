import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import CalendarTypesEnum from '../../../enums/newMoney/budgetaryPayment/BudgetaryCalendarTypesEnum';

const curMoment = dateHelper().add(1, `year`); // todo: выпилить перед тестированием
const curYear = curMoment.year();

function getYearsListForDropdown() {
    let start = 2011;
    const end = curYear + 1;
    const result = [];

    for (;start < end; start += 1) {
        result.push({
            value: start,
            text: `${start}`
        });
    }

    return result;
}

function getMonthsListForDropdown() {
    let start = 0;
    const end = 12;
    const result = [];

    for (; start < end; start += 1) {
        result.push({
            value: start + 1,
            text: `${curMoment.month(start).format(`MM`)} - ${curMoment.month(start).format(`MMMM`)}`
        });
    }

    return result;
}

function getCalendarTypesForDropdown() {
    return [
        { value: CalendarTypesEnum.Month, text: `МС - Месяц` },
        { value: CalendarTypesEnum.Quarter, text: `КВ - Квартал` },
        { value: CalendarTypesEnum.HalfYear, text: `ПЛ - Полугодие` },
        { value: CalendarTypesEnum.Year, text: `ГД - Год` },
        { value: CalendarTypesEnum.NoPeriod, text: `0 - Без периода` }
    ];
}

function getQuarterListForDropdown() {
    return [
        { value: 1, text: `1` },
        { value: 2, text: `2` },
        { value: 3, text: `3` },
        { value: 4, text: `4` }
    ];
}

function getHalfYearListForDropdown() {
    return [
        { value: 1, text: `1` },
        { value: 2, text: `2` }
    ];
}

export default {
    getMonthsListForDropdown,
    getYearsListForDropdown,
    getCalendarTypesForDropdown,
    getQuarterListForDropdown,
    getHalfYearListForDropdown
};
