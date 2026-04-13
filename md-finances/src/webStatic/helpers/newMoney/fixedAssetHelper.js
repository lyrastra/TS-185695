export function mapForFixedAssetAutocomplete(items, query) {
    return {
        data: items.map((item) => {
            return {
                value: item.Id,
                text: `${item.FixedAssetName}`,
                model: mapToServerModel(item),
                description: `${item.InventoryNumber}`
            };
        }),
        value: query
    };
}

export function mapToServerModel(fixedAsset) {
    return {
        Id: fixedAsset.Id,
        Name: fixedAsset.FixedAssetName,
        Number: fixedAsset.InventoryNumber,
        ContractId: fixedAsset.ContractId,
        KontragentId: fixedAsset.KontragentId,
        DocumentBaseId: fixedAsset.DocumentBaseId
    };
}

export default { mapForFixedAssetAutocomplete, mapToServerModel };
