import React from 'react';
import classnames from 'classnames/bind';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import style from './style.m.less';
import MoneySourceType from '../../../../enums/MoneySourceType';
import MoneySourceIcon from '../MoneySourceIcon/MoneySourceIcon';
import moneySourceHelper from '../../../../helpers/newMoney/moneySourceHelper';

const cn = classnames.bind(style);

@observer
class SourceDropdown extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.store;
    }

    onSelect = ({ value }) => {
        if (!value) {
            return;
        }

        const [type, id] = value.split(` `);
        const source = this.getSource().find(({ Type, Id }) => Type === parseInt(type, 10) && Id === parseInt(id, 10));

        if (source) {
            this.props.onSelect({ sourceType: source.Type, sourceId: source.Id });
        }
    };

    getSource = () => {
        return this.store.list.map(item => {
            return {
                ...item,
                value: `${item.Type} ${item.Id}`,
                text: moneySourceHelper.name(item),
                icon: <MoneySourceIcon value={item.Type} />,
                description: moneySourceHelper.description(item),
                additionText: moneySourceHelper.additionText(item)
            };
        });
    }

    getSourceIcon = () => {
        const { sourceType } = this.props.value;
        const icon = parseInt(sourceType, 10) || MoneySourceType.All;

        return <MoneySourceIcon value={icon} className={cn(`icon__source`)} />;
    };

    getValue = () => {
        const defaultSourceId = 0;
        const { sourceType, sourceId } = this.props.value;

        return sourceType && sourceId
            ? `${sourceType} ${sourceId}`
            : `${MoneySourceType.All} ${defaultSourceId}`;
    };

    render() {
        const groupedList = this.getSource().reduce((acc, val) => {
            const key = val.Type;

            if (!acc[key]) {
                acc[key] = [];
            }

            acc[key].push(val);

            return acc;
        }, {});

        const list = Object.values(groupedList).map(group => {
            return {
                data: group.map(source => {
                    return {
                        ...source
                    };
                })
            };
        });

        return <Dropdown
            className={style.dropdown}
            onSelect={this.onSelect}
            data={list}
            loading={this.store.loading}
            icon={this.getSourceIcon()}
            value={this.getValue()}
            scrollToCheckedEl={false}
            width={`auto`}
        />;
    }
}

SourceDropdown.propTypes = {
    store: PropTypes.object,
    value: PropTypes.object,
    onSelect: PropTypes.func
};

export default SourceDropdown;
