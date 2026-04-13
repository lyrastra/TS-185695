import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Link from '@moedelo/frontend-core-react/components/Link';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import BudgetaryPaymentStore from './stores/BudgetaryPaymentStore';
import { actionArray } from '../../../../../../../resources/newMoney/saveButtonResource';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import BusyNumberModal from '../../../commonComponents/BusyNumberModal';
import Sum from '../../../commonComponents/Sum';
import Description from '../../../commonComponents/Description';
import Postings from '../../../commonComponents/Postings';
import AccountTypeRadioGroup from './components/AccountTypeRadioGroup';
import TaxesAndFeesTypeDropdown from './components/TaxesAndFeesTypeDropdown';
import KbkTypesRadioGroup from './components/KbkTypesRadioGroup';
import Period from './components/Period';
import Kbk from './components/Kbk';
import KbkAutoFields from './components/KbkAutoFields';
import TradingObjectDropdown from './components/TradingObjectDropdown';
import TradingObjectWarning from './components/TradingObjectWarning';
import TradingObjectAdditionslWarning from './components/TradingObjectAdditionslWarning';
import SendToBankButton from '../../../commonComponents/SendToBankButton';
import SmsConfirmModal from '../../../commonComponents/SmsConfirmModal';
import TaxDistribution from './components/TaxDistribution';
import PatentDropdown from '../../../commonComponents/PatentDropdown';
import CurrencyInvoices from './components/CurrencyInvoices';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';
import ManualLinkResource from './resources/ManualLinkResource';
import UnifiedBudgetaryPaymentStore from './stores/UnifiedBudgetaryPaymentStore/UnifiedBudgetaryPaymentStore';
import commonStyles from './commonStyles.m.less';
import style from './style.m.less';

const cn = classnames.bind({ style, commonStyles });

// списание бюджетный платеж р.сч

@observer
class OutgoingBudgetaryPayment extends React.Component {
    constructor(props) {
        super(props);

        this.store = new BudgetaryPaymentStore({
            ...getCommonOperationStoreData(props),
            operationTypes: props.operationTypes,
            patents: props.patents,
            UnifiedBudgetaryPaymentStore
        });
    }

    onDelete = async () => {
        await this.store.remove();
        this.props.onDelete();
    };

    onChangeOperationType = ({ value }) => {
        this.props.onChangeOperationType({
            operation: {
                Number: this.store.model.Number,
                Date: this.store.model.Date,
                Direction: this.store.model.Direction,
                SettlementAccountId: this.store.model.SettlementAccountId,
                Sum: this.store.model.Sum,
                IncludeNds: this.store.model.IncludeNds,
                NdsSum: this.store.model.NdsSum,
                Kontragent: {
                    KontragentId: null,
                    KontragentName: ``,
                    KontragentBankName: ``,
                    KontragentINN: ``,
                    KontragentKPP: ``,
                    KontragentSettlementAccount: ``
                },
                NdsType: this.store.model.NdsType,
                Description: this.store.model.Description,
                Status: this.store.model.Status,
                OperationType: value,
                DocumentBaseId: this.store.model.DocumentBaseId,
                initialOperationNumber: this.store.initialOperationNumber
            }
        });
    }

    onClickManualLink = () => {
        mrkStatService.sendEvent({
            event: `budgetary_payment_manual_click_link`
        });

        const { OutgoingBudgetaryPayment2023Form, OutgoingBudgetaryPaymentForm } = ManualLinkResource;
        const href = dateHelper(this.store.model.Date).isAfter(`31.12.2022`) ? OutgoingBudgetaryPayment2023Form.href : OutgoingBudgetaryPaymentForm.href;

        NavigateHelper.open(href, { useRawUrl: true });
    };

