import React from 'react';
import Input from '@moedelo/frontend-core-react/components/Input';
import onClickOutside from 'react-onclickoutside';
import PropTypes from 'prop-types';
import scenarioSectionResource from '@moedelo/frontend-common-v2/apps/onboardingScenario/resources/scenarioSectionResource';
import Button, { Type as ButtonType } from '@moedelo/frontend-core-react/components/buttons/Button';
import { getJsx } from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import filterIcon from '@moedelo/frontend-core-react/icons/filter.m.svg';
import AdvancedSearch from '../../../AdvancedSearch';
import storage from '../../../../../../helpers/newMoney/storage';
import FilterTagsHelper from '../../../../../../helpers/newMoney/FilterTagsHelper';

import style from './style.m.less';

class Filter extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            advancedSearch: false,
            query: this.props.value.query
        };
    }

    shouldComponentUpdate(nextProps, nextState) {
        const props = JSON.stringify(this.props) !== JSON.stringify(nextProps);
        const state = JSON.stringify(this.state) !== JSON.stringify(nextState);

        return props || state;
    }

    componentDidUpdate({ value: { query: prevQuery } }) {
        const { query } = this.props.value;

        if (query !== prevQuery) {
            this.setState({ query });
        }
    }

    onCancel = () => {
        this.setState({ advancedSearch: false });
    };

    onKeyDown = e => {
        if (e.key === `Enter`) {
            this.applyFilter();
        }
    };

    onChange = ({ value }) => {
        this.setState({
            query: value.trim() && encodeURIComponent(value),
            prevQuery: `${value}.`
        });
    };

    search = () => {
        this.applyFilter();
    };

    toggleFilter = () => {
        this.setState({ advancedSearch: !this.state.advancedSearch });
    };

    applyFilter = filter => {
        const { prevQuery } = this.state;
        const clearingFilter = this.cleanFilter(storage.get(`filter`));

        storage.save(`Scroll`, window.scrollY);
        this.setState({ advancedSearch: false });

        const query = this._getQuery();

        if (filter) {
            const resultFilter = Object.assign(clearingFilter, filter);

            this.props.search(resultFilter);

            storage.save(`filter`, resultFilter);
        } else if (prevQuery && prevQuery.length) {
            this.props.search({ query });

            storage.save(`filter`, Object.assign(storage.get(`filter`) || {}, { query }));
        }
    };

    cleanFilter = filter => {
        const newFilter = filter;

        newFilter && Object.keys(newFilter).forEach(propName => {
            if ((newFilter[propName] === null || newFilter[propName] === undefined || newFilter[propName] === 0) &&
                (propName !== `sourceId` && propName !== `sourceType`)) {
                delete newFilter[propName];
            }
        });

        return newFilter;
    }

    clearQuery = () => {
        this.props.search({ query: `` });

        storage.save(`filter`, Object.assign(storage.get(`filter`), { query: `` }));
    };

    _getQuery() {
        const { query } = this.state;

        return query ? encodeURIComponent(decodeURIComponent(query).trim()) : null;
    }

    handleClickOutside = () => {
        this.setState({ advancedSearch: false });
    }

    renderFilter() {
        const { moneySourceStore, value } = this.props;
        const { advancedSearch } = this.state;
        const tagsCount = FilterTagsHelper.getTags({ filter: value })?.length;
        const text = tagsCount || `Фильтры`;

        return (
            <div className={style.filter}>
                <Button
                    onClick={this.toggleFilter}
                    type={ButtonType.Panel}
                    className={style.filterButton}
                >
                    {getJsx({ file: filterIcon })}{text}
                </Button>
                {advancedSearch &&
                    <AdvancedSearch
                        moneySourceStore={moneySourceStore}
                        onApply={this.applyFilter}
                        value={value}
                        onCancel={this.onCancel}
                        visible={advancedSearch}
                    />}
            </div>
        );
    }

    render() {
        const { value } = this.props;
        const { query } = this.state;
        const searchIcon = {
            name: `search`,
            position: `right`,
            onClick: () => this.applyFilter()
        };

        if (value.query && value.query === query) {
            searchIcon.name = `clear`;
            searchIcon.onClick = () => this.clearQuery();
        }

        return (
            <div className={style.filterContainer}>
                <div className={style.search}>
                    <Input
                        value={query && decodeURIComponent(query)}
                        onChange={this.onChange}
                        icons={[searchIcon]}
                        placeholder="Поиск"
                        onKeyDown={this.onKeyDown}
                    />
                    <div id={`tip_${scenarioSectionResource.Finances}_9`} />
                </div>
                {this.renderFilter()}
            </div>
        );
    }
}

Filter.propTypes = {
    moneySourceStore: PropTypes.object,
    value: PropTypes.object,
    search: PropTypes.func
};

export default onClickOutside(Filter);
