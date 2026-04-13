import React from 'react';
import PropTypes from 'prop-types';
import NotificationPanel, { NotificationPanelType as Type } from '@moedelo/frontend-core-react/components/NotificationPanel';

const SendToBankErrorPanel = props => {
    const { message, onClose } = props;

    if (!message) {
        return null;
    }

    return (
        <NotificationPanel onClose={onClose} type={Type.error}>
            {message}
        </NotificationPanel>
    );
};

SendToBankErrorPanel.propTypes = {
    onClose: PropTypes.func,
    message: PropTypes.node
};

export default SendToBankErrorPanel;

