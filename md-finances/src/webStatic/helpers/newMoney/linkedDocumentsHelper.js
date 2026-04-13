import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import { toFloat, toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import { round } from '@moedelo/frontend-core-v2/helpers/mathHelper';

function isOperationSumNotCoveredWithDocs(list, maxSum) {
    return round(list.reduce((sumItems, item) => sumItems + toFloat(item.Sum), 0), 2) < toFloat(maxSum);
}

export function canAddMoreDocuments(list, maxSum) {
    return !maxSum
        || !list.length
        || isOperationSumNotCoveredWithDocs(list, maxSum);
}

export function mapDocumentsToModel(linkedDocuments = [], paymentOperationSum) {
    let maxSum = paymentOperationSum || 0;

    return linkedDocuments.map(item => {
        const key = item.key || getUniqueId();

        const unpaidSum = toFloat(item.DocumentSum) - toFloat(item.PaidSum);
        const currentValue = toFloat(item.Sum);
        const sum = (currentValue && currentValue <= unpaidSum) ? currentValue : Math.max(Math.min(unpaidSum, maxSum), 0);

        maxSum -= sum;

        return {
            ...item,
            Sum: sum ? toAmountString(sum) : 0,
            DocumentSum: item.DocumentSum ? toAmountString(item.DocumentSum) : 0,
            PaidSum: toFloat(item.PaidSum) === false ? 0 : toAmountString(item.PaidSum),
            key
        };
    });
}

export function mapDocumentsToNewBackend(documents = []) {
    return documents?.filter(doc => doc.DocumentBaseId)?.map(({ DocumentBaseId, DocumentType, Sum }) => {
        return {
            Type: DocumentType,
            Sum: toFloat(Sum) || 0,
            DocumentBaseId
        };
    });
}

export default {
    canAddMoreDocuments,
    mapDocumentsToModel,
    mapDocumentsToNewBackend
};
