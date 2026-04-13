import React, { Fragment } from 'react';
import * as mobx from 'mobx';
import { observer, inject } from 'mobx-react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import TableRow from '@moedelo/frontend-core-react/components/table/TableRow';
import Checkbox from '@moedelo/frontend-core-react/components/Checkbox';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import DirectionEnum from '@moedelo/frontend-enums/mdEnums/Direction';
import { toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import { getSymbolByCode } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import OperationAdditionalActions from '../../../OperationAdditionalActions';
import RelatedCharges from '../../../../components/CommonTablesComponents/RelatedCharges';
import {
    isSalary,
    description,
    canCopyOperation
} from '../../../../../../helpers/MoneyOperationHelper';
import RelatedDocs from '../../../RelatedDocs';
import PaymentRules from '../../../PaymentRules';
import PaymentStatus from './components/PaymentStatus';
import stringHelper from '../../../../../../helpers/stringHelper';
import operationTaxesHelper from '../../../../../../helpers/newMoney/operationTaxesHelper';
import { isDownloadable, is1CDisabled } from '../../../../../../helpers/newMoney/operationActionsHelper';
import PrimaryDocsStatusEnum from '../../../../../../enums/PrimaryDocsStatusEnum';
import { getEditUrlHash } from '../../../../../../helpers/newMoney/operationUrlHelper';
import scenarioTipsIdHelper from '../../../../../../helpers/scenarioTipsIdHelper';
import storage from '../../../../../../helpers/newMoney/storage';
import ApproveControl from '../../../ApproveControl/ApproveControl';
import style from './style.m.less';

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

    const onClickStopPropagation = e => {
        e.stopPropagation();

        storage.save(`Scroll`, window.scrollY);
    };

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
            <div
                className={cn(grid.col_4, style.cell, style.priceCell, { incoming: sum > 0, outgoing: sum < 0 })}
            >
                <div
                    id={scenarioTipsIdHelper.getOperationTipId(OperationType, Sum)}
                    onClick={onClickStopPropagation}
                    role={`presentation`}
                />
                {sum > 0 ? `+` : `–`}
                &nbsp;{toFinanceString(sum > 0 ? sum : sum * -1)}<span className={cn(`currencyIcon`)}>{getSymbolByCode(currencyCode)}</span>
                {taxList.length > 0 && <div className={style.taxList}>
                    {taxList.map(tax => {
                        const taxString = operationTaxesHelper.getTax(tax);

                        return taxString ? <div
                            className={style.tax}
                            key={getUniqueId(`${DocumentBaseId}_tax`)}
                        >
                            {taxString}
                            <div
                                id={scenarioTipsIdHelper.getTaxTipId(tax.TaxType, Direction)}
                                onClick={onClickStopPropagation}
                                role={`presentation`}
                            />
                        </div> : false;
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

const DocumentsControl = observer(({ value, tableCount }) => {
    const {
        OperationType,
        LinkedDocumentsCount,
        HasUnbindedSalaryChargePayments,
        Direction,
        DocumentBaseId,
        PrimaryDocsStatus = PrimaryDocsStatusEnum.CantHaveAnyDocs,
        UncoveredSum,
        Currency
    } = value;

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
                tableCount={tableCount}
                primaryDocsStatus={PrimaryDocsStatus}
                uncoveredSum={UncoveredSum}
                operationType={OperationType}
                currency={Currency || rubCurrencyCode}
                isMainTable
            />
        );
    }

    return null;
});

const OperationActions = observer(({
    value, onSendToBank, copyOperation, onDelete, massChangeTaxSystemStore, commonDataStore, onApprove, isHistoryButtonVisible
}) => {
    return <OperationAdditionalActions
        operation={value}
        onDelete={onDelete}
        copyOperation={copyOperation}
        isDownloadable={isDownloadable(value)}
        is1CDisabled={is1CDisabled(value)}
        canCopy={canCopyOperation({ ...value, ...commonDataStore })}
        canRemove={commonDataStore?.isAccessToMoneyEnabled}
        onSendToBank={onSendToBank}
        onApprove={onApprove}
        massChangeTaxSystemStore={massChangeTaxSystemStore}
        isHistoryButtonVisible={isHistoryButtonVisible}
    />;
});

@inject(`massChangeTaxSystemStore`)
@observer
class Row extends React.Component {
    constructor(props) {
        super(props);

        this.value = props.value;
    }

    onClick = () => {
        this.props.onClick(this.props.value);
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

    onChangeChecked = () => {
        this.value.toggleChecked();
        this.props.onSelect();
    }

    onApprove = () => {
        this.props.onApprove({ Id: this.value.DocumentBaseId });
    }

    render() {
        const {
            onSendToBank, copyOperation, onDelete, tableCount, massChangeTaxSystemStore, commonDataStore, isHistoryButtonVisible
        } = this.props;
        const { value } = this;

        const { canShowImportRules, ImportRules, OutsourceImportRules } = value;
        const operationEditHash = `#${getEditUrlHash(value)}`;

        return (
            <a href={operationEditHash} className={cn(style.link, style.rowWrapper)}>
                <TableRow className={style.row} onClickRow={this.onClick} checked={value.isChecked}>
                    <PaymentStatus model={mobx.toJS(value)} />
                    <div className={cn(grid.col_24, style.rowContent)}>
                        <div className={cn(grid.col_1, style.cell, style.wideCheckbox)}>
                            <Checkbox onChange={this.onChangeChecked} checked={value.isChecked} />
                        </div>
                        <OperationBody value={value} operationEditHash />
                        <div className={cn(grid.col_1, style.cell, style[`text--right`])}>
                            <DocumentsControl value={value} tableCount={tableCount} />
                        </div>
                        <div className={cn(grid.col_1, style.cell, style[`text--right`])}>
                            {value.isShowApprove && <ApproveControl isApproved={value.isApproved} />}
                        </div>
                        <div className={cn(grid.col_1, style.cell, style[`text--right`])}>
                            {(canShowImportRules || OutsourceImportRules?.length > 0) && <PaymentRules paymentRules={ImportRules} outsourcePaymentRules={OutsourceImportRules} />}
                        </div>
                        <div
                            className={cn(grid.col_1, style.cell, style[`text--right`])}
                            onClick={this.onClickAdditionalAction}
                            onKeyDown={this.onKeyDown}
                            role={`presentation`}
                        >
                            <OperationActions
                                value={value}
                                onSendToBank={onSendToBank}
                                copyOperation={copyOperation}
                                onDelete={onDelete}
                                massChangeTaxSystemStore={massChangeTaxSystemStore}
                                commonDataStore={commonDataStore}
                                onApprove={this.onApprove}
                                isHistoryButtonVisible={isHistoryButtonVisible}
                            />
                        </div>
                    </div>
                </TableRow>
            </a>
        );
    }
}

Row.propTypes = {
    onClick: PropTypes.func,
    onSelect: PropTypes.func,
    onDelete: PropTypes.func,
    onApprove: PropTypes.func,
    copyOperation: PropTypes.func,
    value: PropTypes.object,
    tableCount: PropTypes.number,
    onSendToBank: PropTypes.oneOfType([
        PropTypes.func,
        PropTypes.bool
    ]),
    massChangeTaxSystemStore: PropTypes.object,
    commonDataStore: PropTypes.object,
    isHistoryButtonVisible: PropTypes.bool
};

export default Row;
