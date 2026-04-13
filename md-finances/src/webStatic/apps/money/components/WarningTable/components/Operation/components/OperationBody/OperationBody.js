import React from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { getSymbolByCode } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import OperationAdditionalActions from '../../../../../OperationAdditionalActions';
import { description } from '../../../../../../../../helpers/MoneyOperationHelper';
import stringHelper from '../../../../../../../../helpers/stringHelper';
import { getEditUrlHash } from '../../../../../../../../helpers/newMoney/operationUrlHelper';
import PaymentRules from '../../../../../PaymentRules';
import ApproveControl from '../../../../../ApproveControl/ApproveControl';
import style from './style.m.less';

const cn = classnames.bind(style);
const rubCurrencyCode = 643;

class OperationBody extends React.Component {
    _onClickAdditionalAction = e => {
        e.stopPropagation();
    };

    _onKeyDown = e => {
        if (e && e.key === `Enter`) {
            return this.onClickAdditionalAction();
        }

        return null;
    };

    _getAdditionalActions = () => {
        const { data, onDelete } = this.props;

        return (
            <div
                className={grid.col_1}
                onClick={this._onClickAdditionalAction}
                onKeyDown={this._onKeyDown}
                role={`presentation`}
            >
                <OperationAdditionalActions
                    operation={data}
                    onDelete={onDelete}
                    canRemove
                    isDownloadable={false}
                    isWarningTable
                />
            </div>
        );
    };

    _getPaymentRules = () => {
        const { ImportRules, OutsourceImportRules } = this.props.data;

        if (ImportRules?.length > 0 || OutsourceImportRules?.length > 0) {
            return <PaymentRules paymentRules={ImportRules} outsourcePaymentRules={OutsourceImportRules} />;
        }

        return null;
    }

    _getBody = () => {
        const { base, data } = this.props;
        const operation = data;
        const { Number, Currency: currencyCode = rubCurrencyCode } = operation;
        const operationType = description(operation.OperationType);
        const operationEditHash = `#${getEditUrlHash(operation)}`;
        const date = dateHelper(operation.Date).format(`DD.MM.YYYY`);

        return (
            <div
                className={cn(`operationBody__content`)}
                key={operation.DocumentBaseId + (10 + (Math.random() * (20 - 10)))}
            >
                <div className={cn(grid.row, `operationBody__row`)}>
                    <div className={grid.col_1} />
                    <div className={grid.col_3}>
                        <a href={operationEditHash} className={cn(`operationBody__link`)}>
                            <div className={cn(`operationBody__date`)}>{date}</div>
                            {Number && <div
                                className={cn(`operationBody__number`)}
                                title={`№${Number}`}
                            >№{stringHelper.clip(Number, 6, `...`)}</div>}
                        </a>
                    </div>
                    <div className={grid.col_6}>
                        <div className={cn(`operationBody__kontragent`)}>
                            {operation.KontragentName}
                        </div>
                        <div className={cn(`operationBody__reason`)}>
                            {operationType}
                        </div>
                    </div>
                    <div className={grid.col_4}>
                        <div
                            className={cn(`operationBody__payment`, { incoming: operation.Direction === Direction.Incoming }, { outgoing: operation.Direction === Direction.Outgoing })}
                        >
                            {operation.Direction === Direction.Incoming ? `+` : `–`}&nbsp;{toAmountString(operation.Sum)}&nbsp;
                            <span className={cn(`operationBody__currency`)}>{getSymbolByCode(currencyCode)}</span>
                        </div>
                    </div>
                    <div className={grid.col_1} />
                    <div className={grid.col_6}>
                        <div className={cn(`operationBody__desc`)}>
                            {operation.Description}
                        </div>
                    </div>
                    <div className={grid.col_1}>
                        {operation.isShowApprove && <ApproveControl isApproved={operation.isApproved} isFromWarningTable />}
                    </div>
                    <div className={grid.col_1} />
                    <div className={grid.col_1}>
                        {this._getPaymentRules()}
                    </div>
                    {!base && this._getAdditionalActions()}
                </div>
            </div>
        );
    };

    _getOriginalBody = () => {
        const { data } = this.props;
        const operation = data;
        const { Number, Currency: currencyCode = rubCurrencyCode } = operation;
        const operationType = description(operation.OperationType);
        const date = dateHelper(operation.Date).format(`DD.MM.YYYY`);

        return (
            <div
                className={cn(`operationBody__content`)}
                key={operation.DocumentBaseId + (20 + (Math.random() * (30 - 20)))}
            >
                <div className={cn(grid.row, `operationBody__row`)}>
                    <div className={grid.col_1} />
                    <div className={grid.col_3}>
                        <div className={cn(`operationBody__date`)}>{date}</div>
                        {Number && <div
                            className={cn(`operationBody__number`)}
                            title={`№${Number}`}
                        >№{stringHelper.clip(Number, 6, `...`)}</div>}
                    </div>
                    <div className={grid.col_6}>
                        <div className={cn(`operationBody__kontragent`)}>
                            {operation.KontragentName}
                        </div>
                        <div className={cn(`operationBody__reason`)}>
                            {operationType}
                        </div>
                    </div>
                    <div className={grid.col_3}>
                        <div
                            className={cn(`operationBody__payment`, { incoming: operation.Direction === Direction.Incoming }, { outgoing: operation.Direction === Direction.Outgoing })}
                        >
                            {operation.Direction === Direction.Incoming ? `+` : `–`}
                            &nbsp;{toAmountString(operation.Sum)}&nbsp;<span
                                className={cn(`operationBody__currency`)}
                            >{getSymbolByCode(currencyCode)}</span>
                        </div>
                    </div>
                    <div className={grid.col_1} />
                    <div className={grid.col_7}>
                        <div className={cn(`operationBody__desc`)}>
                            {operation.Description}
                        </div>
                    </div>
                    {this._getPaymentRules()}
                    <div className={grid.col_1} />
                </div>
            </div>
        );
    };

    render() {
        const { base } = this.props;

        return (
            <div className={cn(`operationBody`, { 'operationBody--original': base })}>
                {!base && this._getBody()}

                {base && this._getOriginalBody()}
            </div>
        );
    }
}

OperationBody.propTypes = {
    data: PropTypes.object,
    base: PropTypes.bool,
    onDelete: PropTypes.func,
    ImportRules: PropTypes.arrayOf(PropTypes.shape({
        id: PropTypes.number.isRequired,
        name: PropTypes.string.isRequired
    }))
};

export default OperationBody;
