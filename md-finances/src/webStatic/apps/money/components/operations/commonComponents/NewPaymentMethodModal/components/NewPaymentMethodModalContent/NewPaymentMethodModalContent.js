import React from 'react';
import PropTypes from 'prop-types';
import { getImage } from './helpers/paymentMethodModalHelper';

import style from './style.m.less';

const NewPaymentMethodModalContent = ({ integrationPartner }) => {
    return <div>
        {getImage(integrationPartner)}
        <p className={style.modalText}>Теперь исполнить платежи можно сразу в сервисе, подтвердив их по смс.</p>
        <p className={style.modalText}>Новый способ оплаты доступен по кнопке &rdquo;Сохранить и отправить в банк&rdquo;.</p>
    </div>;
};

NewPaymentMethodModalContent.propTypes = {
    integrationPartner: PropTypes.number
};

export default NewPaymentMethodModalContent;
