import React from 'react';
import { observer, inject } from 'mobx-react';
import classnames from 'classnames/bind';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { toFloat, toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import DocumentSum from './../DocumentSum';
import style from './style.m.less';

const cn = classnames.bind(style);

export default inject(`operationStore`)(observer(({ operationStore }) => {
    const { AcquiringCommissionDate } = operationStore.model;

    const value = toFloat(operationStore.model.AcquiringCommission) > 0
        ? toFinanceString(operationStore.model.AcquiringCommission)
        : ``;

    const renderDateAndTaxationSystem = () => {
        if (!operationStore.model.AcquiringCommission) {
            return null;
        }

        return <div className={grid.col_4}>
            <Input
                label={`Дата комиссии`}
                value={AcquiringCommissionDate}
                type={InputType.date}
                onChange={operationStore.setAcquiringCommissionDate}
                error={!!operationStore.validationState.AcquiringCommissionDate}
                message={operationStore.validationState.AcquiringCommissionDate}
                showAsText={!operationStore.canEdit}
            />
        </div>;
    };

    return <React.Fragment>
        <div className={cn(style.commission, grid.col_4)}>
            <DocumentSum
                label={`Комиссия (эквайринг)`}
                value={value}
                onChange={operationStore.setAcquiringCommission}
                error={operationStore.validationState.AcquiringCommission}
                showAsText={!operationStore.canEdit}
                className={cn(style.noPadding)}
            />
        </div>
        {operationStore.canEdit &&
        <Tooltip
            wrapperClassName={grid.col_1}
            width={300}
            position={Position.topRight}
            content={operationStore.textTooltip}
        />
            }
        <div className={grid.col_1} />
        {renderDateAndTaxationSystem()}
    </React.Fragment>;
}));
