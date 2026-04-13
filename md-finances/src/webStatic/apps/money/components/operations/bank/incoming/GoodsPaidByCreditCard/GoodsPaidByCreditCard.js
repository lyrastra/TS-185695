import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { observer, Provider } from 'mobx-react';
import SplitButton from '@moedelo/frontend-core-react/components/buttons/SplitButton';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import Link from '@moedelo/frontend-core-react/components/Link';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import GoodsPaidByCreditCardStore from './stores/GoodsPaidByCreditCardStore';
import {
    actionArray,
    actionEnum
} from '../../../../../../../resources/newMoney/saveButtonResource';
import SettlementAccountAndOperationType from '../../../commonComponents/SettlementAccountAndOperationType';
import HeaderOperation from '../../../commonComponents/HeaderOperation';
import Sum from '../../../commonComponents/Sum';
import AcquiringCommission from '../../../commonComponents/AcquiringCommission';
import Description from '../../../commonComponents/Description';
import Postings from '../../../commonComponents/Postings';
import TaxationSystemType from '../../../commonComponents/TaxationSystemType';
import PatentDropdown from '../../../commonComponents/PatentDropdown';
import SaleDate from '../../../commonComponents/SaleDate';
import Mediation from '../../../commonComponents/Mediation/Mediation';
import {
    getSaveButtonTitle,
    getSaveOperationButtonData
} from '../../../../../../../helpers/newMoney/operationSaveButtonsHelper';
import { getCommonOperationStoreData } from '../../helpers/operationStoreHelper';
import Nds from '../../../commonComponents/Nds/Nds';

import style from './style.m.less';

const cn = classnames.bind(style);

// банк эквайринг

@observer
class GoodsPaidByCreditCard extends React.Component {
    constructor(props) {
        super(props);

        this.store = new GoodsPaidByCreditCardStore({
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
                Sum: this.store.model.Sum,
                Description: this.store.model.Description,
                OperationType: value,
                DocumentBaseId: this.store.model.DocumentBaseId,
                initialOperationNumber: this.store.initialOperationNumber
            }
        });
    };

    getTooltip = () => {
        const content = (
            <div>Внимание! Не забудьте отразить в разделе Документы и ЭДО - Покупки - УПД от банка со статусом "1".&nbsp;
                <Link
                    target={`_blank`}
                    href={`https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dengi-prof/postupleniya-prof-2/kak-otobrazit-operatsii-po-ekvajringu-prof`}
                >
                    {` Инструкция.`}
                </Link>
            </div>
        );
    
        return (
            <Tooltip
                wrapperClassName={style.tooltip}
                width={300}
                position={Position.topLeft}
                content={content}
            />
        );
    }

    renderSaleDate = () => {
        return <React.Fragment>
            <div className={grid.col_4}>
                <SaleDate />
            </div>
            <div className={grid.col_1} />
        </React.Fragment>;
    }

    renderButtons = () => {
        const splitButtonData = actionArray.filter(item => item.value !== actionEnum.DownloadAcc
            && item.value !== actionEnum.DownloadPDF
            && item.value !== actionEnum.DownloadXLS);

        const {
            isSavingBlocked,
            canEdit,
            disabledSaveButton,
            onClickSave,
            model: { DocumentBaseId }
        } = this.store;

        const buttonData = getSaveOperationButtonData({
            saveButtonData: splitButtonData,
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
        const { isShowNdsBlock, isShowNdsTooltip } = this.store;
        
        return (
            <Provider operationStore={this.store}>
                <React.Fragment>
                    <HeaderOperation
                        onDelete={this.onDelete}
                    />
                    <div className={grid.row}>
                        <SettlementAccountAndOperationType onChangeOperationType={this.onChangeOperationType} />
                    </div>
                    {this.store.canShowMediation &&
                        <Mediation />}
                    <div className={cn(grid.row, this.store.canEdit && style.sumRow)}>
                        <div className={grid.col_4}>
                            <Sum />
                        </div>
                        {this.store.canEdit ? <div className={grid.col_2} /> : <div className={grid.col_1} />}
                        {this.store.canShowSaleDate && this.renderSaleDate() }
                        {this.store.canShowTaxationSystemTypeDropdown &&
                            <TaxationSystemType operationStore={this.store} />}
                        {this.store.patentSelectVisible &&
                            <PatentDropdown className={grid.col_6} operationStore={this.store} />}
                    </div>
                    <div className={cn(grid.row, this.store.canEdit && style.sumRow)}>
                        <AcquiringCommission />
                        <div className={grid.col_1} />
                        {isShowNdsBlock && <Nds
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
                            isShowNdsWarningIcon={false}
                            currentNdsRateFromAccPolicy={this.store.currentNdsRateFromAccPolicy}
                        /> }
                        {isShowNdsTooltip && this.getTooltip()}
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

GoodsPaidByCreditCard.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.object),
    onCancel: PropTypes.func,
    onDelete: PropTypes.func,
    onChangeOperationType: PropTypes.func,
    activePatents: PropTypes.arrayOf(PropTypes.shape({
        Id: PropTypes.number,
        ShortName: PropTypes.string
    }))
};

export default GoodsPaidByCreditCard;
