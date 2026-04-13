import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';
import { withNotificationAsync } from '@moedelo/frontend-common-v2/helpers/notificationHelper';

export const getAccessibleIntegrationsAsync = () => {
    const errorMessage = `При получении списка доступных интеграций произошла непредвиденная ошибка.`;

    return withNotificationAsync({
        func: () => {
            return get(`/Requisites/Integrations/V2/GetIntegrationDataForPaymentSystemIntegrationUI`)
                .then(res => {
                    if (!res?.Status) {
                        throw new Error(errorMessage);
                    }

                    return res.Value?.Accessible || [];
                });
        },
        errorMessage
    });
};

export default { getAccessibleIntegrationsAsync };
