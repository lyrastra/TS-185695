import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, inject } from 'mobx-react';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import Dropdown, { Type } from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import IconButton from '@moedelo/frontend-core-react/components/IconButton';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import { Color as ButtonColor } from '@moedelo/frontend-core-react/components/buttons/enums';
import Link from '@moedelo/frontend-core-react/components/Link';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import sessionStorageHelper from '@moedelo/frontend-core-v2/helpers/sessionStorageHelper';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import NotificationPanel from '@moedelo/frontend-core-react/components/NotificationPanel';
import DocumentIcon from '@moedelo/frontend-core-react/components/DocumentIcon';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import IconButtonDropdown from '../IconButtonDropdown';
import FormatTypesEnum from '../../../../../../enums/FormatTypesEnum';
import { Colors as DocumentIconColors } from '../../../../../../enums/newMoney/ColorsEnum';
import ClosedPeriodDialog from './../ClosedPeriodDialog';
import PaymentRules from '../../../PaymentRules';
import DocumentStatusEnum from '../../../../../../enums/DocumentStatusEnum';
import SendToBankErrorPanel from '../SendToBankErrorPanel';
import style from './style.m.less';

const cn = classnames.bind(style);

const { Extensions: DocumentTypeEnum } = FormatTypesEnum;
const paidStatusData = [
    {
        value: DocumentStatusEnum.NotPayed,
        text: `Не оплачено`
    },
    {
        value: DocumentStatusEnum.Payed,
        text: `Оплачено`
    }
];

const approvedStatusData = [
    {
        value: false,
        text: `Не обработано`
    },
    {
        value: true,
        text: `Обработано`
    }
];

@inject(`operationStore`)
@observer
class HeaderOperation extends React.Component {
    constructor() {
        super();

        this.state = {
            showDeleteDialog: false,
            canViewPostings: true
        };
    }

    async componentDidMount() {
        await this.props.operationStore.canViewPostings().then(canViewPostings => {
            this.setState({ canViewPostings });
        });
    }

    onDelete = () => {
        this.props.onDelete([this.props.operationStore.model.DocumentBaseId]);
    };

    onCopy = () => {
        const { model: { Id, OperationType, DocumentBaseId } } = this.props.operationStore;

        mrkStatService.sendEventWithoutInternalUser(`copy_stranica_operacii_click_button`);

        if (this.props.operationStore.isBudgetaryPayment) {
            sessionStorageHelper.set(`budgetaryId`, Id);
        }

        NavigateHelper.push(`copy/settlement/${DocumentBaseId}/${OperationType}`);
    };

    onDownload = value => {
        this.props.operationStore.download({
            baseId: this.props.operationStore.model.DocumentBaseId,
            format: value
        });
    };

    getDownloadList = () => {
        return [<div className={style.downloadList}>
            <DocumentIcon
                label={`PDF`}
                color={DocumentIconColors.Red}
                onClick={() => this.onDownload(DocumentTypeEnum.PDF)}
            />
            <DocumentIcon
                label={`XLS`}
                color={DocumentIconColors.Green}
                onClick={() => this.onDownload(DocumentTypeEnum.XLS)}
            />
            <DocumentIcon
                label={`1C`}
                color={DocumentIconColors.Orange}
                onClick={() => this.onDownload(DocumentTypeEnum.TXT)}
            />
        </div>];
    };

    hideDeleteDialog = () => {
        this.setState({ showDeleteDialog: false });
    };

    showDeleteDialog = () => {
        this.setState({ showDeleteDialog: true });
    };

    showClosedPeriodDialog = () => {
        this.setState({ showClosedPeriodDialog: true });
    };

    hideClosedPeriodDialog = () => {
        this.setState({ showClosedPeriodDialog: false });
    };

    renderPaidStatus() {
        const {
            isOutgoing, Status, setStatus, canEdit, canEditStatus, isSalaryProject
        } = this.props.operationStore;

        if (!isOutgoing || this.props.hideStatus) {
            return null;
        }

        /** костылина проклятая, но что поделать */
        const showAsText = isSalaryProject ? !canEdit && !canEditStatus : !canEdit || !canEditStatus;

        return (
            <Dropdown
                data={paidStatusData}
                value={Status}
                type={Type.link}
                onSelect={setStatus}
                showAsText={showAsText}
                width={135}
            />
        );
    }

    renderApprovedStatus() {
        const {
            setIsApproved, isApproved, isShowApprove, canEdit
        } = this.props.operationStore;

        if (!isShowApprove) {
            return null;
        }

        return (
            <Dropdown
                data={approvedStatusData}
                value={isApproved}
                type={Type.link}
                onSelect={setIsApproved}
                showAsText={!canEdit}
                width={150}
            />
        );
    }

