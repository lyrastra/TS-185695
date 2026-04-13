import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import currencyHelper from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import TableFooter from '@moedelo/frontend-core-react/components/table/TableFooter';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import EmptyBankTurnoversAndBalances from '../EmptyBankTurnoversAndBalances';
import style from './style.m.less';

const BankTurnoversAndBalances = props => {
    const [isProfOutsourceUser, setIsProfOutsourceUser] = useState(false);

    useEffect(() => {
        userDataService.get().then(({ IsProfOutsourceUser }) => {
            setIsProfOutsourceUser(IsProfOutsourceUser);
        });
    }, []);

    if (!isProfOutsourceUser) {
        return null;
    }

    if (!props.bankTurnoversAndBalances) {
        return <EmptyBankTurnoversAndBalances />;
    }

    const currencySign = currencyHelper.getSymbolByCode(props.currency) || `₽`;
    const {
        StartBalance, IncomingBalance, OutgoingBalance, EndBalance
    } = props.bankTurnoversAndBalances;

    return <TableFooter className={style.wrapper}>
        <div className={grid.col_6}>
            <div className={style.text}>
                Остаток в банке
            </div>
            <div>{`${toAmountString(StartBalance)} ${currencySign}`}</div>
        </div>
        <div className={grid.col_6}>
            <div className={style.sum}>{`${toAmountString(IncomingBalance)} ${currencySign}`}</div>
        </div>
        <div className={grid.col_6}>
            <div className={style.sum}>{`${toAmountString(OutgoingBalance)} ${currencySign}`}</div>
        </div>
        <div className={grid.col_6}>
            <div className={style.sum}>{`${toAmountString(EndBalance)} ${currencySign}`}</div>
        </div>
    </TableFooter>;
};

BankTurnoversAndBalances.defaultProps = {
    startBalance: 0,
    incomingBalance: 0,
    outgoingBalance: 0,
    endBalance: 0
};

BankTurnoversAndBalances.propTypes = {
    currency: PropTypes.number,
    bankTurnoversAndBalances: PropTypes.shape({
        StartBalance: PropTypes.number,
        IncomingBalance: PropTypes.number,
        OutgoingBalance: PropTypes.number,
        EndBalance: PropTypes.number
    })
};

export default BankTurnoversAndBalances;
