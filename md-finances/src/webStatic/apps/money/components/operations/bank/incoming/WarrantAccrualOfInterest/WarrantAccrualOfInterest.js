import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import TaxationSystemType from '../../../commonComponents/TaxationSystemType';
import WarrantAccrualOfInterestStore from './stores/WarrantAccrualOfInterestStore';
import { actionArray } from '../../../../../../../resources/newMoney/saveButtonResource';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Sum from '../../../commonComponents/Sum';
import Description from '../../../commonComponents/Description';
import Postings from '../../../commonComponents/Postings';
import { getSaveOperationButtonData, getSaveButtonTitle } from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';

import style from './style.m.less';

const cn = classnames.bind(style);

// поступление начисление процентов от банка

@observer
class WarrantAccrualOfInterest extends React.Component {
    constructor(props) {
        super(props);

        this.store = new WarrantAccrualOfInterestStore({
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

    getTooltipContent = () => {
        const isUsn = this.store.TaxationSystem.IsUsn;

        return isUsn ? `С 2026 года проценты по вкладам (депозитам) и остаткам на счетах в банках РФ не учитываются в доходах ИП при УСН.` : `С 2026 года проценты по вкладам (депозитам) и остаткам на счетах в банках РФ не учитываются в доходах ИП.`
    };

    renderButtons = () => {
        const {
            isSavingBlocked,
            canEdit,
            disabledSaveButton,
            onClickSave,
            model: { DocumentBaseId }
        } = this.store;

        const actions = actionArray.slice(-1);

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
                        onClick: onClickSave,
                        disabled: disabledSaveButton
                    }}
                    onSelect={onClickSave}
                    loading={isSavingBlocked}
                    disabled={disabledSaveButton}
                >{getSaveButtonTitle({ documentBaseId: DocumentBaseId })}</SplitButton>
                <Button onClick={this.props.onCancel} color="white">Отмена</Button>
            </ElementsGroup>;
        }

        return <Button onClick={this.props.onCancel} color="white">Отмена</Button>;
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
                    <div className={grid.row}>
                        <div className={grid.col_3}>
                            <Sum />
                        </div>
                        <div className={grid.col_1} />
                        {this.store.canShowTaxationSystemTypeDropdown && <TaxationSystemType operationStore={this.store} /> }
                        {this.store.canShowTooltip && <Tooltip
                            wrapperClassName={style.tooltip}
                            width={320}
                            position={Position.topRight}
                            content={this.getTooltipContent()}
                        />}
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

WarrantAccrualOfInterest.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default WarrantAccrualOfInterest;
