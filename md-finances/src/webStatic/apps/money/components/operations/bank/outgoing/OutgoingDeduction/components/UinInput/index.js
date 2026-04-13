import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import Input, { Type as InputType } from '@moedelo/frontend-core-react/components/Input';

const mask = {
    guide: false,
    mask: [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/]
};

const UinInput = ({ operationStore }) => {
    const {
        model: { Uin }, setUin, canEdit, validationState
    } = operationStore;

    return <Input
        value={Uin}
        label="Код"
        type={InputType.text}
        showAsText={!canEdit}
        onBlur={setUin}
        error={!!validationState.Uin}
        message={validationState.Uin}
        mask={mask}
    />;
};

UinInput.propTypes = {
    operationStore: PropTypes.object
};

export default observer(UinInput);
