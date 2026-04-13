import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer } from 'mobx-react';
import PageUp from '@moedelo/frontend-core-react/components/Pageup';
import logger from '@moedelo/frontend-core-v2/helpers/logger';
import { toDate } from '@moedelo/frontend-core-v2/helpers/converter';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import FloatButtonNameEnum from '@moedelo/frontend-common-v2/apps/floatButtonsGroup/enums/FloatButtonNameEnum';
import floatButtonGroupHelper from '@moedelo/frontend-common-v2/apps/floatButtonsGroup/helpers/floatButtonGroupHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import { paymentOrderOperationResources as operationTypes } from '../../../../../resources/MoneyOperationTypeResources';
import Loader from '../../../../../components/PageLoader';
import UnavailableCurrencyOperation from '../commonComponents/UnavailableCurrencyOperation';
import showImportRulesModal from '../../../../paymentImportRules/helpers/importRulesModalHelper';
import { isCurrency } from '../../../../../helpers/MoneyOperationHelper';
import { getDefaultKontragentAccountCode, isFLKontragent } from '../../../../../helpers/newMoney/kontragentHelper';
import { needToClearContract } from '../../../../../helpers/newMoney/operationHelper';
import { getIsFromImport } from '../../../../../services/newMoney/newMoneyOperationService';
import PaymentNotificationPanel from './outgoing/PaymentNotificationPanel';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class BankOperation extends React.Component {
    constructor(props) {
        super(props);
        const { OperationType, CanEditCurrencyOperations } = props.operation;

        this.state = {
            operation: props.operation,
            taxationSystem: props.taxationSystem,
            date: props.operation.Date,
            activePatents: props.activePatents,
            ndsRatesFromAccPolicy: props.ndsRatesFromAccPolicy,
            OperationComponent: null,
            isInitialTransferFromAnotherAccount: props.operation.isInitialTransferFromAnotherAccount,
            showCurrencyUnavailableBubble: isCurrency(OperationType) && CanEditCurrencyOperations === false
        };

        this.setViewPaymentNotificationPanel = props.setViewPaymentNotificationPanel;
        this.viewPaymentNotificationPanel = props.viewPaymentNotificationPanel;
    }

    componentDidMount() {
        this.getOperationView();

        floatButtonGroupHelper.appendButton(<PageUp isFixed={false} />, FloatButtonNameEnum.Pageup);
    }

    componentDidUpdate(prevProps, prevState) {
        if (this.state.operation.OperationType !== prevState.operation.OperationType) {
            this.getOperationView();
        }
    }

    onChangeOperationType = async ({ operation }) => {
        const defaultFields = {
            AccountingPostings: {},
            TaxPostings: {},
            ProvideInAccounting: true,
            KontragentAccountCode: getDefaultKontragentAccountCode(operation)
        };

        if (!operation.DocumentBaseId) {
            defaultFields.Description = ``;
        }

        if (operation.IncludeNds !== undefined) {
            defaultFields.IncludeNds = null;
        }

        if (!operation.Kontragent) {
            defaultFields.Kontragent = this.getOperationProps().operation.Kontragent || this.getDefaultKontragent();
        }

        if (needToClearContract(operation)) {
            defaultFields.ContractBaseId = 0;
            defaultFields.ProjectNumber = ``;
            defaultFields.Contract = {};
        }

        /**
         * При переключении на новый тип, если операция из импорта и для этого типа операций не написано правил импорта, показывать модальное окно создания правила
         */
        if (this.state.operation.IsFromImport && !this.state.operation.ImportRules?.length) {
            await showImportRulesModal(this.state.operation.OperationType);
        }

        /** костыль для снятия прибыли. при переключении типа на снятие прибыли,
         *  если контрагент не физлицо, очищать. но с другой стороны, если операция
         *  из импорта и контрагент без формы, его нужно оставлять */
        if ((operation.OperationType === operationTypes.PaymentOrderOutgoingProfitWithdrawing.value && !isFLKontragent(operation.Kontragent))) {
            defaultFields.Kontragent = this.getDefaultKontragent();
        }

        /**
         * При переключении на тип Финансовая помощь от учредителя сбрабывать контрагента т.к. нет флага что он учредитель
         */
        if (operation.OperationType === operationTypes.PaymentOrderIncomingMaterialAid.value) {
            defaultFields.Kontragent = this.getDefaultKontragent();
            defaultFields.Contractor = {};
        }

        /**
         * При переключении на тип Поступление от комиссионера сбрабывать контрагента т.к. он может не являться комиссионером
         */
        if (operation.OperationType === operationTypes.PaymentOrderIncomingIncomeFromCommissionAgent.value) {
            defaultFields.Kontragent = this.getDefaultKontragent();
            defaultFields.Contractor = {};
        }

        if (operation.OperationType === operationTypes.BudgetaryPayment.value) {
            defaultFields.Id = 0;
            /** при смене типа операции на БП. нужно обнулить Id,
             *  чтобы загрузить специфичные поля БП, но не загружать
             *  их при открытии на редактирование БП(см выше),
             *  а затем восстановить Id операции для возможности сохранения */
            defaultFields.PreviousOperationId = this.state.operation.Id;
        }

        /** Сбрасываем связанные документы когда поменялась валюта расчетного счета
         * (рубли на иностранную или наоборот) */
        const isOldForeignCurrency = isCurrency(this.state.operation.OperationType); // если предыдущее значение в инвалюте
        const isNewForeignCurrency = isCurrency(operation.OperationType); // если новое значение в инвалюте

        if (isNewForeignCurrency !== isOldForeignCurrency) {
            defaultFields.Documents = [];
        }

        defaultFields.KontragentId = (defaultFields.Kontragent || operation.Kontragent).KontragentId;

        if (operation.Date !== this.state.date) {
            await this.updateTaxationSystem(operation.Date);
            await this.updateActivePatents(operation.Date);
        }

        defaultFields.isInitialTransferFromAnotherAccount = this.state.isInitialTransferFromAnotherAccount;
        defaultFields.IsTypeChanged = true;

        if (operation.DocumentBaseId) {
            await getIsFromImport(operation.DocumentBaseId).then(result => {
                defaultFields.IsFromImport = result;
            });
        }

        const showCurrencyUnavailableBubble = isCurrency(operation.OperationType) && this.props.operation.CanEditCurrencyOperations === false;

        this.setState({
            operation: { ...operation, ...defaultFields },
            date: operation.Date,
            showCurrencyUnavailableBubble
        });

        this.props.setViewPaymentNotificationPanel({ statusCodeInvoice: 0 });
    };

    getOperationView = () => {
        switch (this.state.operation.OperationType) {
            case operationTypes.PaymentOrderIncomingPaymentForGoods.value: {
                import(`./incoming/PaymentFromBuyer`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingReturnFromAccountablePerson.value: {
                import(`./incoming/ReturnFromAccountablePerson`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingMediationFee.value: {
                import(`./incoming/MediationFee`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.MemorialWarrantReceiptFromCash.value: {
                import(`./incoming/WarrantReceiptFromCashbox`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.MemorialWarrantReceiptGoodsPaidCreditCard.value: {
                import(`./incoming/GoodsPaidByCreditCard`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingLoanObtaining.value: {
                import(`./incoming/IncomingLoanObtaining`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingLoanReturn.value: {
                import(`./incoming/IncomingLoanReturn`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.MemorialWarrantCreditingCollectedFunds.value: {
                import(`./incoming/WarrantCreditingCollectedFunds`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.MemorialWarrantAccrualOfInterest.value: {
                import(`./incoming/WarrantAccrualOfInterest`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingFromPurse.value: {
                import(`./incoming/IncomingFromPurse`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingContributionAuthorizedCapital.value: {
                import(`./incoming/IncomingContributionAuthorizedCapital`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingContributionOfOwnFunds.value: {
                import(`./incoming/ContributionOfOwnFunds`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingFromAnotherAccount.value: {
                import(`./incoming/TransferFromAccount`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderPaymentToSupplier.value: {
                import(`./outgoing/PaymentToSupplierNewBackend`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingReturnToBuyer.value: {
                import(`./outgoing/OutgoingReturnToBuyer`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingOther.value: {
                import(`./incoming/Other`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderRefundToSettlementAccount.value: {
                import(`./incoming/RefundToSettlementAccount`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingMaterialAid.value: {
                import(`./incoming/MaterialAid`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentToAccountablePerson.value: {
                import(`./outgoing/PaymentToAccountablePerson`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingTransferToAccount.value: {
                import(`./outgoing/TransferToAccount`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.BankFee.value: {
                import(`./outgoing/BankFee`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.WithdrawalFromAccount.value: {
                import(`./outgoing/WithdrawalFromAccount`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingLoanRepayment.value: {
                import(`./outgoing/LoanRepayment`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingOther.value: {
                import(`./outgoing/OutgoingOther`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingPaymentAgencyContract.value: {
                import(`./outgoing/PaymentAgencyContractNewBackend`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingProfitWithdrawing.value: {
                import(`./outgoing/OutgoingProfitWithdrawing`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.BudgetaryPayment.value:
                // falls through

            case operationTypes.UnifiedBudgetaryPayment.value: {
                import(`./outgoing/OutgoingBudgetaryPayment`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingForTransferSalary.value: {
                import(`./outgoing/OutgoingForTransferSalary`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingCurrencyPurchase.value: {
                import(`./outgoing/OutgoingCurrencyPurchase`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingCurrencyPurchase.value: {
                import(`./incoming/IncomingCurrencyPurchase`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingCurrencySale.value: {
                import(`./outgoing/OutgoingCurrencySale`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingCurrencySale.value: {
                import(`./incoming/IncomingCurrencySale`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingCurrencyOther.value: {
                import(`./incoming/IncomingCurrencyOther`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingCurrencyFromAnotherAccount.value: {
                import(`./incoming/IncomingCurrencyFromAnotherAccount`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods.value: {
                import(`./outgoing/CurrencyPaymentToSupplier`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingCurrencyBankFee.value: {
                import(`./outgoing/OutgoingCurrencyBankFee`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingCurrencyOther.value: {
                import(`./outgoing/OutgoingCurrencyOther`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingCurrencyPaymentFromBuyer.value: {
                import(`./incoming/IncomingCurrencyPaymentFromBuyer`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingCurrencyTransferToAccount.value: {
                import(`./outgoing/CurrencyTransferToAccount`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.RentPayment.value: {
                import(`./outgoing/RentPayment`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingLoanIssue.value: {
                import(`./outgoing/OutgoingLoanIssue`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderIncomingIncomeFromCommissionAgent.value: {
                import(`./incoming/IncomeFromCommissionAgent`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            case operationTypes.PaymentOrderOutgoingDeduction.value: {
                import(`./outgoing/OutgoingDeduction`)
                    .then(module => this.setState({ OperationComponent: module.default }));

                break;
            }

            default:
                logger.log(`Finances Invalid operation type ${JSON.stringify(this.props.operation.OperationType)}`);
                NotificationManager.show({
                    message: `Возникла непредвиденная ошибка. Пожалуйста зайтите позже или обратитесь в тех. поддержку!`,
                    type: `error`,
                    duration: 5000
                });
        }
    };

    getOperationProps = () => {
        const { operation, ...props } = this.props;
        const { taxationSystem, activePatents } = this.state;

        return {
            operation: { ...operation, ...this.state.operation },
            ...props,
            taxationSystem,
            activePatents,
            onChangeOperationType: this.onChangeOperationType,
            viewPaymentNotificationPanel: props.viewPaymentNotificationPanel,
            setViewPaymentNotificationPanel: props.setViewPaymentNotificationPanel
        };
    };

    getDefaultKontragent = () => {
        return {
            KontragentId: null,
            KontragentName: ``,
            KontragentBankName: ``,
            KontragentINN: ``,
            KontragentKPP: ``,
            KontragentSettlementAccount: ``
        };
    };

    updateTaxationSystem = async date => {
        const taxationSystem = await taxationSystemService.getTaxSystem(date);

        this.setState({ taxationSystem });
    };

    updateActivePatents = async date => {
        const activePatents = await taxationSystemService.getActivePatents(toDate(date));

        this.setState({ activePatents });
    };

    render() {
        const { OperationComponent, showCurrencyUnavailableBubble } = this.state;

        return (
            <React.Fragment>
                {showCurrencyUnavailableBubble && <div className={grid.row}><UnavailableCurrencyOperation className={grid.col_24} /></div>}
                <div className={cn(style.operationWrapper)}>
                    {!!this.props.viewPaymentNotificationPanel?.statusCodeInvoice &&
                    <PaymentNotificationPanel viewPaymentNotificationPanel={this.props.viewPaymentNotificationPanel} setViewPaymentNotificationPanel={this.props.setViewPaymentNotificationPanel} /> }
                    {OperationComponent ? <OperationComponent {...this.getOperationProps()} /> : <Loader />}
                </div>
            </React.Fragment>
        );
    }
}

BankOperation.propTypes = {
    isInitialTransferFromAnotherAccount: PropTypes.bool,
    settlementAccounts: PropTypes.arrayOf(PropTypes.object),
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    requisites: PropTypes.object,
    operation: PropTypes.object,
    taxationSystem: PropTypes.object,
    ndsRatesFromAccPolicy: PropTypes.arrayOf(PropTypes.object),
    activePatents: PropTypes.arrayOf(PropTypes.object),
    goBack: PropTypes.func,
    viewPaymentNotificationPanel: PropTypes.object,
    setViewPaymentNotificationPanel: PropTypes.func
};

export default BankOperation;
