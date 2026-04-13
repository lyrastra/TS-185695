import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';

export default {
    async autocomplete({ query, workerId }) {
        if (!workerId) {
            return [];
        }

        const args = {
            workerId,
            query,
            count: 5
        };

        const { List } = await get(`/Accounting/AccountingAdvanceStatement/GetAccountingAdvanceStatementAutocomplete`, args);

        return List;
    }
};
