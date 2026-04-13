import React, { Fragment } from 'react';
import classnames from 'classnames/bind';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import P from '@moedelo/frontend-core-react/components/P';
import NotificationPanel from '@moedelo/frontend-core-react/components/NotificationPanel';
import { observer, inject } from 'mobx-react';
import PropTypes from 'prop-types';
import { toJS } from 'mobx';
import Link from '@moedelo/frontend-core-react/components/Link';
import Row from './components/Row';
import style from './style.m.less';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import PostingDirection from '../../../../../../../../enums/newMoney/TaxPostingDirectionEnum';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { availableDirection, sortPostings } from '../../../../../../../../helpers/newMoney/postingsHelpers';
import { isDifferenceAvailableInTax } from '../../../../../../../../helpers/MoneyOperationHelper';

const cn = classnames.bind(style);

@inject(`operationStore`)
@observer
class SimplifiedPostingsForm extends React.Component {
    onChangeItem = posting => {
        const { operationStore, getValidatedPosting } = this.props;
        const { model, editTaxPostingList } = operationStore;
        const list = toJS(model.TaxPostings.Postings);

        const index = list.findIndex(item => item.key === posting.key);
        list[index] = getValidatedPosting(posting);
        editTaxPostingList(list);
    };

    onDelete = posting => {
        const { model, editTaxPostingList } = this.props.operationStore;
        const list = toJS(model.TaxPostings.Postings);

        const index = list.findIndex(item => item.key === posting.key);
        list.splice(index, 1);

        /** isDeleting нужен для бюджетного платежа */
        editTaxPostingList(list, { isDeleting: true });
    };

    onAdd = () => {
        const { model, editTaxPostingList } = this.props.operationStore;
        const list = toJS(model.TaxPostings.Postings);
        list.push({});
        editTaxPostingList(list);
    };

    defaultDirection = () => {
        return this.props.operationStore.availableTaxPostingDirection !== AvailableTaxPostingDirection.Outgoing
            ? PostingDirection.Incoming
            : PostingDirection.Outgoing;
    };

    renderPostings = () => {
        const { Postings } = this.props.operationStore.model.TaxPostings;
        const {
            disableSum,
            operationStore,
            disableDescription
        } = this.props;
        const {
            canEditTaxPostings,
            isNotTaxable,
            availableTaxPostingDirection
        } = operationStore;

        if (isNotTaxable || !Postings?.length) {
            return null;
        }

        return <Fragment>
            <div className={cn(style.head, grid.row)}>
                <div className={cn(grid.col_3)} />
                <div className={cn(style.labelSum, grid.col_3)}>
                    Сумма
                </div>
                {!disableDescription && (
                    <div className={cn(style.labelDescription, grid.col_18)}>
                        Комментарий
                    </div>
                )}
            </div>
            {Postings.slice().sort(sortPostings).map(posting => {
                return <Row
                    key={posting.key}
                    posting={{ Direction: this.defaultDirection(), ...posting }}
                    readOnly={!canEditTaxPostings}
                    onChange={this.onChangeItem}
                    onDelete={this.onDelete}
                    disableSum={disableSum}
                    canChangeDirection={availableTaxPostingDirection === AvailableTaxPostingDirection.Both}
                    operationDirection={this.props.operationStore.model.Direction}
                    disableDescription={disableDescription}
                />;
            })}
            {this.renderAddLink()}
            {this.renderCommonValidationError()}
        </Fragment>;
    };

    renderAddLink = () => {
        if (!this.props.operationStore.hasTaxPostings) {
            return null;
        }

        const { operationStore } = this.props;
        const { canEditTaxPostings, availableTaxPostingDirection } = this.props.operationStore;
        const { isOsno, isOoo } = operationStore;
        const isIpOsno = isOsno && !isOoo;

        return canEditTaxPostings && !isIpOsno && <Link
            text={`+ ${availableDirection(availableTaxPostingDirection)}`}
            onClick={this.onAdd}
            className={style.addLink}
        />;
    };

    renderCommonValidationError = () => {
        const {
            needAllSumValidation, /* например, в Возврате покупателю сумму пп и сумму НУ записей проверять не нужно */
            needToValidateLoanInterestSum,
            needToValidateExchangeRateDiff,
            needToValidateTotalSum,
            sumOperation,
            model
        } = this.props.operationStore;
        const {
            TaxPostings,
            LoanInterestSum,
            ExchangeRateDiff,
            TotalSum,
            OperationType
        } = model;

        const isDifferenceAllowable = isDifferenceAvailableInTax(OperationType);

        let msg = null;

        if (needAllSumValidation) {
            msg = taxPostingsValidator.getAllSumValidation(TaxPostings.Postings, { Sum: sumOperation });
        }

        if (needToValidateLoanInterestSum) {
            msg = taxPostingsValidator.getAllLoanInterestSumValidation(TaxPostings.Postings, { LoanInterestSum });
        }

        if (needToValidateExchangeRateDiff) {
            msg = taxPostingsValidator.getAllExchangeRateDiffValidation(TaxPostings.Postings, { ExchangeRateDiff });
        }

        if (needToValidateTotalSum) {
            msg = taxPostingsValidator.getAllTotalSumValidation(TaxPostings.Postings, { TotalSum, isDifferenceAllowable });
        }

        if (!msg) {
            return null;
        }

        return (
            <div className={cn(style.error)}>
                { msg }
            </div>
        );
    };

    render() {
        const { ExplainingMessage = null, Error } = this.props.operationStore.model.TaxPostings;

        if (Error) {
            return <NotificationPanel type="error" canClose={false}>{Error}</NotificationPanel>;
        }

        return <Fragment>
            <P className={`explainingMessage`}>{ExplainingMessage}</P>
            {this.renderPostings()}
        </Fragment>;
    }
}

SimplifiedPostingsForm.propTypes = {
    operationStore: PropTypes.object,
    disableDescription: PropTypes.bool,
    getValidatedPosting: PropTypes.func,
    disableSum: PropTypes.bool
};

export default SimplifiedPostingsForm;
