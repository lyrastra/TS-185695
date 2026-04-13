import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import React from 'react';
import PropTypes from 'prop-types';
import TableRow from './TableRow';
import style from './style.m.less';

const Table = ({
    incomingDate, incomingCount, outgoingCount, outgoingDate, items
}) => {
    const getTableRows = () => {
        return items.map(item => {
            return <TableRow key={item.currencySign} {...item} />;
        });
    };

    const outgoingDateMoment = dateHelper(outgoingDate, `YYYY-MM-DD`);

    return (
        <table className={style.table}>
            <thead className={style.tableHead}>
                <tr>
                    <th>Остаток на {dateHelper(incomingDate).format(`DD.MM.YYYY`)}</th>
                    <th>Поступления ({incomingCount})</th>
                    <th>Списания ({outgoingCount})</th>
                    <th>Остаток на {outgoingDateMoment.isSame(dateHelper(), `day`) ? `сегодня` : outgoingDateMoment.format(`DD.MM.YYYY`)}</th>
                </tr>
            </thead>
            <tbody className={style.tableBody}>
                {getTableRows()}
            </tbody>
        </table>
    );
};

Table.propTypes = {
    incomingDate: PropTypes.string,
    outgoingDate: PropTypes.string,
    incomingCount: PropTypes.number,
    outgoingCount: PropTypes.number,
    items: PropTypes.arrayOf(PropTypes.shape(TableRow.propTypes))
};

export default Table;
