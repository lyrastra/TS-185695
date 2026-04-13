import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import Input from '@moedelo/frontend-core-react/components/Input';

const DocumentNumberAutocomplete = ({ operationStore }) => {
    const {
        model: { DeductionWorkerDocumentNumber, DeductionWorkerId }, deductionWorkerDocumentNumberValue,
        setDeductionWorkerDocumentNumber, canEdit, validationState, getDeductionWorkerDocumentsAutocomplete
    } = operationStore;

    const label = `№ документа (108)`;

    if (!canEdit) {
        return <Input
            label={label}
            value={deductionWorkerDocumentNumberValue}
            showAsText
        />;
    }

    return <Autocomplete
        getData={getDeductionWorkerDocumentsAutocomplete}
        label={label}
        value={DeductionWorkerDocumentNumber}
        onChange={setDeductionWorkerDocumentNumber}
        disabled={!DeductionWorkerId}
        iconName={DeductionWorkerDocumentNumber ? `` : `none`}
        error={!!validationState.DeductionWorkerDocumentNumber}
        message={validationState.DeductionWorkerDocumentNumber}
    />;
};

DocumentNumberAutocomplete.propTypes = {
    operationStore: PropTypes.object
};

export default observer(DocumentNumberAutocomplete);
