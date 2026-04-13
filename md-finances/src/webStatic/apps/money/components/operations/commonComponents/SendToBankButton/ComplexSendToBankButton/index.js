import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import ButtonDropdown from '@moedelo/frontend-core-react/components/buttons/ButtonDropdown';
import { actionEnum } from '../../../../../../../resources/newMoney/saveButtonResource';

const ComplexSendToBankButton = ({ operationStore }) => {
    const canEdit = operationStore.canEdit || operationStore.canEditSalary;
    const text = canEdit ? `Сохранить и отправить в банк` : `Отправить в банк`;
    const { SendToBank, SendInvoiceToBank } = actionEnum;
    const handlers = {
        SendToBank: operationStore.sendToBank,
        SendInvoiceToBank: operationStore.sendInvoiceToBank
    };

    const onActionSelect = ({ value }) => {
        if (canEdit) {
            operationStore.onClickSave({ value });
        } else {
            handlers[value](operationStore.model.BaseDocumentId);
        }
    };

    const actions = [
        { value: SendToBank, text: `Отправить черновик` },
        { value: SendInvoiceToBank, text: `Выполнить платеж`, disabled: operationStore.disabledSendInvoiceToBank }
    ];

    return (<ButtonDropdown
        data={actions}
        onSelect={onActionSelect}
        disabled={operationStore.disabledSendToBankButton}
        loading={operationStore.isSavingBlocked}
        color="white"
    >
        {text}
    </ButtonDropdown>);
};

ComplexSendToBankButton.propTypes = {
    operationStore: PropTypes.obj
};

export default observer(ComplexSendToBankButton);

