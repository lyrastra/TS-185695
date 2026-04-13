import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Link from '@moedelo/frontend-core-react/components/Link';

const BusyNumberModal = observer(({ store }) => {
    return (
        <Modal
            header={`Внимание`}
            onClose={store.clearBusyNumberState}
            visible={store.isBusyNumberModalShown}
        >
            <p>Номер документа не уникален, вы хотите добавить еще один документ с таким номером?</p>
            <ElementsGroup>
                <Button onClick={store.handleSaveOperation} loading={store.savePaymentPending}>Да</Button>
                <Link onClick={store.clearBusyNumberState} type={`modal`} loading={store.savePaymentPending}>Отмена</Link>
            </ElementsGroup>
        </Modal>
    );
});

BusyNumberModal.propTypes = {
    store: PropTypes.object.isRequired
};

export default BusyNumberModal;
