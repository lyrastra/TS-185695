import React from 'react';
import { observer, Provider } from 'mobx-react';
import { Switch, Route } from 'react-router-dom';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import NotificationManager, { NotificationType } from '@moedelo/frontend-core-react/helpers/notificationManager';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import FloatButtonNameEnum from '@moedelo/frontend-common-v2/apps/floatButtonsGroup/enums/FloatButtonNameEnum';
import floatButtonGroupHelper from '@moedelo/frontend-common-v2/apps/floatButtonsGroup/helpers/floatButtonGroupHelper';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import PageUp from '@moedelo/frontend-core-react/components/Pageup';
import sessionStorageHelper from '@moedelo/frontend-core-v2/helpers/sessionStorageHelper';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import onboardingHelper from '@moedelo/frontend-common-v2/apps/onboardingScenario/helpers/onboardingHelper';
import scenarioSectionResource from '@moedelo/frontend-common-v2/apps/onboardingScenario/resources/scenarioSectionResource';
import { getImportMessages } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';
import BankIntegrationPanel from './components/BankIntegrationPanel';
import AddButtons from './components/AddButtons/AddButtons';
import SourceDropdown from './components/SourceDropdown/SourceDropdown';
import ActionSource from './components/ActionSource/ActionSource';
import WarningTable from './components/WarningTable/WarningTable';
import SuccessTable from './components/SuccessTable/SuccessTable';
import OutsourceProcessingTable from './components/OutsourceProcessingTable';
import {
    getDefaultFilter, defaultTablesIsOpen, toObject, toQueryString
} from '../../helpers/newMoney/utils';
import storage from '../../helpers/newMoney/storage';
import IntegrationError from './components/IntegrationError';
import requisitesService from '../../services/requisitesService';
import moneySourceHelper from '../../helpers/newMoney/moneySourceHelper';
import MoneyOperationService from '../../services/newMoney/moneyOperationService';
import MainTable from './components/MainTable';
import { getCopyUrlHash, getEditUrlHash } from '../../helpers/newMoney/operationUrlHelper';
import PageLoader from '../../components/PageLoader';
import Footer from './components/Footer';
import SettingActions from './components/SettingActions';
import NotificationForRnkb from './components/NotificationForRnkb';
import PaymentImportStore from './stores/PaymentImportStore';
import PaymentSystemIntegrationUnavailableNotification from './components/PaymentSystemIntegrationUnavailableNotification';
import {
    showMoneyAuditOnboardingModalAsync,
    showMoneyAuditCompleteModalAsync,
    showMoneyReconciliationCompleteModalAsync
} from './components/MoneyAuditModal';
import TableStore from './stores/TableStore';
import WarningTableStore from './stores/WarningTableStore';
import SuccessTableStore from './stores/SuccessTableStore';
import OutsourceProcessingTableStore from './stores/OutsourceProcessingTableStore';
import CommonDataStore from './stores/CommonDataStore';
import MassChangeTaxSystemStore from './stores/MassChangeTaxSystemStore';
import KudirStore from './stores/KudirStore';
import { paymentOrderOperationResources } from '../../resources/MoneyOperationTypeResources';
import { showImportRuleNewFutureDialog } from '../../services/importRuleNewFutureDialogService';

