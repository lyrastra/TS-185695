import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import SmsConfirmDialog from '@moedelo/frontend-common-v2/apps/bankIntegration/components/SmsConfirmModal';

const SmsConfirmModal = ({ operationStore }) => {
    return <SmsConfirmDialog
        visible={operationStore.smsConfirmModalVisible}
        phoneMasked={operationStore.phoneMask}
        source={operationStore.currentSettlementAccount}
        onConfirm={() => operationStore.sendToBank(operationStore.model.DocumentBaseId)}
        onClose={() => operationStore.toggleSmsConfirmDialogVisibility({ isVisible: false })}
    />;
};

SmsConfirmModal.propTypes = {
    operationStore: PropTypes.obj
};

export default observer(SmsConfirmModal);

