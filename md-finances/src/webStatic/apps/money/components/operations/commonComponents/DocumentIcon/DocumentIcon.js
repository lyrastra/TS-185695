import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import style from './style.m.less';
import { Colors } from '../../../../../../enums/newMoney/ColorsEnum';

const cn = classnames.bind(style);

class DocumentIcon extends React.Component {
    getIconClassNames = () => {
        return cn(
            this.props.className,
            `icon`,
            `icon_${this.props.color}`
        );
    };

    render() {
        return <div
            className={style.wrapper}
            onClick={this.props.onClick}
            role={`presentation`}
        >
            {svgIconHelper.getJsx({
                className: this.getIconClassNames(),
                name: `file`
            })}
            <span className={style.label}>
                {this.props.label}
            </span>
        </div>;
    }
}

DocumentIcon.defaultProps = {
    label: `DOC`,
    color: Colors.Blue
};

DocumentIcon.propTypes = {
    className: PropTypes.string,
    label: PropTypes.string,
    color: PropTypes.oneOf(Colors),
    onClick: PropTypes.func
};

export default DocumentIcon;
