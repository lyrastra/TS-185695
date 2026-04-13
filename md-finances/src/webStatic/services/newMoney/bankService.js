import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';

export function getBanksAutocomplete(data) {
    return get(`/Accounting/Banks/GetBanksAutocomplete`, data);
}

export default { getBanksAutocomplete };
