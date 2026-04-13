import React from 'react';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup/ElementsGroup';
import Link from '@moedelo/frontend-core-react/components/Link/Link';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import Loader from '@moedelo/frontend-core-react/components/Loader/Loader';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import requisitesService from '../../../../services/requisitesService';
import Period from '../Period';
import Store from './Store';
import style from './style.m.less';

const cn = classnames.bind(style);

class BankUpdateDialog extends React.Component {
    constructor(props) {
        super(props);
        const { startDate, endDate } = props.filter;

        this.store = new Store({
            startDate,
            endDate
        });

        this.state = {
            loading: true,
            closedPeriod: ``
        };
    }

    componentDidMount() {
        requisitesService.get()
            .then(res => {
                this.store.setMinDate(res.BalanceDate, res.RegistrationDate, res.LastClosedDate);
                this.store.setClosePeriodDate(res.LastClosedDate);
                this.setState({ loading: false, closedPeriod: res.LastClosedDate });
            });
    }

    onChangePeriod = dates => {
        const { startDate, endDate } = dates;

        this.store.setDate({ startDate, endDate });
        this.store.changeErrorMessage();
    };

    onClick = () => {
        const { startDate, endDate } = this.store;
        const { filter } = this.props;
        const { loading } = this.state;
        const localEndDate = endDate || dateHelper().format(`DD.MM.YYYY`);

        this.onChangePeriod({ startDate, endDate: localEndDate });

        if (this.checkPeriod() && this.isValidPeriod() && !loading) {
            this.props.onClick(startDate, localEndDate, filter.sourceType, filter.sourceId);
            this.setState({
                loading: true
            });
        }
    };

    isValidPeriod = () => {
        const { errorMessage } = this.store;

        return !(errorMessage.startDate
                || errorMessage.endDate
                || errorMessage.common);
    };

    checkPeriod = () => {
        const maxDaysInYear = 366;
        const period = this.periodInput.props.value;
        const { errorMessage } = this.store;
        const localEndDate = dateHelper().format(`DD.MM.YYYY`);
        const startDate = dateHelper(period.startDate, `DD.MM.YYYY`);
        const endDate =
            (period.endDate && dateHelper(period.endDate, `DD.MM.YYYY`))
            || dateHelper(localEndDate, `DD.MM.YYYY`);

        if (endDate.diff(startDate, `days`) > maxDaysInYear) {
            errorMessage.common = `Укажите период не более 1 года.`;
        }

        if (!period.startDate) {
            errorMessage.startDate = `Укажите дату начала`;
        }

        if (!this.isValidPeriod()) {
            this.forceUpdate();

            return null;
        }

        return true;
    };

    render() {
        const { valueByPeriod, errorMessage } = this.store;
        const { buttonText, description } = this.props;
        const { loading } = this.state;
        const canLoadPayment = !errorMessage.startDate && !errorMessage.endDate && !errorMessage.common;

        if (!loading) {
            return (
                <div>
                    Макс. срок выписки – 1 год
                    <div className={cn(`inputSection`)}>
                        <Period
                            className={cn(`periodInput`)}
                            value={valueByPeriod}
                            errorMessage={errorMessage}
                            onChange={this.onChangePeriod}
                            ref={ref => { this.periodInput = ref; }}
                            store={this.store}
                        />
                    </div>
                    <div className={cn(`updateInformation`)}>{description}</div>
                    <ElementsGroup className={cn(`modal__footer`)} margin={20}>
                        <Button
                            onClick={this.onClick}
                            disabled={!canLoadPayment}
                        >
                            {buttonText}
                        </Button>
                        <Link text={`Отмена`} onClick={this.props.onClose} />
                    </ElementsGroup>
                </div>
            );
        }

        return (
            <div className={cn(`updateModal`)}>
                <Loader className={cn(`dialogLoader`)} />
            </div>
        );
    }
}

BankUpdateDialog.propTypes = {
    filter: PropTypes.object,
    buttonText: PropTypes.string,
    description: PropTypes.string,
    onClick: PropTypes.oneOfType([
        PropTypes.object,
        PropTypes.func
    ]),
    onClose: PropTypes.func
};

export default observer(BankUpdateDialog);
