// import data from './data.json';
import { get, post, downloadPost } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

const balanceReconciliationService = {
    checkBalance: ({ guid = ``, settlementAccountId = null }) => {
        const url = guid ? `/Finances/Money/Reconciliation/${guid}` : `/Finances/Money/Reconciliation/?settlementAccountId=${settlementAccountId}`;
        
        return get(url);
    },

    produceReconciliation: requestData => {
        return post(`/Finances/Money/Reconciliation/Complete`, requestData);
    },

    cancelReconciliation: sessionId => {
        return post(`/Finances/Money/Reconciliation/Cancel`, { sessionId });
    },

    downloadReconciliationXls: ({ sessionId, excludeOperationsIds = [] }) => {
        return downloadPost(`/Finances/Money/Reconciliation/Report/Xls`, { sessionId, excludeOperationsIds });
    }

};

export default balanceReconciliationService;
