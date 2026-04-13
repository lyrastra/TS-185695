import { post } from '@moedelo/frontend-core-v2/helpers/httpClient';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

export default {
    async autocomplete(requestArgs = { }) {
        const date = dateHelper().format(`DD.MM.YYYY`);
        const { List } = await post(`/Accounting/Bills/GetBillForImcomingPaymentForGoodsAutocomplete`, { ...requestArgs, date, count: 5 });

        return List
            .map(mapDocumentAutocompleteItem);
    }
};

function mapDocumentAutocompleteItem(doc) {
    const { Sum, PayedSum, ...item } = doc;

    return { ...item, DocumentSum: Sum, PaidSum: PayedSum };
}
