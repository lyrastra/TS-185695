import React from 'react';
import PropTypes from 'prop-types';
import cn from 'classnames';
import NotificationPanel from '@moedelo/frontend-core-react/components/NotificationPanel';
import Link, { Type as LinkTypes } from '@moedelo/frontend-core-react/components/Link';

const getSupportLinkOrText = () => {
    const text = `тех. поддержку`;
    const helpLink = document.querySelector(`.js-menuHelp > a`);

    if (helpLink) {
        return <Link type={LinkTypes.modal} onClick={() => helpLink.click()}>{text}</Link>;
    }

    return text;
};

const UnavailableCurrencyOperation = ({ className }) => {
    return <NotificationPanel
        type="warning"
        className={cn(`qa-notification-unavailable-operation`, className)}
        canClose={false}
    >
        Недостаточно прав для работы с иностранной валютой, обратитесь в {getSupportLinkOrText()}.
    </NotificationPanel>;
};

UnavailableCurrencyOperation.propTypes = {
    className: PropTypes.string
};

export default UnavailableCurrencyOperation;
