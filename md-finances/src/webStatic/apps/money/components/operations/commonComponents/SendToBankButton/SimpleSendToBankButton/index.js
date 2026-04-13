import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import { actionEnum } from '../../../../../../../resources/newMoney/saveButtonResource';

const SimpleSendToBankButton = ({ operationStore }) => {
    const canEdit = operationStore.canEdit || operationStore.canEditSalary;
    const onClick = canEdit ?
        () => operationStore.onClickSave({ value: actionEnum.SendToBank }) :
        () => operationStore.sendToBank(operationStore.model.BaseDocumentId);
    const text = canEdit ? `Сохранить и отправить в банк` : `Отправить в банк`;

    return (<Button
        onClick={onClick}
        color="white"
        loading={operationStore.isSavingBlocked}
        disabled={operationStore.disabledSendToBankButton}
    >
        {text}
    </Button>);
};

SimpleSendToBankButton.propTypes = {
    operationStore: PropTypes.obj
};

export default observer(SimpleSendToBankButton);

