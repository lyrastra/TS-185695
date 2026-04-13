import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import { toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import Sum from './Sum';

function ObserverSum(props) {
    const {
        ReserveSum: sum,
        CanEditReserve
    } = props.operationStore.model;

    const {
        setReserveSum,
        sumCurrencySymbol,
        setReserveSumSaved,
        toggleReserve,
        deleteReserve,
        reserve: {
            opened
        }
    } = props.operationStore;

    const { ReserveSum: errorMessage } = props.operationStore.validationState;

    return (
        <Sum
            label={`Резерв`}
            showAsText={!CanEditReserve}
            value={toFinanceString(sum) || ``}
            onChange={setReserveSum}
            onEnter={() => setReserveSumSaved(false)}
            unit={sumCurrencySymbol}
            error={errorMessage}
            onOpen={() => toggleReserve(true)}
            onClose={deleteReserve}
            isOpen={opened}
        />
    );
}

ObserverSum.propTypes = {
    operationStore: PropTypes.object
};

export default observer(ObserverSum);
