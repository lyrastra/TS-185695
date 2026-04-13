import httpClient from '@moedelo/frontend-core-v2/helpers/httpClient';
import restHttpClient from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import { getSetupDataPreloading } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';

const requisitesService = {
    async get() {
        return getSetupDataPreloading();
    },

    async lastClosedDate() {
        const requisites = await this.get();

        return requisites?.LastClosedDate;
    },

    getAllTaxationSystems() {
        return restHttpClient.get(`/Requisites/api/v2/TaxationSystems`).then(({ data }) => data);
    },

    checkSmsCode(partner, code) {
        return httpClient.post(`/Requisites/Integrations/CheckSmsCode`, { partner, code });
    }
};

export default requisitesService;
