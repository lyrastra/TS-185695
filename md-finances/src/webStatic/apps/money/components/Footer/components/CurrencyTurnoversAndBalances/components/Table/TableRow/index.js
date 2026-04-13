import React from 'react';
import PropTypes from 'prop-types';
import Sum from './Sum';

const TableRow = ({
    incomingBalance, startBalance, outgoingBalance, endBalance, currencySign
}) => {
    return <tr>
        <td><Sum value={startBalance} currencySign={currencySign} /></td>
        <td><Sum value={incomingBalance} currencySign={currencySign} useColor usePrefixSign /></td>
        <td><Sum value={outgoingBalance * -1} currencySign={currencySign} useColor usePrefixSign /></td>
        <td><Sum value={endBalance} currencySign={currencySign} /></td>
    </tr>;
};

TableRow.propTypes = {
    incomingBalance: PropTypes.number,
    startBalance: PropTypes.number,
    outgoingBalance: PropTypes.number,
    endBalance: PropTypes.number,
    currencySign: PropTypes.node
};

export default TableRow;
