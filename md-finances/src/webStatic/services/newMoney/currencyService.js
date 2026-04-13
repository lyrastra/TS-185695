import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';
import CurrencyCode from '@moedelo/frontend-enums/mdEnums/CurrencyCode';

export async function getCurrencyRate({ date, currencyCode }) {
    const currency = CurrencyCode[currencyCode];
    const result = await get(`/accounting/settlementaccounts/GetExchangeRate`, { date, currency });

    return result.Value;
}

export default { getCurrencyRate };

