import React from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import style from './style.m.less';
import Period from '../Period/Period';
import localDateHelper from '../../../../helpers/date';
import cashService from '../../../../services/newMoney/cashService';
import Store from './stores/CashDownloadStore';

const cn = classnames.bind(style);

class CashDownloadDialog extends React.Component {
    constructor(props) {
        super(props);

        const startDateValue = this.props.startDate ?
            localDateHelper.toString(this.props.startDate) :
            localDateHelper.toString(localDateHelper.firstDayOfYear((new Date()).getFullYear()));
        this.state = {
            canDownloadFile: true
        };
        this.store = new Store({
            startDate: startDateValue,
            endDate: localDateHelper.toString(Date.now())
        });

        if (this.props.startDate) {
            this.store.setBalanceDate(localDateHelper.toString(this.props.startDate));
        }

        this.store.setMinDate(localDateHelper.toString(this.props.startDate));
    }

    onButtonClick = format => {
        if (!this.state.canDownloadFile) {
            this.store.setErrorMessage({ common: `Нет кассовых документов в указанном периоде` });

            return this.forceUpdate();
        }

        this.setState({ canDownloadFile: false });

        const { startDate, endDate } = this.store;
        const endDateValue = !endDate ? localDateHelper.toString(Date.now()) : endDate;

        this.onChangePeriod({ startDate, endDate: endDateValue });
        this.checkPeriod();

        return this.isValidPeriod() && this._downloadFile(format);
    }

    onChangePeriod = ({ startDate, endDate }) => {
        this.store.setDate({ startDate, endDate });
        this.store.changeErrorMessage();

        const spareDate = endDate || dateHelper().format(`DD.MM.YYYY`);

        if (!startDate || (localDateHelper.year(startDate) !== localDateHelper.year(spareDate)) || this.store.isValidBalanceDate) {
            this.setState({ canDownloadFile: false });
        } else {
            this.setState({ canDownloadFile: true });
        }
    }

    checkPeriod = () => {
        const period = this.periodInput.props.value;
        const { errorMessage } = this.store;

        if (!period.startDate) {
            errorMessage.startDate = `Укажите дату начала`;
        }

        return this.isValidPeriod() ? `` : this.forceUpdate();
    }

    isValidPeriod = () => {
        const { errorMessage } = this.store;

        return !(errorMessage.startDate || errorMessage.endDate || errorMessage.common);
    }

    _downloadFile = format => {
        const { startDate, endDate } = this.store;
        const cashId = this.props.filter.sourceId;
        const timeout = 60000;

        cashService.hasCashOperationInPeriod({ cashId, startDate, endDate })
            .then(resp => {
                if (resp) {
                    cashService.downloadImportFile({
                        cashId, startDate, endDate, format, timeout
                    });

                    return this._onClose();
                }

                this.store.setErrorMessage({ common: `Нет кассовых документов в указанном периоде` });

                return this.forceUpdate();
            })
            .catch(() => {
                NotificationManager.show({
                    message: `При загрузке файла произошла ошибка`,
                    type: `error`,
                    duration: 2000
                });
            });
    }

    _onClose = () => {
        return this.props.onClose();
    }

    _getRefs = item => {
        this.periodInput = item;
    }

    render() {
        const { valueByPeriod, errorMessage } = this.store;

        return (
            <div>
                <Period
                    className={cn(`periodInput`)}
                    value={valueByPeriod}
                    errorMessage={errorMessage}
                    onChange={this.onChangePeriod}
                    ref={this._getRefs}
                />
                <ElementsGroup className={cn(`modal__footer`)} margin={20}>
                    <Button
                        onClick={() => this.onButtonClick(`pdf`)}
                        disabled={!this.state.canDownloadFile}
                        color="red"
                    >
                        {svgIconHelper.getJsx({ name: `download` })}
                        PDF
                    </Button>
                    <Button
                        onClick={() => this.onButtonClick(`xls`)}
                        disabled={!this.state.canDownloadFile}
                    >
                        {svgIconHelper.getJsx({ name: `download` })}
                        XLS
                    </Button>
                </ElementsGroup>
            </div>
        );
    }
}

CashDownloadDialog.propTypes = {
    filter: PropTypes.object,
    onClose: PropTypes.func,
    startDate: PropTypes.Date
};

export default CashDownloadDialog;
