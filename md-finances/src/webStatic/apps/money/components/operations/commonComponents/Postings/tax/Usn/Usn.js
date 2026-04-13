import React from 'react';
import PropTypes from 'prop-types';
import { observer, inject } from 'mobx-react';
import SimplifiedPostingsForm from '../SimplifiedPostingsForm';

@inject(`operationStore`)
@observer
class Usn extends React.Component {
    render() {
        const { getValidatedUsnPosting } = this.props.operationStore;

        return <SimplifiedPostingsForm getValidatedPosting={getValidatedUsnPosting} />;
    }
}

Usn.propTypes = {
    operationStore: PropTypes.object
};

export default Usn;
