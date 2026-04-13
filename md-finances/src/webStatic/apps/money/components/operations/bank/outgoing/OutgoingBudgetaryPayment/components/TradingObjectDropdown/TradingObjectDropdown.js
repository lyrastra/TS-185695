import React from 'react';
import { observer } from 'mobx-react';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import * as PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';

const TradingObjectDropdown = ({ operationStore }) => {
    if (!operationStore.isTradingFee) {
        return null;
    }

    const { TradingObject: errorMessage } = operationStore.validationState;

    return (
        <div className={grid.row}>
            <div className={grid.col_9}>
                <Dropdown
                    label={`Торговый объект`}
                    value={operationStore.model.TradingObjectId || 0}
                    onSelect={operationStore.setTradingObjectId}
                    data={operationStore.TradingObjectList}
                    error={!!errorMessage}
                    message={errorMessage}
                    width={400}
                    showAsText={!operationStore.canEdit}
                />
            </div>
            <div className={grid.col_1} />
        </div>
    );
};

TradingObjectDropdown.propTypes = {
    operationStore: PropTypes.object
};

export default observer(TradingObjectDropdown);
