import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import SimplifiedPostingsForm from '../SimplifiedPostingsForm';

const otherOperationTypes = [14, 24, 157];

const IpOsno = observer(({ operationStore }) => {
    const { getValidatedIpOsnoPosting } = operationStore;
    const disableSum = !otherOperationTypes.includes(operationStore.model.OperationType);

    return <SimplifiedPostingsForm
        disableDescription
        disableSum={disableSum}
        getValidatedPosting={getValidatedIpOsnoPosting}
        operationStore={operationStore}
    />;
});

IpOsno.propTypes = {
    operationStore: PropTypes.object.isRequired
};

export default IpOsno;
