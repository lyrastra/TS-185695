import React from 'react';
import $ from 'jquery';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import DirectionEnum from '@moedelo/frontend-enums/mdEnums/Direction';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import userInfoService from '@moedelo/frontend-core-v2/services/userDataService';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import MoneyOperationService from '../../../../services/newMoney/moneyOperationService';
import PageLoader from '../../../../components/PageLoader';

import style from './style.m.less';

const cn = classnames.bind(style);

class CashOperationDialog extends React.Component {
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

    load = () => {
        const {
            DocumentBaseId,
            SourceId,
            CashId,
            Direction,
            ContractId,
            OperationType
        } = this.props.value;

        if (SourceId) {
            return MoneyOperationService.getCopyCashOperation({ sourceId: SourceId });
        }

        if (Direction && ContractId) {
            return MoneyOperationService.getDefaultCashOperationWithContract({ cashId: CashId, direction: Direction, contractId: ContractId });
        }

        if (OperationType) {
            return MoneyOperationService.getDefaultCashOperationWithType({ operationType: OperationType });
        }

        if (CashId && Direction) {
            return MoneyOperationService.getDefaultCashOperation({ cashId: CashId, direction: Direction });
        }

        return MoneyOperationService.getCashOperation({ documentBaseId: DocumentBaseId });
    }

    render() {
        if (this.state.disableRender) {
            return false;
        }

        this.load()
            .then(operation => renderOperation(operation, this.onSave, this.onDelete)
                .then($el => $(this.content).html($el)));

        return <div ref={el => { this.content = el; }} className={cn(`cashOperation`, `content`)}>
            <PageLoader />
        </div>;
    }
}

function renderOperation(data, onSave, onDelete) {
    const operation = data;

    return new Promise(resolve => {
        const Model = getModelByDirection(operation.Direction);

        let action = operation.Id > 0 ? `edit` : `new`;

        if (window.location.hash.match(/copy/gi)) {
            action = `copy`;
        }

        operation.action = action;

        const model = new Model(operation);
        model.on(`saved`, onSave).on(`delete`, onDelete);
        model.afterLoading();

        const cashListLoading = new window.Cash.Collections.CashCollection().load();
        const requisites = new window.Common.FirmRequisites().load();
        const userInfoDfd = turnPromiseIntoDeferrer(userInfoService.get());
        const taxSystemDfd = turnPromiseIntoDeferrer(taxationSystemService.getTaxSystem());

        $.when(cashListLoading, requisites, userInfoDfd, taxSystemDfd).done((cashList, requisitesData, userInfo, taxSystem) => {
            const View = getViewByDirection(operation.Direction);
            const orderView = new View({
                el: $(`<div/>`),
                model,
                userInfo,
                taxSystem
            });

            orderView.on(`after:render`, () => resolve(orderView.$el));
            orderView.render();
        });
    });

    function getModelByDirection(direction) {
        if (direction === DirectionEnum.Outgoing) {
            return window.Cash.Models.OutgoingCashOrder;
        }

        return window.Cash.Models.IncomingCashOrder;
    }

    function getViewByDirection(direction) {
        if (direction === DirectionEnum.Outgoing) {
            return window.Cash.Views.OutgoingCashOrderView;
        }

        return window.Cash.Views.IncomingCashOrderView;
    }
}

function turnPromiseIntoDeferrer(promise) {
    const dfd = $.Deferred();

    promise
        .then(response => {
            dfd.resolve(response);
        });

    return dfd.promise();
}

CashOperationDialog.propTypes = {
    DocumentBaseId: PropTypes.number,
    SourceId: PropTypes.number,
    CashId: PropTypes.number,
    Direction: PropTypes.number,
    onSave: PropTypes.func,
    onDelete: PropTypes.func,
    value: PropTypes.oneOfType([
        PropTypes.array,
        PropTypes.object
    ])
};

export default CashOperationDialog;