import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class MoneyMain extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            currentSource: {}
        };

        this.moneySourceStore = props.moneySourceStore;
        this.paymentImportStore = new PaymentImportStore();
        this.tableStore = new TableStore();
        this.warningTableStore = new WarningTableStore();
        this.successTableStore = new SuccessTableStore();
        this.outsourceProcessingTableStore = new OutsourceProcessingTableStore();
        this.commonDataStore = new CommonDataStore();
        this.userInfo = props.userInfo;
        this.modalShowed = false;
        this.requisites = {};
    }

    async componentDidMount() {
        await requisitesService.get().then(data => {
            this.requisites = data || {};
        });

        const today = new Date();

        await taxationSystemService.getTaxSystem(today).then(data => {
            this.taxationSystem = data;
            const { IsOsno, IsUsn, UsnSize } = this.taxationSystem;
            this.kudirStore = new KudirStore({
                isOsno: IsOsno, isUsn: IsUsn, usnSize: UsnSize, userInfo: this.userInfo
            });
        });
        const taxationSystemsForAllYears = await taxationSystemService.getAll();
        const patentsForAllYears = await taxationSystemService.getAllPatents();
        const lastClosedPeriod = await MoneyOperationService.getLastClosedPeriod();

        this.MassChangeTaxSystemStore = new MassChangeTaxSystemStore({
            currentTaxationSystem: this.taxationSystem,
            hasPatents: patentsForAllYears.length !== 0,
            taxationSystemsForAllYears,
            patentsForAllYears,
            lastClosedPeriod
        });

        this.moneySourceStore.loadList()
            .then(async () => {
                this.setCurrentSource(storage.get(`filter`));
                await this.showMoneyModals();
            });

        this._loadAllOperations({ reset: true, ...storage.get(`tableData`) });

        floatButtonGroupHelper.appendButton(<PageUp isFixed={false} />, FloatButtonNameEnum.Pageup);

        showImportRuleNewFutureDialog();

        this.commonDataStore.loadEnpStartDate();
    }

    onChangeTable = (options = { reset: false, newAccount: { sourceId: false, sourceType: false } }) => {
        this.moneySourceStore.loadList()
            .then(() => {
                if (options.newAccount && options.newAccount.sourceId) {
                    const { sourceType, sourceId } = options.newAccount;

                    this.onSelectSourceType({ sourceType, sourceId });
                } else {
                    this._loadAllOperations(options);
                }
            });
    };

    onSelectSourceType = ({ sourceType, sourceId }) => {
        const filter = storage.get(`filter`) || getDefaultFilter();

        filter.sourceType = sourceType;
        filter.sourceId = sourceId;

        storage.save(`Scroll`, 0);
        storage.save(`filter`, filter);

        this.setCurrentSource(filter);
        this._loadAllOperations({ reset: true });

        NavigateHelper.push(toQueryString(filter));
    };

    onClickIncomingButton = () => {
        mrkStatService.sendEventWithoutInternalUser(`dobavit_postuplenie_blok_dobavit_stranitsa_dengi_click_button`);
        localStorage.setItem(`referrerUrl`, `finances`);
        NavigateHelper.push(`add/incoming`);
    };

    onClickOutgoingButton = () => {
        mrkStatService.sendEventWithoutInternalUser(`dobavit_spisanie_block_dobavit_stranitsa_dengi_click_button`);
        localStorage.setItem(`referrerUrl`, `finances`);
        NavigateHelper.push(`add/outgoing`);
    };

    onClickTableRow = operation => {
        localStorage.setItem(`referrerUrl`, `finances`);
        NavigateHelper.push(getEditUrlHash(operation));
    };

    getSelectedSource = filter => {
        const selectedFilter = filter || storage.get(`filter`) || {};

        return {
            sourceType: selectedFilter.sourceType,
            sourceId: selectedFilter.sourceId
        };
    };

    setCurrentSource = (filter = storage.get(`filter`) || getDefaultFilter()) => {
        const currentSource = moneySourceHelper.getCurrentSource(this.moneySourceStore.sourceList, filter.sourceId);

        currentSource.Id !== undefined && this.setState({
            currentSource: moneySourceHelper.getCurrentSource(this.moneySourceStore.sourceList, filter.sourceId)
        });
    };

    checkModalShowed = () => {
        this.modalShowed = true;
    };

    showMoneyModals = async () => {
        await showMoneyAuditOnboardingModalAsync(this.checkModalShowed);
        !this.modalShowed && await showMoneyAuditCompleteModalAsync();
        !this.modalShowed && await showMoneyReconciliationCompleteModalAsync();
    }

    copyOperation = (operation = {}) => {
        localStorage.setItem(`referrerUrl`, `finances`);

        if (operation.OperationType === paymentOrderOperationResources.BudgetaryPayment.value) {
            sessionStorageHelper.set(`budgetaryId`, operation.Id);
        }

        const url = getCopyUrlHash(operation);

        NavigateHelper.push(url);
    };

    toggleTable = (type = null) => {
        if (type) {
            const { warningTableStore, successTableStore, outsourceProcessingTableStore } = this;
            const filter = storage.get(`filter`) || getDefaultFilter();
            const tablesIsOpenStorageId = `${filter.sourceId}-tablesIsOpen`;
            const tablesIsOpen = storage.get(tablesIsOpenStorageId) || defaultTablesIsOpen();

            switch (type) {
                case `warning`:
                    if (!warningTableStore.operationsLoaded) {
                        warningTableStore.loadOperations();
                    }

                    tablesIsOpen.warning = !tablesIsOpen.warning;
                    warningTableStore.isOpen = tablesIsOpen.warning;

                    break;
                case `success`:
                    if (!successTableStore.loaded) {
                        successTableStore.loadOperations();
                    }

                    tablesIsOpen.success = !tablesIsOpen.success;
                    successTableStore.isOpen = tablesIsOpen.success;

                    break;
                case `outsourceProcessing`:
                    if (!outsourceProcessingTableStore.loaded) {
                        outsourceProcessingTableStore.loadOperations();
                    }

                    tablesIsOpen.outsourceProcessing = !tablesIsOpen.outsourceProcessing;
                    outsourceProcessingTableStore.isOpen = tablesIsOpen.outsourceProcessing;

                    break;
            }

            storage.save(tablesIsOpenStorageId, tablesIsOpen);
        }
    };

    closeTable = (type = null) => {
        if (type) {
            const { warningTableStore, successTableStore, outsourceProcessingTableStore } = this;
            const filter = storage.get(`filter`) || getDefaultFilter();
            const tablesIsOpenStorageId = `${filter.sourceId}-tablesIsOpen`;
            const tablesIsOpen = storage.get(tablesIsOpenStorageId) || defaultTablesIsOpen();

            switch (type) {
                case `warning`:
                    tablesIsOpen.warning = !tablesIsOpen.warning;
                    warningTableStore.isOpen = tablesIsOpen.warning;

                    break;
                case `success`:
                    tablesIsOpen.success = false;
                    successTableStore.isOpen = false;

                    break;
                case `outsourceProcessing`:
                    tablesIsOpen.outsourceProcessing = false;
                    outsourceProcessingTableStore.isOpen = false;

                    break;
            }

            storage.save(tablesIsOpenStorageId, tablesIsOpen);
        }
    };

    _loadAllOperations = (options = {}) => {
        const {
            tableStore, warningTableStore, successTableStore, outsourceProcessingTableStore
        } = this;
        const filter = storage.get(`filter`) || getDefaultFilter();
        const tablesIsOpenStorageId = `${filter.sourceId}-tablesIsOpen`;
        const tablesIsOpen = storage.get(tablesIsOpenStorageId) || defaultTablesIsOpen();

        successTableStore.setMoneySource(filter.sourceId, filter.sourceType);
        warningTableStore.setMoneySource(filter.sourceId, filter.sourceType);
        outsourceProcessingTableStore.setMoneySource(filter.sourceId, filter.sourceType);

        tableStore.loadOperations(options, filter)
            .then(() => {
                if (tableStore.operations.length) {
                    if (tablesIsOpen.warning) {
                        warningTableStore.loadOperations(options);
                        warningTableStore.isOpen = true;
                    } else {
                        warningTableStore.loadOperationsCount();
                        warningTableStore.isOpen = false;
                    }

                    if (tablesIsOpen.success) {
                        successTableStore.loadOperations(options);
                        successTableStore.isOpen = true;
                    } else {
                        successTableStore.loadOperationsCount();
                        successTableStore.isOpen = false;
                    }

                    if (tablesIsOpen.outsourceProcessing) {
                        outsourceProcessingTableStore.loadOperations(options);
                        outsourceProcessingTableStore.isOpen = true;
                    } else {
                        outsourceProcessingTableStore.loadOperationsCount();
                        outsourceProcessingTableStore.isOpen = false;
                    }
                } else {
                    warningTableStore.loadOperations(options)
                        .then(() => {
                            if (warningTableStore.operations.length) {
                                warningTableStore.isOpen = true;
                                tablesIsOpen.warning = true;
                                storage.save(tablesIsOpenStorageId, tablesIsOpen);
                            }
                        });
                    successTableStore.loadOperations(options)
                        .then(() => {
                            if (successTableStore.operations.length) {
                                successTableStore.isOpen = true;
                                tablesIsOpen.success = true;
                                storage.save(tablesIsOpenStorageId, tablesIsOpen);
                            }
                        });
                    outsourceProcessingTableStore.loadOperations(options)
                        .then(() => {
                            if (outsourceProcessingTableStore.operations.length) {
                                outsourceProcessingTableStore.isOpen = true;
                                tablesIsOpen.outsourceProcessing = true;
                                storage.save(tablesIsOpenStorageId, tablesIsOpen);
                            }
                        });
                }
            }).then(() => {
                options.reset && onboardingHelper.startShowTooltips({ section: scenarioSectionResource.Finances });
            });
    };

    table(filter) {
        return (
            <Provider massChangeTaxSystemStore={this.MassChangeTaxSystemStore}>
                <MainTable
                    onClickRow={this.onClickTableRow}
                    filter={filter}
                    moneySourceStore={this.moneySourceStore}
                    paymentImportStore={this.paymentImportStore}
                    onAddOperation={options => {
                        this.moneySourceStore.loadList(options);
                    }}
                    onAddIncoming={this.onClickIncomingButton}
                    onChangeList={this.onChangeTable}
                    onAddOutgoing={this.onClickOutgoingButton}
                    copyOperation={this.copyOperation}
                    store={this.tableStore}
                    warningTableStore={this.warningTableStore}
                    successTableStore={this.successTableStore}
                    commonDataStore={this.commonDataStore}
                    currentSource={this.state.currentSource}
                />
            </Provider>
        );
    }

    showStatementRequestMessage = () => {
        getImportMessages().then(resp => {           
            if (resp) {
                NotificationManager.show({
                    message: resp,
                    type: NotificationType.warning,
                    duration: 10000,
                    onClick: () => NotificationManager.hide()
                });
            }
        });
    };

    render() {
        if (this.tableStore.loading && this.moneySourceStore.loading) {
            return <PageLoader />;
        }

        this.showStatementRequestMessage();

        return (
            <React.Fragment>
                <NotificationForRnkb />
                <IntegrationError />
                <PaymentSystemIntegrationUnavailableNotification />
                <div className={cn(`page`)}>
                    <div className={cn(`blockWrapper`)}>
                        <div className={cn(style.topPanel, grid.row)}>
                            <div className={grid.row}>
                                <div className={cn(style.addButtonsContainer)}>
                                    <AddButtons
                                        store={this.moneySourceStore}
                                        commonDataStore={this.commonDataStore}
                                        onClickIncomingButton={this.onClickIncomingButton}
                                        onClickOutgoingButton={this.onClickOutgoingButton}
                                        filter={storage.get(`filter`)}
                                    />
                                    <div id={`tip_${scenarioSectionResource.Finances}_0`} />
                                </div>
                                <div>
                                    <Switch>
                                        <Route
                                            path={`/add/:direction`}
                                            render={() => {
                                                return <SourceDropdown
                                                    store={this.moneySourceStore}
                                                    value={this.getSelectedSource()}
                                                    onSelect={this.onSelectSourceType}
                                                />;
                                            }}
                                        />
                                        <Route
                                            path={`/:filter`}
                                            render={({ match }) => {
                                                const filter = toObject(match.params.filter);

                                                return <SourceDropdown
                                                    store={this.moneySourceStore}
                                                    value={this.getSelectedSource(filter)}
                                                    onSelect={this.onSelectSourceType}
                                                />;
                                            }}
                                        />
                                    </Switch>
                                </div>
                            </div>
                            <div className={cn(style.rightBlock)}>
                                <ActionSource
                                    onImportSuccess={this.onChangeTable}
                                    commonDataStore={this.commonDataStore}
                                    moneySourceStore={this.moneySourceStore}
                                    kudirStore={this.kudirStore}
                                    paymentImportStore={this.paymentImportStore}
                                    filter={storage.get(`filter`)}
                                />
                                <SettingActions
                                    isUsn={this.taxationSystem?.IsUsn}
                                    registrationDate={this.requisites?.RegistrationDate}
                                    patentList={this.MassChangeTaxSystemStore?.patentsForAllYears}
                                />
                            </div>
                        </div>
                        <BankIntegrationPanel refreshSourceList={() => this.moneySourceStore.loadList({ withoutLoading: true })} />
                    </div>
                    <div className={cn(`blockWrapper`)}>
                        <WarningTable
                            store={this.warningTableStore}
                            onChangeList={this.onChangeTable}
                            toggleTable={this.toggleTable}
                            closeTable={this.closeTable}
                            commonDataStore={this.commonDataStore}
                            onClickRow={this.onClickTableRow}
                            filter={storage.get(`filter`)}
                        />
                    </div>
                    <div className={cn(`blockWrapper`)}>
                        <Provider massChangeTaxSystemStore={this.MassChangeTaxSystemStore}>
                            <OutsourceProcessingTable
                                store={this.outsourceProcessingTableStore}
                                onClickRow={this.onClickTableRow}
                                filter={storage.get(`filter`)}
                                onChangeList={this.onChangeTable}
                                toggleTable={this.toggleTable}
                                closeTable={this.closeTable}
                                copyOperation={this.copyOperation}
                                mainTableStore={this.tableStore}
                                commonDataStore={this.commonDataStore}
                                moneySourceStore={this.moneySourceStore}
                            />
                        </Provider>
                    </div>
                    <div className={cn(`blockWrapper`)}>
                        <Provider massChangeTaxSystemStore={this.MassChangeTaxSystemStore}>
                            <SuccessTable
                                onClickRow={this.onClickTableRow}
                                filter={storage.get(`filter`)}
                                onChangeList={this.onChangeTable}
                                toggleTable={this.toggleTable}
                                closeTable={this.closeTable}
                                copyOperation={this.copyOperation}
                                store={this.successTableStore}
                                mainTableStore={this.tableStore}
                                commonDataStore={this.commonDataStore}
                            />
                        </Provider>
                    </div>
                    <div className={cn(`blockWrapper`)}>
                        <div className={cn(`table`)}>
                            <Switch>
                                <Route
                                    path={`/:filter`}
                                    render={() => {
                                        const filter = window.location.hash.slice(1);

                                        return this.table(toObject(filter));
                                    }}
                                />
                                <Route render={() => this.table()} />
                            </Switch>
                        </div>
                        {(this.successTableStore.totalCount > 0 || this.tableStore.totalCount > 0) && <Footer
                            outgoingDate={this.tableStore.outgoingDate}
                            outgoingCount={this.tableStore.outgoingCount}
                            outgoingBalance={this.tableStore.outgoingBalance}
                            incomingDate={this.tableStore.incomingDate}
                            incomingCount={this.tableStore.incomingCount}
                            incomingBalance={this.tableStore.incomingBalance}
                            endBalance={this.tableStore.endBalance}
                            currency={this.tableStore.currency}
                            operationsGroupedByCurrency={this.tableStore.operationsGroupedByCurrency}
                            startBalance={this.tableStore.startBalance}
                            bankTurnoversAndBalances={this.tableStore.bankBalance}
                            canShowBankTurnoversAndBalances={!!this.tableStore.canShowBankTurnoversAndBalances}
                        />}
                    </div>
                </div>
            </React.Fragment>
        );
    }
}

MoneyMain.propTypes = {
    moneySourceStore: PropTypes.object.isRequired,
    userInfo: PropTypes.object
};

export default MoneyMain;
