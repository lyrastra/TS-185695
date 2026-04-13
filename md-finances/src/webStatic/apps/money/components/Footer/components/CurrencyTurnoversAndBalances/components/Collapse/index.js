import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import style from './style.m.less';

const cn = classnames.bind(style);

class Collapse extends React.PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            open: props.open
        };
    }

    getDefaultTogglerWithString(togglerCaption) {
        return (<span className={style.collapseTogglerDefault}>
            {togglerCaption}
            {svgIconHelper.getJsx({
                name: `dropdownArrow`,
                className: cn(`collapseTogglerDefaultIcon`, { collapseTogglerActive: this.state.open })
            })}
        </span>);
    }

    getToggler() {
        if (typeof this.props.toggler === `string`) {
            return this.getDefaultTogglerWithString(this.props.toggler);
        }

        return this.props.toggler;
    }

    handlerClick = () => {
        this.setState({ open: !this.state.open });
    };

    renderToggler() {
        const toggler = this.getToggler();

        return (<button className={style.collapseToggler} onClick={this.handlerClick}>
            <div className={this.props.togglerClass}>{toggler}</div>
        </button>);
    }

    renderBody() {
        return (<div className={cn(`collapseBody`, this.props.bodyClass, {
            collapseBodyOpen: this.state.open,
            collapseBodyClose: !this.state.open
        })}
        >
            {this.props.children}
        </div>);
    }

    render() {
        return (
            <div className={style.collapse}>
                {this.renderToggler()}
                {this.renderBody()}
            </div>
        );
    }
}

Collapse.defaultProps = {
    open: false
};

Collapse.propTypes = {
    /** Элемент или текст клик на который приводит к раскрытию/скрытию блока */
    toggler: PropTypes.oneOf(PropTypes.node, PropTypes.arrayOf(PropTypes.node)),
    /** Класс переключателя */
    togglerClass: PropTypes.string,
    /** Класс тела */
    bodyClass: PropTypes.string,
    /** Открыт или закрыт по умолчанию */
    open: PropTypes.bool
};

export default Collapse;
