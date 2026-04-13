import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { toJS } from 'mobx';
import { observer, Provider } from 'mobx-react';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Button, { Color } from '@moedelo/frontend-core-react/components/buttons/Button';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import PaymentFromBuyerStore from './stores/PaymentFromBuyerStore';
import { actionArray } from '../../../../../../../resources/newMoney/saveButtonResource';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import ContractorAutocomplete from '../../../commonComponents/ContractorAutocomplete';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import ContractAutocomplete from '../../../commonComponents/ContractAutocomplete';
import Sum from '../../../commonComponents/Sum';
import Nds from '../../../commonComponents/Nds';
import Documents from '../../../commonComponents/Documents';
import Description from '../../../commonComponents/Description';
import IntermediaryFee from '../../../commonComponents/IntermediaryFee';
import Mediation from '../../../commonComponents/Mediation/Mediation';
import Bills from '../../../commonComponents/Bills';
import Postings from '../../../commonComponents/Postings';
import TaxationSystemType from '../../../commonComponents/TaxationSystemType';
import ReserveSum from '../../../commonComponents/ReserveSum';
import PatentDropdown from '../../../commonComponents/PatentDropdown';
import linkedDocumentService from '../../../../../../../services/newMoney/linkedDocumentService';
import SyntheticAccountCodesEnum from '../../../../../../../enums/SyntheticAccountCodesEnum';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';

import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';
import style from './style.m.less';

const cn = classnames.bind(style);

// поступление оплата от покупателя

@observer
class PaymentFromBuyer extends React.Component {
    constructor(props) {
        super(props);

        this.store = new PaymentFromBuyerStore({
            ...getCommonOperationStoreData(props),
            operationTypes: props.operationTypes,
            activePatents: props.activePatents
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
                Bills: toJS(this.store.model.Bills),
                OperationType: value,
                DocumentBaseId: this.store.model.DocumentBaseId,
                initialOperationNumber: this.store.initialOperationNumber
            }
        });
    };

    renderButtons = () => {
        const {
            isSavingBlocked,
            canEdit,
            disabledSaveButton,
            onClickSave,
            model: { DocumentBaseId }
        } = this.store;

        const buttonData = getSaveOperationButtonData({
            saveButtonData: actionArray,
            documentBaseId: DocumentBaseId
        });

        if (canEdit) {
            return <ElementsGroup>
                <SplitButton
                    data={buttonData}
                    mainButton={{
                        className: `split`,
                        onClick: onClickSave,
                        disabled: disabledSaveButton
                    }}
                    onSelect={onClickSave}
                    loading={isSavingBlocked}
                    disabled={disabledSaveButton}
                >{getSaveButtonTitle({ documentBaseId: DocumentBaseId })}</SplitButton>
                <Button onClick={this.props.onCancel} color={Color.White}>Отмена</Button>
            </ElementsGroup>;
        }

        return <Button onClick={this.props.onCancel} color={Color.White}>Отмена</Button>;
    };

    render() {
        const {
            model, isUsn, needToUseSettlementAccountMask, canShowTaxationSystemTypeDropdown, patentSelectVisible, isAfter2025WithTaxation: { isAfter2025, IsUsn }
        } = this.store;

        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation
                        onDelete={this.onDelete}
                    />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                    </div>
                    { isUsn && <Mediation /> }
                    <ContractorAutocomplete
                        useMask={needToUseSettlementAccountMask}
                        operationStore={this.store}
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
                            isShowNdsWarningIcon={this.store.isShowNdsWarningIcon && !this.store.model.IsMediation}
                            currentNdsRateFromAccPolicy={this.store.currentNdsRateFromAccPolicy}
                        />
                    </div>
                    {(canShowTaxationSystemTypeDropdown || patentSelectVisible) &&
                        <div className={cn(grid.row, style.sumRow)}>
                            {canShowTaxationSystemTypeDropdown && <TaxationSystemType operationStore={this.store} />}
                            {patentSelectVisible && <PatentDropdown className={grid.col_6} operationStore={this.store} />}
                        </div>
                    }
                    {model.IsMediation && <div className={cn(grid.row, this.store.canEdit && style.sumRow)}>
                        <IntermediaryFee />
                        {isAfter2025 && IsUsn && <Nds
                            NdsSum={this.store.model.MediationCommissionNdsSum}
                            NdsType={this.store.model.MediationCommissionNdsType}
                            IncludeNds={this.store.model.IncludeMediationCommissionNds}
                            setNdsType={this.store.setMediationCommissionNdsType}
                            setNdsSum={this.store.setMediationCommissionNdsSum}
                            ndsTypes={this.store.ndsTypes}
                            setIncludeNds={this.store.setIncludeMediationCommissionNds}
                            hasNds={this.store.hasMediationCommissionNds}
                            canEdit={this.store.canEdit}
                            validationState={this.store.validationState.MediationCommissionNdsSum}
                            operationStore={this.store}
                            qaNdsSumClassName="qa-inputMediationNdsSum"
                            isShowNdsWarningIcon={this.store.isShowMediationCommissionNdsWarningIcon}
                            currentNdsRateFromAccPolicy={this.store.currentNdsRateFromAccPolicy}
                        />}
                    </div>}
                    <Description className={cn(grid.row, style.description)} />
                    <Bills className={cn(grid.row, style.bills)} />
                    <Documents
                        operationStore={this.store}
                        className={cn(grid.row, style.linkedDocuments)}
                        autocomplete={linkedDocumentService.autocomplete}
                    />
                    <ReserveSum operationStore={this.store} className={cn(grid.row)} />
                    <Postings
                        kontragentAccountCodes={model.IsMediation ? null : { main: SyntheticAccountCodesEnum._62_02, other: SyntheticAccountCodesEnum._76_06 }}
                    />
                    <div className={cn(grid.row, style.buttons)}>
                        {this.renderButtons()}
                    </div>
                </React.Fragment>
            </Provider>
        );
    }
}

PaymentFromBuyer.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func,
    activePatents: PropTypes.arrayOf(PropTypes.object)
};

export default PaymentFromBuyer;
