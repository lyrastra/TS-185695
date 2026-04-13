import React from 'react';
import PropTypes from 'prop-types';
import style from './style.m.less';

const GapLine = props => {
    const {
        left,
        right,
        top,
        bottom,
        height
    } = props;

    const getStyle = () => {
        let gapStyle = { height: `${height}px` };

        if (left) {
            gapStyle = { ...gapStyle, left: `${left}px` };
        }

        if (right) {
            gapStyle = { ...gapStyle, right: `${right}px` };
        }

        if (top) {
            gapStyle = { ...gapStyle, top: `${top}px` };
        }

        if (bottom) {
            gapStyle = { ...gapStyle, bottom: `${bottom}px` };
        }

        return gapStyle;
    };

    return (
        <div className={style.gap} style={getStyle()} />
    );
};

GapLine.defaultProps = {
    height: 10
};

GapLine.propTypes = {
    left: PropTypes.number,
    right: PropTypes.number,
    top: PropTypes.number,
    bottom: PropTypes.number,
    height: PropTypes.number
};

export default GapLine;
