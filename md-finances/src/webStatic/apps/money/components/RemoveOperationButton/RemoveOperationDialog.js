import React from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup/ElementsGroup';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import Link from '@moedelo/frontend-core-react/components/Link/Link';
import style from './style.m.less';

const cn = classnames.bind(style);

class RemoveOperationDialog extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            loading: false
        };
    }

    componentDidUpdate({ visible: prevVisible }) {
        if (prevVisible && !this.props.visible) {
            this.setState({
                loading: false
            });
        }
    }

    onClick = () => {
        this.setState({
            loading: true
        });
        this.props.onButtonClick();
    }

    render() {
        const {
            description, disabled, onClose, visible, header
        } = this.props;
        const { loading } = this.state;

        return (
            <Modal
                onClose={onClose}
                header={header}
                visible={visible}
                width={`307px`}
            >
                {description()}
                <ElementsGroup className={cn(`modal__footer`)} margin={20}>
                    <Button
                        onClick={this.onClick}
                        color={disabled() ? `red` : `grey`}
                        disabled={!disabled()}
                        loading={loading}
                    >
                        Удалить
                    </Button>
                    <Link text={`Отмена`} onClick={onClose} />
                </ElementsGroup>
            </Modal>
        );
    }
}

RemoveOperationDialog.propTypes = {
    onButtonClick: PropTypes.func,
    description: PropTypes.func,
    disabled: PropTypes.func,
    onClose: PropTypes.func,
    visible: PropTypes.bool,
    header: PropTypes.string
};

export default RemoveOperationDialog;
