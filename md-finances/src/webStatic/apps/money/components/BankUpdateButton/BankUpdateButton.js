import React from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Loader from '@moedelo/frontend-core-react/components/Loader/Loader';
import IntegrationPartnerEnum from '@moedelo/frontend-enums/mdEnums/IntegrationPartner';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import ReconciliationByHandDialog from '@moedelo/frontend-common-v2/apps/finances/components/ReconciliationByHandDialog';
import SmsConfirmModal from '@moedelo/frontend-common-v2/apps/bankIntegration/components/SmsConfirmModal';
import { showPeriodDialog } from '@moedelo/frontend-common-v2/apps/common/PeriodDialog';
import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import scenarioSectionResource from '@moedelo/frontend-common-v2/apps/onboardingScenario/resources/scenarioSectionResource';
import MoneyOperationService from '../../../../services/newMoney/moneyOperationService';
import BankUpdateDialog from './BankUpdateDialog';
import storage from '../../../../helpers/newMoney/storage';
import MoneySourceType from '../../../../enums/MoneySourceType';
import BalanceReconciliationStatusEnum from '../../../../enums/newMoney/BalanceReconciliationStatusEnum';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class BankUpdateButton extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isExpired: false,
            updateDialogOpen: false,
            downloadDialogOpen: false,
            statementLoading: false,
            statementSent: props.statementSent || false,
            isReconciliationProcessing: props.source.IsReconciliationProcessing,
            hasEmployees: props.source.HasEmployees
        };
        this.store = props.store;
        this.commonDataStore = props.commonDataStore;
        this.moneySourceStore = props.moneySourceStore;
    }

    componentDidMount() {
        userDataService.get()
            .then(data => this.setState({ isExpired: data.ExpiredDate === null }));
    }

    componentDidUpdate(prevProps) {
        const { statementSent } = prevProps;

        if (this.props.statementSent !== statementSent) {
            this.setState({ statementSent });
        }
    }

    onChange = e => {
        const { onInputFileChange } = this.props;

        onInputFileChange && onInputFileChange(e.target);
    };

    onClickConfirmationSmsRequestDialog = () => {
        const {
            startDate, endDate, sourceType, sourceId
        } = this.store.paymentImportParams;

        this.updateFromBank(startDate, endDate, sourceType, sourceId);
    };

    onConfirmReconciliation = ({ status }) => {
        status === BalanceReconciliationStatusEnum.InProgress && this.setState({
            isReconciliationProcessing: true
        });
    };

    getText = () => {
        if (this.state.statementLoading) {
            return `Запрос`;
        } else if (this.state.statementSent) {
            return `Запрос отправлен`;
        }

        return this.props.defaultButtonText;
    };

    getAdditionalActions = (statementLoading, statementSent, disabledByAccess) => {
        const sourceTypeFromFilter = parseInt(this.props.filter.sourceType, 10);
        const isTochka = (this.props.source?.IntegrationPartner === IntegrationPartnerEnum.PointBank);
        const { isExpired } = this.state;

        const additionalActions = [
            {
                value: this.uploadStatement,
                text: sourceTypeFromFilter === MoneySourceType.Purse ? `Загрузить отчёт` : `Загрузить выписку`,
                disabled: disabledByAccess
            }
        ];

        if (sourceTypeFromFilter === MoneySourceType.SettlementAccount
            && !isExpired) {
            additionalActions.push({
                value: this.openReconciliationByHandModal,
                toolTipText: isTochka ? `Сверка с Точка банком доступна с 01.02.2024г.` : ``,
                text: `Запросить сверку`
            });

            additionalActions.push({
                value: this.viewReconciliationResults,
                text: `Результаты сверки`
            });
        }

        if (window._preloading.IsProfOutsource) {
            additionalActions.push({
                value: this.downloadLatestStatements,
                text: `Скачать 5 последних выписок`,
                disabled: statementLoading || statementSent || disabledByAccess
            });
        }

        return additionalActions;
    };

    openReconciliationByHandModal = () => {
        this.store.toggleReconciliationByHandDialogVisibility(true);
    };

    closeReconciliationByHandModal = () => {
        this.store.toggleReconciliationByHandDialogVisibility(false);
    };

    viewReconciliationResults = () => {
        NavigateHelper.push(`viewReconciliationResults/`);
    }

    removeDownloadDialog = () => {
        this.setState({
            downloadDialogOpen: false
        });
    }

    removeSmsConfirmationDialog = () => {
        this.setState({
            smsConfirmationDialogOpen: false
        });
    };

    uploadStatement = () => {
        storage.save(`Scroll`, window.scrollY);

        this.downloadFile.click();
    };

    downloadFile = () => {
        this.removeDownloadDialog();
    };

    downloadLatestStatements = () => {
        const { sourceId } = this.props.filter;
        MoneyOperationService.downloadLatestStatements(sourceId);
    };

    update = ({
        startDate, endDate, sourceType, sourceId
    }) => {
        this.setState({
            statementLoading: true
        });

        const source = this.moneySourceStore.getBySourceId(sourceId);

        if (sourceType === MoneySourceType.All && source.IntegrationPartner !== IntegrationPartnerEnum.Vtb24Bank) {
            return MoneyOperationService.updateFromBank(startDate, endDate);
        }

        return MoneyOperationService.updateFromBankBySource(startDate, endDate, sourceType, sourceId);
    };

    showNotification = (response, type) => {
        let message = ``;

        if (response && response.MessageList && response.MessageList.length) {
            message = response.MessageList.join(`<br>`);
        }

        NotificationManager.show({
            message: message || response.Message || `Запрос выписки успешно отправлен.`,
            type,
            duration: 4000
        });
    };

    updateFromBank = (startDate, endDate) => {
        const { filter } = this.props;
        const { sourceType, sourceId } = filter;

        this.update({
            startDate, endDate, sourceType, sourceId
        }).then(response => {
            if (response.IsSuccess && response.PhoneMask) {
                this.store.setPaymentImportParams(startDate, endDate, sourceId, sourceType);
                this.setState({
                    phoneMask: response.PhoneMask,
                    smsConfirmationDialogOpen: true,
                    statementLoading: false
                });
            } else if (response.IsSuccess) {
                this.setState({
                    statementLoading: false,
                    smsConfirmationDialogOpen: false,
                    statementSent: true
                });
                this.showNotification(response, `success`);
            } else {
                this.setState({
                    statementLoading: false
                });
                this.showNotification(response, `error`);
            }
        })
            .catch(() => {
                NotificationManager.show({
                    message: `При отправке запроса произошла ошибка.`,
                    type: `error`,
                    duration: 4000
                });
            });
    };

    updateFromBankWithDate = () => {
        const { filter } = this.props;

        localStorage.setItem(`Scroll`, window.scrollY);

        showPeriodDialog({
            header: `Обновить за период`,
            message: `Макс. срок выписки – 1 год`,
            filter,
            onPeriodSet: this.updateFromBank,
            buttonText: `Обновить`,
            isTodayMaxDate: true
        });
    };

    renderReconciliationByHandModal = () => {
        const { source } = this.props;
        const { isReconciliationProcessing } = this.state;
        const { reconciliationByHandDialogShown } = this.store;

        if (!reconciliationByHandDialogShown) {
            return null;
        }

        return (
            <ReconciliationByHandDialog
                visible
                isProcessing={isReconciliationProcessing}
                settlementAccountId={source.Id}
                onReconcile={this.onConfirmReconciliation}
                onClose={this.closeReconciliationByHandModal}
            />
        );
    };

    renderButton = () => {
        const { isArchiveSource, hasUnprocessedRequests } = this.props;
        const { statementLoading, statementSent } = this.state;
        const { importLoading } = this.store;
        const disabledByAccess = !this.commonDataStore.hasAccessToMoneyEdit;
        const disabledButton = hasUnprocessedRequests || statementSent || disabledByAccess || isArchiveSource;

        const mainButtonData = {
            className: cn(`updateSplit`, {
                statementLoading
            }),
            disabled: disabledButton,
            onClick: this.updateFromBankWithDate
        };

        const additionalActions = this.getAdditionalActions(statementLoading, statementSent, disabledByAccess);

        return (
            <SplitButton
                data={additionalActions}
                color="white"
                onSelect={({ value }) => {
                    value();
                }}
                disabled={disabledButton}
                loading={statementLoading || importLoading}
                mainButton={mainButtonData}
                dropdownWidth={245}
            >
                { this.getText() }
            </SplitButton>
        );
    };

    render() {
        const {
            statementLoading,
            downloadDialogOpen,
            phoneMask,
            smsConfirmationDialogOpen
        } = this.state;
        const { filter, source, accept } = this.props;
        const { loadingAccessToMoney } = this.commonDataStore;

        if (loadingAccessToMoney) {
            return <section className={cn(`loaderSection`)}>
                <Loader className={cn(`loader`)} />
            </section>;
        }

        return (
            <div className={cn(`bankUpdateButton`)}>
                {this.renderButton()}
                <div id={`tip_${scenarioSectionResource.Finances}_1`} />
                <SmsConfirmModal
                    visible={smsConfirmationDialogOpen}
                    phoneMasked={phoneMask}
                    source={source}
                    loading={statementLoading}
                    onConfirm={this.onClickConfirmationSmsRequestDialog}
                    onClose={this.removeSmsConfirmationDialog}
                />
                <Modal
                    onClose={this.removeDownloadDialog}
                    header="Скачать выписку"
                    width="307px"
                    visible={downloadDialogOpen}
                >
                    <BankUpdateDialog
                        filter={filter}
                        onClick={this.downloadFile}
                        buttonText="Запросить"
                        onClose={this.removeDownloadDialog}
                    />
                </Modal>
                {this.renderReconciliationByHandModal()}
                <input ref={ref => { this.downloadFile = ref; }} type="file" accept={accept} onChange={this.onChange} className={cn(`downloadFileInput`)} />
            </div>
        );
    }
}

BankUpdateButton.defaultProps = {
    source: {
        isReconciliationProcessing: false,
        hasEmployees: false
    }
};

BankUpdateButton.propTypes = {
    statementSent: PropTypes.bool,
    filter: PropTypes.object,
    store: PropTypes.object,
    moneySourceStore: PropTypes.object,
    source: PropTypes.shape({
        Id: PropTypes.number,
        IsReconciliationProcessing: PropTypes.bool,
        HasEmployees: PropTypes.bool,
        IntegrationPartner: PropTypes.number
    }),
    commonDataStore: PropTypes.object,
    onInputFileChange: PropTypes.func,
    isArchiveSource: PropTypes.bool,
    hasUnprocessedRequests: PropTypes.bool,
    defaultButtonText: PropTypes.string,
    accept: PropTypes.string
};

export default BankUpdateButton;

