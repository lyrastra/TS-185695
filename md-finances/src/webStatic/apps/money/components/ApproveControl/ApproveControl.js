import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import SvgIcon from '@moedelo/frontend-core-react/components/SvgIcon';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import error from '@moedelo/frontend-core-react/icons/error.m.svg';
import success2 from '@moedelo/frontend-core-react/icons/success2.m.svg';
import style from './style.m.less';

const cn = classnames.bind(style);

const ApproveControl = ({ isApproved, isFromWarningTable }) => {
    const icon = isApproved ? success2 : error;
    const color = isApproved ? `#41c380` : `#ee5f49`;
    const content = isApproved ? `Обработано` : `Не обработано`;

    if (isApproved === null) {
        return null;
    }

    return <Tooltip
        className={style.approveTooltip}
        content={content}
        position={Position.bottom}
        width="auto"
    >
        <SvgIcon
            color={color}
            icon={icon}
            className={cn(`icon`, isFromWarningTable && `icon__warning`)}
        />
    </Tooltip>;
};

ApproveControl.propTypes = {
    isApproved: PropTypes.bool,
    isFromWarningTable: PropTypes.bool
};

export default ApproveControl;
