import React from 'react';
import PropTypes from 'prop-types';
import NotificationPanel from '@moedelo/frontend-core-react/components/NotificationPanel';
import Link from '@moedelo/frontend-core-react/components/Link';

import style from './style.m.less';

const PaymentNotificationPanel = props => {
    const {
        type, message, href, text
    } = props.viewPaymentNotificationPanel;

    const onClose = () => {
        props.setViewPaymentNotificationPanel({ statusCodeInvoice: 0 });
    };

    return <NotificationPanel onClose={onClose} type={type} className={style.notificationPanel}>
        {message}&nbsp;{(href && text) && <Link text={text} href={href} target={`_blank`} />}
    </NotificationPanel>;
};

PaymentNotificationPanel.propTypes = {
    viewPaymentNotificationPanel: PropTypes.shape({
        type: PropTypes.string,
        message: PropTypes.string,
        href: PropTypes.string,
        text: PropTypes.string
    }),
    setViewPaymentNotificationPanel: PropTypes.func
};

export default PaymentNotificationPanel;
