import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Input, { Type as InputType } from '@moedelo/frontend-core-react/components/Input';
import Sum from '../Sum';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class CurrencySumWithCourse extends React.Component {
    renderCalculatedFields = () => {
        if (!this.props.operationStore.showCalculatedFields) {
            return null;
        }

        const {
            totalSumCurrencySymbol,
            model: {
                TotalSum
            }
        } = this.props.operationStore;

        return <React.Fragment>
            <span className={cn(grid.col_1, style.separator)}>=</span>
            <div className={grid.col_6}>
                <Input
                    type={InputType.number}
                    label={`Итого`}
                    value={toAmountString(toFloat(TotalSum))}
                    decimalLimit={2}
                    allowDecimal
                    unit={totalSumCurrencySymbol || `₽`}
                    showAsText
                />
            </div>
        </React.Fragment>;
    };

    render() {
        const {
            totalSumCurrencySymbol,
            model: { CentralBankRate }
        } = this.props.operationStore;

        return <div className={cn(grid.col_12, style.wrap)}>
            <div className={grid.col_6}>
                <Sum label={`Сумма`} />
            </div>
            <span className={cn(grid.col_1, style.separator)}>×</span>
            <div className={grid.col_4}>
                <Input
                    type={InputType.number}
                    label={`Курс ЦБ`}
                    value={toAmountString(CentralBankRate, 4) || ``}
                    decimalLimit={4}
                    allowDecimal
                    unit={totalSumCurrencySymbol || `₽`}
                    showAsText
                />
            </div>
            {this.renderCalculatedFields()}
        </div>;
    }
}

CurrencySumWithCourse.propTypes = {
    operationStore: PropTypes.shape({
        validationState: {
            Sum: PropTypes.string
        },
        model: PropTypes.shape({
            CentralBankRate: PropTypes.number,
            Sum: PropTypes.number,
            TotalSum: PropTypes.number
        }),
        showCalculatedFields: PropTypes.boolean,
        sumCurrencySymbol: PropTypes.string,
        totalSumCurrencySymbol: PropTypes.string
    })
};

export default CurrencySumWithCourse;
