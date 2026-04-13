import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';

function PKOAutocomplete({ query = ``, count = 5 }) {
    const url = `/Accounting/BankAutocompletes/GetCashOrderRemoveFromSettlementAccountAutocomplete`;

    return get(url, { query, count });
}

function rkoAutocomplete({ query = ``, baseId = ``, count = 5 }) {
    const url = `/Accounting/BankAutocompletes/GetCashOrderIncomeFromCashAutocomplete`;

    return get(url, { query, baseId, count });
}

export default rkoAutocomplete;

export { PKOAutocomplete, rkoAutocomplete };
