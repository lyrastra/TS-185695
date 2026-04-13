import React, { Fragment } from 'react';
import * as mobx from 'mobx';
import { observer, inject } from 'mobx-react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import TableRow from '@moedelo/frontend-core-react/components/table/TableRow';
import DirectionEnum from '@moedelo/frontend-enums/mdEnums/Direction';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import Checkbox from '@moedelo/frontend-core-react/components/Checkbox';
import { getSymbolByCode } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import OperationAdditionalActions from '../../../OperationAdditionalActions';
import RelatedCharges from '../../../../components/CommonTablesComponents/RelatedCharges';
import {
    isSalary,
    description
} from '../../../../../../helpers/MoneyOperationHelper';
import style from './style.m.less';
import storage from '../../../../../../helpers/newMoney/storage';
import stringHelper from '../../../../../../helpers/stringHelper';
import { getEditUrlHash } from '../../../../../../helpers/newMoney/operationUrlHelper';
import operationTaxesHelper from '../../../../../../helpers/newMoney/operationTaxesHelper';
import { isDownloadable, is1CDisabled } from '../../../../../../helpers/newMoney/operationActionsHelper';
import PrimaryDocsStatusEnum from '../../../../../../enums/PrimaryDocsStatusEnum';
import RelatedDocs from '../../../RelatedDocs';
import PaymentRules from '../../../PaymentRules';
import ApproveControl from '../../../ApproveControl/ApproveControl';

const cn = classnames.bind(style);
const rubCurrencyCode = 643;

const OperationBody = observer(({ value, operationEditHash }) => {
    const {
        Sum, KontragentName, Description, Number, Taxes, DocumentBaseId, OperationType, Date, Direction, Currency: currencyCode = rubCurrencyCode
    } = value;
    const date = dateHelper(Date).format(`DD.MM.YYYY`).split(` `);
    const sum = Direction === DirectionEnum.Outgoing ? (-1) * Sum : Sum;
    const operationName = description(OperationType);
    const taxList = mobx.toJS(Taxes);

    return (
        <Fragment>
            <div className={cn(grid.col_3, style.cell)}>
                <a href={operationEditHash} className={style.link}>
                    <div className={style.date}>{date}</div>
                    { Number && <div className={style.number} title={`№${Number}`}>№{stringHelper.clip(Number, 6, `...`)}</div> }
                </a>
            </div>
            <div className={cn(grid.col_6, style.cell)}>
                <div className={style.kontragentName} title={KontragentName}>{KontragentName}</div>
                <div className={style.operationType}>{operationName}</div>
            </div>
            <div className={cn(grid.col_4, style.cell, style.priceCell, { incoming: sum > 0, outgoing: sum < 0 })}>
                {sum > 0 ? `+` : `–`}
                &nbsp;{toAmountString(sum > 0 ? sum : sum * -1)}<span className={style.currencyIcon}>{getSymbolByCode(currencyCode)}</span>
                {taxList.length > 0 && <div className={style.taxList}>
                    {taxList.map(tax => {
                        const taxString = operationTaxesHelper.getTax(tax);

                        return taxString ? <div className={style.tax} key={getUniqueId(`${DocumentBaseId}_tax_`)}>{taxString}</div> : false;
                    })}
                </div>}
            </div>
            <div className={cn(grid.col_1, style.cell)} />
            <div className={cn(grid.col_6, style.cell, style.description)}>
                {Description}
            </div>
        </Fragment>
    );
});

