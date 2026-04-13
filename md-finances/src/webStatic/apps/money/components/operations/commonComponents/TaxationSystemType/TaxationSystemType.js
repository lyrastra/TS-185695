import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';

const TaxationSystemType = observer(({ operationStore }) => {
    const {
        setTaxationSystemType,
        canEdit,
        taxationSystemTypeData,
        taxationSystemTypeValue,
        validationState
    } = operationStore;

    return <React.Fragment>
        <div className={grid.col_3}>
            <Dropdown
                onSelect={setTaxationSystemType}
                data={taxationSystemTypeData}
                label={`Учесть в`}
                value={taxationSystemTypeValue}
                showAsText={!canEdit}
                error={!!validationState.TaxationSystemType}
                message={validationState.TaxationSystemType}
            />
        </div>
        <div className={grid.col_1} />
    </React.Fragment>;
});

TaxationSystemType.propTypes = {
    operationStore: PropTypes.shape({
        setTaxationSystemType: PropTypes.func.isRequired,
        model: PropTypes.object.isRequired,
        canEdit: PropTypes.bool.isRequired,
        validationState: PropTypes.object,
        taxationSystemTypeData: PropTypes.arrayOf(PropTypes.shape({
            value: PropTypes.number,
            text: PropTypes.text
        }))
    })
};

export default TaxationSystemType;
