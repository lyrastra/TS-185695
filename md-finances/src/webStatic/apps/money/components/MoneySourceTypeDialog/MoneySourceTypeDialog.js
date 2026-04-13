import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import ComplexList from '@moedelo/frontend-core-react/components/list/ComplexList';
import { getSymbolByCode } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import MoneySourceType from '../../../../enums/MoneySourceType';
import MoneySourceIcon from './../MoneySourceIcon';
import moneySourceHelper from '../../../../helpers/newMoney/moneySourceHelper';
import style from './style.m.less';

const cn = classnames.bind(style);

class MoneySourceTypeDialog extends React.Component {
    constructor(props) {
        super(props);

        this.state = { loading: true };
    }

    componentDidMount() {
        const list = this.props.sourceList.filter(item => item.Type !== MoneySourceType.All);

        this.setState({ loading: false, value: list });
    }

    renderItem = item => {
        const description = `${item.IsTransit ? `Транзитный` : ``} ${item.Description || ``}`;

        return <div
            className={cn(`sourceStyle`)}
            onClick={() => {
                this.props.onSelect(item);
            }}
            role={`presentation`}
        >
            <div className={cn(`item`)}>
                <MoneySourceIcon className={cn(`icon`)} value={item.Type} />
                <div className={cn(`name`)}>
                    <div className={cn(`title`)}>{moneySourceHelper.name(item)}</div>
                    <div className={cn(`description`)}>{description}</div>
                </div>
                <div className={cn(`sum`)}>
                    {toAmountString(item.Balance).replace(`-`, `– `)}
                    &nbsp;<span className={cn(`currency`)}>{getSymbolByCode(item.Currency)}</span>
                </div>
                {svgIconHelper.getJsx({ name: `dropdownArrow`, className: cn(`arrow`) })}
            </div>
        </div>;
    };

    renderList = () => {
        let list = this.state.value.filter(item => item.IsActive !== false);

        const groupedList = list.reduce((acc, val) => {
            const key = val.Type;

            if (!acc[key]) {
                acc[key] = [];
            }
    
            acc[key].push(val);
            
            return acc;
        }, {});

        list = Object.values(groupedList).reduce((a, b) => [...a, ...b], []).map(item => this.renderItem(item));

        return <div className={cn(`list`)}>
            <ComplexList
                listHeight={365}
                data={list}
                isSearch={false}
            />
        </div>;
    };

    render() {
        if (this.state.loading) {
            return null;
        }

        return <Modal
            header={`Выберите счет для добавления операции`}
            onClose={this.props.onClose}
            visible
            width={`390px`}
        >
            {this.renderList()}
        </Modal>;
    }
}

MoneySourceTypeDialog.propTypes = {
    onClose: PropTypes.func,
    onSelect: PropTypes.func,
    sourceList: PropTypes.arrayOf(PropTypes.object)
};

export default MoneySourceTypeDialog;
