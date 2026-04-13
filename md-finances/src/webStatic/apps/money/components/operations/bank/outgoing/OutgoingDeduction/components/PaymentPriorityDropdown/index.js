import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import { paymentPriorityData } from './resources/paymentPriorityData';

const PaymentPriorityDropdown = ({ operationStore }) => {
    const { model: { PaymentPriority }, setPaymentPriority, canEdit } = operationStore;

    return <Dropdown
        label={`Очередность платежа`}
        value={PaymentPriority}
        data={paymentPriorityData}
        onSelect={setPaymentPriority}
        showAsText={!canEdit}
    />;
};

PaymentPriorityDropdown.propTypes = {
    operationStore: PropTypes.object
};

export default observer(PaymentPriorityDropdown);
