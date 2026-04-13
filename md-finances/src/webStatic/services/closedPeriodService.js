import httpClient from '@moedelo/frontend-core-v2/helpers/httpClient';

export default {
    openPeriod(date) {
        return httpClient.post(`/Accounting/ClosedPeriods/OpenPeriod`, { date });
    },

    getClosedPeriod(date) {
        return httpClient.get(`/Accounting/ClosedPeriods/GetClosedPeriod`, { date });
    }
};
