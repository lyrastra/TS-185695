import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { toJS } from 'mobx';
import { observer, Provider } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import {
    actionArray,
    actionEnum
} from '../../../../../../../resources/newMoney/saveButtonResource';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Postings from '../../../commonComponents/Postings';
import OutgoingCurrencyOtherStore from './stores/OutgoingCurrencyOtherStore';
import style from './style.m.less';
import ContractorAutocomplete from '../../../commonComponents/ContractorAutocomplete';
import ContractAutocomplete from '../../../commonComponents/ContractAutocomplete';
import Description from '../../../commonComponents/Description';
import CurrencySumWithCourse from '../../../commonComponents/CurrencySumWithCourse/CurrencySumWithCourse';
import ActionEnum from '../../../../../../../enums/newMoney/ActionEnum';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';
import Nds from '../../../commonComponents/Nds';

const cn = classnames.bind(style);

// списание валюта прочее

@observer
class OutgoingCurrencyOther extends React.Component {
    constructor(props) {
        super(props);

        this.store = new OutgoingCurrencyOtherStore({
            ...getCommonOperationStoreData(props),
            operationTypes: props.operationTypes
        });
    }

    onSelectAction = async ({ value }) => {
        const { SavedBaseId, data: { DocumentBaseId = null } = {} } = await this.store.save();

        if (value === actionEnum.CreateNew) {
            NavigateHelper.push(`reload/add/outgoing/settlement/${this.store.model.SettlementAccountId}`);

            return;
        }

        const baseId = SavedBaseId || DocumentBaseId;

        await this.store.download({ baseId, format: value });
        this.props.onSave(toJS(this.store.model));
    };

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
                Description: this.store.model.Description,
                OperationType: value,
                DocumentBaseId: this.store.model.DocumentBaseId,
                Status: this.store.model.Status,
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
                    onSelect={this.onSelectAction}
                    loading={isSavingBlocked}
                    disabled={disabledSaveButton}
                >{getSaveButtonTitle({ documentBaseId: DocumentBaseId })}</SplitButton>
                <Button onClick={this.props.onCancel} color="white">Отмена</Button>
            </ElementsGroup>;
        }

        return <Button onClick={this.props.onCancel} color="white">Отмена</Button>;
    };

    render() {
        const { contractorIsWorker, isSelfKontragent } = this.store;

        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation
                        onDelete={this.onDelete}
                        hideStatus
                    />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                    </div>
                    <ContractorAutocomplete
                        operationStore={this.store}
                    />
                    <ContractAutocomplete canAddContract={!contractorIsWorker && !isSelfKontragent} />
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
                    <Postings />
                    <div className={cn(grid.row, style.buttons)}>
                        {this.renderButtons()}
                    </div>
                </React.Fragment>
            </Provider>
        );
    }
}

OutgoingCurrencyOther.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onSave: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func
};

export default OutgoingCurrencyOther;
