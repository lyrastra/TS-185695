import React from 'react';
import PropTypes from 'prop-types';
import onClickOutside from 'react-onclickoutside';
import AdvancedSearch from '../../../AdvancedSearch';
import style from './style.m.less';

const Filter = ({
    moneySourceStore, closeFilter, filters, setFilters
}) => {
    const onApplyFilter = value => {
        setFilters(value);
        closeFilter();
    };

    return <div className={style.filterWrapper}>
        <AdvancedSearch
            moneySourceStore={moneySourceStore}
            value={filters}
            onApply={onApplyFilter}
            onCancel={closeFilter}
            isApproveSectionVisible={false}
            isClosingDocumentsSectionVisible={false}
            isTaxSectionVisible={false}
            isProvideInTaxSectionVisible={false}
        />
    </div>;
};

Filter.propTypes = {
    moneySourceStore: PropTypes.object,
    closeFilter: PropTypes.func.isRequired,
    setFilters: PropTypes.func.isRequired,
    filters: PropTypes.object
};

const clickOutsideConfig = {
    handleClickOutside: instance => {
        return e => {
            if (e.target?.className?.includes(`additionalButton`)) {
                return;
            }

            instance.props.closeFilter();
        };
    }
};

export default onClickOutside(Filter, clickOutsideConfig);
