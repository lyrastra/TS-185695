import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { toJS } from 'mobx';
import { observer, Provider } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Switch from '@moedelo/frontend-core-react/components/Switch';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import { actionArray } from '../../../../../../../resources/newMoney/saveButtonResource';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Postings from '../../../commonComponents/Postings';
import OtherStore from './stores/OtherStore';
import style from './style.m.less';
import ContractorAutocomplete from '../../../commonComponents/ContractorAutocomplete';
import ContractAutocomplete from '../../../commonComponents/ContractAutocomplete';
import Sum from '../../../commonComponents/Sum';
import Nds from '../../../commonComponents/Nds';
import Description from '../../../commonComponents/Description';
import Bills from '../../../commonComponents/Bills';
import NoAutoDeleteOperationSwitch from '../../../commonComponents/NoAutoDeleteOperationSwitch';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import TaxationSystemType from '../../../commonComponents/TaxationSystemType';
import PatentDropdown from '../../../commonComponents/PatentDropdown';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';

const cn = classnames.bind(style);

// поступление прочее

@observer
class Other extends React.Component {
    constructor(props) {
        super(props);

        this.store = new OtherStore({
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
                WorkerName: this.store.contractorIsWorker && this.store.model.Kontragent.KontragentName,
                SalaryWorkerId: this.store.model.Kontragent.SalaryWorkerId,
                Sum: this.store.model.Sum,
                IncludeNds: this.store.model.IncludeNds,
                NdsSum: this.store.model.NdsSum,
                NdsType: this.store.model.NdsType,
                Description: this.store.model.Description,
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
                <Button onClick={this.props.onCancel} color="white">Отмена</Button>
            </ElementsGroup>;
        }

        return <Button onClick={this.props.onCancel} color="white">Отмена</Button>;
    };

    renderTargetIncomeSwitch = () => {
        return (
            <Switch
                text={`Целевое поступление`}
                onChange={this.store.setIsTargetIncome}
                checked={this.store.model.IsTargetIncome}
                disabled={!this.store.canEdit}
            />
        );
    }

    render() {
        const {
            canShowTaxationSystemTypeDropdown, patentSelectVisible, isIp, isOsno, isUsn
        } = this.store;
        const isIpOsno = isOsno && isIp;
        const isIpUsn = isUsn && isIp;

        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation
                        onDelete={this.onDelete}
                    />
                    <div className={cn(grid.row, style.underHeadRow)}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                        <NoAutoDeleteOperationSwitch />
                    </div>
                    {(isIpUsn) &&
                        <div className={grid.row}>
                            {this.renderTargetIncomeSwitch()}
                        </div>
                    }
                    <ContractorAutocomplete
                        useMask={this.store.needToUseSettlementAccountMask}
                        operationStore={this.store}
                    />
                    <ContractAutocomplete canAddContract={!this.store.contractorIsWorker} />
                    <div className={cn(grid.row, style.sumRow)}>
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
                            isShowNdsWarningIcon={this.store.isShowNdsWarningIcon}
                            currentNdsRateFromAccPolicy={this.store.currentNdsRateFromAccPolicy}
                        />
                    </div>
                    {(isIpOsno && (canShowTaxationSystemTypeDropdown || patentSelectVisible)) &&
                        <div className={cn(grid.row, style.sumRow)}>
                            {canShowTaxationSystemTypeDropdown && <TaxationSystemType operationStore={this.store} />}
                            {patentSelectVisible && <PatentDropdown className={grid.col_6} operationStore={this.store} />}
                        </div>
                    }
                    <Description className={cn(grid.row, style.description)} />
                    <Bills className={cn(grid.row, style.bills)} />
                    <Postings />
                    <div className={cn(grid.row, style.buttons)}>
                        {this.renderButtons()}
                    </div>
                </React.Fragment>
            </Provider>
        );
    }
}

Other.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func,
    activePatents: PropTypes.arrayOf(PropTypes.object)
};

export default Other;
