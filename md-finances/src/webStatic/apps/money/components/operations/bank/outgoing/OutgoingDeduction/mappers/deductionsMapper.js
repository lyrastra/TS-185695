export const mapDeductionWorkersToAutocomplete = (deductionWorkers, query) => {
    return {
        data: deductionWorkers.map(x => ({
            value: x.Id,
            text: x.Fio,
            original: x
        })),
        value: query
    };
};

export const mapDeductionWorkerDocumentsToAutocomplete = (documents, query) => {
    return {
        data: documents.map(x => ({
            value: x,
            text: x
        })),
        value: query
    };
};

export default { mapDeductionWorkersToAutocomplete, mapDeductionWorkerDocumentsToAutocomplete };
