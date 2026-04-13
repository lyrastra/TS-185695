import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import { Size } from '@moedelo/frontend-core-react/components/buttons/enums';
import style from './style.m.less';

const SaveReserve = props => {
    const {
        canSaveReserveSum,
        submitChangesOfReserve
    } = props.operationStore;

    return canSaveReserveSum && <Button size={Size.Small} className={style.save} onClick={submitChangesOfReserve}>обновить резерв</Button>;
};

SaveReserve.propTypes = {
    operationStore: PropTypes.shape({
        canSaveReserveSum: PropTypes.bool,
        submitChangesOfReserve: PropTypes.func
    })
};

export default observer(SaveReserve);
