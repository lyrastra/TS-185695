import React from 'react';
import PropTypes from 'prop-types';
import bank from '@moedelo/frontend-core-react/icons/bank.m.svg';
import cash from '@moedelo/frontend-core-react/icons/cash.m.svg';
import purse from '@moedelo/frontend-core-react/icons/purse.m.svg';
import wallet from '@moedelo/frontend-core-react/icons/wallet.m.svg';
import { getJsx } from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import MoneySourceType from '../../../../enums/MoneySourceType';

class MoneySourceIcon extends React.Component {
    render() {
        switch (this.props.value) {
            case MoneySourceType.All:
                return getJsx({ file: wallet, className: this.props.className });
            case MoneySourceType.SettlementAccount:
                return getJsx({ file: bank, className: this.props.className });
            case MoneySourceType.Cash:
                return getJsx({ file: cash, className: this.props.className });
            case MoneySourceType.Purse:
                return getJsx({ file: purse, className: this.props.className });
        }

        return null;
    }
}

MoneySourceIcon.propTypes = {
    value: PropTypes.number,
    className: PropTypes.string
};

export default MoneySourceIcon;
