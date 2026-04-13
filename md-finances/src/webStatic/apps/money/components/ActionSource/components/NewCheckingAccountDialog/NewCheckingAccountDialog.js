import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import Link from '@moedelo/frontend-core-react/components/Link/Link';

const NewCheckingAccountDialog = observer(props => {
    const {
        onClose, onConfirm, store: { importLoading }
    } = props;

    return (
        <ElementsGroup>
            <Button loading={importLoading} onClick={onConfirm}>Продолжить</Button>
            <Link disabled={importLoading} text={`Отмена`} onClick={onClose} />
        </ElementsGroup>
    );
});

NewCheckingAccountDialog.propTypes = {
    onClose: PropTypes.func,
    onConfirm: PropTypes.func,
    store: PropTypes.object
};

export default NewCheckingAccountDialog;
