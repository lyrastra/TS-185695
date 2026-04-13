import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import Input, { Type as InputType } from '@moedelo/frontend-core-react/components/Input';

const mask = {
    guide: false,
    mask: [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/]
};

const KbkInput = ({ operationStore }) => {
    const {
        kbkValue, setKbk, canEdit, validationState
    } = operationStore;

    return <Input
        value={kbkValue}
        label="КБК"
        type={InputType.text}
        showAsText={!canEdit}
        onBlur={setKbk}
        error={!!validationState.Kbk}
        message={validationState.Kbk}
        mask={mask}
    />;
};

KbkInput.propTypes = {
    operationStore: PropTypes.object
};

export default observer(KbkInput);
