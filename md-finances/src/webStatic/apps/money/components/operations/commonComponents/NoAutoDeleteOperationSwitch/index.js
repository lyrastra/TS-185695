import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import { observer, inject } from 'mobx-react';
import cn from 'classnames';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Switch from '@moedelo/frontend-core-react/components/Switch';
import style from './style.m.less';

function NoAutoDeleteOperationSwitch(props) {
    return (
        <Fragment>
            <div className={grid.col_1} />
            <div className={cn(grid.col_5, style.wrapper)}>
                <Switch
                    text={`Не удалять автоматически`}
                    onChange={props.operationStore.setNoAutoDeleteOperation}
                    checked={props.operationStore.model.NoAutoDeleteOperation}
                />
            </div>
        </Fragment>
    );
}

NoAutoDeleteOperationSwitch.propTypes = {
    operationStore: PropTypes.shape({
        model: PropTypes.object.isRequired,
        setNoAutoDeleteOperation: PropTypes.func.isRequired
    })
};

export default inject(`operationStore`)(observer(NoAutoDeleteOperationSwitch));
