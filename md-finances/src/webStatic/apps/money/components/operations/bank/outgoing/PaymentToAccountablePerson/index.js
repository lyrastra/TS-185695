import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Tooltip from '@moedelo/frontend-core-react/components/Tooltip';
import PaymentToAccountablePersonStore from './stores/PaymentToAccountablePersonStore';
import {
    actionArray
} from '../../../../../../../resources/newMoney/saveButtonResource';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Sum from '../../../commonComponents/Sum';
import WorkerAutocomplete from '../../../commonComponents/WorkerAutocomplete';
import Description from '../../../commonComponents/Description';
import Postings from '../../../commonComponents/Postings';
import BusyNumberModal from '../../../commonComponents/BusyNumberModal';
import AdvancedStatement from './components/AdvancedStatement';
import SendToBankButton from '../../../commonComponents/SendToBankButton';
import SmsConfirmModal from '../../../commonComponents/SmsConfirmModal';
import AdvancedStatementNotification from './components/AdvancedStatementNotification';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';
import style from './style.m.less';

const cn = classnames.bind(style);
const ipAsWorkerDefaultId = -1;

@observer
class PaymentToAccountablePerson extends React.Component {
    constructor(props) {
        super(props);

        this.store = new PaymentToAccountablePersonStore({
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
                Sum: this.store.model.Sum,
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
            disabledSaveButton,
            model: { DocumentBaseId, SalaryWorkerId }
        } = this.store;

        const buttonData = getSaveOperationButtonData({
            saveButtonData: actionArray,
            documentBaseId: DocumentBaseId
        });

        const isIpAsWorker = SalaryWorkerId === ipAsWorkerDefaultId;

        return <ElementsGroup>
            {canEdit && <SplitButton
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
            {canSendToBank && !isIpAsWorker && <SendToBankButton operationStore={this.store} />}
            <Button onClick={this.props.onCancel} color="white">Отмена</Button>
        </ElementsGroup>;
    };

    render() {
        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <AdvancedStatementNotification />
                    <HeaderOperation onDelete={this.onDelete} />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                    </div>
                    <WorkerAutocomplete />
                    <div className={cn(grid.row, style.sumRow)}>
                        <div className={grid.col_3}>
                            <Sum canEdit={this.store.canContractorEdit} />
                        </div>
                        <div className={cn(grid.col_3, style.tooltip)}>
                            <Tooltip
                                width={300}
                                position="topRight"
                                content="Выдача под отчет проводится при условии полного погашения подотчетным лицом задолженности по ранее полученной сумме."
                            />
                        </div>
                        <div className={grid.col_1} />
                    </div>
                    <AdvancedStatement store={this.store} />
                    <Description className={cn(grid.row, style.description)} />
                    <Postings />
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

PaymentToAccountablePerson.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default PaymentToAccountablePerson;
