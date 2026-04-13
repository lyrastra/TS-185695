import React from 'react';
import { observer } from 'mobx-react';
import RadioGroup from '@moedelo/frontend-core-react/components/RadioGroup';
import * as PropTypes from 'prop-types';

const KbkTypesRadioGroup = ({ operationStore }) => {
    if (!operationStore.kbkTypesRadioData || operationStore.kbkTypesRadioData.length === 0) {
        return null;
    }
    
    return (
        <RadioGroup
            value={operationStore.model.KbkPaymentType}
            onChange={operationStore.setKbkPaymentType}
            data={operationStore.kbkTypesRadioData}
        />
    );
};

KbkTypesRadioGroup.propTypes = {
    operationStore: PropTypes.object
};

export default observer(KbkTypesRadioGroup);
