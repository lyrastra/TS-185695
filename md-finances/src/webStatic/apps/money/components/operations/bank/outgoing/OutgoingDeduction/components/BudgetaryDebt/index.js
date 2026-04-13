import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import Switch from '@moedelo/frontend-core-react/components/Switch';
import Tooltip, { Position as TooltipPosition } from '@moedelo/frontend-core-react/components/Tooltip';
import style from './style.m.less';

const tooltip = `Включите настройку при заполнении платежки на перечисление долга перед бюджетом. Если же вы перечисляете неналоговые долги вашего работника (например, его задолженность перед физ. лицами и организациями), настройку включать не нужно.`;

const BudgetaryDebt = ({ operationStore }) => {
    const { model: { IsBudgetaryDebt }, setIsBudgetaryDebt, canEdit } = operationStore;

    return <div className={style.accountCode}>
        <Switch
            text={`Взыскивается долг перед бюджетом`}
            onChange={setIsBudgetaryDebt}
            checked={IsBudgetaryDebt}
            disabled={!canEdit}
        />
        { !!canEdit && <Tooltip
            width={300}
            position={TooltipPosition.topRight}
            content={tooltip}
        /> }
    </div>;
};

BudgetaryDebt.propTypes = {
    operationStore: PropTypes.object
};

export default observer(BudgetaryDebt);
