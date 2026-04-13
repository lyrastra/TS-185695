import React from 'react';
import { observer } from 'mobx-react';
import * as PropTypes from 'prop-types';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import PayerStatusData from './resources/PayersStatusData';

const PayerStatusDropdown = ({ operationStore }) => {
    const { model: { PayerStatus }, validationState: { PayerStatus: validation } } = operationStore;

    return (
        <Dropdown
            label={`Статус плательщика (101)`}
            value={PayerStatus || 0}
            onSelect={operationStore.setPayerStatus}
            data={PayerStatusData}
            width={`100%`}
            showAsText={!operationStore.canEdit}
            error={!!validation}
            message={validation}
        />
    );
};

PayerStatusDropdown.propTypes = {
    operationStore: PropTypes.object
};

export default observer(PayerStatusDropdown);
