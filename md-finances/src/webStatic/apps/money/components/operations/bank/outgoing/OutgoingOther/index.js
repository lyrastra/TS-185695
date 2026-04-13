import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { toJS } from 'mobx';
import { observer, Provider } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import {
    actionArray
} from '../../../../../../../resources/newMoney/saveButtonResource';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Postings from '../../../commonComponents/Postings';
import OtherStore from './stores/OtherStore';
import style from './style.m.less';
import ContractorAutocomplete from '../../../commonComponents/ContractorAutocomplete';
import ContractAutocomplete from '../../../commonComponents/ContractAutocomplete';
import Sum from '../../../commonComponents/Sum';
import Nds from '../../../commonComponents/Nds';
import Description from '../../../commonComponents/Description';
import BusyNumberModal from '../../../commonComponents/BusyNumberModal';
import SendToBankButton from '../../../commonComponents/SendToBankButton';
import SmsConfirmModal from '../../../commonComponents/SmsConfirmModal';
import NoAutoDeleteOperationSwitch from '../../../commonComponents/NoAutoDeleteOperationSwitch';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';

const cn = classnames.bind(style);

@observer
class Other extends React.Component {
    constructor(props) {
        super(props);

        this.store = new OtherStore({
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
                Kontragent: this.store.model.Kontragent.KontragentId ? this.store.model.Kontragent : {},
                WorkerName: this.store.contractorIsWorker && this.store.model.Kontragent.KontragentName,
                SalaryWorkerId: this.store.model.Kontragent.SalaryWorkerId,
                Sum: this.store.model.Sum,
                IncludeNds: this.store.model.IncludeNds,
                NdsSum: this.store.model.NdsSum,
                NdsType: this.store.model.NdsType,
                Description: this.store.model.Description,
                Bills: toJS(this.store.model.Bills),
                OperationType: value,
                DocumentBaseId: this.store.model.DocumentBaseId
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
            model: { DocumentBaseId },
            isCancelBlocked
        } = this.store;

        const buttonData = getSaveOperationButtonData({
            saveButtonData: actionArray,
            documentBaseId: DocumentBaseId
        });

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

    render() {
        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation onDelete={this.onDelete} />
                    <div className={cn(grid.row, style.underHeadRow)}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                        <NoAutoDeleteOperationSwitch />
                    </div>
                    <ContractorAutocomplete
                        operationStore={this.store}
                    />
                    <ContractAutocomplete canAddContract={!this.store.contractorIsWorker && !this.store.contractIsMainFirm} />
                    <div className={cn(grid.row, this.store.canEdit && style.sumRow)}>
                        <div className={grid.col_3}>
                            <Sum />
                        </div>
                        <div className={grid.col_1} />
                        <Nds
                            NdsSum={this.store.model.NdsSum}
                            IncludeNds={this.store.model.IncludeNds}
                            setNdsType={this.store.setNdsType}
                            setNdsSum={this.store.setNdsSum}
                            ndsTypes={this.store.ndsTypes}
                            NdsType={this.store.model.NdsType}
                            setIncludeNds={this.store.setIncludeNds}
                            hasNds={this.store.hasNds}
                            canEdit={this.store.canEdit}
                            validationState={this.store.validationState?.NdsSum}
                            operationStore={this.store}
                            nds={this.store.model.NdsType}
                            qaNdsSumClassName="qa-inputNdsSum"
                        />
                    </div>
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

Other.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default Other;
