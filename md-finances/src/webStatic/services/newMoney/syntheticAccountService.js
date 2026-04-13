import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';

export function getSyntheticAccountAutocomplete() {
    return get(`/Accounting/ChartOfAccounts/GetSyntheticAccountAutocomplete`).then(({ List }) => List);
}

export default { getSyntheticAccountAutocomplete };
