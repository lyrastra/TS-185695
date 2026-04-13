import { get, post } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

const controller = `/Money/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment`;

export async function getAccountCodesAsync() {
    const { data } = await get(`${controller}/GetAccountCodes`);

    return data;
}

export async function getUnifiedBudgetaryPaymentDescriptionBySubPayments({ params, date }) {
    const { data } = await post(`/Money/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/GetDescription?paymentDate=${date}`, params);

    return data;
}

export default {
    getAccountCodesAsync
};
