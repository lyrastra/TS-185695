import React from 'react';
import PropTypes from 'prop-types';
import onClickOutside from 'react-onclickoutside';
import IconButton from '@moedelo/frontend-core-react/components/IconButton';
import DropdownList from '@moedelo/frontend-core-react/components/dropdown/DropdownList';
import style from './style.m.less';

class IconButtonDropdown extends React.Component {
    constructor() {
        super();
        this.state = { dropped: false };
    }

    onClickButton = () => {
        this.toggleDropped();
    };

    toggleDropped = () => {
        this.setState({
            dropped: !this.state.dropped
        });
    };

    handleClickOutside = () => {
        if (this.state.dropped) {
            this.toggleDropped();
        }
    };

    renderDropdownList = () => {
        if (!this.state.dropped) {
            return null;
        }

        return (
            <DropdownList
                className={style.dropdownList}
                data={this.props.data}
                onClick={this.props.onSelect}
            />
        );
    };

    render() {
        return <div className={style.wrapper}>
            <IconButton
                icon={`download`}
                onClick={this.onClickButton}
                className={this.props.className}
            />
            {this.renderDropdownList()}
        </div>;
    }
}

IconButtonDropdown.propTypes = {
    data: PropTypes.arrayOf(PropTypes.oneOfType([
        PropTypes.object,
        PropTypes.array
    ])).isRequired,
    onSelect: PropTypes.func,
    className: PropTypes.string
};

export default onClickOutside(IconButtonDropdown);
