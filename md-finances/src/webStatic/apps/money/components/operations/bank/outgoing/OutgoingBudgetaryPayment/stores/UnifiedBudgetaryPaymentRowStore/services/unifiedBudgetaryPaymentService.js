import { get } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

const controller = `/Money/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment`;

export async function getAccountCodesAsync() {
    const { data } = await get(`${controller}/GetAccountCodes`);

    return data;
}

export default {
    getAccountCodesAsync
};
