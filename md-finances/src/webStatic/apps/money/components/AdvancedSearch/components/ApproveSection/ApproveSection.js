import React from 'react';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import PropTypes from 'prop-types';
import approveSectionResource from './ApproveSectionResource';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class ApproveSection extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.store;
    }

    onChange = ({ value }) => {
        this.store.approvedCondition = value;
    };
    
    render() {
        return (
            <div className={cn(`approveSection`)}>
                {/* eslint-disable-next-line jsx-a11y/label-has-for */}
                <label htmlFor="approveSection">
                    Статус обработки
                    <Dropdown
                        data={approveSectionResource}
                        type={`link`}
                        value={this.store.approvedCondition}
                        onSelect={this.onChange}
                        className={cn(`dropdown`)}
                    />
                </label>
            </div>
        );
    }
}

ApproveSection.propTypes = {
    store: PropTypes.object
};

export default ApproveSection;
