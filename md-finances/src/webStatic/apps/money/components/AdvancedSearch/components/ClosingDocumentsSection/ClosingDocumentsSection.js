import React from 'react';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import PropTypes from 'prop-types';
import closingDocumentsResource from './ClosingDocumentsResource';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class ClosingDocumentsSection extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.store;
    }

    onChangeType = ({ value }) => {
        this.store.closingDocumentsCondition = value;
    };
    
    render() {
        return (
            <div className={cn(`closingDocuments`)}>
                {/* eslint-disable-next-line jsx-a11y/label-has-for */}
                <label htmlFor="closingDocuments">
                    Закрывающие документы
                    <Dropdown
                        data={closingDocumentsResource}
                        type={`link`}
                        value={this.store.closingDocumentsCondition}
                        onSelect={this.onChangeType}
                        className={cn(`dropdown`)}
                    />
                </label>
            </div>
        );
    }
}

ClosingDocumentsSection.propTypes = {
    store: PropTypes.object
};

export default ClosingDocumentsSection;
