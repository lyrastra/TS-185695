import React from 'react';
import { observer } from 'mobx-react';
import dateHelper, { DateFormat } from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Input from '@moedelo/frontend-core-react/components/Input';
import * as PropTypes from 'prop-types';

import style from './style.m.less';

const OktmoOkato105 = ({ operationStore }) => {
    const {
        canEdit, model, validationState, setOktmoOkato105, isUnifiedBudgetaryPayment
    } = operationStore;

    const { Date, Recipient } = model;
    const isDateBefore2014 = dateHelper(Date, `DD.MM.YYYY`).isBefore(`2014-01-01`);
    const label = isDateBefore2014 ? `ОКАТО` : `ОКТМО`;
    const value = isDateBefore2014 ? Recipient.Okato : Recipient.Oktmo;
    const errorMessage = isDateBefore2014 ? validationState.Okato : validationState.Oktmo;
    const maxLength = isDateBefore2014 ? 11 : 8;
    const isShowAsText = (dateHelper(Date, DateFormat.ru).isAfter(`31.03.2026`, DateFormat.ru) && isUnifiedBudgetaryPayment) || !canEdit;

    return (
        <div className={style.wrapper}>
            <Input
                label={`${label} (105)`}
                value={value || ``}
                onChange={setOktmoOkato105}
                maxLength={maxLength}
                message={errorMessage}
                error={!!errorMessage}
                showAsText={isShowAsText}
                className={style.input}
            />
        </div>
    );
};

OktmoOkato105.propTypes = {
    operationStore: PropTypes.object
};

export default observer(OktmoOkato105);
