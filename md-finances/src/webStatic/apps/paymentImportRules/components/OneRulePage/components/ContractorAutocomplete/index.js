import React from 'react';
import PropTypes from 'prop-types';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import { autocomplete } from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentAutocompleteService';

const ContractorAutocomplete = ({
    className, onChange, value, disabled, error, kontragentName
}) => {
    const getItems = async ({ query }) => {
        const list = await autocomplete({ query: query || kontragentName });
        const autocompleteData = list.map(item => ({
            value: item,
            text: item.Name
        }));

        return {
            data: autocompleteData,
            value: query
        };
    };

    const onChangeHandler = ({ value: val }) => {
        if (!val?.Id) {
            onChange({ Id: null, Name: `` });

            return;
        }

        onChange(val);
    };

    return <Autocomplete
        placeholder={`Выберите контрагента`}
        value={value}
        getData={getItems}
        onChange={onChangeHandler}
        className={className}
        disabled={disabled}
        error={!!error}
        message={error}
        multilineText
    />;
};

ContractorAutocomplete.defaultProps = {
    disabled: false
};

ContractorAutocomplete.propTypes = {
    onChange: PropTypes.func.isRequired,
    className: PropTypes.string,
    value: PropTypes.string,
    disabled: PropTypes.bool,
    error: PropTypes.string,
    kontragentName: PropTypes.string
};

export default ContractorAutocomplete;