const DocumentsControl = observer(props => {
    const {
        OperationType, LinkedDocumentsCount, Direction, DocumentBaseId, PrimaryDocsStatus = 0, UncoveredSum, Currency, HasUnbindedSalaryChargePayments
    } = props.value;
    const canShowRelatedDocs = (PrimaryDocsStatus !== PrimaryDocsStatusEnum.CantHaveAnyDocs) &&
        !(PrimaryDocsStatus === PrimaryDocsStatusEnum.CantHavePrimaryDocs && LinkedDocumentsCount === 0);

    if (isSalary(OperationType)) {
        return <RelatedCharges needAttention={HasUnbindedSalaryChargePayments} />;
    }

    if (canShowRelatedDocs) {
        return (
            <RelatedDocs
                operationDirection={Direction}
                count={LinkedDocumentsCount}
                operationId={DocumentBaseId}
                tableCount={props.tableCount}
                primaryDocsStatus={PrimaryDocsStatus}
                uncoveredSum={UncoveredSum}
                operationType={OperationType}
                currency={Currency || rubCurrencyCode}
            />
        );
    }

    return null;
});

const OperationActions = observer(({
    value, onDelete, massChangeTaxSystemStore, commonDataStore, onApprove
}) => {
    return (
        <OperationAdditionalActions
            operation={value}
            onDelete={onDelete}
            canRemove
            isDownloadable={isDownloadable(value)}
            is1CDisabled={is1CDisabled(value)}
            massChangeTaxSystemStore={massChangeTaxSystemStore}
            commonDataStore={commonDataStore}
            onApprove={onApprove}
        />
    );
});

@inject(`massChangeTaxSystemStore`)
@observer
class Row extends React.Component {
    constructor(props) {
        super(props);

        this.value = props.value;

        this.state = {
            isApproved: this.value?.isApproved || false
        };
    }

    onClick = () => {
        storage.save(`successOperation`, true);
        this.props.onClick(this.props.value);
    }

    onChangeChecked = () => {
        this.value.toggleChecked();
        this.props.onSelect();
    }

    onClickAdditionalAction = e => {
        e.stopPropagation();
    }

    onKeyDown = e => {
        if (e && e.key === `Enter`) {
            return this.onClickAdditionalAction();
        }

        return null;
    }

    onApproveHandle = () => {
        this.props.onApprove({ Id: this.value.DocumentBaseId });

        this.setState({ isApproved: true });
        this.value = { ...this.value, isApproved: true };
    }

    render() {
        const {
            onDelete, tableCount, massChangeTaxSystemStore, commonDataStore
        } = this.props;
        const { value } = this;
        const { canShowImportRules, ImportRules } = value;
        const operationEditHash = `#${getEditUrlHash(value)}`;

        return (
            <a href={operationEditHash}>
                <TableRow className={style.row} onClickRow={this.onClick} checked={value.isChecked}>
                    <div className={cn(grid.col_1, style.cell, style.wideCheckbox)}>
                        <Checkbox onChange={this.onChangeChecked} checked={value.Checked} />
                    </div>
                    <OperationBody value={value} operationEditHash={operationEditHash} />
                    <div className={cn(grid.col_1, style.cell)}>
                        <DocumentsControl value={value} tableCount={tableCount} />
                    </div>
                    <div className={cn(grid.col_1, style.cell)}>
                        {value.isShowApprove && <ApproveControl isApproved={value.isApproved} />}
                    </div>
                    <div className={cn(grid.col_1, style.cell)}>
                        {canShowImportRules && <PaymentRules paymentRules={ImportRules} />}
                    </div>
                    <div
                        className={cn(grid.col_1, style.cell, style.cellRight)}
                        onClick={this.onClickAdditionalAction}
                        onKeyDown={this.onKeyDown}
                        role={`presentation`}
                    >
                        <OperationActions
                            value={value}
                            onDelete={onDelete}
                            massChangeTaxSystemStore={massChangeTaxSystemStore}
                            commonDataStore={commonDataStore}
                            onApprove={this.onApproveHandle}
                        />
                    </div>
                </TableRow>
            </a>
        );
    }
}

Row.propTypes = {
    onClick: PropTypes.func,
    onSelect: PropTypes.func,
    value: PropTypes.object,
    onDelete: PropTypes.func,
    tableCount: PropTypes.number,
    massChangeTaxSystemStore: PropTypes.object,
    commonDataStore: PropTypes.object,
    onApprove: PropTypes.func
};

export default Row;
