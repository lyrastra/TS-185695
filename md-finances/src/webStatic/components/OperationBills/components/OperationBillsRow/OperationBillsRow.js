import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer } from 'mobx-react';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import BillAutocomplete from '../BillAutocomplete';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class OperationBillsRow extends React.Component {
    constructor(props) {
        super(props);

        this.bill = props.bill;
        this.store = props.bill.store;
    }

    onBlurSum = e => {
        const input = e.target;
        const { value } = input;

        this.bill.onBlurSumField(value);
        input.value = toAmountString(value) || ``;
    }

    onChangeSum = e => {
        this.bill.onChangeSumField(e.target.value);
    }

    removeBIll = () => {
        this.bill.removeBill();
    }

    render() {
        const { bill } = this;
        const { error, errorMsg } = bill.state;

        return (
            <div className={cn(`billRow`)}>
                <BillAutocomplete
                    bill={bill}
                    className={cn(`billCell`)}
                />
                <div className={cn(`billCell`)} key={bill.getSum}>
                    <input
                        type="text"
                        defaultValue={bill.getSum}
                        style={{ width: `100px` }}
                        onChange={this.onChangeSum}
                        onBlur={this.onBlurSum}
                    />
                </div>
                <div className={cn(`billCell`)}>
                    <i className={cn(`md-icon--x`, `removeBillRow`)} role={`presentation`} onClick={this.removeBIll} />
                </div>
                {error &&
                <div className={`field-validation-error`} style={{ width: `258px` }} >{errorMsg}</div>}
            </div>
        );
    }
}

OperationBillsRow.defaultProps = {
    showSum: false,
    showRemoveButton: false
};

OperationBillsRow.propTypes = {
    bill: PropTypes.object
};

export default OperationBillsRow;
