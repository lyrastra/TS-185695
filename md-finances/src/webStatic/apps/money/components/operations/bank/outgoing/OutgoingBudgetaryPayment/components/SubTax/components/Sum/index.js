import React from 'react';
import { observer } from 'mobx-react';
import { toFloat, toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import DocumentSum from '../DocumentSum';

import style from './style.m.less';

export default observer(({ operationStore, canEdit = true }) => {
    const sum = toFloat(operationStore.model.Sum) > 0 || !operationStore.canEdit || !canEdit ? toFinanceString(operationStore.model.Sum) : ``;

    return (
        <DocumentSum
            onChange={operationStore.setSum}
            error={operationStore.validationState.Sum}
            label={`Сумма`}
            value={sum}
            showAsText={!operationStore.canEdit || !canEdit}
            className={style.currency}
            unit={operationStore.sumCurrencySymbol}
        />
    );
});
