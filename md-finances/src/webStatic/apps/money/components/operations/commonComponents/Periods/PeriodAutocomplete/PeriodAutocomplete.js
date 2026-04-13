import React from 'react';
import { observer, inject } from 'mobx-react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import style from '../style.m.less';

const cn = classnames.bind(style);

@inject(`operationStore`)
@observer
class PeriodAutocomplete extends React.Component {
    constructor() {
        super();

        this.autocompleteRef = React.createRef();
    }

    render() {
        const { operationStore, value, className } = this.props;
        const {
            model,
            canEdit,
            operationToModelMapping,
            contractLoading,
            validationState
        } = operationStore;
        const text = value.Description || (canEdit ? `` : `не указано`);
        const showError = validationState.Period && !value.Description;
        const loading = operationToModelMapping || contractLoading;

        return <Autocomplete
            label={`Период`}
            className={className ? cn(className) : ``}
            getData={operationStore.getPeriodAutocomplete}
            value={text}
            onChange={this.props.onChange}
            iconName={`none`}
            showAsText={!canEdit}
            error={!!showError}
            message={validationState.Period}
            ref={this.autocompleteRef}
            loading={loading}
            disabled={loading || !model.Contract.ContractBaseId}
        />;
    }
}

PeriodAutocomplete.defaultProps = {
};

PeriodAutocomplete.propTypes = {
    operationStore: PropTypes.object,
    value: PropTypes.object,
    className: PropTypes.string,
    onChange: PropTypes.func
};

export default PeriodAutocomplete;
