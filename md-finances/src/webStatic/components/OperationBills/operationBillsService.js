import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';

const operationBillsService = {
    get: ({
        excludeIds = [], kontragentId = null, baseDocumentId = 0, contractBaseId, query = ``
    } = {}) => {
        const url = `/Accounting/Bills/GetBillForImcomingPaymentForGoodsAutocomplete`;
        const data = {
            date: dateHelper().format(`DD.MM.YYYY`),
            count: 5,
            excludeIds,
            kontragentId,
            baseDocumentId,
            contractBaseId,
            query
        };

        return get(url, data);
    }
};

export default operationBillsService;
