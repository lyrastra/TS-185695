import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import React from 'react';
import { observer } from 'mobx-react';
import { toJS } from 'mobx';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import Link from '@moedelo/frontend-core-react/components/Link';
import converter from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import requisitesService from '@moedelo/frontend-common-v2/apps/requisites/services/requisitesService';
import UsnType from '@moedelo/frontend-enums/mdEnums/UsnType';
import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import sessionStorageHelper from '@moedelo/frontend-core-v2/helpers/sessionStorageHelper';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import DateFilter from '../MainTable/components/Filter/DateFilter';
import KontragentSection from './components/KontragentSection';
import Store from './Store';
import KontragentType from '../../../../enums/KontragentType';
import OperationType from '../MainTable/components/Filter/OperationType';
import style from './style.m.less';
import SumSection from './components/SumSection';
import ProvideInTaxSection from './components/ProvideInTaxSection';
import ClosingDocumentsSection from './components/ClosingDocumentsSection';
import TaxationSystemSection from './components/TaxationSystemSection';
import ApproveSection from './components/ApproveSection/ApproveSection';

const cn = classnames.bind(style);

class AdvancedSearch extends React.Component {
    constructor(props) {
        super(props);
        const {
            startDate,
            endDate,
            kontragentType,
            direction,
            kontragentId,
            operationType,
            budgetaryType,
            sumCondition,
            sum,
            sumFrom,
            sumTo,
            provideInTax,
            closingDocumentsCondition,
            taxationSystemType,
            patentId,
            approvedCondition
        } = props.value;

        this.store = new Store({
            startDate,
            endDate,
            kontragentId,
            kontragentType: parseInt(kontragentType, 10) || KontragentType.All,
            direction: parseInt(direction, 10),
            operationType: this.getOperationType(operationType),
            budgetaryType: parseInt(budgetaryType, 10),
            sumCondition: converter.toInt(sumCondition),
            sum: converter.toFloat(sum),
            sumFrom: converter.toFloat(sumFrom),
            sumTo: converter.toFloat(sumTo),
            provideInTax: converter.toInt(provideInTax),
            closingDocumentsCondition: converter.toInt(closingDocumentsCondition),
            approvedCondition: converter.toInt(approvedCondition),
            taxationSystemType: converter.toInt(taxationSystemType),
            patentId: converter.toInt(patentId)
        });

        this.state = {
            loading: true
        };
    }

    componentDidMount() {
        Promise.all([
            requisitesService.get(),
            taxationSystemService.getTaxSystem(),
            taxationSystemService.getAll(),
            taxationSystemService.getAllPatents(),
            userDataService.get()
        ]).then(([requisites, taxationSystem, taxationSystemList, patentList, userData]) => {
            const { BalanceDate, RegistrationDate } = requisites;

            const isoRegistrationDate = dateHelper(RegistrationDate, `DD.MM.YYYY`).format(`YYYY-MM-DD`);
            const dates = [isoRegistrationDate];

            this.store.setUserData(userData);
            this.store.setIsOutsourceUser(userData);

            if (BalanceDate && !this.store.needCheckFilterForSourceFirm) {
                const momentBalanceDate = dateHelper(BalanceDate, `DD.MM.YYYY`);
                const isoBalanceDate = momentBalanceDate.startOf(`Year`).format(`YYYY-MM-DD`);
                dates.push(isoBalanceDate);
            }

            this.store.setMinDate(...dates);

            const isCurrencyAvailable = taxationSystem.IsUsn &&
                [UsnType.Profit, UsnType.ProfitAndOutgo].includes(taxationSystem.UsnType) &&
                this.props.moneySourceStore.hasCurrencySettlementAccount;
            this.store.setIsCurrencyOperationsAvailable({ isCurrencyAvailable });
            this.store.setTaxationSystems(taxationSystemList);
            this.store.setPatents(patentList);

            this.setState({ loading: false });
        });
    }

    onClickButton = () => {
        const {
            startDate,
            endDate,
            direction
        } = this.store;

        if (this.store.isFilterForSourceFirm) {
            sessionStorageHelper.set(`accMoneyFilterForBiz`, JSON.stringify({ startDate, endDate, direction }));
            NavigateHelper.push(`/Business?_companyId=${this.store.userData.SourceFirmId}`);

            return;
        }

        const { onApply } = this.props;

        onApply && this.store.setSum();

        const {
            kontragentType,
            operationType,
            budgetaryType,
            kontragentId,
            sumCondition,
            sum,
            sumFrom,
            sumTo,
            provideInTax,
            closingDocumentsCondition,
            taxationSystemType,
            patentId,
            approvedCondition
        } = this.store;

        onApply && onApply({
            startDate,
            endDate,
            kontragentType,
            direction,
            kontragentId,
            operationType: toJS(operationType),
            budgetaryType,
            sumCondition,
            sum,
            sumFrom,
            sumTo,
            provideInTax,
            closingDocumentsCondition,
            taxationSystemType,
            patentId,
            approvedCondition
        });
    };

    getOperationType(value) {
        let data = value || [];

        if (data && typeof data.valueOf() === `string`) {
            data = data.split(`,`);
        }

        return data.map(item => Number(item));
    }

    cancel = () => {
        this.props.onCancel();
    }

    render() {
        const { loading } = this.state;
        const {
            moneySourceStore, isProvideInTaxSectionVisible, isTaxSectionVisible, isClosingDocumentsSectionVisible, isApproveSectionVisible
        } = this.props;

        if (loading) {
            return null;
        }

        return (
            <div className={style.advancedSearch}>
                <DateFilter
                    store={this.store}
                />
                <div className={cn(`row`)}>
                    <KontragentSection store={this.store} />
                </div>
                <div className={cn(`row`)}>
                    <OperationType
                        cashCount={moneySourceStore.cashAccounts.length}
                        bankCount={moneySourceStore.settlementAccounts.length}
                        store={this.store}
                    />
                </div>
                <div className={cn(`row`)}>
                    <SumSection store={this.store} />
                </div>
                {isProvideInTaxSectionVisible && <div className={cn(`row`)}>
                    <ProvideInTaxSection store={this.store} />
                </div>}
                {(this.store.taxationSystemFilterVisible && isTaxSectionVisible) && <div className={cn(`input`)}>
                    <TaxationSystemSection store={this.store} />
                </div>}
                {isClosingDocumentsSectionVisible && <div className={cn(`row`)}>
                    <ClosingDocumentsSection store={this.store} />
                </div>}
                {(this.store.isOutsourceUser && isApproveSectionVisible) && <div className={cn(`row`)}>
                    <ApproveSection store={this.store} />
                </div>}
                <div className={cn(`buttons`)}>
                    <Button
                        onClick={this.onClickButton}
                        className={cn(`button`)}
                        disabled={!this.store.isValid}
                    >Применить</Button>
                    <Link onClick={this.cancel}>Отмена</Link>
                </div>
            </div>
        );
    }
}

AdvancedSearch.defaultProps = {
    isTaxSectionVisible: true,
    isClosingDocumentsSectionVisible: true,
    isApproveSectionVisible: true,
    isProvideInTaxSectionVisible: true
};

AdvancedSearch.propTypes = {
    value: PropTypes.object,
    onApply: PropTypes.func,
    onCancel: PropTypes.func,
    moneySourceStore: PropTypes.object,
    isTaxSectionVisible: PropTypes.bool,
    isClosingDocumentsSectionVisible: PropTypes.bool,
    isApproveSectionVisible: PropTypes.bool,
    isProvideInTaxSectionVisible: PropTypes.bool
};

export default observer(AdvancedSearch);
