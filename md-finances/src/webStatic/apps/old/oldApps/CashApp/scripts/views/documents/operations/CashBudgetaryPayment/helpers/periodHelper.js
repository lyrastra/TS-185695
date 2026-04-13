export function getClearPeriod(period) {
    const field = getFieldByType(period.CalendarType);

    if (!field) {
        return { CalendarType: period.CalendarType };
    }

    return {
        CalendarType: period.CalendarType,
        [field]: period[field],
        Year: period.Year
    };
}

function getClearPeriodFields(period) {
    const field = getFieldByType(period.CalendarType);

    if (!field) {
        return [`CalendarType`];
    }

    if (field === `Year`) {
        return [`CalendarType`, `Year`];
    }

    return [`CalendarType`, field, `Year`];
}

export function arePeriodEquals(current, newValue) {
    const fields = getClearPeriodFields(newValue);

    return fields.every(f => Number(newValue[f]) === Number(current[f]));
}

function getFieldByType(type) {
    const t = Number(type);
    const { Common } = window;

    if (t === Common.Data.CalendarTypes.WithoutPeriod) {
        return null;
    }

    if (t === Common.Data.CalendarTypes.Year) {
        return `Year`;
    }

    if (t === Common.Data.CalendarTypes.HalfYear) {
        return `HalfYear`;
    }

    if (t === Common.Data.CalendarTypes.Quarter) {
        return `Quarter`;
    }

    if (t === Common.Data.CalendarTypes.Month) {
        return `Month`;
    }

    return null;
}
