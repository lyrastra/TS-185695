import React from 'react';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import dateHelper, { DateFormat } from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Input from '@moedelo/frontend-core-react/components/Input';
import Tooltip from '@moedelo/frontend-core-react/components/Tooltip';
import * as PropTypes from 'prop-types';
import style from './style.m.less';

const cn = classnames.bind(style);
const tooltipText = `Указывается УИН (уникальный идентификатор
  начисления/платежа), установленный получателем средств (при
  перечислении недоимки, штрафов, пени). По текущим платежам указывается "0"`;

const tooltipTextAfterTransition = `УИН заполняется в случае наличия данных по нему в требовании от 
    ведомства на уплату недоимки, штрафа и пени. По текущим платежам или 
    при отсутствии данных по УИН указывается "0".`;

const UinInput = ({ operationStore }) => {
    const {
        model, setUin, validationState, canEdit, isUnifiedBudgetaryPayment
    } = operationStore;

    const isBeforeTransition = dateHelper(model.Date, DateFormat.ru).isBefore(`01.04.2026`, DateFormat.ru);
    const isShowAsText = !canEdit || (isUnifiedBudgetaryPayment && isBeforeTransition);

    const shouldShowTooltip = !isBeforeTransition || !isUnifiedBudgetaryPayment;

    const currentTooltipText = isBeforeTransition ? tooltipText : tooltipTextAfterTransition;

    return (
        <div className={cn(style.uinContainer)}>
            <div className={style.input}>
                <Input
                    label={`Код (22)`}
                    value={model.Uin}
                    onChange={setUin}
                    message={validationState.Uin}
                    error={!!validationState.Uin}
                    maxLength={25}
                    showAsText={isShowAsText}
                />

            </div>
            {shouldShowTooltip && (
            <Tooltip
                width={300}
                position="right"
                content={currentTooltipText}
            />
            )}
        </div>
    );
};

UinInput.propTypes = {
    operationStore: PropTypes.object
};

export default observer(UinInput);
