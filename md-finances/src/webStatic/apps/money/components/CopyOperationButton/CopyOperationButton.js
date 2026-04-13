import React from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import { Type } from '@moedelo/frontend-core-react/components/buttons/enums';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper/svgIconHelper';
import style from './style.m.less';

const cn = classnames.bind(style);

const CopyOperationButton = props => {
    const {
        copyOperation, disabled, wide, className
    } = props;

    const onClick = () => {
        mrkStatService.sendEventWithoutInternalUser(`copy_tablitsa_click_button`);

        copyOperation && copyOperation();
    };

    return (
        <Button
            className={cn({ wide }, className)}
            disabled={disabled}
            onClick={onClick}
            type={Type.Panel}
        >
            {svgIconHelper.getJsx({ name: `copy` })}
            Копировать
        </Button>
    );
};

CopyOperationButton.propTypes = {
    copyOperation: PropTypes.func,
    disabled: PropTypes.bool,
    wide: PropTypes.bool,
    className: PropTypes.string
};

export default CopyOperationButton;
