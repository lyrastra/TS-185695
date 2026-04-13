import React from 'react';
import PropTypes from 'prop-types';
import ActionsPanel from '@moedelo/frontend-core-react/components/ActionsPanel';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Button, { Color as ButtonColor } from '@moedelo/frontend-core-react/components/buttons/Button';

import style from './style.m.less';

const ControlButtons = ({
    onSave, onCancel, isInProgress, isDisabled
}) => {
    return <ActionsPanel withPadding>
        <ElementsGroup margin={10} className={style.footer}>
            <Button onClick={onCancel} color={ButtonColor.White}>Отмена</Button>
            <Button onClick={onSave} loading={isInProgress} color={ButtonColor.Blue} disabled={isDisabled}>Сохранить</Button>
        </ElementsGroup>
    </ActionsPanel>;
};

ControlButtons.propTypes = {
    onSave: PropTypes.func.isRequired,
    onCancel: PropTypes.func.isRequired,
    isInProgress: PropTypes.bool.isRequired,
    isDisabled: PropTypes.bool.isRequired
};

export default ControlButtons;
