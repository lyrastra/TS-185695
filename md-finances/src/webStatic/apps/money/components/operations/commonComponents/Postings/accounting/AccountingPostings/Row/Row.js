import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { toFloat, toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import Autocomplete from '@moedelo/frontend-core-react/components/Autocomplete';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import Loader, { Size } from '@moedelo/frontend-core-react/components/Loader';
import { subcontoLevelForAccount } from '../../../../../../../../../services/newMoney/subcontoService';
import accountingPostingsValidator from '../../../../../validation/accountingPostingsValidator';
import SubcontoType from '../../../../../../../../../enums/newMoney/SubcontoTypeEnum';
import Subconto from '../Subconto';
import style from './style.m.less';

const cn = classnames.bind(style);

class Row extends React.Component {
    onChangeDebitNumber = async value => {
        if (typeof value === `string`) {
            return;
        }

        const { posting } = this.props;

        if (posting.Debit && posting.Debit.Code === value.value.Code) {
            this.props.onChange({ ...posting, Debit: value.value });

            return;
        }

        this.props.onChange({ ...posting, Debit: value.value, loadingDebitSubconto: true });
        const subcontoDebit = await subcontoLevelForAccount({
            settlementAccountId: this.props.settlementAccountId,
            syntheticAccountTypeId: value.value.TypeId
        });

        // eslint-disable-next-line no-param-reassign,no-return-assign
        subcontoDebit.forEach(item => item.key = getUniqueId());
        this.props.onChange(accountingPostingsValidator.getValidatedByDebit({
            ...posting,
            Debit: value.value,
            SubcontoDebit: subcontoDebit,
            loadingDebitSubconto: false
        }));
    };

    onBlurDebitNumber = value => {
        if (!value) {
            this.props.onChange(accountingPostingsValidator.getValidatedByDebit({ ...this.props.posting, Debit: null, SubcontoDebit: null }));

            return;
        }

        this.props.onChange(accountingPostingsValidator.getValidatedByDebit(this.props.posting));
    };

    onChangeCreditNumber = async value => {
        if (typeof value === `string`) {
            return;
        }

        const { posting } = this.props;

        if (posting.Credit && posting.Credit.Code === value.value.Code) {
            this.props.onChange({ ...posting, Credit: value.value });

            return;
        }

        this.props.onChange({ ...posting, Credit: value.value, loadingCreditSubconto: true });
        const subcontoCredit = await subcontoLevelForAccount({
            settlementAccountId: this.props.settlementAccountId,
            syntheticAccountTypeId: value.value.TypeId
        });

        // eslint-disable-next-line no-param-reassign,no-return-assign
        subcontoCredit.forEach(item => item.key = getUniqueId());
        this.props.onChange(accountingPostingsValidator.getValidatedByCredit({
            ...posting,
            Credit: value.value,
            SubcontoCredit: subcontoCredit,
            loadingCreditSubconto: false
        }));
    };

    onBlurCreditNumber = value => {
        if (!value) {
            this.props.onChange(accountingPostingsValidator.getValidatedByCredit({ ...this.props.posting, Credit: null, SubcontoCredit: null }));

            return;
        }

        this.props.onChange(accountingPostingsValidator.getValidatedByCredit(this.props.posting));
    };

    onChangeDescription = ({ value }) => {
        this.props.onChange({ ...this.props.posting, Description: value });
    };

    onChangeSum = ({ value }) => {
        const sum = toFloat(value);
        this.props.onChange(accountingPostingsValidator.getValidatedBySum({ ...this.props.posting, Sum: sum !== false ? sum : null }));
    };

    onChangeSubcontoDebit = subconto => {
        const subcontoDebit = this.props.posting.SubcontoDebit;
        const index = subcontoDebit.findIndex(item => item.key === subconto.key);
        subcontoDebit[index] = subconto;

        this.props.onChange(accountingPostingsValidator.getValidatedByDebitSubconto({ ...this.props.posting, SubcontoDebit: subcontoDebit }));
    };

    onChangeSubcontoCredit = subconto => {
        const subcontoCredit = this.props.posting.SubcontoCredit;
        const index = subcontoCredit.findIndex(item => item.key === subconto.key);
        subcontoCredit[index] = subconto;
        this.props.onChange(accountingPostingsValidator.getValidatedByCreditSubconto({ ...this.props.posting, SubcontoCredit: subcontoCredit }));
    };

    renderDebitSubconto = () => {
        const { posting, dateDocument, messageNoAccountingObjects } = this.props;
        const {
            SubcontoDebit, SubcontoDebitError, Debit, loadingDebitSubconto
        } = posting;

        if (!Debit) {
            return null;
        }

        if (loadingDebitSubconto) {
            return <Loader className={style.loader} size={Size.small} />;
        }

        if (!SubcontoDebit || !SubcontoDebit.length) {
            return messageNoAccountingObjects;
        }

        return SubcontoDebit.map((subconto, i) => <Subconto
            key={subconto.key}
            subconto={subconto}
            accountCode={Debit.Code}
            dateDocument={dateDocument}
            kontragent={getKontragent(SubcontoDebit)}
            onChange={this.onChangeSubcontoDebit}
            error={!!SubcontoDebitError}
            message={(i === SubcontoDebit.length - 1) ? SubcontoDebitError : ``}
        />);
    };

    renderCreditSubconto = () => {
        const { posting, dateDocument, messageNoAccountingObjects } = this.props;
        const {
            SubcontoCredit, SubcontoCreditError, Credit, loadingCreditSubconto
        } = posting;

        if (!Credit) {
            return null;
        }

        if (loadingCreditSubconto) {
            return <Loader className={style.loader} width={30} />;
        }

        if (!SubcontoCredit || !SubcontoCredit.length) {
            return messageNoAccountingObjects;
        }

        return SubcontoCredit.map((subconto, i) => <Subconto
            key={subconto.key}
            subconto={subconto}
            accountCode={Credit.Code}
            dateDocument={dateDocument}
            kontragent={getKontragent(SubcontoCredit)}
            onChange={this.onChangeSubcontoCredit}
            error={!!SubcontoCreditError}
            message={(i === SubcontoCredit.length - 1) ? SubcontoCreditError : ``}
        />);
    };

    render() {
        const {
            posting,
            getDebits,
            getCredits
        } = this.props;
        const {
            Debit,
            DebitError,
            Credit,
            CreditError,
            Sum,
            SumError,
            Description,
            ReadOnly
        } = posting;

        return <div className={cn(style.row, grid.row, `qa-AccPostingRow`)}>
            <Autocomplete
                className={cn(grid.col_2, `qa-debitNumber`)}
                onChange={this.onChangeDebitNumber}
                onBlur={this.onBlurDebitNumber}
                getData={getDebits}
                value={Debit ? Debit.Number : ``}
                showAsText={Debit && Debit.ReadOnly}
                iconName={`none`}
                maxWidth={620}
                notOnlyFromList
                error={!!DebitError}
                message={DebitError}
            />
            <div className={cn(grid.col_5, `qa-debitSubconto`)}>
                {this.renderDebitSubconto()}
            </div>
            <Autocomplete
                className={cn(grid.col_2, `qa-creditNumber`)}
                onChange={this.onChangeCreditNumber}
                onBlur={this.onBlurCreditNumber}
                getData={getCredits}
                value={Credit ? Credit.Number : ``}
                showAsText={Credit && Credit.ReadOnly}
                iconName={`none`}
                maxWidth={620}
                notOnlyFromList
                error={!!CreditError}
                message={CreditError}
            />
            <div className={cn(grid.col_5, `qa-creditSubconto`)}>
                {this.renderCreditSubconto()}
            </div>
            <div className={cn(style.sum, grid.col_3)}>
                <Input
                    className={cn(style.ieFix, `qa-sumAccPosting`)}
                    value={Sum === null ? `` : toAmountString(Sum)}
                    onBlur={this.onChangeSum}
                    textAlign="right"
                    type={InputType.number}
                    error={!!SumError}
                    message={SumError}
                    showAsText={ReadOnly}
                    allowDecimal
                    decimalLimit={2}
                />&nbsp;₽
            </div>
            <div className={grid.col_6}>
                <Input
                    className={`qa-commentAccPosting`}
                    value={Description}
                    showAsText={ReadOnly}
                    onBlur={this.onChangeDescription}
                />
            </div>
        </div>;
    }
}

function getKontragent(subcontos) {
    const kontragentSubconto = subcontos.find(item => item.Type === SubcontoType.Kontragent);

    if (kontragentSubconto && kontragentSubconto.Subconto) {
        return {
            SubcontoId: kontragentSubconto.Subconto.SubcontoId || kontragentSubconto.Subconto.Id,
            Name: kontragentSubconto.Subconto.Name,
            Id: kontragentSubconto.Subconto.Id
        };
    }

    return {};
}

Row.propTypes = {
    posting: PropTypes.object,
    onChange: PropTypes.func,
    getDebits: PropTypes.func,
    getCredits: PropTypes.func,
    settlementAccountId: PropTypes.number,
    dateDocument: PropTypes.string,
    messageNoAccountingObjects: PropTypes.string
};

export default Row;
