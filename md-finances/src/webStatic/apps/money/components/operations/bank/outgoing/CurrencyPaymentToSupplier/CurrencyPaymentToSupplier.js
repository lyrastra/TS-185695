import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import CurrencyPaymentToSupplierStore from './stores/CurrencyPaymentToSupplierStore';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import ContractorAutocomplete from '../../../commonComponents/ContractorAutocomplete';
import CurrencySumWithCourse from '../../../commonComponents/CurrencySumWithCourse';
import SendToBankButton from '../../../commonComponents/SendToBankButton';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import BusyNumberModal from '../../../commonComponents/BusyNumberModal';
import { actionArray } from '../../../../../../../resources/newMoney/saveButtonResource';
import Description from '../../../commonComponents/Description';
import Postings from '../../../commonComponents/Postings';
import style from './style.m.less';
import ActionEnum from '../../../../../../../enums/newMoney/ActionEnum';
import Documents from '../../../commonComponents/Documents';
import linkedDocumentService from '../../../../../../../services/newMoney/linkedDocumentService';
import { getSaveOperationButtonData, getSaveButtonTitle } from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';
import Nds from '../../../commonComponents/Nds';

const cn = classnames.bind(style);

// списание оплата поставшику валюта

@observer
class CurrencyPaymentToSupplier extends React.Component {
    constructor(props) {
        super(props);

        this.store = new CurrencyPaymentToSupplierStore({
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
                Description: this.store.model.Description,
                Contract: this.store.model.Contract,
                Status: this.store.model.Status,
                OperationType: value,
                DocumentBaseId: this.store.model.DocumentBaseId,
                initialOperationNumber: this.store.initialOperationNumber,
                IncludeNds: this.store.model.IncludeNds
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

        const actions = actionArray
            .filter(action => action.value !== ActionEnum.DownloadAcc
                && action.value !== ActionEnum.DownloadPDF
                && action.value !== ActionEnum.DownloadXLS);

        const buttonData = getSaveOperationButtonData({
            saveButtonData: actions,
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
        const { IsOoo } = this.store.Requisites;

        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation onDelete={this.onDelete} hideStatus />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                    </div>
                    <ContractorAutocomplete
                        operationStore={this.store}
                        useMask={this.store.needToUseSettlementAccountMask}
                    />
                    {/* <ContractAutocomplete /> @todo открыть когда договор для операции будет актуален */}
                    <div className={cn(grid.row, this.store.canEdit && style.sumRow)}>
                        <CurrencySumWithCourse className={style.sumRow} operationStore={this.store} />
                        <Nds
                            NdsSum={this.store.model.NdsSum}
                            IncludeNds={this.store.model.IncludeNds}
                            setNdsType={this.store.setNdsType}
                            setNdsSum={this.store.setNdsSum}
                            ndsTypes={this.store.ndsTypes}
                            NdsType={this.store.model.NdsType}
                            setIncludeNds={this.store.setIncludeNds}
                            hasNds={this.store.hasNds}
                            canEdit={this.store.canEdit && this.store.isAfter2025WithTaxation.isAfter2025}
                            validationState={this.store.validationState?.NdsSum}
                            qaNdsSumClassName="qa-inputNdsSum"
                        />
                    </div>
                    <Description className={cn(grid.row, style.description)} />
                    {!IsOoo && <Documents
                        operationStore={this.store}
                        className={cn(grid.row, style.linkedDocuments)}
                        autocomplete={linkedDocumentService.purchasesCurrencyInvoiceAutocomplete}
                    />}
                    <Postings />
                    <div className={cn(grid.row, style.buttons)}>
                        {this.renderButtons()}
                    </div>
                    <BusyNumberModal store={this.store} />
                </React.Fragment>
            </Provider>
        );
    }
}

CurrencyPaymentToSupplier.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default CurrencyPaymentToSupplier;
