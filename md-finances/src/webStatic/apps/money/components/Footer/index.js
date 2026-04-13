import React from 'react';
import PropTypes from 'prop-types';
import Sticky from 'react-sticky-el';
import cn from 'classnames';
import TurnoversAndBalances from './components/TurnoversAndBalances';
import CurrencyTurnoversAndBalances from './components/CurrencyTurnoversAndBalances';
import BankTurnoversAndBalances from './components/BankTurnoversAndBalances';
import style from './style.m.less';

const Footer = props => {
    if (props.operationsGroupedByCurrency.length) {
        return <Sticky mode="bottom" stickyClassName={cn(style.fixed)} positionRecheckInterval={30} style={{ zIndex: 2 }}>
            <CurrencyTurnoversAndBalances
                incomingDate={props.incomingDate}
                outgoingDate={props.outgoingDate}
                incomingCount={props.incomingCount}
                outgoingCount={props.outgoingCount}
                items={props.operationsGroupedByCurrency}
            />
            <BankTurnoversAndBalances />
        </Sticky>;
    }

    return <Sticky mode="bottom" stickyClassName={cn(style.fixed)} positionRecheckInterval={30} style={{ zIndex: 2 }}>
        <TurnoversAndBalances
            currency={props.currency}
            endBalance={props.endBalance}
            incomingBalance={props.incomingBalance}
            incomingCount={props.incomingCount}
            incomingDate={props.incomingDate}
            outgoingBalance={props.outgoingBalance}
            outgoingCount={props.outgoingCount}
            outgoingDate={props.outgoingDate}
            startBalance={props.startBalance}
        />
        {props.canShowBankTurnoversAndBalances && <BankTurnoversAndBalances
            currency={props.currency}
            bankTurnoversAndBalances={props.bankTurnoversAndBalances}
        />}
    </Sticky>;
};

Footer.propTypes = {
    operationsGroupedByCurrency: PropTypes.oneOfType(PropTypes.arrayOf(PropTypes.object), PropTypes.object),
    outgoingDate: PropTypes.string,
    currency: PropTypes.number,
    incomingDate: PropTypes.string,
    startBalance: PropTypes.number,
    incomingBalance: PropTypes.number,
    incomingCount: PropTypes.number,
    outgoingBalance: PropTypes.number,
    endBalance: PropTypes.number,
    outgoingCount: PropTypes.number,
    bankTurnoversAndBalances: PropTypes.shape({
        StartBalance: PropTypes.number,
        IncomingBalance: PropTypes.number,
        OutgoingBalance: PropTypes.number,
        EndBalance: PropTypes.number
    }),
    canShowBankTurnoversAndBalances: PropTypes.bool
};

export default Footer;
