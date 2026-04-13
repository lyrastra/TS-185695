import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import Input, { Type as InputType } from '@moedelo/frontend-core-react/components/Input';

const mask = {
    guide: false,
    mask: [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/]
};

const OktmoInput = ({ operationStore }) => {
    const {
        model: { Oktmo }, setOktmo, canEdit, validationState
    } = operationStore;

    return <Input
        value={Oktmo}
        label="ОКТМО"
        type={InputType.text}
        showAsText={!canEdit}
        onBlur={setOktmo}
        error={!!validationState.Oktmo}
        message={validationState.Oktmo}
        mask={mask}
    />;
};

OktmoInput.propTypes = {
    operationStore: PropTypes.object
};

export default observer(OktmoInput);
