import React from 'react';
import NotificationPanel, { NotificationPanelType } from '@moedelo/frontend-core-react/components/NotificationPanel';
import gridStyle from '@moedelo/frontend-core-v2/styles/grid.m.less';
import useIntegrationTemporaryUnavailableNotification from './hooks/useIntegrationTemporaryUnavailableNotifications';

function PaymentSystemIntegrationUnavailableNotification() {
    const notifications = useIntegrationTemporaryUnavailableNotification();

    if (!notifications.length) {
        return null;
    }

    return notifications.map(notification => <NotificationPanel
        key={notification.partnerId}
        className={gridStyle.row}
        type={NotificationPanelType.info}
        message={notification.message}
    />);
}

export default PaymentSystemIntegrationUnavailableNotification;
