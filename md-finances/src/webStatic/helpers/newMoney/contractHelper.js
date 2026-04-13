export function mapForAutocomplete(items, query) {
    return {
        data: items.map((item) => {
            return {
                value: item.Id,
                text: item.IsMainContract ? `${item.Number}` : `№ ${item.Number} от ${item.Date}`,
                model: mapToServerModel(item)
            };
        }),
        value: query
    };
}

export function mapToServerModel(contract) {
    return {
        ContractBaseId: contract.DocumentBaseId,
        KontragentId: contract.KontragentId,
        KontragentName: contract.KontragentName,
        ProjectNumber: contract.Number,
        ProjectId: contract.Id,
        Date: contract.Date,
        IsLongTermContract: contract.IsLongTermContract,
        Sum: contract.Sum
    };
}

export default { mapForAutocomplete, mapToServerModel };
