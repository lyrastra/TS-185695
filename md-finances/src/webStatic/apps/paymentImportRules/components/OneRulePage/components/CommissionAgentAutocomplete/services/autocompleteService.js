import { get } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

export async function autocomplete({ query = ``, count = 5 }) {
    const url = `/Money/api/v1/PaymentOrders/Incoming/IncomeFromCommissionAgent/Autocomplete/CommissionAgent`;
    const response = await get(url, { query, count });

    return response.data.map(({ KontragentId, Name }) => ({
        Id: KontragentId,
        Name
    }));
}

export default { autocomplete };
