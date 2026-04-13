import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

export function mapCurrencyInvoicesToBackendModel(list = []) {
    return list.map(currencyInvoice => {
        return {
            DocumentBaseId: currencyInvoice.DocumentBaseId,
            Sum: currencyInvoice.LinkSum
        };
    });
}

export function mapCurrencyInvoicesToFrontendModel(list = []) {
    return list?.map(currencyInvoice => {
        return {
            DocumentBaseId: currencyInvoice.DocumentBaseId,
            Name: `Инвойс №${currencyInvoice.Number} от ${dateHelper(currencyInvoice.Date, `YYYY-MM-DD`).format(`DD.MM.YYYY`)}`,
            LinkSum: currencyInvoice.LinkSum
        };
    }) || [];
}

export default {
    mapCurrencyInvoicesToBackendModel
};
