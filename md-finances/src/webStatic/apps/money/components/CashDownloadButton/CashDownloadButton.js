import PropTypes from 'prop-types';
import React from 'react';
import Loader from '@moedelo/frontend-core-react/components/Loader/Loader';
import classnames from 'classnames/bind';
import Button, { Color } from '@moedelo/frontend-core-react/components/buttons/Button';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import style from './style.m.less';
import CashDownloadDialog from './CashDownloadDialog';
import cashService from '../../../../services/newMoney/cashService';

const cn = classnames.bind(style);

class CashDownloadButton extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            loading: true
        };
    }

    componentDidMount() {
        cashService.getRegistrationDate()
            .then(res => {
                this.setState({
                    startDate: res.BalanceDate,
                    loading: false
                });
            });
    }

    onCloseDialog = () => {
        this.setState({ isOpen: false });
    };

    openDialog = () => {
        setTimeout(() => {
            this.setState({
                isOpen: true
            });
        }, 100);
    };

    render() {
        const { isOpen } = this.state;
        const { filter } = this.props;
        const { loading } = this.state;
        const { startDate } = this.state;

        if (!loading) {
            return (
                <div>
                    <Button color={Color.White} onClick={this.openDialog}>
                        {svgIconHelper.getJsx({ name: `download` })}
                        Кассовая книга
                    </Button>
                    <Modal
                        header="Скачать кассовую книгу"
                        onClose={this.onCloseDialog}
                        width="307px"
                        visible={isOpen}
                    >
                        <CashDownloadDialog filter={filter} startDate={startDate} onClose={this.onCloseDialog} />
                    </Modal>
                </div>
            );
        }

        return (
            <div className={cn(`loaderSection`)}>
                <Loader className={cn(`loader`)} />
            </div>
        );
    }
}

CashDownloadButton.propTypes = {
    filter: PropTypes.object
};

export default CashDownloadButton;
