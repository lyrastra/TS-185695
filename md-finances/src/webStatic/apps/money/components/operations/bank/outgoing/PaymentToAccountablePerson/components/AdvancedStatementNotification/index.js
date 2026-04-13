import React from 'react';
import { inject, observer } from 'mobx-react';
import NotificationPanel from '@moedelo/frontend-core-react/components/NotificationPanel';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import style from './style.m.less';

const cn = classnames.bind(style);

@inject(`operationStore`)
@observer
class AdvancedStatementNotification extends React.Component {
    constructor() {
        super();

        this.state = {
            canViewPostings: true
        };
    }

    componentDidMount = async () => {
        await this.props.operationStore.canViewPostings().then((canViewPostings) => {
            this.setState({ canViewPostings });
        });
    };

    render() {
        if (!this.state.canViewPostings || this.props.operationStore.canContractorEdit) {
            return null;
        }

        return <NotificationPanel type="info" className={cn(style.notification)}>
            Для полного редактирования операции отвяжите авансовый отчет
        </NotificationPanel>;
    }
}

AdvancedStatementNotification.propTypes = {
    operationStore: PropTypes.object
};

export default AdvancedStatementNotification;
