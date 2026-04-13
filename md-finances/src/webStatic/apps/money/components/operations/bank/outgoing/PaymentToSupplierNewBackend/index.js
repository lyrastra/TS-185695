import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Button, { Color } from '@moedelo/frontend-core-react/components/buttons/Button';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import PaymentToSupplierStore from './stores/PaymentToSupplierStore';
import ContractorAutocomplete from '../../../commonComponents/ContractorAutocomplete';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import ContractAutocomplete from '../../../commonComponents/ContractAutocomplete';
import BusyNumberModal from '../../../commonComponents/BusyNumberModal';
import Sum from '../../../commonComponents/Sum';
import Nds from '../../../commonComponents/Nds';
import Documents from '../../../commonComponents/Documents';
import Description from '../../../commonComponents/Description';
import Postings from '../../../commonComponents/Postings';
import style from './style.m.less';
import ReserveSum from '../../../commonComponents/ReserveSum';
import SmsConfirmModal from '../../../commonComponents/SmsConfirmModal';
import SendToBankButton from '../../../commonComponents/SendToBankButton';
import SyntheticAccountCodesEnum from '../../../../../../../enums/SyntheticAccountCodesEnum';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { actionArray } from '../../../../../../../resources/newMoney/saveButtonResource';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';

const cn = classnames.bind(style);

// списание оплата поставщику

@observer
class PaymentToSupplierNewBackend extends React.Component {
    constructor(props) {
        super(props);

        this.store = new PaymentToSupplierStore({
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
                Kontragent: this.store.model.Kontragent,
                Sum: this.store.model.Sum,
                IncludeNds: this.store.model.IncludeNds,
                NdsSum: this.store.model.NdsSum,
                NdsType: this.store.model.NdsType,
                Description: this.store.model.Description,
                Contract: this.store.model.Contract,
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
                color={Color.White}
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
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                    </div>
                    <ContractorAutocomplete
                        operationStore={this.store}
                        useMask={this.store.needToUseSettlementAccountMask}
                    />
                    <ContractAutocomplete />
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
                    <Documents
                        operationStore={this.store}
                        className={cn(grid.row, style.linkedDocuments)}
                        autocomplete={this.store.getDocumentAutocomplete}
                    />
                    <ReserveSum operationStore={this.store} className={cn(grid.row)} />
                    <Postings
                        kontragentAccountCodes={{
                            main: SyntheticAccountCodesEnum._60_02,
                            other: SyntheticAccountCodesEnum._76_05
                        }}
                    />
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

PaymentToSupplierNewBackend.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default PaymentToSupplierNewBackend;
