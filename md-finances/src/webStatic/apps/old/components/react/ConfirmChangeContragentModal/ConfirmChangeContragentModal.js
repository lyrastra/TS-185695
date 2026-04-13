import React from 'react';
import PropTypes from 'prop-types';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import Link from '@moedelo/frontend-core-react/components/Link';
import style from './style.m.less';

class ConfirmChangeContragentModal extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            visible: true
        };
    }

    onClose = () => {
        const { onReject } = this.props;
        this.setState({
            visible: false
        });
        onReject && onReject();
    }

    onConfirm = () => {
        const { onConfirm } = this.props;
        this.setState({
            visible: false
        });
        onConfirm && onConfirm();
    }

    render() {
        return (
            <Modal
                header="Внимание"
                width={`380`}
                visible={this.state.visible}
                needCloseIcon
                onClose={this.onClose}
            >
                <p>
                    При изменении контрагента<br />
                    связь с документом на возврат будет разорвана.
                </p>

                <ElementsGroup className={style.footer}>
                    <Button onClick={this.onConfirm}>Продолжить</Button>
                    <Link text="Отмена" onClick={this.onClose} />
                </ElementsGroup>
            </Modal>
        );
    }
}

ConfirmChangeContragentModal.propTypes = {
    onReject: PropTypes.func.isRequired,
    onConfirm: PropTypes.func.isRequired
};

export default ConfirmChangeContragentModal;
