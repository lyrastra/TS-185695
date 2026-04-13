import React, { Fragment } from 'react';
import { observer, inject } from 'mobx-react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import P from '@moedelo/frontend-core-react/components/P';
import Switch from '@moedelo/frontend-core-react/components/Switch';
import NotificationPanel from '@moedelo/frontend-core-react/components/NotificationPanel';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import Row from './Row';
import LinkedDocumentsList from '../../components/LinkedDocumentsList';
import style from './style.m.less';
import accountingPostingsValidator from '../../../../validation/accountingPostingsValidator';

const cn = classnames.bind(style);

@inject(`operationStore`)
@observer
class AccountingPostings extends React.Component {
    constructor() {
        super();

        this.state = {
            isLinkedDocumentsVisible: false
        };
    }

    onChangePostingState = ({ checked }) => {
        this.props.operationStore.setProvideInAccounting(checked);
    };

    renderLinkedDocuments = () => {
        const { LinkedDocuments = [] } = this.props.operationStore.model.AccountingPostings;

        if (!LinkedDocuments.length) {
            return null;
        }

        return <LinkedDocumentsList
            linkedDocuments={LinkedDocuments}
            isOsno={this.props.operationStore.isOsno}
        />;
    };

    renderPostings = () => {
        const {
            model,
            getDebits,
            getCredits,
            updateAccountingPostingList,
            messageNoAccountingObjects
        } = this.props.operationStore;

        if (!model.ProvideInAccounting) {
            return null;
        }

        const { Postings } = model.AccountingPostings;

        if (!Postings || !Postings.length) {
            return null;
        }

        return <Fragment>
            <div className={cn(style.head, grid.row)}>
                <div className={cn(style.debet, grid.col_2)}>
                    Дебет
                </div>
                <div className={cn(style.debetSubconto, grid.col_5)}>
                    Объект учета
                </div>
                <div className={cn(style.credit, grid.col_2)}>
                    Кредит
                </div>
                <div className={cn(style.creditSubconto, grid.col_5)}>
                    Объект учета
                </div>
                <div className={grid.col_3}>
                    <div className={style.sum}>Сумма</div>
                </div>
                <div className={cn(style.description, grid.col_6)}>
                    Комментарий
                </div>
            </div>
            {Postings.map(posting => {
                return <Row
                    key={posting.key}
                    posting={posting}
                    getDebits={getDebits}
                    getCredits={getCredits}
                    onChange={updateAccountingPostingList}
                    dateDocument={model.Date}
                    settlementAccountId={model.SettlementAccountId}
                    messageNoAccountingObjects={messageNoAccountingObjects}
                />;
            })}
        </Fragment>;
    };

    renderSwitch = () => {
        const { model, canToggleProvideInAccounting, canShowAccountingPostingsSwitch } = this.props.operationStore;

        if (!canShowAccountingPostingsSwitch) {
            return null;
        }

        return <div className={grid.row}>
            <Switch
                text={`Учитывается`}
                onChange={this.onChangePostingState}
                checked={model.ProvideInAccounting}
                disabled={!canToggleProvideInAccounting}
            />
        </div>;
    };

    renderError = () => {
        const { model, sumOperation } = this.props.operationStore;
        const msg = accountingPostingsValidator.getAllSumValidation(model.AccountingPostings.Postings, { Sum: sumOperation });

        if (msg) {
            return <div className={cn(style.error)}>
                {msg}
            </div>;
        }

        return null;
    };

    render() {
        const { ExplainingMessage = null, Error } = this.props.operationStore.model.AccountingPostings;
        const { accountingPostingsLoading, needAllSumValidation } = this.props.operationStore;

        if (accountingPostingsLoading) {
            return <Loader className={style.loader} width={30} />;
        }

        if (Error) {
            return <NotificationPanel type="error" canClose={false}>{Error}</NotificationPanel>;
        }

        if (ExplainingMessage) {
            return <P>{ExplainingMessage}</P>;
        }

        return <Fragment>
            {this.renderSwitch()}
            {this.renderPostings()}
            {this.renderLinkedDocuments()}
            {needAllSumValidation && this.renderError()}
        </Fragment>;
    }
}

AccountingPostings.propTypes = {
    operationStore: PropTypes.object
};

export default AccountingPostings;
