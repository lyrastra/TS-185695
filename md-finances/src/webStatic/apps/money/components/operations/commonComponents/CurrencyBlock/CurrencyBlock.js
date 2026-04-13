import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Link from '@moedelo/frontend-core-react/components/Link';
import LinkSize from '@moedelo/frontend-core-react/components/Link/enums/Size';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import Input, { Type as InputType } from '@moedelo/frontend-core-react/components/Input';
import Sum from '../Sum';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class CurrencyBlock extends React.Component {
    renderCalculatedFields = () => {
        if (!this.props.operationStore.showCalculatedFields) {
            return null;
        }

        const {
            totalSumCurrencySymbol,
            model: {
                TotalSum,
                ExchangeRateDiff
            }
        } = this.props.operationStore;

        return <React.Fragment>
            <span className={cn(grid.col_1, style.separator)}>=</span>
            <div className={grid.col_3}>
                <Input
                    type={InputType.number}
                    label={`Итого`}
                    value={toAmountString(TotalSum)}
                    decimalLimit={2}
                    allowDecimal
                    unit={totalSumCurrencySymbol || `₽`}
                    showAsText
                />
            </div>
            {!!ExchangeRateDiff && <div className={grid.col_3}>
                <Input
                    type={InputType.number}
                    label={`Курсовая разница`}
                    value={toAmountString(ExchangeRateDiff)}
                    decimalLimit={2}
                    allowDecimal
                    allowNegative
                    unit={`₽`}
                    showAsText
                />
            </div>}
        </React.Fragment>;
    };

    renderCentralBankRate = () => {
        const {
            setRateByCentralBank,
            model: {
                CentralBankRate
            }
        } = this.props.operationStore;

        if (!CentralBankRate) {
            return null;
        }

        return <span>
            ЦБ: <Link
                size={LinkSize.small}
                onClick={setRateByCentralBank}
                text={`${toAmountString(CentralBankRate, 4, true)} ₽`}
            />
        </span>;
    };

    render() {
        const {
            setExchangeRate,
            validationState,
            separatorSymbol,
            model: { ExchangeRate }
        } = this.props.operationStore;

        return <div className={grid.row}>
            <div className={grid.col_3}>
                <Sum label={`Сумма`} />
            </div>
            <span className={cn(grid.col_1, style.separator)}>{separatorSymbol}</span>
            <div className={grid.col_3}>
                <Input
                    type={InputType.number}
                    label={`Курс банка`}
                    value={toAmountString(ExchangeRate, 4) || ``}
                    onBlur={setExchangeRate}
                    decimalLimit={4}
                    allowDecimal
                    error={!!validationState.ExchangeRate}
                    message={validationState.ExchangeRate}
                    unit={`₽`}
                    hint={this.renderCentralBankRate()}
                />
            </div>
            {this.renderCalculatedFields()}
        </div>;
    }
}

CurrencyBlock.propTypes = {
    operationStore: PropTypes.shape({
        validationState: {
            Sum: PropTypes.string,
            ExchangeRate: PropTypes.string
        },
        model: PropTypes.shape({
            CentralBankRate: PropTypes.number,
            Sum: PropTypes.number,
            TotalSum: PropTypes.number,
            ExchangeRate: PropTypes.number,
            ExchangeRateDiff: PropTypes.number
        }),
        showCalculatedFields: PropTypes.boolean,
        setRateByCentralBank: PropTypes.function,
        setSum: PropTypes.function,
        setExchangeRate: PropTypes.function,
        sumCurrencySymbol: PropTypes.string,
        separatorSymbol: PropTypes.string,
        totalSumCurrencySymbol: PropTypes.string
    })
};

export default CurrencyBlock;
