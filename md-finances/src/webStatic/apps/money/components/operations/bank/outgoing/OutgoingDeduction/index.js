import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import {
    actionArray
} from '../../../../../../../resources/newMoney/saveButtonResource';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Postings from '../../../commonComponents/Postings';
import ContractorAutocomplete from '../../../commonComponents/ContractorAutocomplete';
import ContractAutocomplete from '../../../commonComponents/ContractAutocomplete';
import Sum from '../../../commonComponents/Sum';
import Description from '../../../commonComponents/Description';
import BusyNumberModal from '../../../commonComponents/BusyNumberModal';
import SendToBankButton from '../../../commonComponents/SendToBankButton';
import SmsConfirmModal from '../../../commonComponents/SmsConfirmModal';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';

import PaymentPriorityDropdown from './components/PaymentPriorityDropdown';
import BudgetaryDebt from './components/BudgetaryDebt';
import DeductionStore from './stores/DeductionStore';
import DeductionWorkerAutocomplete from './components/DeductionWorkerAutocomplete';
import DocumentNumberAutocomplete from './components/DocumentNumberAutocomplete';
import PayerStatusDropdown from './components/PayerStatusDropdown';
import KbkInput from './components/KbkInput';
import DeductionTooltip from './components/DeductionTooltip';
import OktmoInput from './components/OktmoInput';
import UinInput from './components/UinInput';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class OutgoingDeduction extends React.Component {
    constructor(props) {
        super(props);

        this.store = new DeductionStore({
            ...getCommonOperationStoreData(props),
            operationTypes: props.operationTypes
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
                Kontragent: this.store.model.Kontragent.KontragentId ? this.store.model.Kontragent : {},
                WorkerName: this.store.contractorIsWorker && this.store.model.Kontragent.KontragentName,
                SalaryWorkerId: this.store.model.Kontragent.SalaryWorkerId,
                Kbk: { // фикс случая, если переключаемся на БП
                    Id: null,
                    Number: ``
                },
                Sum: this.store.model.Sum,
                Description: this.store.model.Description,
                OperationType: value,
                DocumentBaseId: this.store.model.DocumentBaseId,
                initialOperationNumber: this.store.initialOperationNumber
            }
        });
    };

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

    renderDebtFields = () => {
        const { model: { IsBudgetaryDebt }, canEdit } = this.store;

        if (!IsBudgetaryDebt) {
            return undefined;
        }

        return <React.Fragment>
            <div className={cn(grid.row)}>
                <div className={grid.col_9}><DeductionWorkerAutocomplete operationStore={this.store} /></div>
                <div className={grid.col_1} />
                <div className={grid.col_8}><KbkInput operationStore={this.store} /></div>
                <div className={grid.col_1}>{ !!canEdit && <DeductionTooltip content={`Укажите КБК при наличии`} /> }</div>
            </div>
            <div className={cn(grid.row)}>
                <div className={grid.col_9}><OktmoInput operationStore={this.store} /></div>
                <div className={grid.col_1} />
                <div className={grid.col_9}><DocumentNumberAutocomplete operationStore={this.store} /></div>
                <div className={grid.col_1}>{ !!canEdit && <DeductionTooltip content={`Поле необязательное, если указан ИНН сотрудника-плательщика удержания или УИН в поле «Код»`} /> }</div>
            </div>
        </React.Fragment>;
    }

    render() {
        const { canEdit } = this.store;

        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation onDelete={this.onDelete} />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                    </div>
                    <ContractorAutocomplete operationStore={this.store} />
                    <ContractAutocomplete canAddContract={!this.store.contractorIsWorker && !this.store.contractIsMainFirm} />
                    <div className={cn(grid.row, style.debtRow)}>
                        <div className={grid.col_9}><PayerStatusDropdown operationStore={this.store} /></div>
                        <div className={grid.col_15} />
                    </div>
                    <div className={cn(grid.row, style.debtRow)}>
                        <div className={grid.col_9}><PaymentPriorityDropdown operationStore={this.store} /></div>
                        <div className={grid.col_15} />
                    </div>
                    <div className={cn(grid.row)}>
                        <div className={grid.col_8}><UinInput operationStore={this.store} /></div>
                        <div className={grid.col_1}>{ !!canEdit && <DeductionTooltip content={`Указывается УИН (уникальный идентификатор платежа), установленный получателем платежа. При его отсутствии указывается 0.`} /> }</div>
                        <div className={grid.col_1} />
                        <div className={grid.col_14}><BudgetaryDebt operationStore={this.store} /></div>
                    </div>
                    {this.renderDebtFields()}
                    <div className={cn(grid.row)}>
                        <div className={grid.col_3}>
                            <Sum />
                        </div>
                        <div className={grid.col_1} />
                    </div>
                    <Description
                        className={cn(grid.row, style.description)}
                        tooltip={`Укажите сведения, которые помогут идентифицировать платеж: Ф.И.О. должника, реквизиты постановления, номер исполнительного производства и др.`}
                    />
                    <Postings />
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

OutgoingDeduction.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default OutgoingDeduction;
