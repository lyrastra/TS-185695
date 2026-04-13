import { post } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import { post as httpPost } from '@moedelo/frontend-core-v2/helpers/httpClient';

export function getDefaultFieldsByKbk(data) {
    return post(`/Money/api/v1/PaymentOrders/Outgoing/BudgetaryPayment/DefaultFieldsByKbk`, data).then(resp => {
        return resp.data;
    });
}

export function getUnifiedBudgetaryPaymentDefaultFieldsByKbk(data) {
    return post(`/Money/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/DefaultFieldsByKbk`, data).then(resp => {
        // т.к. описание получаем из другого метода
        const { Description, ...result } = resp.data;

        return result;
    });
}

export function getBudgetaryPaymentKbkAutocomplete(data) {
    return post(`/Money/api/v1/PaymentOrders/Outgoing/BudgetaryPayment/KbkAutocomplete`, data).then(resp => {
        return resp.data;
    });
}

export function getUnifiedBudgetaryPaymentKbkAutocomplete(data) {
    return post(`/Money/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/KbkAutocomplete`, data).then(resp => {
        return resp.data;
    });
}

/** for cash budgetary payment. using httpClient */
export async function budgetaryPaymentAutocomplete({
    query, accountCode, paymentType, date, period
}) {
    const request = {
        query, accountCode, paymentType, date, period
    };

    const { List } = await httpPost(`/Accounting/Subcontos/GetKbkAutocomplateForBudgetaryPayment`, request);

    return List;
}

export async function getDefaultPaymentFieldsByKbk({
    kbkId, account, tradingObjectId, date, period
}) {
    const request = {
        id: kbkId,
        account,
        tradingObjectId,
        date,
        period
    };

    return httpPost(`/Accounting/PaymentOrders/GetDefaultFieldsByKbk`, request);
}

export default { getDefaultFieldsByKbk, getBudgetaryPaymentKbkAutocomplete, getUnifiedBudgetaryPaymentKbkAutocomplete };