    renderClosedPeriodButton = () => {
        const { isClosed } = this.props.operationStore;

        if (isClosed) {
            return <React.Fragment>
                <Button onClick={this.showClosedPeriodDialog} color="white" className={style.actionBlockButton}>
                    {svgIconHelper.getJsx({ name: `lock` })}
                    В закрытом периоде
                </Button>
                {this.renderClosedPeriodDialog()}
            </React.Fragment>;
        }

        return null;
    };

    renderActionButton = () => {
        const {
            canDownload, canCopy, showDeleteIcon, isTypeChanged, canShowImportRules
        } = this.props.operationStore;
        const { ImportRules, OutsourceImportRules } = this.props.operationStore.model;

        if (isTypeChanged) {
            return null;
        }

        return <React.Fragment>
            {(canShowImportRules || OutsourceImportRules) && <PaymentRules
                paymentRules={ImportRules}
                outsourcePaymentRules={OutsourceImportRules && [OutsourceImportRules]}
            />}
            {canDownload && <IconButtonDropdown
                data={this.getDownloadList()}
                onSelect={this.onDownload}
                className="qa-downloadIcon"
            />}
            {canCopy && <IconButton
                icon={`copy`}
                onClick={this.onCopy}
                className="qa-copyIcon"
            />}
            {showDeleteIcon && <IconButton
                icon={`remove`}
                onClick={this.showDeleteDialog}
                className="qa-removeIcon"
            />}
        </React.Fragment>;
    };

    renderClosedPeriodDialog() {
        if (!this.state.showClosedPeriodDialog) {
            return null;
        }

        const date = this.props.operationStore.model.Date;

        return <ClosedPeriodDialog onClose={this.hideClosedPeriodDialog} date={date} />;
    }

    renderActionBlock() {
        const colsAmount = this.props.operationStore.isClosed ? grid.col_8 : grid.col_5;

        return <div className={cn(colsAmount, style.actionBlockWrapper)}>
            {this.renderClosedPeriodButton()}
            {this.renderActionButton()}
        </div>;
    }

    renderDeleteDialog() {
        if (!this.state.showDeleteDialog) {
            return null;
        }

        return <Modal
            width={`300px`}
            header={`Удаление`}
            onClose={this.hideDeleteDialog}
            canClose
            visible
        >
            Вы уверены, что хотите удалить эту операцию?
            <ElementsGroup className={style.buttonDeleteDialog}>
                <Button onClick={this.onDelete} color={ButtonColor.Red}>
                    Удалить
                </Button>
                <Link onClick={this.hideDeleteDialog}>
                    Отмена
                </Link>
            </ElementsGroup>
        </Modal>;
    }

    renderNumber = () => {
        const {
            validationState,
            setNumber,
            model,
            canEditNumber
        } = this.props.operationStore;

        if (!canEditNumber) {
            return <span className={cn(style.number)}>{model.Number}</span>;
        }

        return <div className={grid.col_4}>
            <Input
                className={cn(style.number, `qa-number`)}
                value={model.Number}
                onBlur={setNumber}
                error={!!validationState.Number}
                message={validationState.Number}
                size="large"
            />
        </div>;
    };

    renderDate = () => {
        const {
            validationState,
            model,
            setDate,
            minDate
        } = this.props.operationStore;

        return <div className={cn(grid.col_4, style.date)}>
            <Input
                placeholder="Дата"
                onChange={setDate}
                mask="date"
                value={model.Date}
                minDate={minDate}
                error={!!validationState.Date}
                message={validationState.Date}
                size="large"
                type={InputType.date}
            />
        </div>;
    };

    renderNotification = () => {
        if (this.state.canViewPostings) {
            return null;
        }

        return <NotificationPanel type="info" className={style.notification}>
            Документ проведен бухгалтером и недоступен для редактирования
        </NotificationPanel>;
    };

    renderSendToBankError = () => {
        const { sendToBankErrorMessage, setSendToBankErrorMessage } = this.props.operationStore;

        if (!sendToBankErrorMessage) {
            return null;
        }

        return (
            <div className={style.notification}>
                <SendToBankErrorPanel
                    message={sendToBankErrorMessage}
                    onClose={setSendToBankErrorMessage}
                />
            </div>
        );
    }

    render() {
        return (
            <React.Fragment>
                {this.renderSendToBankError()}
                {this.renderNotification()}
                <div className={cn(grid.row, style.numberAndDate)}>
                    <div className={cn(style.text)}>{this.props.operationStore.operationDirectionText} №</div>
                    {this.renderNumber()}
                    <div className={cn(style.text)}> от </div>
                    {this.renderDate()}
                    <div className={grid.col_1} />
                    {this.renderPaidStatus()}
                    <div className={grid.col_1} />
                    {this.renderApprovedStatus()}
                    {this.renderActionBlock()}
                    {this.renderDeleteDialog()}
                </div>

            </React.Fragment>
        );
    }
}

HeaderOperation.defaultProps = {
    hideStatus: false
};

HeaderOperation.propTypes = {
    operationStore: PropTypes.object,
    onDelete: PropTypes.func,
    hideStatus: PropTypes.bool
};

export default HeaderOperation;
