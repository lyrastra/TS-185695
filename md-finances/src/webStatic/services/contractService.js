import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';

export function autocomplete({
    query = ``,
    kontragentId,
    count = 5,
    kind = [],
    mediationType = [],
    withMainContract
}) {
    const url = `/Contract/Autocomplete`;

    return get(url, {
        query, kontragentId, count, kind, mediationType, withMainContract
    });
}

export async function middlemanContractAutocomplete({ query, kontragentId }) {
    const args = {
        kontragentId,
        query,
        count: 5
    };

    const { List } = await get(`/Contract/Autocomplete/MiddlemanContractAutocomplete`, args);

    return List;
}

export async function getContractById({ contractId }) {
    const url = `/Contract/Get`;

    const { Value } = await get(url, { id: contractId });

    return Value;
}

export default { autocomplete, middlemanContractAutocomplete, getContractById };
