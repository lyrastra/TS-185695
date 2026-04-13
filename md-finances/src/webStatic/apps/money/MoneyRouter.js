import React, { Fragment } from 'react';
import {
    HashRouter as Router, Switch, Route, Redirect
} from 'react-router-dom';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { toInt } from '@moedelo/frontend-core-v2/helpers/converter';
import OpenOperationActions from '../../enums/newMoney/OpenOperationActionsEnum';
import BalanceReconciliation from './components/BalanceReconciliation';
import operationTypes from './../../resources/MoneyOperationTypeResources';
import MoneySourceType from '../../enums/MoneySourceType';
import MoneyMain from './MoneyMain';
import { getEditUrlHash } from '../../helpers/newMoney/operationUrlHelper';
import style from './style.m.less';

const cn = classnames.bind(style);

// Only routing logic
const MoneyRouter = props => {
    return (
        <Router hashType={`noslash`}>
            <Switch>
                <Route
                    path={`/viewReconciliationResults/:guid?`}
                    render={({ match }) => {
                        return (
                            <div className={cn(style.page)}>
                                <div className={cn(style.balanceReconciliationContainer)}>
                                    <BalanceReconciliation guid={match.params.guid} />
                                </div>
                            </div>
                        );
                    }}
                />
                <Route
                    path={`/edit/settlement/:documentBaseId/:operationType?`}
                    render={({ match }) => props.moneyOperation({
                        DocumentBaseId: match.params.documentBaseId,
                        Action: OpenOperationActions.edit,
                        SourceType: MoneySourceType.SettlementAccount,
                        OperationType: match.params.operationType
                    })}
                />
                <Route
                    path={`/copy/settlement/:id/:operationType?`}
                    render={({ match }) => props.moneyOperation({
                        DocumentBaseId: match.params.id,
                        Action: OpenOperationActions.copy,
                        OperationType: match.params.operationType
                    })}
                />
                <Route
                    path={`/add/incoming/settlement/fromContract/:contractId`}
                    render={({ match }) => props.moneyOperation({
                        Direction: Direction.Incoming,
                        ContractId: match.params.contractId,
                        Action: OpenOperationActions.new
                    })}
                />
                <Route
                    path={`/add/incoming/settlement/:settlementId/:operationType?`}
                    render={({ match }) => props.moneyOperation({
                        SettlementAccountId: parseInt(match.params.settlementId, 10),
                        Direction: Direction.Incoming,
                        Action: OpenOperationActions.new,
                        SourceType: MoneySourceType.SettlementAccount,
                        OperationType: match.params.operationType
                    })}
                />
                <Route
                    path={`/add/outgoing/settlement/fromContract/:contractId`}
                    render={({ match }) => props.moneyOperation({
                        ContractId: match.params.contractId,
                        Direction: Direction.Outgoing,
                        Action: OpenOperationActions.new
                    })}
                />
                <Route
                    path={`/add/outgoing/settlement/:settlementId/:operationType?`}
                    render={({ match }) => props.moneyOperation({
                        SettlementAccountId: parseInt(match.params.settlementId, 10),
                        Direction: Direction.Outgoing,
                        Action: OpenOperationActions.new,
                        SourceType: MoneySourceType.SettlementAccount,
                        OperationType: match.params.operationType
                    })}
                />
                <Route
                    path={`/add/paymentForMd/:settlementId/:billNumber`}
                    render={({ match }) => props.moneyOperation({
                        SettlementAccountId: parseInt(match.params.settlementId, 10),
                        Direction: Direction.Outgoing,
                        Action: OpenOperationActions.newPaymentForMd,
                        SourceType: MoneySourceType.SettlementAccount,
                        OperationType: operationTypes.PaymentOrderPaymentToSupplier.value,
                        BillNumber: match.params.billNumber,
                        SkipPaymentChecking: true
                    })}
                />
                <Route
                    path={`/edit/cash/:documentBaseId`}
                    render={({ match }) => props.cashOperation({
                        DocumentBaseId: match.params.documentBaseId,
                        Action: OpenOperationActions.edit
                    })}
                />
                <Route
                    path={`/copy/cash/:id`}
                    render={({ match }) => props.cashOperation({
                        SourceId: match.params.id,
                        Action: OpenOperationActions.copy
                    })}
                />
                <Route
                    path={`/add/incoming/cash/fromContract/:contractId`}
                    render={({ match }) => props.cashOperation({
                        Direction: Direction.Incoming,
                        ContractId: match.params.contractId,
                        Action: OpenOperationActions.new
                    })}
                />
                <Route
                    path={`/add/incoming/cash/:cashId/:operationType?`}
                    render={({ match }) => props.cashOperation({
                        CashId: match.params.cashId,
                        Direction: Direction.Incoming,
                        Action: OpenOperationActions.new,
                        OperationType: match.params.operationType
                    })}
                />
                <Route
                    path={`/add/outgoing/cash/fromContract/:contractId`}
                    render={({ match }) => props.cashOperation({
                        Direction: Direction.Outgoing,
                        ContractId: match.params.contractId,
                        Action: OpenOperationActions.new,
                        OperationType: match.params.operationType
                    })}
                />
                <Route
                    path={`/add/outgoing/cash/:cashId/:operationType?`}
                    render={({ match }) => props.cashOperation({
                        CashId: match.params.cashId,
                        Direction: Direction.Outgoing,
                        Action: OpenOperationActions.new
                    })}
                />
                <Route
                    path={`/add/cash/forRetailReport`}
                    render={() => props.cashOperation({
                        OperationType: operationTypes.CashOrderIncomingFromRetailRevenue.value,
                        Action: OpenOperationActions.new
                    })}
                />
                <Route
                    path={`/edit/purse/:documentBaseId`}
                    render={({ match }) => props.purseOperation({
                        DocumentBaseId: match.params.documentBaseId,
                        Action: OpenOperationActions.edit
                    })}
                />
                <Route
                    path={`/copy/purse/:documentBaseId`}
                    render={({ match }) => props.purseOperation({
                        DocumentBaseId: match.params.documentBaseId,
                        Action: OpenOperationActions.copy
                    })}
                />
                <Route
                    path={`/add/incoming/purse/:purseId`}
                    render={({ match }) => props.purseOperation({
                        PurseId: match.params.purseId,
                        Direction: Direction.Incoming,
                        Action: OpenOperationActions.new
                    })}
                />
                <Route
                    path={`/add/outgoing/purse/:purseId`}
                    render={({ match }) => props.purseOperation({
                        PurseId: match.params.purseId,
                        Direction: Direction.Outgoing,
                        Action: OpenOperationActions.new
                    })}
                />
                <Route
                    path={`/edit/:documentBaseId/:operationType`}
                    render={({ match }) => {
                        const operationUrl = getEditUrlHash({
                            DocumentBaseId: match.params.documentBaseId,
                            OperationType: toInt(match.params.operationType)
                        });

                        return <Redirect to={`/${operationUrl || ``}`} />;
                    }}
                />
                <Route
                    path={`/reload/:url`}
                    render={() => {
                        const url = window.location.hash.replace(`#reload`, ``);

                        return <Redirect to={url} />;
                    }}
                />
                <Route render={() => {
                    return (
                        <Fragment>
                            <MoneyMain {...props} />
                            <Switch>
                                <Route path={`/add/incoming`} render={() => props.addIncoming()} />
                                <Route path={`/add/outgoing`} render={() => props.addOutgoing()} />
                            </Switch>
                        </Fragment>
                    );
                }}
                />
            </Switch>
        </Router>
    );
};

MoneyRouter.propTypes = {
    moneyOperation: PropTypes.func.isRequired,
    cashOperation: PropTypes.func.isRequired,
    purseOperation: PropTypes.func.isRequired,
    moneySourceStore: PropTypes.object.isRequired,
    addIncoming: PropTypes.func.isRequired,
    addOutgoing: PropTypes.func.isRequired
};

export default MoneyRouter;
