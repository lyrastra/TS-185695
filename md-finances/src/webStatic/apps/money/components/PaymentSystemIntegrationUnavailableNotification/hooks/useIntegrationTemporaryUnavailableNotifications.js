import React from 'react';
import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper/dateHelper';
import ProductPlatform from '@moedelo/frontend-enums/mdEnums/ProductPlatform';
import IntegrationPartner from '@moedelo/frontend-enums/mdEnums/IntegrationPartner';
import { getAccessibleIntegrationsAsync } from '../services/integrationsService';

const useIntegrationTemporaryUnavailableNotification = () => {
    const [notifications, setNotifications] = React.useState([]);
    const mountedRef = React.useRef(true);
    const unavailableIntegrationPartnerIds = [
        { id: IntegrationPartner.Robokassa, nameInstrumental: `Робокассой` },
        { id: IntegrationPartner.ModulKassa, nameInstrumental: `Модулькассой` }
    ];

    const getNotifications = ({ productPlatform, sourceFirmId, accessibleIntegrations }) => {
        const res = [];

        // TODO: Убрать проверку на год в 2023 году
        if (productPlatform !== ProductPlatform.Acc || !sourceFirmId || dateHelper().year() <= 2022) {
            return res;
        }

        unavailableIntegrationPartnerIds.forEach(partner => {
            const partnerInfo = accessibleIntegrations.find(i => i.Id === partner.id);

            if (partnerInfo?.IsOn) {
                res.push({
                    partnerId: partner.id,
                    message: `Интеграция с ${partner.nameInstrumental} временно недоступна. О сроках ее реализации в новом кабинете мы Вам сообщим дополнительно.`
                });
            }
        });

        return res;
    };

    React.useEffect(() => {
        (async () => {
            const res = [];
            const userInfo = await userDataService.get();
            const accessibleIntegrations = await getAccessibleIntegrationsAsync();

            if (!userInfo || !accessibleIntegrations.length || !mountedRef.current) {
                return;
            }

            res.push(...getNotifications({
                productPlatform: userInfo.ProductPlatform,
                sourceFirmId: userInfo.SourceFirmId,
                accessibleIntegrations
            }));

            setNotifications(res);
        })();

        return () => {
            mountedRef.current = false;
        };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return notifications;
};

export default useIntegrationTemporaryUnavailableNotification;
