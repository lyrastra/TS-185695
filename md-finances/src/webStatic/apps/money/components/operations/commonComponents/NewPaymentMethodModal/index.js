import React from 'react';
import { showModal } from '@moedelo/frontend-core-react/helpers/confirmModalHelper';
import firmFlagService from '@moedelo/frontend-common-v2/services/firmFlagService';
import NewPaymentMethodModalContent from './components/NewPaymentMethodModalContent';

const showNewPaymentMethodModal = ({ header, IntegrationPartner, flagName }) => {
    const onConfirm = () => {
        firmFlagService.enable({ name: flagName });
    };

    showModal({
        header,
        children: <NewPaymentMethodModalContent integrationPartner={IntegrationPartner} />,
        confirmButtonText: `Понятно`,
        cancelButtonVisible: false,
        onConfirm
    });
};

export default showNewPaymentMethodModal;
