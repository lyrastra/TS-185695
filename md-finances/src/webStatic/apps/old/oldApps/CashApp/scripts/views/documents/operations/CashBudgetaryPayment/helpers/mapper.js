import dateHelper, { DateFormat } from '@moedelo/frontend-core-v2/helpers/dateHelper';
import UnifiedAccountsResource from '../resources/UnifiedAccountsResource';
import { getClearPeriod } from './periodHelper';
import { isSubPaymentEmpty } from './checkHelper';

export function getUnifiedPaymentsOptions() {
    return UnifiedAccountsResource.map(t => {
        return {
            text: t.Name,
            value: t.Code
        };
    });
}

export function getPatentOptions(patents) {
    return patents.map(p => ({
        text: p.ShortName,
        value: p.Id
    }));
}

export function getKbkOptions(kbks = []) {
    return kbks.map(k => ({
        text: k.Name,
        value: k.Id
    }));
}

export function getPeriodForTaxPostings(period) {
    return getClearPeriod({ ...period, Type: period.CalendarType });
}

export function getDataForTaxPostings(data) {
    return {
        ...data,
        Date: dateHelper(data.Date, DateFormat.ru).format(DateFormat.iso),
        Period: getPeriodForTaxPostings(data.Period)
    };
}

export function getPayloadPayments(data) {
    return data.filter(p => !isSubPaymentEmpty(p))
        .map(p => getPayloadSubPayment(p));
}

function getPayloadSubPayment(data) {
    const { TaxPosting } = data;

    return {
        ...data,
        TaxPosting: TaxPosting?.Sum ? TaxPosting : null
    };
}
