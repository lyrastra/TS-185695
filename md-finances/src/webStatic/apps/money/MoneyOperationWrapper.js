import React from 'react';
import { inject, observer } from 'mobx-react';
import PropTypes from 'prop-types';
import _ from 'underscore';
import BankOperation from './components/operations/bank';
import PageLoader from '../../components/PageLoader';
import bankOperationService from '../../services/newMoney/bankOperationService';
import { getNdsRatesAsync } from '../../services/ndsRatesFromAccPolService';

@inject(`moneySourceStore`)
@observer
class MoneyOperationWrapper extends React.Component {
    constructor(props) {
        super(props);

        this.state = { loaded: false };
    }

    async componentDidMount() {
        this.loadOperationData();
    }

    async componentDidUpdate(prevProps) {
        if (!_.isEqual(prevProps.options, this.props.options)) {
            this.setState({ loaded: false });
            this.loadOperationData();
        }
    }

    loadOperationData = async () => {
        const { options, moneySourceStore: { sourceList } } = this.props;
        const data = Object.assign({}, options, { sourceList });

        this.setState({
            loaded: true,
            operationData: await bankOperationService.loadBigOperationData(data),
            ndsRatesFromAccPolicy: await getNdsRatesAsync() || []
        });
    };

    newMoneyOperation = () => {
        const { moneySourceStore: { viewPaymentNotificationPanel, setViewPaymentNotificationPanel } } = this.props;

        return <BankOperation
            {...this.state.operationData}
            ndsRatesFromAccPolicy={this.state.ndsRatesFromAccPolicy}
            onSave={this.props.onSave}
            onDelete={this.props.onDelete}
            onCancel={this.props.onCancel}
            viewPaymentNotificationPanel={viewPaymentNotificationPanel}
            setViewPaymentNotificationPanel={setViewPaymentNotificationPanel}
        />;
    };

    render() {
        if (!this.state.loaded) {
            return <PageLoader />;
        }

        return this.newMoneyOperation();
    }
}

MoneyOperationWrapper.propTypes = {
    options: PropTypes.object,
    onSave: PropTypes.func,
    onDelete: PropTypes.func,
    onCancel: PropTypes.func,
    moneySourceStore: PropTypes.object
};

export default MoneyOperationWrapper;
