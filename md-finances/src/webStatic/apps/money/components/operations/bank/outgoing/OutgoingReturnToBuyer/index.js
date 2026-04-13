import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Button, { Color } from '@moedelo/frontend-core-react/components/buttons/Button';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Link from '@moedelo/frontend-core-react/components/Link';
import ReturnToBuyerStore from './stores/ReturnToBuyerStore';
import {
    actionArray
} from '../../../../../../../resources/newMoney/saveButtonResource';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import TaxationSystemType from '../../../commonComponents/TaxationSystemType';
import PatentDropdown from '../../../commonComponents/PatentDropdown';
import ContractorAutocomplete from '../../../commonComponents/ContractorAutocomplete';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import ContractAutocomplete from '../../../commonComponents/ContractAutocomplete';
import Sum from '../../../commonComponents/Sum';
import Description from '../../../commonComponents/Description';
import Postings from '../../../commonComponents/Postings';
import style from './style.m.less';
import BusyNumberModal from '../../../commonComponents/BusyNumberModal';
import Nds from '../../../commonComponents/Nds';
import SendToBankButton from '../../../commonComponents/SendToBankButton';
import SmsConfirmModal from '../../../commonComponents/SmsConfirmModal';
import SyntheticAccountCodesEnum from '../../../../../../../enums/SyntheticAccountCodesEnum';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';

const cn = classnames.bind(style);

// списание возврат покупателю

@observer
class ReturnToBuyer extends React.Component {
    constructor(props) {
        super(props);

        this.store = new ReturnToBuyerStore({
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
                Status: this.store.model.Status,
                OperationType: value,
                DocumentBaseId: this.store.model.DocumentBaseId,
                initialOperationNumber: this.store.initialOperationNumber
            }
        });
    }

    renderButtons = () => {
        const {
            canEdit,
            isSavingBlocked,
            canSendToBank,
            onClickSave,
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
                    onClick: onClickSave
                }}
                onSelect={onClickSave}
                loading={isSavingBlocked}
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
        const { canShowTaxationSystemTypeDropdown, patentSelectVisible, isAfter2025WithTaxation: { isAfter2025, IsUsn, IsOsno } } = this.store;

        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation onDelete={this.onDelete} />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                        <div className={grid.col_1} />
                        <div className={cn(grid.col_6, style.guideLinkContainer)}>
                            {IsOsno && <Link href="https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dokumenty-prof/ukd-prodaga" target="_blank">Как отразить вычет НДС</Link>}
                        </div>
                    </div>
                    <ContractorAutocomplete
                        operationStore={this.store}
                    />
                    <ContractAutocomplete />
                    <div className={cn(grid.row, this.store.canEdit && style.sumRow)}>
                        <div className={grid.col_3}>
                            <Sum />
                        </div>
                        <div className={grid.col_1} />
                        {(IsOsno || (isAfter2025 && IsUsn)) && <Nds
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
                        />}
                        {canShowTaxationSystemTypeDropdown && <TaxationSystemType operationStore={this.store} />}
                        {patentSelectVisible && <PatentDropdown className={grid.col_6} operationStore={this.store} />}
                    </div>
                    <Description className={cn(grid.row, style.description)} />
                    <Postings
                        kontragentAccountCodes={{ main: SyntheticAccountCodesEnum._62_02, other: SyntheticAccountCodesEnum._76_06 }}
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

ReturnToBuyer.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    activePatents: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default ReturnToBuyer;
