import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import SimplifiedPostingsForm from '../SimplifiedPostingsForm';

const Usn = observer(props => {
    const { operationStore } = props;
    const { getValidatedUsnPosting } = operationStore;

    return <SimplifiedPostingsForm operationStore={operationStore} getValidatedPosting={getValidatedUsnPosting} />;
});

Usn.propTypes = {
    operationStore: PropTypes.object.isRequired
};

export default Usn;
