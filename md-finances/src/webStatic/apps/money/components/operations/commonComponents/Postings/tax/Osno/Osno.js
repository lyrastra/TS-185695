import React, { Fragment } from 'react';
import { observer, inject } from 'mobx-react';
import { toJS } from 'mobx';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import P from '@moedelo/frontend-core-react/components/P';
import Link from '@moedelo/frontend-core-react/components/Link';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import NotificationPanel from '@moedelo/frontend-core-react/components/NotificationPanel';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import PostingDirection from '../../../../../../../../enums/newMoney/TaxPostingDirectionEnum';
import osnoPostingTypes from '../../../../../../../../resources/newMoney/osnoPostingTypes';
import Row from './components/Row';
import { availableDirection, sortPostings } from '../../../../../../../../helpers/newMoney/postingsHelpers';
import style from './style.m.less';

const cn = classnames.bind(style);

@inject(`operationStore`)
@observer
class Osno extends React.Component {
    onChangeItem = posting => {
        const { model, editTaxPostingList } = this.props.operationStore;
        const list = toJS(model.TaxPostings.Postings);

        const index = list.findIndex(item => item.key === posting.key);
        list[index] = taxPostingsValidator.getValidatedOsnoPosting(posting);
        editTaxPostingList(list);
    };

    onAdd = () => {
        const { model, editTaxPostingList } = this.props.operationStore;
        const list = toJS(model.TaxPostings.Postings);
        list.push({});
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

    getTransferTypeList = posting => {
        return this.props.operationStore.getTransferType(posting).map(type => {
            return {
                text: osnoPostingTypes.transferTypePostings[type],
                value: type
            };
        });
    };

    getTransferKindList = posting => {
        return this.props.operationStore.getTransferKind(posting).map(type => {
            return {
                text: osnoPostingTypes.transferKindPostings[type],
                value: type
            };
        });
    };

    getNormalizedCostTypeList = posting => {
        return this.props.operationStore.getNormalizedCostType(posting).map(type => {
            return {
                text: osnoPostingTypes.normalizedCostTypePostings[type],
                value: type
            };
        });
    };

    getPostingsHead = () => {
        return (
            <div className={cn(style.head, grid.row)}>
                <div className={grid.col_3} />
                <div className={cn(style.labelSum, grid.col_3)}>
                    Сумма
                </div>
                <div className={cn(style.label, grid.col_5)}>
                    Тип
                </div>
                <div className={cn(style.label, grid.col_5)}>
                    Вид
                </div>
                <div className={cn(style.label, grid.col_11)}>
                    Нормируемый
                </div>
                <div className={grid.col_1} />
            </div>
        );
    }

    defaultDirection = () => {
        return this.props.operationStore.availableTaxPostingDirection !== AvailableTaxPostingDirection.Outgoing
            ? PostingDirection.Incoming
            : PostingDirection.Outgoing;
    };

    renderError = () => {
        const { model, sumOperation } = this.props.operationStore;

        const msg = taxPostingsValidator.getAllSumValidation(model.TaxPostings.Postings, { Sum: sumOperation });

        if (msg) {
            return <div className={cn(style.error)}>
                { msg }
            </div>;
        }

        return null;
    };

    renderPostings = () => {
        const { Postings = [], LinkedDocuments = [] } = this.props.operationStore.model.TaxPostings;
        const {
            canEditTaxPostings,
            isNotTaxable,
            availableTaxPostingDirection
        } = this.props.operationStore;

        if (isNotTaxable || (!Postings.length && !LinkedDocuments.length)) {
            return null;
        }

        if (!Postings.length && LinkedDocuments.length) {
            return this.getPostingsHead();
        }

        return <Fragment>
            {this.getPostingsHead()}
            {Postings.slice().sort(sortPostings).map(posting => {
                return <Row
                    key={posting.key}
                    posting={{ Direction: this.defaultDirection(), ...posting }}
                    transferType={this.getTransferTypeList(posting)}
                    transferKind={this.getTransferKindList(posting)}
                    normalizedCostType={this.getNormalizedCostTypeList(posting)}
                    readOnly={!canEditTaxPostings}
                    onChange={this.onChangeItem}
                    onDelete={this.onDelete}
                    canChangeDirection={availableTaxPostingDirection === AvailableTaxPostingDirection.Both}
                />;
            })}
            {this.renderAddLink()}
            {this.renderError()}
        </Fragment>;
    };

    renderAddLink = () => {
        const { canEditTaxPostings, availableTaxPostingDirection } = this.props.operationStore;

        return canEditTaxPostings && <Link
            text={`+ ${availableDirection(availableTaxPostingDirection)}`}
            onClick={this.onAdd}
            className={style.addLink}
        />;
    };

    render() {
        const { ExplainingMessage = null, Error } = this.props.operationStore.model.TaxPostings;

        if (Error) {
            return <NotificationPanel type="error" canClose={false}>{Error}</NotificationPanel>;
        }

        return <Fragment>
            <P>{ExplainingMessage}</P>
            {this.renderPostings()}
        </Fragment>;
    }
}

Osno.propTypes = {
    operationStore: PropTypes.object
};

export default Osno;
