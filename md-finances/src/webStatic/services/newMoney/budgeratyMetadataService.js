import { get } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

export async function getBudgetaryMetadataAsync(paymentDate) {
    return get(`/Money/api/v1/PaymentOrders/Outgoing/BudgetaryPayment/Metadata`, { paymentDate }).then(resp => {
        return resp.data;
    });
}

export async function getUnifiedBudgetaryPaymentBudgetaryMetadataAsync(paymentDate) {
    return get(`/Money/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/Metadata`, { paymentDate }).then(resp => {
        return resp.data;
    });
}

export default {
    getBudgetaryMetadataAsync
};
