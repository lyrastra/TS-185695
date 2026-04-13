import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';

const CashOrderAutocomplete = observer(({ operationStore, label = `m  ` }) => {
    const { model, canEdit } = operationStore;
    const { CashOrder } = model;
    const value = CashOrder.DocumentName || (canEdit ? `` : `не указано`);

    return (
        <Autocomplete
            label={label}
            className={grid.col_9}
            getData={operationStore.getCashOrderAutocomplete}
            value={value}
            onChange={operationStore.setCashOrder}
            iconName={CashOrder.DocumentName ? `` : `none`}
            showAsText={!canEdit}
        />
    );
});

CashOrderAutocomplete.propTypes = {
    label: PropTypes.string,
    operationStore: PropTypes.shape({
        getCashOrderAutocomplete: PropTypes.func.isRequired,
        setCashOrder: PropTypes.func.isRequired,
        model: PropTypes.object.isRequired,
        canEdit: PropTypes.bool.isRequired
    })
};

export default CashOrderAutocomplete;
