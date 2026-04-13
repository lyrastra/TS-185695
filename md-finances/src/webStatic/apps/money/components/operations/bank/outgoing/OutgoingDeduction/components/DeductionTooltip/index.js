import React from 'react';
import PropTypes from 'prop-types';
import Tooltip, { Position as TooltipPosition } from '@moedelo/frontend-core-react/components/Tooltip';
import style from './style.m.less';

const DeductionTooltip = props => {
    return <Tooltip
        position={TooltipPosition.topRight}
        content={props.content}
        wrapperClassName={style.tooltip}
    />;
};

DeductionTooltip.propTypes = {
    content: PropTypes.node
};

export default DeductionTooltip;
