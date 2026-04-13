import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import cn from 'classnames';
import Sum from './ObserverSum';
import { hasReserveFeature } from '../../../../../../helpers/MoneyOperationHelper';
import SaveReserve from './SaveReserve';
import style from './style.m.less';

function ReserveSum(props) {
    const {
        ReserveSum: sum,
        OperationType,
        CanEditReserve
    } = props.operationStore.model;

    if (!hasReserveFeature(OperationType) || (!sum && !CanEditReserve)) {
        return null;
    }

    return (
        <div className={cn(props.className, style.wrapper)}>
            <Sum
                operationStore={props.operationStore}
            />
            <SaveReserve operationStore={props.operationStore} />
        </div>
    );
}

ReserveSum.propTypes = {
    className: PropTypes.string,
    operationStore: PropTypes.object
};

export default observer(ReserveSum);
