import React from 'react';
import classnames from 'classnames/bind';
import { observer } from 'mobx-react';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import PropTypes from 'prop-types';
import style from './style.m.less';
import provideInTaxResource from './ProvideInTaxResource';

const cn = classnames.bind(style);

@observer
class ProvideInTaxSection extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.store;
    }

    onChangeType = ({ value }) => {
        this.store.provideInTax = value;
    };
    
    render() {
        return (
            <div className={cn(`provideInTax`)}>
                {/* eslint-disable-next-line jsx-a11y/label-has-for */}
                <label htmlFor="provideInTax">
                    Учтены в НУ
                    <Dropdown
                        data={provideInTaxResource}
                        type={`link`}
                        value={this.store.provideInTax}
                        onSelect={this.onChangeType}
                        className={cn(`dropdown`)}
                    />
                </label>
            </div>
        );
    }
}

ProvideInTaxSection.propTypes = {
    store: PropTypes.object
};

export default ProvideInTaxSection;
