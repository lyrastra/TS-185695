import React from 'react';
import PropTypes from 'prop-types';
import cn from 'classnames';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Button, { Color as BtnColor } from '@moedelo/frontend-core-react/components/buttons/Button';
import { pluralNoun } from '@moedelo/frontend-core-v2/helpers/PluralHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import ResultRow from '../ResultRow';
import style from './style.m.less';

const ResultTable = ({
    data, onLoadMore, totalCount, isLoading, limit
}) => {
    const diffCount = totalCount - data.length;
    const moreCount = diffCount > limit ? limit : diffCount;
    const showMoreCountText = pluralNoun(moreCount, `операция`, `операции`, `операций`, true);

    const showMoreHandler = () => {
        onLoadMore({ limit: moreCount, offset: data.length });
    };

    return <React.Fragment>
        <section className={cn(grid.rowLarge, style.table, `qa-resultTable`)} role="list">
            {data.map(row => <ResultRow
                key={row.id}
                date={dateHelper(row.date).format(`DD.MM.YYYY`)}
                number={row.number}
                contractorName={row.contractorName}
                purpose={row.purpose}
                operationName={row.operationName}
                sum={row.sum}
                direction={row.direction}
                currencyCode={row.currencyCode}
            />)}
        </section>
        {moreCount > 0 && <div className={cn(grid.rowLarge, style.moreButton)}>
            <Button onClick={showMoreHandler} color={BtnColor.White} loading={isLoading}>Показать еще {showMoreCountText}</Button>
        </div>}
    </React.Fragment>;
};

ResultTable.propTypes = {
    data: PropTypes.arrayOf(PropTypes.shape(ResultRow.propTypes)).isRequired,
    onLoadMore: PropTypes.func.isRequired,
    totalCount: PropTypes.number.isRequired,
    isLoading: PropTypes.bool,
    limit: PropTypes.number
};

export default ResultTable;
