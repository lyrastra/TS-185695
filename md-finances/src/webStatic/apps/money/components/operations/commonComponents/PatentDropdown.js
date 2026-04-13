import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';

const PatentDropdown = observer(({ className, operationStore }) => {
    const {
        canEdit,
        patentSelectData,
        patentValue,
        setPatentId,
        validationState
    } = operationStore;

    return <div className={className}>
        <Dropdown
            onSelect={setPatentId}
            data={patentSelectData}
            label={`Патент`}
            value={patentValue}
            showAsText={!canEdit}
            className={`qa-patent`}
            error={!!validationState.Patent}
            message={validationState.Patent}
        />
    </div>;
});

PatentDropdown.propTypes = {
    className: PropTypes.string,
    operationStore: PropTypes.shape({
        canEdit: PropTypes.bool.isRequired,
        patentSelectData: PropTypes.arrayOf(PropTypes.object),
        setPatentId: PropTypes.func.isRequired,
        patentValue: PropTypes.number,
        validationState: PropTypes.object
    })
};

export default PatentDropdown;
