import { post } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

export function generateTaxPostings(data) {
    return post(`TaxPostingsMoney/api/v1/CashOrders/Outgoing/UnifiedBudgetaryPayment/SubPayment`, data);
}
