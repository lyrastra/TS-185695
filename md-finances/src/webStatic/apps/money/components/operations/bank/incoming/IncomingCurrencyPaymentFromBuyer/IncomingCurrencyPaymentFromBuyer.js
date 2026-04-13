import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import { actionArray } from '../../../../../../../resources/newMoney/saveButtonResource';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Postings from '../../../commonComponents/Postings';
import IncomingCurrencyPaymentFromBuyerStore from './stores/IncomingCurrencyPaymentFromBuyerStore';
import style from './style.m.less';
import ContractorAutocomplete from '../../../commonComponents/ContractorAutocomplete';
import Description from '../../../commonComponents/Description';
import CurrencySumWithCourse from '../../../commonComponents/CurrencySumWithCourse/CurrencySumWithCourse';
import ActionEnum from '../../../../../../../enums/newMoney/ActionEnum';
import Documents from '../../../commonComponents/Documents';
import linkedDocumentService from '../../../../../../../services/newMoney/linkedDocumentService';
import { getSaveOperationButtonData, getSaveButtonTitle } from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';
import TaxationSystemType from '../../../commonComponents/TaxationSystemType';
import PatentDropdown from '../../../commonComponents/PatentDropdown';
import Nds from '../../../commonComponents/Nds';

const cn = classnames.bind(style);

// поступление оплата от покупателя валюта

@observer
class IncomingCurrencyPaymentFromBuyer extends React.Component {
    constructor(props) {
        super(props);

        this.store = new IncomingCurrencyPaymentFromBuyerStore({
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
                Kontragent: this.store.model.Kontragent.KontragentId ? this.store.model.Kontragent : {},
                Sum: this.store.model.Sum,
                Description: this.store.model.Description,
                OperationType: value,
                DocumentBaseId: this.store.model.DocumentBaseId,
                initialOperationNumber: this.store.initialOperationNumber,
                IncludeNds: this.store.model.IncludeNds
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

        const actions = actionArray
            .filter(action => action.value !== ActionEnum.DownloadAcc
                && action.value !== ActionEnum.DownloadPDF
                && action.value !== ActionEnum.DownloadXLS);

        const buttonData = getSaveOperationButtonData({
            saveButtonData: actions,
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
                <Button onClick={this.props.onCancel} color="white">Отмена</Button>
            </ElementsGroup>;
        }

        return <Button onClick={this.props.onCancel} color="white">Отмена</Button>;
    };

    render() {
        const {
            canShowTaxationSystemTypeDropdown, patentSelectVisible
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
                    <ContractorAutocomplete
                        operationStore={this.store}
                    />

                    <div className={cn(grid.row, style.sumRow)}>
                        {/** @todo временно убрана возможност выбирать договор <ContractAutocomplete /> */}
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
                            isShowNdsWarningIcon={this.store.isShowNdsWarningIcon}
                            currentNdsRateFromAccPolicy={this.store.currentNdsRateFromAccPolicy}
                        />
                    </div>
                    {(canShowTaxationSystemTypeDropdown || patentSelectVisible) &&
                        <div className={cn(grid.row, style.sumRow)}>
                            {canShowTaxationSystemTypeDropdown && <TaxationSystemType operationStore={this.store} />}
                            {patentSelectVisible && <PatentDropdown className={grid.col_6} operationStore={this.store} />}
                        </div>
                    }
                    <Description className={cn(grid.row, style.description)} />
                    <Documents
                        operationStore={this.store}
                        className={cn(grid.row, style.linkedDocuments)}
                        autocomplete={linkedDocumentService.salesCurrencyInvoiceAutocomplete}
                    />
                    <Postings />
                    <div className={cn(grid.row, style.buttons)}>
                        {this.renderButtons()}
                    </div>
                </React.Fragment>
            </Provider>
        );
    }
}

IncomingCurrencyPaymentFromBuyer.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func,
    activePatents: PropTypes.arrayOf(PropTypes.object)
};

export default IncomingCurrencyPaymentFromBuyer;
