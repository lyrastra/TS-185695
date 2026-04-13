import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer } from 'mobx-react';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import OperationBillsRow from './components/OperationBillsRow';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class OperationBills extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.store;
    }

    addEmptyBill = e => {
        e.preventDefault();

        this.store.addEmptyBill();
    }

    renderBillsList = () => {
        return (
            <div>
                {this.store.bills.map(bill => {
                    return (
                        <OperationBillsRow
                            key={getUniqueId(`operationBill_${bill.Id}`)}
                            bill={bill}
                        />
                    );
                })}
            </div>
        );
    }

    render() {
        const { renderBillsList, addEmptyBill, store } = this;

        return (
            <div>
                <div className={cn(`billHeader`)}>По счету</div>
                <div className={cn(`billHeader`)}>Сумма</div>
                {renderBillsList()}
                {store.state.commonError.visible && <div className={`field-validation-error`} style={{ width: `258px` }} >{store.state.commonError.message}</div>}
                <div>
                    <a href="" onClick={addEmptyBill} className={cn(`addLink`)}>+ счет</a>
                </div>
            </div>
        );
    }
}

OperationBills.propTypes = {
    store: PropTypes.object
};

export default OperationBills;

