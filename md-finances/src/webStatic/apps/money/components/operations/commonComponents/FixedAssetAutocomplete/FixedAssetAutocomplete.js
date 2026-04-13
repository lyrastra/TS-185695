import React from 'react';
import { observer, inject } from 'mobx-react';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';

@inject(`operationStore`)
@observer
class FixedAssetAutocomplete extends React.Component {
    constructor() {
        super();

        this.autocompleteRef = React.createRef();
    }

    render() {
        const { operationStore } = this.props;
        const { model, canEdit } = operationStore;
        const { FixedAsset } = model;
        const text = `${FixedAsset.Name || ``}`;
        const value = text.trim() || (canEdit ? `` : `не указано`);

        return <div className={grid.row}>
            <Autocomplete
                label={`Основное средство`}
                className={grid.col_9}
                getData={operationStore.getFixedAssetAutocomplete}
                error={!!operationStore.validationState.FixedAsset}
                message={operationStore.validationState.FixedAsset}
                value={value}
                onChange={operationStore.setFixedAsset}
                iconName={FixedAsset.Number ? `` : `none`}
                showAsText={!canEdit}
                ref={this.autocompleteRef}
            />
            <div className={grid.col_1} />
        </div>;
    }
}

FixedAssetAutocomplete.defaultProps = {
};

FixedAssetAutocomplete.propTypes = {
    operationStore: PropTypes.object
};

export default FixedAssetAutocomplete;
