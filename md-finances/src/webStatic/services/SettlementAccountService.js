import { get, post } from '@moedelo/frontend-core-v2/helpers/httpClient';

const SettlementAccountService = {
    setName(id, name) {
        return post(`/Requisites/SettlementAccounts/SetName`, { id, name });
    },

    get() {
        return get(`/Requisites/SettlementAccounts/Get`);
    }
};

export default SettlementAccountService;
