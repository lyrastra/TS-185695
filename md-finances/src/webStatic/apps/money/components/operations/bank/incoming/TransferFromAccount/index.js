import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import NotificationPanel, { NotificationPanelType } from '@moedelo/frontend-core-react/components/NotificationPanel';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Input from '@moedelo/frontend-core-react/components/Input';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import TransferFromAccountStore from './stores/TransferFromAccountStore';
import { actionArray } from '../../../../../../../resources/newMoney/saveButtonResource';
import StateMessageResource from '../../../../../../../resources/newMoney/StateMessageResource';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Sum from '../../../commonComponents/Sum';
import Description from '../../../commonComponents/Description';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import Postings from '../../../commonComponents/Postings';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class TransferFromAccount extends React.Component {
    constructor(props) {
        super(props);

        this.store = new TransferFromAccountStore({
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
            isSavingBlocked,
            disabledSaveButton,
            isClosed,
            onClickSave,
            model: { DocumentBaseId }
        } = this.store;

        const buttonData = getSaveOperationButtonData({
            saveButtonData: actionArray,
            documentBaseId: DocumentBaseId
        });

        return <ElementsGroup>
            {!isClosed && <SplitButton
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
            <Button onClick={this.props.onCancel} color="white">Отмена</Button>
        </ElementsGroup>;
    };

    renderBubble = () => {
        if (!this.store.canShowBubble) {
            return null;
        }

        return <NotificationPanel className={style.bubble} type={NotificationPanelType.warning}>
            {StateMessageResource[this.store.model.OperationState].bubble}
        </NotificationPanel>;
    };

    render() {
        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    {this.renderBubble()}
                    <HeaderOperation
                        onDelete={this.onDelete}
                    />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                    </div>
                    <div className={grid.row}>
                        <Input
                            value={this.store.fromSettlementAccountNumber}
                            showAsText
                            label={`Расчетный счет`}
                        />
                    </div>
                    <div className={grid.row}>
                        <div className={grid.col_3}>
                            <Sum />
                        </div>
                    </div>
                    <Description className={cn(grid.row, style.description)} />
                    <Postings />
                    <div className={cn(grid.row, style.buttons)}>
                        {this.renderButtons()}
                    </div>
                </React.Fragment>
            </Provider>
        );
    }
}

TransferFromAccount.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default TransferFromAccount;
