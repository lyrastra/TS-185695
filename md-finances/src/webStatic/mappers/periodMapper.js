export function mapForPeriodAutocomplete(items, query) {
    const getDescription = (item) => {
        return `${item.Sum}₽`;
    };

    return {
        data: items.map((item) => {
            const model = mapPeriodToModel(item);

            return {
                value: item.Id,
                text: item.Description,
                model,
                description: getDescription(item)
            };
        }),
        value: query
    };
}

export function mapPeriodToModel(period) {
    return {
        Id: period.Id,
        Sum: period.Sum,
        DefaultSum: period.Sum,
        Description: period.Description,
        PaymentType: period.PaymentType,
        PaymentRequiredSum: period.PaymentRequiredSum
    };
}

export function mapPeriodsToModel(periods) {
    if (periods?.length > 0) {
        return periods.map(item => {
            return {
                Id: item.Id,
                Sum: item.Sum,
                Description: item.Description,
                DefaultSum: item.DefaultSum || item.Sum,
                PaymentRequiredSum: item.PaymentRequiredSum || 0,
                PaymentType: item.PaymentType,
                key: item.Id
            };
        });
    }

    return [];
}

export function mapPeriodsToSavingModel(periods) {
    if (periods?.length > 0) {
        return periods.map(item => {
            return {
                Id: item.Id,
                Sum: item.Sum
            };
        });
    }

    return [];
}

export function mapServerPeriodsToModel(periods) {
    if (periods?.length > 0) {
        return periods.map(item => {
            return mapPeriodToModel(item);
        });
    }

    return [];
}

export default { mapForPeriodAutocomplete, mapPeriodsToModel, mapServerPeriodsToModel };
