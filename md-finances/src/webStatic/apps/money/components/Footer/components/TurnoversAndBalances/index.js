import React from 'react';
import TableFooter from '@moedelo/frontend-core-react/components/table/TableFooter';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import currencyHelper from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import style from './style.m.less';

const cn = classnames.bind(style);

const TurnoversAndBalances = props => {
    const outgoingDate = dateHelper(props.outgoingDate);
    const currencySign = currencyHelper.getSymbolByCode(props.currency) || `₽`;

    return <TableFooter className={cn(`tableFooter`)}>
        <div className={cn(`footer__content`)}>
            <div className={cn(`block`, grid.col_6)}>
                <div className={cn(`title`)}>Остаток на {dateHelper(props.incomingDate).format(`DD.MM.YYYY`)}</div>
                {toAmountString(props.startBalance)} <span className={cn(`gray`)}>{currencySign}</span>
            </div>
            <div className={cn(`block`, grid.col_6)}>
                <div className={cn(`title`)}>Поступления ({props.incomingCount}):</div>
                {props.incomingBalance > 0 &&
                <span className={cn(`incoming`)}>+ {toAmountString(props.incomingBalance)} {currencySign}</span>
                }
                {props.incomingBalance === 0 &&
                <span>{toAmountString(props.incomingBalance)} {currencySign}</span>
                }
            </div>
            <div className={cn(`block`, grid.col_6)}>
                <div className={cn(`title`)}>Списания ({props.outgoingCount}):</div>
                {props.outgoingBalance > 0 &&
                <span className={cn(`outgoing`)}>– {toAmountString(props.outgoingBalance)} {currencySign}</span>
                }
                {props.outgoingBalance === 0 &&
                <span>{toAmountString(props.outgoingBalance)} {currencySign}</span>
                }
            </div>
            <div className={cn(`block`, grid.col_6)}>
                <div className={cn(`title`)}>Остаток
                    на {outgoingDate.isSame(dateHelper(), `day`) ? `сегодня` : outgoingDate.format(`DD.MM.YYYY`)}</div>
                {toAmountString(props.endBalance).replace(`-`, `– `)} <span className={cn(`gray`)}>{currencySign}</span>
            </div>
        </div>
    </TableFooter>;
};

TurnoversAndBalances.propTypes = {
    outgoingDate: PropTypes.string,
    currency: PropTypes.number,
    incomingDate: PropTypes.string,
    startBalance: PropTypes.number,
    incomingBalance: PropTypes.number,
    incomingCount: PropTypes.number,
    outgoingBalance: PropTypes.number,
    endBalance: PropTypes.number,
    outgoingCount: PropTypes.number
};

export default TurnoversAndBalances;
