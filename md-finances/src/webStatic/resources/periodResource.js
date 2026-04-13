import PeriodTypeEnum from '../enums/newMoney/AnalyticsPeriodTypeEnum';

const months = [
    {
        value: 12,
        text: `декабрь`,
        dateValue: 12,
        type: PeriodTypeEnum.Month
    },
    {
        value: 11,
        text: `ноябрь`,
        dateValue: 11,
        type: PeriodTypeEnum.Month
    },
    {
        value: 10,
        text: `октябрь`,
        dateValue: 10,
        type: PeriodTypeEnum.Month
    },
    {
        value: 9,
        text: `сентябрь`,
        dateValue: 9,
        type: PeriodTypeEnum.Month
    },
    {
        value: 8,
        text: `август`,
        dateValue: 8,
        type: PeriodTypeEnum.Month
    },
    {
        value: 7,
        text: `июль`,
        dateValue: 7,
        type: PeriodTypeEnum.Month
    },
    {
        value: 6,
        text: `июнь`,
        dateValue: 6,
        type: PeriodTypeEnum.Month
    },
    {
        value: 5,
        text: `май`,
        dateValue: 5,
        type: PeriodTypeEnum.Month
    },
    {
        value: 4,
        text: `апрель`,
        dateValue: 4,
        type: PeriodTypeEnum.Month
    },
    {
        value: 3,
        text: `март`,
        dateValue: 3,
        type: PeriodTypeEnum.Month
    },
    {
        value: 2,
        text: `февраль`,
        dateValue: 2,
        type: PeriodTypeEnum.Month
    },
    {
        value: 1,
        text: `январь`,
        dateValue: 1,
        type: PeriodTypeEnum.Month
    }
];

const quarter = [
    {
        value: 13,
        text: `4 квартал`,
        dateValue: 4,
        type: PeriodTypeEnum.Quarter
    },
    {
        value: 14,
        text: `3 квартал`,
        dateValue: 3,
        type: PeriodTypeEnum.Quarter
    },
    {
        value: 15,
        text: `2 квартал`,
        dateValue: 2,
        type: PeriodTypeEnum.Quarter
    },
    {
        value: 16,
        text: `1 квартал`,
        dateValue: 1,
        type: PeriodTypeEnum.Quarter
    }
];

const allPeriod = quarter.concat(months);

export default allPeriod;

export {
    quarter,
    months
};

