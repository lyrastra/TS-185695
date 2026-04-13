import React from 'react';
import PropTypes from 'prop-types';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import { observer } from 'mobx-react';
import { toJS } from 'mobx';
import _ from 'underscore';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import classnames from 'classnames/bind';
import OperationDirectionResource from './../../../../../../../resources/OperationDirectionResources';
import LegalType from '../../../../../../../enums/LegalTypeEnum';
import { getIncomingOperationTypeByLegalType, getOutgoingOperationTypeByLegalType } from './../../../../../../../resources/OperationTypeResources';
import { cashOrderOperationResources, paymentOrderOperationResources } from './../../../../../../../resources/MoneyOperationTypeResources';
import { getBudgetaryTypeByLegalType } from './../../../../../../../resources/BudgetaryTypeResources';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class OperationType extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.store;
        this.isCurrencyOperationsAvailable = this.store.isCurrencyOperationsAvailable;
    }

    getOperationTypeData = () => {
        const legalType = window._preloading.IsOoo ? LegalType.Ooo : LegalType.Ip;

        switch (this.store.direction) {
            case Direction.Incoming:
                return this.getOperationTypeByDirection(getIncomingOperationTypeByLegalType(legalType, this.isCurrencyOperationsAvailable).data);
            case Direction.Outgoing:
                return this.getOperationTypeByDirection(getOutgoingOperationTypeByLegalType(legalType, this.isCurrencyOperationsAvailable).data);
            default:
                return this.getAllOperationTypes(legalType);
        }
    }

    getBudgetaryOperationTypeData = () => {
        const legalType = window._preloading.IsOoo ? LegalType.Ooo : LegalType.Ip;

        const all = {
            value: 0,
            text: `Все`
        };

        const budgetaryTypes = getBudgetaryTypeByLegalType(legalType);

        return [all, ...budgetaryTypes];
    }

    getAllOperationTypes = legalType => {
        const all = [{
            value: [0],
            text: `Все`
        }];

        const outgoingOperationTypeList = getOutgoingOperationTypeByLegalType(legalType, this.isCurrencyOperationsAvailable);
        outgoingOperationTypeList.data = this.filterTypes(outgoingOperationTypeList.data);

        return [all, outgoingOperationTypeList, getIncomingOperationTypeByLegalType(legalType, this.isCurrencyOperationsAvailable)];
    }

    getOperationTypeByDirection = operationTypes => {
        const all = {
            value: [0],
            text: `Все`
        };

        const opTypes = this.filterTypes(operationTypes);

        return [all, ...opTypes];
    }

    filterTypes(operationTypes) {
        const { cashCount, bankCount } = this.props;
        let opTypes;

        if (cashCount) {
            opTypes = _.filter(operationTypes, item => {
                return !_.isEqual(item.value, [cashOrderOperationResources.CashOrderOutgoingTranslatedToOtherCash])
                    || !_.isEqual(item.value, [cashOrderOperationResources.CashOrderIncomingFromAnotherCash]);
            });
        }

        if (bankCount) {
            opTypes = _.filter(operationTypes, item => {
                return !_.isEqual(item.value, [paymentOrderOperationResources.PaymentOrderOutgoingTransferToAccount])
                    || !_.isEqual(item.value, [paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount]);
            });
        }

        return opTypes;
    }

    render() {
        return (<div className={cn(`operationTypeFilter`)}>
            <Dropdown
                data={OperationDirectionResource}
                value={this.store.direction}
                type={`link`}
                onSelect={e => this.store.setDirection(e)}
            />
            <Dropdown
                className={cn(`operationTypeFilter__select`)}
                data={this.getOperationTypeData()}
                value={toJS(this.store.operationType)}
                onSelect={e => this.store.setOperationType(e)}
            />
            { this.store.isBudgetaryType && <Dropdown
                className={cn(`operationTypeFilter__select`)}
                data={this.getBudgetaryOperationTypeData()}
                multilineText
                value={this.store.budgetaryType}
                onSelect={e => this.store.setBudgetaryType(e)}
            /> }
        </div>
        );
    }
}

OperationType.propTypes = {
    store: PropTypes.object,
    cashCount: PropTypes.number,
    bankCount: PropTypes.number
};

export default OperationType;
