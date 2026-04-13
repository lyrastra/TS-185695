import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';

const DeductionWorkerAutocomplete = ({ operationStore }) => {
    const {
        model: { DeductionWorkerFio }, setDeductionWorker, canEdit,
        getDeductionWorkersAutocomplete, validationState
    } = operationStore;

    return <Autocomplete
        getData={getDeductionWorkersAutocomplete}
        label={`Сотрудник-плательщик удержаний`}
        value={DeductionWorkerFio}
        onChange={setDeductionWorker}
        showAsText={!canEdit}
        iconName={DeductionWorkerFio ? `` : `none`}
        error={!!validationState.DeductionWorkerFio}
        message={validationState.DeductionWorkerFio}
    />;
};

DeductionWorkerAutocomplete.propTypes = {
    operationStore: PropTypes.object
};

export default observer(DeductionWorkerAutocomplete);
