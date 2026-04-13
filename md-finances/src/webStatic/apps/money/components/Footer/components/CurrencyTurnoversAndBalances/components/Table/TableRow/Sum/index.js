import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import style from './style.m.less';

const cn = classnames.bind(style);

const getFormatParts = sum => {
    const defaultFormat = {
        prefixSign: ``,
        classColor: style.gray
    };

    if (sum > 0) {
        return {
            ...defaultFormat,
            prefixSign: `+\u2009`,
            classColor: style.incoming
        };
    }

    if (sum < 0) {
        return {
            ...defaultFormat,
            prefixSign: `-\u2009`,
            classColor: style.outgoing
        };
    }

    return defaultFormat;
};

const Sum = ({
    value, currencySign, useColor, usePrefixSign
}) => {
    const {
        prefixSign,
        classColor
    } = getFormatParts(value);

    return <span className={cn(style.sum, useColor ? classColor : style.default)}>
        {(usePrefixSign || value < 0) && prefixSign}{toAmountString(Math.abs(value))}{ currencySign && <span className={style.currency}>&thinsp;{currencySign}</span> }
    </span>;
};

Sum.defaultProps = {
    useColor: false,
    usePrefixSign: false
};

Sum.propTypes = {
    value: PropTypes.number,
    currencySign: PropTypes.node,
    useColor: PropTypes.bool,
    usePrefixSign: PropTypes.bool
};

export default Sum;
