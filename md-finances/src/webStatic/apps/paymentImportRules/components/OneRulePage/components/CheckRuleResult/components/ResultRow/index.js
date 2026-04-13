import React from 'react';
import PropTypes from 'prop-types';
import cn from 'classnames';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import { getSymbolByCode } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import style from './style.m.less';

const ResultRow = ({
    date,
    number,
    contractorName,
    purpose,
    operationName,
    sum,
    direction,
    currencyCode
}) => {
    return <div role="listitem" className={cn(grid.row, `qa-resultTableRow`)}>
        <div className={grid.col_4}>
            <div className={cn(`qa-resultTableRowDate`, style.text)}>{date}</div>
            <div className={cn(`qa-resultTableRowNumber`, style.smallGrey)}>№{number}</div>
        </div>
        <div className={grid.col_8}>
            <div className={cn(`qa-resultTableRowContractorName`, style.text)}>{contractorName}</div>
            <div className={cn(`qa-resultTableRowOperationName`, style.smallGrey)}>{operationName}</div>
        </div>
        <div className={grid.col_4}>
            <div className={cn(`qa-resultTableRowSum`, style.text, style.sum, { [style.negative]: direction === Direction.Outgoing, [style.positive]: direction === Direction.Incoming })}>
                {direction === Direction.Incoming && `+ `}{direction === Direction.Outgoing && `– `}{toFinanceString(sum)}&thinsp;{getSymbolByCode(currencyCode)}
            </div>
        </div>
        <div className={grid.col_1} />
        <div className={grid.col_7}>
            <div className={cn(`qa-resultTableRowPurpose`, style.smallGrey)}>{purpose}</div>
        </div>
    </div>;
};

ResultRow.propTypes = {
    date: PropTypes.string, // isoDate
    number: PropTypes.string,
    contractorName: PropTypes.string,
    purpose: PropTypes.string,
    operationName: PropTypes.string,
    sum: PropTypes.number,
    direction: PropTypes.oneOf(Object.values(Direction)),
    currencyCode: PropTypes.number
};

export default ResultRow;
