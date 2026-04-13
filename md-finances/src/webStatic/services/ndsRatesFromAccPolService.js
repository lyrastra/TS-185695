import { get } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import { withNotificationAsync } from '@moedelo/frontend-common-v2/helpers/notificationHelper';

const apiEndpoint = `/requisitesapi/api/v1/NdsRatePeriods`;

export async function getNdsRatesAsync() {
    const { data } = await withNotificationAsync({
        func: () => get(`${apiEndpoint}`),
        errorMessage: `Не удалось загрузить список ставок`
    });
    
    return data;
}

export default { getNdsRatesAsync };
