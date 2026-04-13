import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import HeaderOperation from '../../../commonComponents/HeaderOperation/HeaderOperation';
import {
    actionArray
} from '../../../../../../../resources/newMoney/saveButtonResource';
import Description from '../../../commonComponents/Description/Description';
import CurrencyBlock from '../../../commonComponents/CurrencyBlock';
import OutgoingCurrencyPurchaseStore from './stores/OutgoingCurrencyPurchaseStore';
import BusyNumberModal from '../../../commonComponents/BusyNumberModal';
import Postings from '../../../commonComponents/Postings';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import MoneySourceDropdown from '../../../commonComponents/MoneySourceDropdown';
import ActionEnum from '../../../../../../../enums/newMoney/ActionEnum';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class OutgoingCurrencyPurchase extends React.Component {
    constructor(props) {
        super(props);

        this.store = new OutgoingCurrencyPurchaseStore({
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
                Status: this.store.model.Status,
                initialOperationNumber: this.store.initialOperationNumber,
                IncludeNds: this.store.model.IncludeNds
            }
        });
    };

    renderToSettlementAccountRow = () => {
        const {
            model,
            setToSettlementAccount,
            toSettlementAccounts,
            validationState
        } = this.store;

        return <div className={cn(grid.row)}>
            <div className={cn(grid.col_9)}>
                <MoneySourceDropdown
                    onSelect={setToSettlementAccount}
                    value={model.ToSettlementAccountId}
                    data={toSettlementAccounts}
                    emptyMessage={`Нет валютного счета`}
                    label={`Зачислить на счет`}
                    error={!!validationState.ToSettlementAccountId}
                    message={validationState.ToSettlementAccountId}
                    allowEmpty
                />
            </div>
            <div className={cn(grid.col_1)} />
            <div className={cn(grid.col_7)} />
        </div>;
    };

    renderButtons = () => {
        const {
            isSavingBlocked,
            canEdit,
            onClickSave,
            model: { DocumentBaseId }
        } = this.store;

        const actions = actionArray
            .filter(action => action.value !== ActionEnum.DownloadAcc
                && action.value !== ActionEnum.DownloadPDF
                && action.value !== ActionEnum.DownloadXLS);

        const buttonData = getSaveOperationButtonData({
            saveButtonData: actions,
            documentBaseId: DocumentBaseId
        });

        if (canEdit) {
            return <ElementsGroup>
                <SplitButton
                    data={buttonData}
                    mainButton={{
                        className: `split`,
                        onClick: onClickSave
                    }}
                    onSelect={onClickSave}
                    loading={isSavingBlocked}
                >{getSaveButtonTitle({ documentBaseId: DocumentBaseId })}</SplitButton>
                <Button onClick={this.props.onCancel} color="white">Отмена</Button>
            </ElementsGroup>;
        }

        return <Button onClick={this.props.onCancel} color="white">Отмена</Button>;
    };

    render() {
        return <Provider operationStore={this.store}>
            <React.Fragment>
                <HeaderOperation
                    onDelete={this.onDelete}
                    hideStatus
                />
                <div className={grid.row}>
                    <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                </div>
                {this.renderToSettlementAccountRow()}
                <CurrencyBlock operationStore={this.store} />
                <Description className={cn(grid.row, style.description)} />
                <Postings operationStore={this.store} />
                <div className={cn(grid.row, style.buttons)}>
                    {this.renderButtons()}
                </div>
                <BusyNumberModal store={this.store} />
            </React.Fragment>
        </Provider>;
    }
}

OutgoingCurrencyPurchase.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default OutgoingCurrencyPurchase;
