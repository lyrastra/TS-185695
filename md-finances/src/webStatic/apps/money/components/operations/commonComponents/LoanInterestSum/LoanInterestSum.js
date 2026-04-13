import React from 'react';
import { observer, inject } from 'mobx-react';
import classnames from 'classnames/bind';
import { toFloat, toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import style from './style.m.less';

const cn = classnames.bind(style);

export default inject(`operationStore`)(observer(({ operationStore }) => {
    const { LoanInterestSum } = operationStore.model;
    const value = toFloat(LoanInterestSum) !== false ? toAmountString(LoanInterestSum) : ``;
    const { validationState } = operationStore;

    if (!operationStore.canEdit) {
        const text = toFloat(value) === false ? value : `${value} ₽`;

        return (
            <div className={cn(style.noWrap)}>
                <Input
                    value={text}
                    label={`В т.ч. проценты`}
                    type={InputType.number}
                    allowDecimal
                    showAsText
                />
            </div>
        );
    }

    return (
        <div className={value ? cn(style.currency) : ``}>
            <Input
                value={value}
                onBlur={operationStore.setLoanInterestSum}
                decimalLimit={2}
                type={InputType.number}
                label={`В т.ч. проценты`}
                allowDecimal
                error={!!validationState.LoanInterestSum}
                message={validationState.LoanInterestSum}

            />
        </div>
    );
}));
