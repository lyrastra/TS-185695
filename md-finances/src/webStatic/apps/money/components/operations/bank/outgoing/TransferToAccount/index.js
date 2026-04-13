import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import TransferToAccountStore from './stores/TransferToAccountStore';
import {
    actionArray
} from '../../../../../../../resources/newMoney/saveButtonResource';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Sum from '../../../commonComponents/Sum';
import Description from '../../../commonComponents/Description';
import Postings from '../../../commonComponents/Postings';
import BusyNumberModal from '../../../commonComponents/BusyNumberModal';
import style from './style.m.less';
import MoneySourceDropdown from '../../../commonComponents/MoneySourceDropdown';
import MoneySourceIcon from '../../../../MoneySourceIcon';
import MoneySourceType from '../../../../../../../enums/MoneySourceType';
import SendToBankButton from '../../../commonComponents/SendToBankButton';
import SmsConfirmModal from '../../../commonComponents/SmsConfirmModal';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';

const cn = classnames.bind(style);

@observer
class TransferToAccount extends React.Component {
    constructor(props) {
        super(props);

        this.store = new TransferToAccountStore({
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
                Kontragent: {},
                SettlementAccountId: this.store.model.SettlementAccountId,
                Sum: this.store.model.Sum,
                Description: this.store.model.Description,
                Status: this.store.model.Status,
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

    renderTransferSettlementAccountDropDown = () => {
        const {
            setToSettlementAccountId,
            getToSettlementAccountId,
            transferSettlementAccountList,
            canEdit
        } = this.store;

        return (
            <div className={grid.row}>
                <div className={cn(grid.col_9)}>
                    <MoneySourceDropdown
                        onSelect={setToSettlementAccountId}
                        value={getToSettlementAccountId}
                        data={transferSettlementAccountList}
                        icon={<MoneySourceIcon value={MoneySourceType.SettlementAccount} />}
                        label={`Расчетный счет`}
                        showAsText={!canEdit}
                    />
                </div>
                <div className={cn(grid.col_1)} />
            </div>
        );
    };

    render() {
        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation onDelete={this.onDelete} />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                    </div>
                    {this.renderTransferSettlementAccountDropDown()}
                    <div className={cn(grid.row, style.sumRow)}>
                        <div className={grid.col_3}>
                            <Sum />
                        </div>
                        <div className={grid.col_1} />
                    </div>
                    <Description className={cn(grid.row, style.description)} />
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

TransferToAccount.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default TransferToAccount;
