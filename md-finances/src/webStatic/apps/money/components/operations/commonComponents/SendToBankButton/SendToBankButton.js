import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import SimpleSendToBankButton from './SimpleSendToBankButton';
import ComplexSendToBankButton from './ComplexSendToBankButton';

const SendToBankButton = ({ operationStore }) => {
    return operationStore.canSendInvoiceToBank
        ? <ComplexSendToBankButton operationStore={operationStore} />
        : <SimpleSendToBankButton operationStore={operationStore} />;
};

SendToBankButton.propTypes = {
    operationStore: PropTypes.obj
};

export default observer(SendToBankButton);

