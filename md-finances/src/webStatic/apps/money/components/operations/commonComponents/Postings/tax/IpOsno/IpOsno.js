import React from 'react';
import PropTypes from 'prop-types';
import { observer, inject } from 'mobx-react';
import SimplifiedPostingsForm from '../SimplifiedPostingsForm';

const otherOperationTypes = [14, 24, 157];

@inject(`operationStore`)
@observer
class IpOsno extends React.Component {
    render() {
        const { getValidatedIpOsnoPosting } = this.props.operationStore;
        const disableSum = !otherOperationTypes.includes(this.props.operationStore.model.OperationType);

        return <SimplifiedPostingsForm
            disableDescription
            disableSum={disableSum}
            getValidatedPosting={getValidatedIpOsnoPosting}
        />;
    }
}

IpOsno.propTypes = {
    operationStore: PropTypes.object
};

export default IpOsno;
