import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import DocumentIcon from '@moedelo/frontend-core-react/components/DocumentIcon';
import Link from '@moedelo/frontend-core-react/components/Link';
import OutgoingForTransferSalaryStore from './stores/OutgoingForTransferSalaryStore';
import { actionArray } from '../../../../../../../resources/newMoney/saveButtonResource';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Description from '../../../commonComponents/Description';
import Postings from '../../../commonComponents/Postings';
import BusyNumberModal from '../../../commonComponents/BusyNumberModal';
import ContractType from './components/ContractType';
import style from './style.m.less';
import WorkerCharges from './components/WorkerCharges';
import { Colors as DocumentIconColors } from '../../../../../../../enums/newMoney/ColorsEnum';
import SendToBankButton from '../../../commonComponents/SendToBankButton';
import SmsConfirmModal from '../../../commonComponents/SmsConfirmModal';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';

const cn = classnames.bind(style);

@observer
class OutgoingForTransferSalary extends React.Component {
    constructor(props) {
        super(props);

        this.store = new OutgoingForTransferSalaryStore({
            ...getCommonOperationStoreData(props),
            operationTypes: props.operationTypes
        });
    }

    onDelete = async () => {
        await this.store.remove();
        this.props.onDelete();
    };

    onChangeOperationType = ({ value }) => {
        this.props.onChangeOperationType({
            operation: {
                Number: this.store.model.Number,
                Date: this.store.model.Date,
                Direction: this.store.model.Direction,
                SettlementAccountId: this.store.model.SettlementAccountId,
                WorkerName: this.store.model.WorkerName,
                SalaryWorkerId: this.store.model.SalaryWorkerId,
                Kontragent: {},
                Description: this.store.model.Description,
                Status: this.store.model.Status,
                OperationType: value,
                DocumentBaseId: this.store.model.DocumentBaseId,
                initialOperationNumber: this.store.initialOperationNumber
            }
        });
    };

    renderButtons = () => {
        const {
            canEdit,
            isSavingBlocked,
            canSendToBank,
            onClickSave,
            canEditSalary,
            disabledSaveButton,
            model: { DocumentBaseId },
            isCancelBlocked
        } = this.store;

        const actions = this.store.canSaveAndDownload ? actionArray : actionArray.slice(-1);

        const buttonData = getSaveOperationButtonData({
            saveButtonData: actions,
            documentBaseId: DocumentBaseId
        });

        return <ElementsGroup>
            {(canEdit || canEditSalary) && <SplitButton
                data={buttonData}
                mainButton={{
                    className: `split`,
                    onClick: onClickSave,
                    disabled: disabledSaveButton
                }}
                onSelect={onClickSave}
                loading={isSavingBlocked}
                disabled={disabledSaveButton}
            >{getSaveButtonTitle({ documentBaseId: DocumentBaseId })}</SplitButton>}
            {canSendToBank && <SendToBankButton operationStore={this.store} />}
            <Button
                onClick={this.props.onCancel}
                color="white"
                disabled={isCancelBlocked}
            >
                Отмена
            </Button>
        </ElementsGroup>;
    };

    renderDownloadRegistryIcon = () => {
        if (!this.store.isSalaryProject) {
            return null;
        }

        return (
            <ElementsGroup margin={5} className={cn(style.downloadRegistryContainer)}>
                <DocumentIcon
                    label={`XML`}
                    color={DocumentIconColors.Green}
                    onClick={this.store.downloadRegistry}
                />
                <Link type={`modal`} onClick={this.store.downloadRegistry} text={`Скачать реестр`} />
            </ElementsGroup>
        );
    };

    render() {
        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation onDelete={this.onDelete} />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                        <div className={grid.col_1} />
                        <div className={grid.col_6}>
                            <ContractType operationStore={this.store} />
                        </div>
                    </div>
                    <WorkerCharges operationStore={this.store} />
                    <Description className={cn(grid.row, style.description)} />
                    <Postings />
                    {this.renderDownloadRegistryIcon()}
                    <div className={cn(grid.row, style.buttons)}>
                        {this.renderButtons()}
                    </div>
                    <BusyNumberModal store={this.store} />
                    <SmsConfirmModal operationStore={this.store} />
                </React.Fragment>
            </Provider>
        );
    }
}

OutgoingForTransferSalary.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default OutgoingForTransferSalary;
