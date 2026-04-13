import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import Dropdown, { Type } from '@moedelo/frontend-core-react/components/dropdown/Dropdown';

const TaxationSystemSection = observer(({ store }) => {
    const onSelect = ({ value }) => {
        store.setTaxationSystemFilterValue(value);
    };

    return <Dropdown
        width="100%"
        data={store.taxationSystemTypeFilterData}
        type={Type.input}
        value={store.taxationSystemTypeFilterValue}
        onSelect={onSelect}
    />;
});

TaxationSystemSection.propTypes = {
    store: PropTypes.object
};

export default TaxationSystemSection;
