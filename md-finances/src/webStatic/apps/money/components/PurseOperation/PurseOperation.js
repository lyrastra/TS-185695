import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import PageLoader from '../../../../components/PageLoader';

import style from './style.m.less';
import OpenOperationActions from '../../../../enums/newMoney/OpenOperationActionsEnum';

const cn = classnames.bind(style);

class PurseOperation extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            disableRender: false
        };
    }

    componentDidMount() {
        window.scrollTo(0, 0);
    }

    onSave = () => {
        this.setState({
            disableRender: true
        });

        if (localStorage.getItem(`referrerUrl`) === `finances`) {
            this.props.onSave();
        } else {
            const current = window.location.href;
            const urlToBack = sessionStorage.getItem(`urlToBack`);

            if (urlToBack) {
                sessionStorage.removeItem(`urlToBack`);

                NavigateHelper.replace(urlToBack);

                return;
            }

            window.history.back();

            setTimeout(() => {
                if (window.location.href === current) {
                    NavigateHelper.push(`/Finances`);
                }
            }, 1000);
        }
    }

    onDelete = () => {
        const { onDelete } = this.props;

        this.setState({
            disableRender: true
        }, () => {
            onDelete && onDelete();
        });
    }

    createViewOperation = () => {
        const { Bank } = window;
        const purseOperationGetter = new Bank.PurseOperationGetter();

        return new Promise(resolve => {
            const {
                DocumentBaseId, PurseId, Direction, Action
            } = this.props.value;

            if (Action === OpenOperationActions.copy && DocumentBaseId) {
                return purseOperationGetter.copyOperation(DocumentBaseId, view => resolve(view));
            }

            if (PurseId && Direction) {
                return resolve(purseOperationGetter.createNewOperation(Direction, PurseId));
            }

            return purseOperationGetter.loadOperation(DocumentBaseId, view => resolve(view));
        });
    }

    render() {
        if (this.state.disableRender) {
            return false;
        }

        const { Backbone, $, Bank } = window;
        const settlementGetter = new Bank.SettlementGetter();
        const purseGetter = new Bank.PurseGetter();

        Promise.all([settlementGetter.loadSettlements(), purseGetter.loadPurses()]).then(() => {
            this.createViewOperation().then(docView => {
                docView.model.on(`save`, this.onSave).on(`delete`, this.onDelete);
                Backbone.Wreqr.radio.channel(`document`).reqres.setHandler(`get`, docView.model.get.bind(docView.model));
                $(this.content).html(docView.$el);
            });
        });

        return <div ref={el => { this.content = el; }} className={cn(`purseOperation`, `content`)}>
            <PageLoader />
        </div>;
    }
}

PurseOperation.propTypes = {
    onSave: PropTypes.func,
    onDelete: PropTypes.func,
    value: PropTypes.shape({
        Direction: PropTypes.number,
        SourceId: PropTypes.number,
        DocumentBaseId: PropTypes.string,
        PurseId: PropTypes.number,
        Action: PropTypes.number
    })
};

export default PurseOperation;
