import React from 'react';
import { observer, inject } from 'mobx-react';
import classnames from 'classnames/bind';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { toFloat, toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import DocumentSum from './../DocumentSum';
import style from './style.m.less';

const cn = classnames.bind(style);

export default inject(`operationStore`)(observer(({ operationStore }) => {
    const { MediationCommission } = operationStore.model;

    let value = toFloat(MediationCommission) !== false ? toFinanceString(MediationCommission) : ``;

    if (!operationStore.canEdit && !value) {
        value = `не указано`;
    }

    return <div className={grid.col_9}>
        <div className={cn(grid.col_9, style.sum)}>
            <DocumentSum
                label={`Мое вознаграждение`}
                value={value}
                onChange={operationStore.setMediationCommission}
                error={operationStore.validationState.MediationCommission}
                showAsText={!operationStore.canEdit}
                className={style.currency}
            />
        </div>
        {operationStore.canEdit &&
            <Tooltip
                wrapperClassName={style.tooltip}
                width={300}
                position={Position.topRight}
                content={`Укажите сумму, если по условию посреднического договора комиссия посредника удерживается в момент поступления денежных средств от покупателя. Эта сумма будет учтена как доход в УСН.`}
            />}
    </div>;
}));
