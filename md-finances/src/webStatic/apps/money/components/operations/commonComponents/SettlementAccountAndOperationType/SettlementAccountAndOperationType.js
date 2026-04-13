import React from 'react';
import classnames from 'classnames/bind';
import { observer, inject } from 'mobx-react';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import DirectionEnum from '@moedelo/frontend-enums/mdEnums/Direction';
import MoneySourceType from '../../../../../../enums/MoneySourceType';
import MoneySourceDropdown from '../MoneySourceDropdown';
import MoneySourceIcon from '../../../MoneySourceIcon';
import moneySourceHelper from '../../../../../../helpers/newMoney/moneySourceHelper';
import { checkAndShowPaymentModal } from '../../bank/helpers/paymentModalHelper';

import style from './style.m.less';

const cn = classnames.bind(style);

export default inject(`operationStore`)(observer(({ operationStore, onChangeOperationType }) => {
    const {
        model,
        getPrimarySourceList,
        setSettlementAccount,
        // setOperationType,
        OperationTypes,
        canEdit,
        isCurrency,
        currentSettlementAccount
    } = operationStore;

    const {
        SettlementAccountId,
        OperationType
    } = model;

    React.useEffect(() => {
        const isOutgoing = model.Direction === DirectionEnum.Outgoing;

        if (!isOutgoing) {
            return;
        }

        checkAndShowPaymentModal({ currentSettlementAccount });
    }, [model.Direction, currentSettlementAccount]);

    return (
        <React.Fragment>
            <div className={cn(grid.col_9)}>
                <MoneySourceDropdown
                    onSelect={setSettlementAccount}
                    value={SettlementAccountId}
                    data={moneySourceHelper.mapSourceTypeForOperation(getPrimarySourceList())}
                    icon={<MoneySourceIcon value={MoneySourceType.SettlementAccount} />}
                    label={isCurrency ? `Валютный счет` : `Расчетный счет`}
                    showAsText={!canEdit}
                />
            </div>
            <div className={cn(grid.col_1)} />
            <div className={cn(grid.col_7)}>
                <Dropdown
                    onSelect={onChangeOperationType}
                    data={OperationTypes}
                    value={OperationType}
                    label={`Тип платежа`}
                    showAsText={!canEdit}
                />
            </div>
        </React.Fragment>
    );
}));

