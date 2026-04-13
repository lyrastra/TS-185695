import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import P from '@moedelo/frontend-core-react/components/P';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import H4 from '@moedelo/frontend-core-react/components/headers/H4';
import Arrow from '@moedelo/frontend-core-react/components/Arrow';
import Link from '@moedelo/frontend-core-react/components/Link';
import style from './style.m.less';
import { availableDirection } from '../../../../../../../../helpers/newMoney/postingsHelpers';
import osnoPostingTypes from '../../../../../../../../resources/newMoney/osnoPostingTypes';

const cn = classnames.bind(style);

class LinkedDocumentsList extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isLinkedDocumentsVisible: false
        };
    }

    getUsnTaxLinkedDocuments = ({ Postings = [] }) => {
        return Postings.map((posting) => {
            const {
                Description,
                Direction,
                Sum
            } = posting;

            return (
                <div className={cn(grid.row, style.row)}>
                    <div className={cn(grid.col_3, `ld_direction`)} >
                        {availableDirection(Direction)}
                    </div>
                    <div className={cn(style.sum, grid.col_3, `ld_sum`)}>
                        {Sum === null ? `` : toAmountString(Sum)}&nbsp;₽
                    </div>
                    <div className={cn(grid.col_18, `ld_description`)}>
                        {Description}
                    </div>
                </div>
            );
        });
    }

    getOsnoTaxLinkedDocuments = ({ Postings = [] }) => {
        return Postings.map((posting) => {
            const {
                Direction, Sum, Type, Kind, NormalizedCostType
            } = posting;
            const type = osnoPostingTypes.transferTypePostings[Type] || ``;
            const kind = osnoPostingTypes.transferKindPostings[Kind] || ``;
            const costType = osnoPostingTypes.normalizedCostTypePostings[NormalizedCostType] || ``;

            return (
                <div className={cn(grid.row, style.row)}>
                    <div className={cn(grid.col_3, `ld_direction`)} >
                        {availableDirection(Direction)}
                    </div>
                    <div className={cn(style.sum, grid.col_3, `ld_sum`)}>
                        {Sum === null ? `` : toAmountString(Sum)}&nbsp;₽
                    </div>
                    <div className={cn(grid.col_5, `ld_type`)}>
                        {type}
                    </div>
                    <div className={cn(grid.col_5, `ld_kind`)}>
                        {kind}
                    </div>
                    <div className={cn(grid.col_11, `ld_normalizedCostType`)}>
                        {costType}
                    </div>
                    <div className={cn(grid.col_1)} />
                </div>
            );
        });
    }

    toggleLinkedDocumentsVisibility = () => {
        this.setState(({ isLinkedDocumentsVisible }) => {
            return {
                isLinkedDocumentsVisible: !isLinkedDocumentsVisible
            };
        });
    }

    renderSubconto = (subcontoList = []) => {
        if (!subcontoList || !subcontoList.length) {
            return null;
        }

        return subcontoList.map(({ Subconto }) => {
            return <P>{Subconto.Name}</P>;
        });
    };

    renderAccountingLinkedDocuments = ({ Postings = [] }) => {
        if (!Postings.length) {
            return null;
        }

        return Postings.map((posting) => {
            const {
                Debit,
                Credit,
                Sum,
                Description,
                SubcontoDebit,
                SubcontoCredit
            } = posting;

            return (
                <div className={cn(grid.row, style.row)}>
                    <div className={cn(grid.col_2, `ld_debit`)} >
                        <P>{Debit ? Debit.Number : ``}</P>
                    </div>
                    <div className={cn(grid.col_5, `ld_subconto_debit`)}>
                        {this.renderSubconto(SubcontoDebit)}
                    </div>
                    <div className={cn(grid.col_2, `ld_credit`)} >
                        <P>{Credit ? Credit.Number : ``}</P>
                    </div>
                    <div className={cn(grid.col_5, `ld_subconto_credit`)}>
                        {this.renderSubconto(SubcontoCredit)}
                    </div>
                    <div className={cn(style.sum, grid.col_3, `ld_sum`)}>
                        <P>{Sum === null ? `` : toAmountString(Sum)}&nbsp;₽</P>
                    </div>
                    <div className={cn(grid.col_6, style.description, `ld_description`)}>
                        {Description}
                    </div>
                </div>
            );
        });
    }

    renderTaxLinkedDocuments = ({ Postings = [] }) => {
        if (!Postings.length) {
            return null;
        }

        if (this.props.isOsno) {
            return this.getOsnoTaxLinkedDocuments({ Postings });
        }

        return this.getUsnTaxLinkedDocuments({ Postings });
    }

    render() {
        const { linkedDocuments = {}, isTax } = this.props;
        const { isLinkedDocumentsVisible } = this.state;

        return (
            <Fragment>
                <div className={grid.row}>
                    <Link
                        className={style.hint}
                        onClick={this.toggleLinkedDocumentsVisibility}
                    >
                        В связанных документах ({linkedDocuments.length})
                        <Arrow
                            direction={isLinkedDocumentsVisible ? `up` : `down`}
                            className={cn(style.LinkedDocumentsHeaderArrow)}
                        />
                    </Link>
                </div>
                <div className={cn(style.LinkedDocumentsContainer, { isVisible: isLinkedDocumentsVisible })}>
                    {linkedDocuments.map(posting => {
                        return <React.Fragment>
                            <H4 className={cn(style.linkedDocumentName)}>{posting.Name}</H4>
                            {isTax ? this.renderTaxLinkedDocuments(posting) : this.renderAccountingLinkedDocuments(posting)}
                        </React.Fragment>;
                    })}
                </div>
            </Fragment>
        );
    }
}

LinkedDocumentsList.propTypes = {
    linkedDocuments: PropTypes.oneOfType([
        PropTypes.object,
        PropTypes.array
    ]),
    isOsno: PropTypes.bool,
    isTax: PropTypes.bool
};

export default LinkedDocumentsList;