    getContent = () => {
        const { patentSelectVisible, isUnifiedBudgetaryPayment } = this.store;

        return <React.Fragment>
            <div className={cn(isUnifiedBudgetaryPayment ? grid.rowLarge : grid.row, commonStyles.endItems)}>
                <div className={grid.col_9}>
                    <TaxesAndFeesTypeDropdown operationStore={this.store} />
                </div>
                <div className={grid.col_1} />
                <div className={cn(grid.col_9, style.kbkTypesContainer)} >
                    <KbkTypesRadioGroup operationStore={this.store} />
                </div>
            </div>
            {isUnifiedBudgetaryPayment && (
                <TaxDistribution
                    store={this.store.UnifiedBudgetaryPaymentStore}
                    sumValidationMessage={this.store.validationState.Sum}
                />
            )}
            {!isUnifiedBudgetaryPayment && <TradingObjectDropdown operationStore={this.store} />}
            {!isUnifiedBudgetaryPayment && patentSelectVisible && <div className={grid.row}>
                <PatentDropdown className={grid.col_9} operationStore={this.store} />
                <div className={grid.col_1} />
            </div>}
            {!isUnifiedBudgetaryPayment && <Period operationStore={this.store} />}
            {!isUnifiedBudgetaryPayment &&
                <React.Fragment>
                    <div className={grid.row}>
                        <div className={grid.col_9}>
                            <Kbk operationStore={this.store} />
                        </div>
                        <div className={grid.col_1} />
                    </div>
                    <div className={cn(grid.row, style.sumRow)}>
                        <div className={grid.col_3}>
                            <Sum label={`Сумма (7)`} />
                        </div>
                        <div className={grid.col_1} />
                    </div>
                </React.Fragment>
            }
        </React.Fragment>;
    }

    getTooltipContent() {
        return <div>
            Для платежей, перечисляемых вне ЕНП, указывается описание согласно Приказу Минфина РФ № 58н.<br />
            Примеры: <br />
            &quot;Уплата гос.пошлины для обращения в суд, НДС не облагается&quot;<br />
            &quot;Уплата штрафа на основании Постановления №__ от ________г., НДС не облагается&quot;<br />
        </div>;
    }

    renderButtons = () => {
        const {
            canEdit,
            isSavingBlocked,
            canSendToBank,
            onClickSave,
            disabledSaveButton,
            model: { DocumentBaseId },
            isCancelBlocked
        } = this.store;

        const buttonData = getSaveOperationButtonData({
            saveButtonData: actionArray,
            documentBaseId: DocumentBaseId
        });

        return <ElementsGroup>
            {canEdit && <SplitButton
                data={buttonData}
                mainButton={{
                    className: `split`,
                    onClick: onClickSave,
                    disabled: disabledSaveButton
                }}
                onSelect={onClickSave}
                loading={isSavingBlocked}
                disabled={disabledSaveButton}
            >{getSaveButtonTitle({ documentBaseId: DocumentBaseId })}</SplitButton>}
            {canSendToBank && <SendToBankButton operationStore={this.store} />}
            <Button
                onClick={this.props.onCancel}
                color="white"
                disabled={isCancelBlocked}
            >
                Отмена
            </Button>
        </ElementsGroup>;
    };

    render() {
        if (this.store.loading) {
            return <div className={cn(commonStyles.loaderContainer)}>
                <Loader width={100} />
            </div>;
        }

        const isShowTooltip = dateHelper(this.store.model.Date).isAfter(`31.03.2026`) && this.store.isOtherPayment;

        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation onDelete={this.onDelete} />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                        <div className={cn(grid.col_1)} />
                        <div className={cn(grid.col_5)}>
                            <Link className={style.manual} onClick={this.onClickManualLink} text={ManualLinkResource.OutgoingBudgetaryPaymentForm.text} />
                        </div>
                    </div>
                    <div className={grid.row}>
                        <AccountTypeRadioGroup operationStore={this.store} />
                    </div>
                    {this.getContent()}
                    <CurrencyInvoices store={this.store} />
                    <div className={style.tooltip}>
                        <Description
                            className={cn(grid.row, style.description)}
                            label={`Назначение (24)`}
                        />
                        {isShowTooltip && <Tooltip
                            width={300}
                            position={Position.topLeft}
                            content={this.getTooltipContent()}
                        />}
                    </div>
                    <KbkAutoFields operationStore={this.store} />
                    <TradingObjectWarning operationStore={this.store} />
                    <Postings />
                    <TradingObjectAdditionslWarning operationStore={this.store} />
                    <div className={cn(grid.row, style.buttons)}>
                        {this.renderButtons()}
                    </div>
                    <BusyNumberModal store={this.store} />
                    <SmsConfirmModal operationStore={this.store} />
                </React.Fragment>
            </Provider>
        );
    }
}

OutgoingBudgetaryPayment.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    patents: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default OutgoingBudgetaryPayment;
