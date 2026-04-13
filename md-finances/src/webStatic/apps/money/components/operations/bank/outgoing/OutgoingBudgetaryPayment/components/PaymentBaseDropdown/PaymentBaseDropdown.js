import React from 'react';
import { observer } from 'mobx-react';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import * as PropTypes from 'prop-types';

const PaymentBaseDropdown = ({ operationStore }) => {
    const {
        model, setPaymentBase, PaymentBaseList, canEdit, isUnifiedBudgetaryPayment
    } = operationStore;

    return (
        <Dropdown
            label={`Основание платежа (106)`}
            value={model.PaymentBase || ``}
            onSelect={setPaymentBase}
            data={PaymentBaseList}
            width={400}
            showAsText={!canEdit || isUnifiedBudgetaryPayment}
        />
    );
};

PaymentBaseDropdown.propTypes = {
    operationStore: PropTypes.object
};

export default observer(PaymentBaseDropdown);
