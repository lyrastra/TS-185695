import React from 'react';
import renderHTML from 'react-render-html';
import classnames from 'classnames/bind';
import { observer } from 'mobx-react';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import PropTypes from 'prop-types';
import icon from './icon.html';
import style from './style.m.less';

const cn = classnames.bind(style);

const EmptyTable = observer((props) => {
    const isArchiveSource = props.moneySourceStore.isArchiveSource();

    return (
        <div className={cn(`component`)}>
            <div className={cn(`center`)}>
                <div className={cn(`icon`)}>{renderHTML(icon)}</div>
                <h3>Создайте первую операцию</h3>
                <Button color={`white`} disabled={isArchiveSource} className={cn(`rightMargin`)} onClick={props.onAddIncoming}>Поступление</Button>
                <Button color={`white`} disabled={isArchiveSource} onClick={props.onAddOutgoing}>Списание</Button>
            </div>
        </div>
    );
});

EmptyTable.propTypes = {
    onAddIncoming: PropTypes.func,
    onAddOutgoing: PropTypes.func
};

export default EmptyTable;
