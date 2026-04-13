import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import IncomingCurrencyFromAnotherAccountStore from './stores/IncomingCurrencyFromAnotherAccountStore';
import { actionArray } from '../../../../../../../resources/newMoney/saveButtonResource';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Sum from '../../../commonComponents/Sum';
import Description from '../../../commonComponents/Description';
import Postings from '../../../commonComponents/Postings';

import style from './style.m.less';
import MoneySourceType from '../../../../../../../enums/MoneySourceType';
import MoneySourceIcon from '../../../../MoneySourceIcon';
import MoneySourceDropdown from '../../../commonComponents/MoneySourceDropdown';
import ActionEnum from '../../../../../../../enums/newMoney/ActionEnum';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';

const cn = classnames.bind(style);

@observer
class IncomingCurrencyFromAnotherAccount extends React.Component {
    constructor(props) {
        super(props);

        this.store = new IncomingCurrencyFromAnotherAccountStore({
            ...getCommonOperationStoreData(props),
            operationTypes: props.operationTypes,
            moneySourceStore: props.moneySourceStore
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
                initialOperationNumber: this.store.initialOperationNumber,
                IncludeNds: this.store.model.IncludeNds
            }
        });
    };

    renderFromSettlementAccountDropDown = () => {
        const {
            setFromSettlementAccount,
            fromSettlementAccountList,
            canEdit,
            model,
            validationState
        } = this.store;

        const { FromSettlementAccountId } = model;

        return (
            <div className={grid.row}>
                <div className={cn(grid.col_9)}>
                    <MoneySourceDropdown
                        onSelect={setFromSettlementAccount}
                        value={FromSettlementAccountId}
                        data={fromSettlementAccountList}
                        icon={<MoneySourceIcon value={MoneySourceType.SettlementAccount} />}
                        label={`Валютный счет отправителя`}
                        showAsText={!canEdit}
                        error={!!validationState.FromSettlementAccountId}
                        message={validationState.FromSettlementAccountId}
                        allowEmpty

                    />
                </div>
                <div className={cn(grid.col_1)} />
            </div>);
    }

    renderButtons = () => {
        const {
            isSavingBlocked,
            canEdit,
            disabledSaveButton,
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
            <Button onClick={this.props.onCancel} color="white">Отмена</Button>
        </ElementsGroup>;
    };

    render() {
        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation
                        onDelete={this.onDelete}
                    />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                    </div>
                    {this.renderFromSettlementAccountDropDown()}
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

IncomingCurrencyFromAnotherAccount.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    moneySourceStore: PropTypes.object,
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default IncomingCurrencyFromAnotherAccount;
