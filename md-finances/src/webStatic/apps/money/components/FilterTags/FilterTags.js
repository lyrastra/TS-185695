import React from 'react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import { getJsx } from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import clearIcon from '@moedelo/frontend-core-react/icons/clear.m.svg';
import Button, { Type as ButtonType } from '@moedelo/frontend-core-react/components/buttons/Button';
import style from './style.m.less';

const cn = classnames.bind(style);

class FilterTags extends React.Component {
    _mapChildren = () => {
        return React.Children.map(this.props.children, child => {
            return React.createElement(`div`, { className: style.tag }, child);
        });
    };

    renderClearButton() {
        if (!React.Children.count(this.props.children) > 1) {
            return null;
        }

        return (
            <Button
                onClick={this.props.onClearFilter}
                type={ButtonType.Panel}
            >
                {getJsx({ file: clearIcon })}Сбросить
            </Button>
        );
    }

    render() {
        return <div className={cn(`component`)}>
            <div className={cn(`tagsList`)}>
                {this._mapChildren()}
            </div>
            <div className={style.clearButton}>
                {this.renderClearButton()}
            </div>
        </div>;
    }
}

FilterTags.propTypes = {
    onClearFilter: PropTypes.func
};

export default FilterTags;
