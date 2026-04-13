import httpClient from '@moedelo/frontend-core-v2/helpers/httpClient';
import { getIntegrationErrors } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';

const setUrl = `/Finances/Integrations/SetIntegrationErrorAsRead`;

class IntegrationErrorService {
    async get() {
        return getIntegrationErrors();
    }

    setReedState(errorIds) {
        return httpClient.post(setUrl, errorIds);
    }
}

export default new IntegrationErrorService();
