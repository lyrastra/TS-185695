import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';

const ContractType = ({ operationStore }) => {
    return (
        <Dropdown
            label={`Тип договора`}
            value={operationStore.model.UnderContract}
            data={operationStore.getContractTypesData()}
            onSelect={operationStore.setUnderContract}
            showAsText={!operationStore.canEdit}
            className={`qa-contractType`}
        />
    );
};

ContractType.propTypes = {
    operationStore: PropTypes.object
};

export default observer(ContractType);
