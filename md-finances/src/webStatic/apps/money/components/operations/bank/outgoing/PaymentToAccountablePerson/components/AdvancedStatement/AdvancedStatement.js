import React from 'react';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import Link from '@moedelo/frontend-core-react/components/Link';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';

import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import style from './style.m.less';

const cn = classnames.bind(style);

const AdvancedStatement = observer(({ store }) => {
    const advanceStatements = store.model.AdvanceStatements || [];

    if (!advanceStatements.length) {
        return null;
    }

    return (<div className={cn(grid.row)}>
        <div className={grid.col_24}>
            <label className={cn(style.advanceStatementLabel)}>На основании</label>
            { advanceStatements.map(advanceStatement => {
                const href = `/AccDocuments/AdvanceStatement/Edit/${advanceStatement.DocumentBaseId}`;

                return <div className={cn(style.advanceStatementLink)}>
                    <Link href={href}>{advanceStatement.Name}, {toAmountString(advanceStatement.LinkSum)} р.</Link>
                </div>;
            })}
        </div>
    </div>);
});

AdvancedStatement.propTypes = {
    store: PropTypes.object.isRequired
};

export default AdvancedStatement;
