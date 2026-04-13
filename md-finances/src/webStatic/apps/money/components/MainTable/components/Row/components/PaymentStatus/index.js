import React from 'react';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import cn from 'classnames';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';

import { getPaymentState } from './paymentStatusHelper';

import style from './style.m.less';

const PaymentStatus = props => {
    const paymentState = getPaymentState(props.model);

    if (!paymentState) {
        return null;
    }

    const {
        text, icon, iconClassName, tooltipText
    } = paymentState;

    const renderIcon = () => {
        const iconJsx = svgIconHelper.getJsx({ file: icon, className: iconClassName });

        if (tooltipText) {
            return (
                <Tooltip
                    position={Position.bottomRight}
                    content={tooltipText}
                >
                    {iconJsx}
                </Tooltip>
            );
        }

        return iconJsx;
    };

    return (
        <div className={cn(grid.col_24, style.paymentStatusContainer)}>
            {renderIcon()}
            {text}
        </div>
    );
};

PaymentStatus.propTypes = {
    model: PropTypes.object
};

export default PaymentStatus;
