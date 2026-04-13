import React from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import converter from '@moedelo/frontend-core-v2/helpers/converter';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import Input from '@moedelo/frontend-core-react/components/Input/Input';
import createNumberMask from 'text-mask-addons/dist/createNumberMask';
import style from './style.m.less';
import sumConditionEnum from '../../../../../../enums/newMoney/SumConditionEnum';
import sumConditionResource from './SumConditionResources';

const cn = classnames.bind(style);
const maskForSumCondition = createNumberMask({
    prefix: ``,
    thousandsSeparatorSymbol: ` `,
    allowDecimal: true,
    decimalSymbol: `,`,
    integerLimit: 9
});
const dotKeyCodes = [190, 191];

@observer
class SumSection extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.store;
    }

    onChangeType = ({ value }) => {
        this.store.sumCondition = value;
    };

    onChangeSum = ({ value }) => {
        this.store.sum = converter.toFloat(value) || null;
    };

    onKeyDown = e => {
        if (dotKeyCodes.some(keyCode => keyCode === e.keyCode)) {
            e.target.value += `,`;
        }
    }

    onChangeSumFrom = ({ value }) => {
        this.store.sumFrom = converter.toFloat(value) || null;
    };

    onChangeSumTo = ({ value }) => {
        this.store.sumTo = converter.toFloat(value) || null;
    };

    getSumTemplate = () => {
        let { sum } = this.store;
        sum = sum ? sum.toString().replace(`.`, `,`) : null;

        switch (this.store.sumCondition) {
            case sumConditionEnum.Great:
            case sumConditionEnum.Less:
            case sumConditionEnum.Equal:
                return <Input
                    mask={maskForSumCondition}
                    placeholder={`Сумма`}
                    onChange={this.onChangeSum}
                    value={sum}
                    onKeyDown={this.onKeyDown}
                />;
            case sumConditionEnum.Range:
                return this.getSumRangeTemplate();
            default:
                return null;
        }
    };

    getSumRangeTemplate = () => {
        let { sumFrom, sumTo } = this.store;
        sumFrom = sumFrom ? sumFrom.toString().replace(`.`, `,`) : null;
        sumTo = sumTo ? sumTo.toString().replace(`.`, `,`) : null;

        return <div className={cn(`rangeSum`)}>
            <Input
                mask={maskForSumCondition}
                placeholder={`Сумма`}
                className={cn(`rangeSum__input`)}
                onKeyDown={this.onKeyDown}
                onChange={this.onChangeSumFrom}
                value={sumFrom}
            />
            <span>–</span>
            <Input
                mask={maskForSumCondition}
                placeholder={`Сумма`}
                className={cn(`rangeSum__input`)}
                onKeyDown={this.onKeyDown}
                onChange={this.onChangeSumTo}
                value={sumTo}
            />
        </div>;
    };

    render() {
        return (
            <div className={cn(`sumCondition`)}>
                {/* eslint-disable-next-line jsx-a11y/label-has-for */}
                <label htmlFor="sumCondition">
                    Сумма
                    <Dropdown
                        data={sumConditionResource}
                        type={`link`}
                        value={this.store.sumCondition}
                        onSelect={this.onChangeType}
                        className={cn(`dropdown`)}
                    />
                </label>
                { this.getSumTemplate() }
            </div>
        );
    }
}

SumSection.propTypes = {
    store: PropTypes.object
};

export default SumSection;
