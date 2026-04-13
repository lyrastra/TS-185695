import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';

export function mapCurrencyInvoicesDocumentsToSave(documents = []) {
    return documents
        .filter(doc => doc.DocumentBaseId > 0)
        .map(doc => ({
            Type: doc.Type,
            DocumentBaseId: doc.DocumentBaseId,
            Sum: toFloat(doc.Sum)
        }));
}

export default {
    mapCurrencyInvoicesDocumentsToSave
};
