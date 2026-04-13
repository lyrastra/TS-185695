import React from 'react';
import PropTypes from 'prop-types';
import cn from 'classnames';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import NotificationPanel from '@moedelo/frontend-core-react/components/NotificationPanel';
import H3 from '@moedelo/frontend-core-react/components/headers/H3';
import { pluralNoun } from '@moedelo/frontend-core-v2/helpers/PluralHelper';

import ResultRow from './components/ResultRow';
import ResultTable from './components/ResultTable';
import ApplyRuleSetting from './components/ApplyRuleSetting';

const CheckRuleResult = ({
    totalCount, data,
    onLoadMore, isTableLoading,
    limit,
    isShowWarning, onHideWarning
}) => {
    return <section className={grid.wrapper}>
        { isShowWarning &&
        <NotificationPanel
            type="warning"
            className={cn(grid.rowLarge)}
            onClose={onHideWarning}
        >
            Внимательно проверьте все операции, попадающие под создаваемое правило.<br />
            Если попали лишние операции - скорректируйте правило.
        </NotificationPanel> }
        <H3>{pluralNoun(totalCount, `операция, попадающая`, `операции, попадающие`, `операций, попадающих`, true)} под правило</H3>
        <ApplyRuleSetting />
        <ResultTable
            totalCount={totalCount}
            onLoadMore={onLoadMore}
            data={data}
            isLoading={isTableLoading}
            limit={limit}
        />
    </section>;
};

CheckRuleResult.propTypes = {
    totalCount: PropTypes.number.isRequired,
    data: PropTypes.arrayOf(PropTypes.shape(ResultRow.propTypes)).isRequired,
    onLoadMore: PropTypes.func.isRequired,
    isTableLoading: PropTypes.bool,
    limit: PropTypes.number,
    isShowWarning: PropTypes.bool,
    onHideWarning: PropTypes.func
};

export default CheckRuleResult;
