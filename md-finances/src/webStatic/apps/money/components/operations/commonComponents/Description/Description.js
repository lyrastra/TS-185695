import React from 'react';
import { observer, inject } from 'mobx-react';
import Textarea from '@moedelo/frontend-core-react/components/Textarea';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Tooltip, { Position as TooltipPosition } from '@moedelo/frontend-core-react/components/Tooltip';
import style from './style.m.less';

export default inject(`operationStore`)(observer(({
    operationStore, className, label, tooltip
}) => {
    const {
        canEdit, setDescription, validationState, isUnifiedBudgetaryPayment
    } = operationStore;
    const description = operationStore.description
        || (canEdit ? `` : `не указано`);

    return (
        <div className={className}>
            <div className={tooltip ? grid.col_23 : grid.col_24}>
                <Textarea
                    rows={1}
                    label={label || `Назначение`}
                    onChange={setDescription}
                    value={description}
                    error={!!validationState.Description}
                    message={validationState.Description}
                    showAsText={!canEdit || isUnifiedBudgetaryPayment}
                />
            </div>
            { !!tooltip && canEdit &&
            <div className={grid.col_1}>
                <Tooltip wrapperClassName={style.tooltip} content={tooltip} position={TooltipPosition.topLeft} />
            </div> }
        </div>
    );
}));
