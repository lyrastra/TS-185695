import React from 'react';
import { observer, inject } from 'mobx-react';
import { toFloat, toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import DocumentSum from './../DocumentSum';
import style from './style.m.less';

export default inject(`operationStore`)(observer(({ operationStore, label, canEdit = true }) => {
    const sum = toFloat(operationStore.model.Sum) > 0 || !operationStore.canEdit || !canEdit ? toFinanceString(operationStore.model.Sum) : ``;
    const l = label || (operationStore.model.Direction === Direction.Incoming ? `Поступило` : `К оплате`);

    return (
        <DocumentSum
            onChange={operationStore.setSum}
            error={operationStore.validationState.Sum}
            label={l}
            value={sum}
            showAsText={!operationStore.canEdit || !canEdit}
            className={style.currency}
            unit={operationStore.sumCurrencySymbol}
        />
    );
}));
