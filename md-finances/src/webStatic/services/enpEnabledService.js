import { get } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

let promise = null;

export async function getStartEnpDate() {
    if (!promise) {
        promise = get(`/Money/api/v1/Data/UnifiedBudgetaryPayments/StartDate`);
    }

    const res = await promise;

    return res?.data?.enpStartDate;
}

export default { getStartEnpDate };
