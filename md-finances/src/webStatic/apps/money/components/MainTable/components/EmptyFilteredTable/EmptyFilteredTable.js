import React from 'react';
import renderHTML from 'react-render-html';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import style from './style.m.less';
import icon from './icon.html';

const cn = classnames.bind(style);

class EmptyFilteredTable extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            text: this.isFiltered(props.filter) ? `Ничего не найдено` : `Нет операций`
        };
    }

    isFiltered(filter) {
        let isFiltered = false;
        
        if (filter && filter.sourceId >= 0 && filter.sourceType >= 0 && Object.keys(filter).length > 2) {
            isFiltered = true;
        }

        return isFiltered;
    }

    render() {
        return (
            <div className={cn(`component`)}>
                <div className={cn(`center`)}>
                    <div className={cn(`icon`)}>{renderHTML(icon)}</div>
                    <h3>{this.state.text}</h3>
                </div>
            </div>
        );
    }
}

EmptyFilteredTable.propTypes = {
    filter: PropTypes.shape({})
};

export default EmptyFilteredTable;
