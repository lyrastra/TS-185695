import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Link, { Size } from '@moedelo/frontend-core-react/components/Link';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import Button, { Color } from '@moedelo/frontend-core-react/components/buttons/Button';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import MaterialAidStore from './stores/MaterialAidStore';
import { actionArray } from '../../../../../../../resources/newMoney/saveButtonResource';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import ContractorAutocomplete from '../../../commonComponents/ContractorAutocomplete';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Sum from '../../../commonComponents/Sum';
import Description from '../../../commonComponents/Description';
import Postings from '../../../commonComponents/Postings';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';

import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class MaterialAid extends React.Component {
    constructor(props) {
        super(props);

        this.store = new MaterialAidStore({
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
                Kontragent: this.store.model.Kontragent,
                Sum: this.store.model.Sum,
                Description: this.store.model.Description,
                OperationType: value,
                DocumentBaseId: this.store.model.DocumentBaseId,
                initialOperationNumber: this.store.initialOperationNumber
            }
        });
    };

    getKontragentTooltip = () => {
        const content = (
            <div>Если учредитель еще не заведен в системе, то создайте его в разделе&nbsp;
                <Link
                    target={`_blank`}
                    href={`/Estate/Home#/authorizedCapital/`}
                    size={Size.small}
                >
                    Уставный капитал
                </Link>
            </div>
        );

        return (
            <Tooltip
                wrapperClassName={style.tooltip}
                width={300}
                position={Position.topRight}
                content={content}
            />
        );
    }

    renderButtons = () => {
        const {
            isSavingBlocked,
            canEdit,
            disabledSaveButton,
            onClickSave,
            model: { DocumentBaseId }
        } = this.store;

        const buttonData = getSaveOperationButtonData({
            saveButtonData: actionArray,
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
                <Button onClick={this.props.onCancel} color={Color.White}>Отмена</Button>
            </ElementsGroup>;
        }

        return <Button onClick={this.props.onCancel} color={Color.White}>Отмена</Button>;
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
                    <ContractorAutocomplete
                        operationStore={this.store}
                        getTooltip={this.getKontragentTooltip}
                        canAddKontragent={false}
                    />
                    <div className={cn(grid.row, style.sumRow)}>
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

MaterialAid.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default MaterialAid;
