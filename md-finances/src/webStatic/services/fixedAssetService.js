import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';

export async function getFixedAssetAutocomplete({
    query = ``,
    contractId,
    kontragentId,
    count = 5
}) {
    const url = `/Accounting/InventoryCardAutocomplete/FromRent`;
    const { List } = await get(url, {
        query, contractId, kontragentId, count
    });

    return List;
}

export async function getInventoryCardByBaseId({ documentBaseId }) {
    const url = `/Accounting/InventoryCard/GetByBaseId`;

    return get(url, { documentBaseId });
}

export default { getFixedAssetAutocomplete, getInventoryCardByBaseId };
