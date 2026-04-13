import { observer, inject } from 'mobx-react';
import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import TabGroup from '@moedelo/frontend-core-react/components/TabGroup';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Switch from '@moedelo/frontend-core-react/components/Switch';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import AccountingPostings from './accounting/AccountingPostings';
import Osno from './tax/Osno';
import Usn from './tax/Usn';
import IpOsno from './tax/IpOsno';
import taxPostingsValidator from '../../validation/taxPostingsValidator';
import accountingPostingsValidator from '../../validation/accountingPostingsValidator';
import Tabs from '../../../../../../enums/newMoney/PostingsTabsEnum';
import LinkedDocumentsList from './components/LinkedDocumentsList';
import { isDifferenceAvailableInTax } from '../../../../../../helpers/MoneyOperationHelper';
import SyntheticAccountCodesEnum from '../../../../../../enums/SyntheticAccountCodesEnum';
import style from './style.m.less';

const cn = classnames.bind(style);

@inject(`operationStore`)
@observer
class Postings extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            currentTab: Tabs.tax
        };
    }

    onChangeTab = ({ value }) => {
        const { setCurrentPostingsTab } = this.props.operationStore;

        this.setState({ currentTab: value });
        setCurrentPostingsTab && setCurrentPostingsTab(value);
    };

    onSwitchKontragentAccount = ({ checked }) => {
        const { main, other } = this.props.kontragentAccountCodes;
        this.props.operationStore.setKontragentAccountCode({ value: checked ? main : other });
    };

    getTabs = () => {
        const { canViewTaxPostings, canViewAccountingPostings } = this.props.operationStore;

        const list = [];

        if (canViewTaxPostings) {
            list.push({ text: `Налоговый учет`, value: Tabs.tax });
        }

        if (canViewAccountingPostings) {
            list.push({ text: `Бухгалтерский учет`, value: Tabs.accounting });
        }

        return list;
    };

    getTaxPostingsView = () => {
        if (this.props.operationStore.isUsnTaxPostingsView) {
            return Usn;
        } else if (this.props.operationStore.isOsno && this.props.operationStore.isIp) {
            return IpOsno;
        }

        return Osno;
    }

    renderLinkedDocuments = () => {
        const { LinkedDocuments = [] } = this.props.operationStore.model.TaxPostings;

        if (!LinkedDocuments.length) {
            return null;
        }

        return <LinkedDocumentsList
            linkedDocuments={LinkedDocuments}
            isOsno={this.props.operationStore.isOsno}
            isTax
        />;
    };

    renderTaxPostings = () => {
        if (this.props.operationStore.taxPostingsLoading) {
            return <Loader className={style.loader} width={30} />;
        }

        const TaxPostings = this.getTaxPostingsView();

        return (
            <Fragment>
                <TaxPostings />
                {this.renderLinkedDocuments()}
            </Fragment>
        );
    };

    renderPosting() {
        switch (this.state.currentTab) {
            case Tabs.accounting:
                return <AccountingPostings />;
            default:
                return this.renderTaxPostings();
        }
    }

    renderError() {
        const {
            canViewTaxPostings, canViewAccountingPostings, model, sumOperation, isOsno, needAllSumValidation, accountingPostingsLoading, taxPostingsLoading
        } = this.props.operationStore;
        const isDifferenceAllowable = isDifferenceAvailableInTax(model.operationType);

        const { currentTab } = this.state;
        const options = {
            Sum: sumOperation,
            LoanInterestSum: model.LoanInterestSum,
            needAllSumValidation,
            isOsno,
            isDifferenceAllowable
        };

        if (canViewTaxPostings
            && currentTab !== Tabs.tax
            && !taxPostingsLoading
            && !taxPostingsValidator.isValid(model.TaxPostings.Postings, options)) {
            return <div className={style.error}>Проверьте проводки во вкладке &quot;Налоговый учёт&quot;</div>;
        }

        if (canViewAccountingPostings
            && model.ProvideInAccounting
            && currentTab !== Tabs.accounting
            && !accountingPostingsLoading
            && !accountingPostingsValidator.isValid(model.AccountingPostings.Postings, options)
        ) {
            return <div className={style.error}>Проверьте проводки во вкладке &quot;Бухгалтерский учёт&quot;</div>;
        }

        return null;
    }

    renderKontragentAccountCode = () => {
        const { kontragentAccountCodes } = this.props;
        const { model, canEdit, isKontragentAccountCodeDisabled } = this.props.operationStore;

        if (!canEdit || !kontragentAccountCodes) {
            return null;
        }

        return <div className={cn(grid.row, style.accountCode)}>
            <Switch
                text={`Основной контрагент`}
                onChange={this.onSwitchKontragentAccount}
                checked={model.KontragentAccountCode === kontragentAccountCodes.main || model.IsMainContractor}
                disabled={isKontragentAccountCodeDisabled}
            />
            <Tooltip
                width={300}
                position={Position.topLeft}
                content="Выключите, если хотите, чтобы контрагент учитывался как прочий (для опытных бухгалтеров)."
            />
        </div>;
    };

    render() {
        const tabs = this.getTabs();

        if (!tabs.length) {
            return null;
        }

        return <React.Fragment>
            <TabGroup
                tabs={tabs}
                currentTab={this.state.currentTab}
                onChange={this.onChangeTab}
            />
            <div className={cn(style.wrapper)}>
                <div className={cn(style.postings)}>
                    {this.renderPosting()}
                    {this.renderError()}
                </div>
                {this.renderKontragentAccountCode()}
            </div>
        </React.Fragment>;
    }
}

Postings.propTypes = {
    operationStore: PropTypes.object,
    kontragentAccountCodes: PropTypes.shape({
        main: PropTypes.oneOf(SyntheticAccountCodesEnum),
        other: PropTypes.oneOf(SyntheticAccountCodesEnum)
    })
};

export default Postings;
