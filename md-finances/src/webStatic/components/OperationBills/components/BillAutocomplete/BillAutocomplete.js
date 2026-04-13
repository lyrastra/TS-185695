/* global _ */

import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import onClickOutside from 'react-onclickoutside';
import classnames from 'classnames/bind';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class BillAutocomplete extends React.Component {
    constructor(props) {
        super(props);

        this.bill = props.bill;
        this.input = React.createRef();

        this.getBillsDebounced = _.debounce(() => {
            this.bill.getBillsAutocomplete();
        }, 300);
    }

    componentDidUpdate() {
        const { current } = this.input;

        current && !current.value && current.focus();
    }

    onSelectNewBill = (e, newBill) => {
        e.preventDefault();

        this.bill.setBill(newBill);

        this.handleClickOutside();
    }

    onBlur = () => {
        const { bill } = this;

        !bill.state.autocompleteOpen && bill.checkBill();
    }

    onChange = (e) => {
        e.persist();
        const { value } = e.target;
        const { Name } = this.bill;

        if (Name.length && Name === value) {
            return;
        }

        if (!value.length) {
            this.bill.clearBill();
        } else {
            this.bill.Name = e.target.value;
        }

        this.bill.state.loading = true;
        this.getBillsDebounced();
    }

    handleClickOutside() {
        const { bill } = this;

        bill && bill.state.autocompleteOpen && bill.clearAutocomplete();
    }

    renderBillsList = () => {
        const { autocompleteList } = this.bill;

        if (!autocompleteList.length) {
            return null;
        }

        return (
            <div className={'mdSaleAutocomplete'} style={{ display: 'block' }}>
                <ul className={'mdSaleAutocomplete-items'}>
                    {autocompleteList.map((bill) => {
                        const name = `№${bill.Number} от ${bill.Date}`;

                        return <li key={bill.Id} className={cn('autocompleteRow')}><a href="" className={cn('billName')} onClick={(e) => this.onSelectNewBill(e, bill)}>{name}</a></li>;
                    })}
                </ul>
            </div>
        );
    }

    render() {
        const { bill } = this;
        const { className } = this.props;

        return (
            <div className={className}>
                <input
                    type="text"
                    value={bill.Name}
                    className={cn({ 'mdSaleAutocomplete-loading': bill.state.loading })}
                    style={{ width: '128px' }}
                    placeholder={'№'}
                    onClick={this.onChange}
                    onChange={this.onChange}
                    onBlur={this.onBlur}
                    ref={this.input}
                />
                {bill.state.autocompleteOpen && this.renderBillsList()}
            </div>
        );
    }
}

BillAutocomplete.defaultProps = {
    name: ''
};

BillAutocomplete.propTypes = {
    bill: PropTypes.any,
    index: PropTypes.number,
    className: PropTypes.string,
    onSelectNewBill: PropTypes.func
};

export default onClickOutside(BillAutocomplete);
