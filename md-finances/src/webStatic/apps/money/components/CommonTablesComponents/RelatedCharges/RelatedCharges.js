import React from 'react';
import PropTypes from 'prop-types';
import Tooltip from '@moedelo/frontend-core-react/components/Tooltip';
import IconButton, { IconButtonColor as ButtonColor } from '@moedelo/frontend-core-react/components/IconButton';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import icon from './userIcon.m.svg';

const RelatedCharges = ({ needAttention = false } = {}) => {
    const cancelClickEvent = e => {
        e.stopPropagation();
        e.preventDefault();
    };
    
    const getIcon = () => {
        return <IconButton
            onClick={cancelClickEvent}
            icon={svgIconHelper.getJsx({ file: icon })}
            color={needAttention ? ButtonColor.red : ButtonColor.green}
        />;
    };

    if (needAttention) {
        return <Tooltip
            content={`Необходимо указать типы начислений`}
            width={260}
        >
            {getIcon()}
        </Tooltip>;
    }

    return getIcon();
};

RelatedCharges.propTypes = {
    needAttention: PropTypes.bool
};

export default RelatedCharges;
