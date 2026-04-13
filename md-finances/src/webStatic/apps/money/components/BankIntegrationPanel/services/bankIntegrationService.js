import { withNotificationAsync } from '@moedelo/frontend-common-v2/helpers/notificationHelper';
import { get } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

export const getBankIntegrationData = () => {
    return withNotificationAsync({
        func: () => get(`/bankIntegrations/gateway/web/v1/userIntegrationInfo/getData`),
        errorMessage: `Не удалось загрузить данные для интеграции с банком`
    });
    // return {
    //     data: {
    //         TurnedOn: [
    //             {
    //                 IntegrationPartner: 1027,
    //                 Name: `Сбер`
    //             },
    //             {
    //                 IntegrationPartner: 1042,
    //                 Name: `Что-то-Банк`
    //             }
    //         ],
    //         Accessible: [
    //             {
    //                 IntegrationPartner: 1005,
    //                 Name: `Альфа-Банк`
    //             },
    //             {
    //                 IntegrationPartner: 1047,
    //                 Name: `Райффайзен`
    //             },
    //             {
    //                 IntegrationPartner: 1052,
    //                 Name: `Открытие`
    //             }
    //         ],
    //         UserIntegrationState: 1,
    //         IsShowUpsaleMessage: true,
    //         HasLimit: true
    //     }
    // };
};

export default { getBankIntegrationData };
