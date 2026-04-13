/* eslint-disable react/no-multi-comp */

import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import cn from 'classnames';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Input, { Size as InputSize } from '@moedelo/frontend-core-react/components/Input';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import Button, { Color as ButtonColor } from '@moedelo/frontend-core-react/components/buttons/Button';
import P from '@moedelo/frontend-core-react/components/P';
import NotificationPanel, { NotificationPanelType } from '@moedelo/frontend-core-react/components/NotificationPanel';
import Link, { Type } from '@moedelo/frontend-core-react/components/Link';
import Arrow from '@moedelo/frontend-core-react/components/Arrow';
import CollapsiblePanel from '@moedelo/frontend-core-react/components/CollapsiblePanel';
import YouTubeVideo from '@moedelo/frontend-core-react/components/YouTubeVideo';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import navigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import H1 from '@moedelo/frontend-core-react/components/headers/H1';
import requisitesService from '@moedelo/frontend-common-v2/apps/requisites/services/requisitesService';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

import OperationTypeSelect from './components/OperationTypeSelect';
import RuleActionType from '../../enums/RuleActionType';
import { getConditionTypeDropdownData } from '../../helpers/dropdownDataHelpers';
import ConditionRow from './components/ConditionRow';
import ActionTypeSelect from './components/ActionTypeSelect';
import CheckRuleResult from './components/CheckRuleResult';
import ControlButtons from './components/ControlButtons';
import {
    filterConditionsAvailableForContractor,
    getConditionsAndUpdateById,
    getConditionsWithoutOneById
} from '../../helpers/conditionsHelper';
import {
    getDefaultModelForActionType,
    getEmptyCondition,
    getNegativeConditionsIsHidden,
    getRequiredConditionObject
} from '../../helpers/settingsHelper';
import {
    affectedRuleValidationFields,
    isValidForSave,
    validateConditions,
    validateRule
} from '../../helpers/validateHelper';
import {
    getAvailableOperationTypesAsync,
    getIgnoreNumberDataAsync,
    getMediationDataAsync,
    getOneRuleAsync,
    getOperationsAffectedByRuleAsync,
    getTaxationSystemsDataAsync,
    saveRuleAsync,
    updateRuleAsync
} from '../../services/paymentImportRulesService';
import { buildRuleModelForCheck, buildRuleModelForSave, mapConditionOperationTypes } from '../../helpers/paymentImportRulesMapper';
import style from './style.m.less';
import { metrics, sendMetric } from '../../helpers/metricsHelper';
import TaxationSystemTypeSelect from './components/TaxationSystemTypeSelect';
import RuleConditionObject from '../../enums/RuleConditionObject';
import { ApplyRuleSettingContextProvider } from './components/CheckRuleResult/components/ApplyRuleSetting';
import { paymentOrderOperationResources } from '../../../../resources/MoneyOperationTypeResources';
import EmployeeAutocomplete from './components/EmployeeAutocomplete';
import ContractAutocomplete from './components/ContractAutocomplete';
import TaxableSumTypeResource from '../../resources/TaxableSumTypeResource';

/** Максимальное кол-во условий для правила */
const limitConditionsCount = 10;

/** Кол-во операций попадающих под правило на "странице" (сколько выводит кнопка показать еще) */
const checkResultLimit = 10;

const defaultActionType = RuleActionType.ChangeOperationType;

const getDefaultConditions = () => [getEmptyCondition(defaultActionType)];

const getDefaultData = propsId => {
    const defaultModel = getDefaultModelForActionType(defaultActionType);

    return { ...defaultModel, id: propsId || null };
};

const contractAvailableOperationTypes = [
    paymentOrderOperationResources.PaymentOrderIncomingLoanObtaining.value,
    paymentOrderOperationResources.PaymentOrderIncomingLoanReturn.value,
    paymentOrderOperationResources.PaymentOrderOutgoingLoanIssue.value,
    paymentOrderOperationResources.PaymentOrderOutgoingLoanRepayment.value,
    paymentOrderOperationResources.PaymentOrderIncomingIncomeFromCommissionAgent.value,
    paymentOrderOperationResources.PaymentOrderOutgoingPaymentAgencyContract.value
];

const OneRulePage = ({ id: propsId }) => {
    const [conditions, setConditions] = useState(getDefaultConditions);
    const [data, setData] = useState(() => getDefaultData(propsId));
    const [inProgress, setInProgress] = useState(false);
    const [isResultTableLoading, setIsResultTableLoading] = useState(false);
    const [resultCount, setResultCount] = useState(0);
    const [resultData, setResultData] = useState([]);
    const [isInstructionShowed, setIsInstructionShowed] = useState(false);
    const [isRuleChecked, setIsRuleChecked] = useState(false);
    const [isShowWarning, setIsShowWarning] = useState(true);
    const [operationTypes, setOperationTypes] = useState(null);
    const [taxationSystemsData, setTaxationSystemsData] = useState(null);
    const [ignoreNumberData, setIgnoreNumberOperationData] = useState(null);
    const [mediationData, setMediationData] = useState(null);
    const [lastClosedPeriodDate, setLastClosedPeriodDate] = useState(null);
    const [firmTaxationSystem, setFirmTaxationSystem] = useState(null);

    const isNew = !data.id;
    const negativeConditionsIsHidden = React.useMemo(() => {
        return getNegativeConditionsIsHidden(data.actionType);
    }, [data.actionType]);
    const requiredConditionObject = React.useMemo(() => {
        return getRequiredConditionObject(data.actionType);
    }, [data.actionType]);
    const conditionOperationTypes = React.useMemo(() => {
        if (data.actionType === RuleActionType.ChangeTaxationSystem) {
            return taxationSystemsData;
        }

        if (data.actionType === RuleActionType.ChangeIgnoreNumber) {
            return ignoreNumberData;
        }

        if (data.actionType === RuleActionType.ChangeMediation) {
            return mediationData;
        }

        return [];
    }, [data.actionType, ignoreNumberData, mediationData, taxationSystemsData]);
    const isApplyRuleVisible = resultCount > 0 && data.actionType === RuleActionType.ChangeIgnoreNumber;

    useEffect(() => {
        const fetch = async () => {
            setInProgress(true);
            const types = await getAvailableOperationTypesAsync();
            setOperationTypes(types);

            const taxationSystems = await getTaxationSystemsDataAsync();
            setTaxationSystemsData(taxationSystems);

            const ignoreNumberOperationTypes = await getIgnoreNumberDataAsync();
            setIgnoreNumberOperationData(ignoreNumberOperationTypes);

            const mediationTypes = await getMediationDataAsync();
            setMediationData(mediationTypes);

            const lastClosedPeriod = await requisitesService.getDocumentMinDate({ useClosePeriod: true });
            setLastClosedPeriodDate(lastClosedPeriod);

            const firmTaxSystem = await taxationSystemService.getTaxSystem();
            setFirmTaxationSystem(firmTaxSystem);

            if (propsId) {
                const { conditions: ruleConditions, ...ruleData } = await getOneRuleAsync(propsId);

                setData(ruleData);
                setConditions(ruleConditions);
            }

            setInProgress(false);
        };

        fetch();
    }, [propsId]);

    const isAvailableWithContractor = ({ operationType, actionType }) => {
        if (actionType === RuleActionType.ChangeOperationType) {
            return operationTypes?.find(x => x.OperationType === operationType)?.AvailableWithContractor;
        }

        const actionsWithOperationTypeCondition = [RuleActionType.ChangeTaxationSystem, RuleActionType.ChangeIgnoreNumber, RuleActionType.ChangeMediation];

        if (actionsWithOperationTypeCondition.includes(actionType)) {
            const operationTypeFromCondition = conditions.find(x => x.type === RuleConditionObject.OperationType)?.operationType;

            return operationTypes?.find(x => x.OperationType === operationTypeFromCondition)?.AvailableWithContractor;
        }

        return true;
    };

    const isAvailableWithCommissionAgent = ({ operationType, actionType }) => {
        if (actionType === RuleActionType.ChangeOperationType) {
            return operationType === paymentOrderOperationResources.PaymentOrderIncomingIncomeFromCommissionAgent.value;
        }

        return false;
    };

    const updateActionType = actionType => {
        const model = { ...data, ...getDefaultModelForActionType(actionType) };

        const initConditions = [getEmptyCondition(actionType)];

        setData(model);
        setConditions(initConditions);
        setIsRuleChecked(false); // скрываем таблицу результатов
    };

    const updateRule = patch => {
        const newData = { ...data, ...patch };
        const patchKeys = Object.keys(patch);
        const needValidateKeys = patchKeys.filter(patchKey => affectedRuleValidationFields.includes(patchKey));
        const concreteValidateField = needValidateKeys.length === 1 ? needValidateKeys[0] : null;

        setConditions(filterConditionsAvailableForContractor(conditions, isAvailableWithContractor({
            operationType: newData.operationType,
            actionType: newData.actionType
        })));

        setData(needValidateKeys.length ? validateRule(newData, concreteValidateField) : newData);
        setIsRuleChecked(false); // скрываем таблицу результатов
    };

    const updateApplyToOperations = ({ checked }) => {
        setData({ ...data, applyToOperations: checked });
    };

    const updateStartDate = ({ value }) => {
        const minDateObject = dateHelper(lastClosedPeriodDate);
        const newDateObject = dateHelper(value);
        const dateObjectToSet = minDateObject.isBefore(newDateObject)
            ? newDateObject
            : minDateObject;

        setData({ ...data, startDate: dateObjectToSet.format(`DD.MM.YYYY`) });
    };

    const addConditionRow = () => {
        setConditions([...conditions, ...getDefaultConditions()]);
        setIsRuleChecked(false); // скрываем таблицу результатов
    };

    const deleteConditionRow = idDel => {
        setConditions(getConditionsWithoutOneById(conditions, idDel));
        setIsRuleChecked(false); // скрываем таблицу результатов
    };

    const updateConditionRow = (idCond, patch) => {
        setConditions(getConditionsAndUpdateById(conditions, idCond, patch));
        setIsRuleChecked(false); // скрываем таблицу результатов
    };

    const checkRulesHandler = async () => {
        setInProgress(true);

        const ruleData = validateRule(data);
        const conditionsData = validateConditions(conditions);

        setData(ruleData);
        setConditions(conditionsData);
        sendMetric(metrics.one_rule_check_rule_button_click);

        if (!isValidForSave(ruleData, conditionsData)) {
            setInProgress(false);

            return;
        }

        try {
            const modelForCheck = buildRuleModelForCheck(ruleData, conditionsData);
            const { totalCount, data: operationsAffectedByRule } = await getOperationsAffectedByRuleAsync({
                data: modelForCheck,
                limit: checkResultLimit,
                offset: 0
            });

            setResultCount(totalCount);
            setResultData(operationsAffectedByRule);

            setIsRuleChecked(true);
        } catch (e) {
            NotificationManager.show({
                message: `При проверке возникла ошибка`,
                type: `error`,
                duration: 3000
            });
        } finally {
            setInProgress(false);
        }
    };

    const onLoadMoreHandler = async ({ offset, limit }) => {
        setIsResultTableLoading(true);
        const modelForCheck = buildRuleModelForCheck(data, conditions);
        const { totalCount, data: operationsAffectedByRule } = await getOperationsAffectedByRuleAsync({
            data: modelForCheck,
            limit,
            offset
        });

        setResultCount(totalCount);
        setResultData([...resultData, ...operationsAffectedByRule]);
        setIsResultTableLoading(false);
    };

    const onSaveHandler = async () => {
        setInProgress(true);

        const ruleData = validateRule(data);
        const conditionsData = validateConditions(conditions);

        setData(ruleData);
        setConditions(conditionsData);
        sendMetric(metrics.one_rule_save_button_button_click, isNew);

        if (!isValidForSave(ruleData, conditionsData)) {
            setInProgress(false);

            return;
        }

        const modelForSave = buildRuleModelForSave(data, conditions);

        try {
            if (!isNew) {
                await updateRuleAsync(data.id, modelForSave);
                navigateHelper.back();
                NotificationManager.show({
                    message: `Обновлено`,
                    type: `success`,
                    duration: 3000
                });
            } else {
                const { data: { RuleId } } = await saveRuleAsync(modelForSave);
                updateRule({ id: RuleId });
                navigateHelper.back();
                NotificationManager.show({
                    message: `Сохранено`,
                    type: `success`,
                    duration: 3000
                });
            }
        } catch (e) {
            NotificationManager.show({
                message: `При сохранении возникла ошибка`,
                type: `error`,
                duration: 3000
            });
        } finally {
            setInProgress(false);
        }
    };

    const onCancelHandler = async () => {
        sendMetric(metrics.one_rule_cancel_button_button_click);
        navigateHelper.back();
    };

    const onHideWarningHandler = () => {
        setIsShowWarning(false);
    };

    const renderOperationTypeSelect = () => {
        return <OperationTypeSelect
            operationTypes={operationTypes}
            value={data.operationType}
            onChange={value => updateRule({
                operationType: value, contractId: null, employeeId: null, taxableSumType: null, syntheticAccountCode: null
            })}
            className={cn(style.typeSelect, `qa-paymentImportRulesFormOperationType`)}
            disabled={data.isDeleted || inProgress}
            error={data.operationTypeError}
        />;
    };

    const renderTaxationSystemTypeSelect = () => {
        // если попытаться рисовать TaxationSystemTypeSelect, когда не удалось получить condition и вытащить из него operationType,
        // то при отрисовке значение Dropdown сбросится в первое попавшееся
        // поэтому, пока operationType не получен, не отрисосываем
        return conditions.find(cond => cond.operationType)?.operationType
            ? <TaxationSystemTypeSelect
                    taxationSystemsData={taxationSystemsData}
                    value={data?.taxationSystemType}
                    onChange={value => updateRule({ taxationSystemType: value })}
                    className={cn(style.typeSelect, `qa-paymentImportRulesFormTaxationSystems`)}
                    disabled={data.isDeleted || inProgress}
                    operationType={conditions.find(cond => cond.operationType)?.operationType}
                    error={data.taxationSystemTypeError}
            />
            : false;
    };

    const renderActionTypeForm = () => {
        if (data.actionType === RuleActionType.ChangeOperationType) {
            return renderOperationTypeSelect();
        }

        if (data.actionType === RuleActionType.ChangeTaxationSystem) {
            return renderTaxationSystemTypeSelect();
        }

        return null;
    };

    const renderEmployeeAutocomplete = () => {
        const operationTypesWithEmployee = [
            paymentOrderOperationResources.PaymentToAccountablePerson.value,
            paymentOrderOperationResources.PaymentOrderIncomingReturnFromAccountablePerson.value
        ];

        if (operationTypesWithEmployee.includes(data.operationType)) {
            return <EmployeeAutocomplete
                selectEmployee={value => {
                    const newData = { ...data, employeeId: value };
                    setData(newData);
                }}
                employeeId={data.employeeId}
                startDate={data.startDate}
                disabled={data.isDeleted || inProgress}
            />;
        }

        return null;
    };

    const renderContractAutocomplete = () => {
        if (contractAvailableOperationTypes.includes(data.operationType)) {
            return <ContractAutocomplete
                selectContractId={value => {
                    const newData = { ...data, contractId: value?.Id, kontragentName: value?.KontragentName };
                    setData(newData);
                }}
                contractId={data.contractId}
                operationType={data.operationType}
                disabled={data.isDeleted || inProgress}
                conditions={conditions}
            />;
        }

        return null;
    };

    const canShowMediationRule = () => {
        return !firmTaxationSystem?.IsOsno;
    };

    const onInstructionClick = () => setIsInstructionShowed(!isInstructionShowed);

    const applyRuleContext = {
        visible: isApplyRuleVisible,
        checked: data.applyToOperations,
        startDate: data.startDate,
        minStartDate: lastClosedPeriodDate,
        onCheckedChange: updateApplyToOperations,
        onStartDateChange: updateStartDate
    };

    const renderTaxableSumType = () => {
        const { taxableSumType, isDeleted, operationType } = data;
        const isAvailableTaxableSumType = operationTypes?.find(({ OperationType }) => OperationType === operationType)?.AvailableTaxableSumType;

        if (!isAvailableTaxableSumType) {
            return null;
        }

        const taxableSumData = firmTaxationSystem?.IsOsno ? TaxableSumTypeResource : TaxableSumTypeResource.slice(0, 2);

        return <div className={cn(grid.row, grid.rowLarge)}>
            <P className={cn(grid.col_3, style.formVerticalCentred)}>
                <strong className={cn({ [style.muteColorText]: isDeleted || inProgress })}>Проводка в НУ</strong>
            </P>
            <div className={cn(grid.col_5, style.formVerticalCentred)}>
                <Dropdown
                    className={`qa-paymentImportTaxableSumType`}
                    data={taxableSumData}
                    onSelect={({ value }) => {
                        updateRule({ taxableSumType: value });
                    }}
                    value={taxableSumType}
                    disabled={isDeleted || inProgress}
                />
            </div>
        </div>;
    };

    const renderAccPostings = () => {
        const { syntheticAccountCode, isDeleted, operationType } = data;
        const availableAccPosting = operationTypes?.find(({ OperationType }) => OperationType === operationType)?.AvailableAccPosting;

        if (!availableAccPosting) {
            return null;
        }

        const syntheticAccountCodeFromPostingsData = () => availableAccPosting.Accounts.find(({ Code }) => Code === syntheticAccountCode);
        const code = typeof syntheticAccountCode === `object` ? syntheticAccountCode : syntheticAccountCodeFromPostingsData();

        const mappedAvailableAccPosting = availableAccPosting.Accounts.map(value => ({ text: value.Number, value, description: value.Name }));

        const renderSubcontos = () => {
            if (!syntheticAccountCode) {
                return null;
            }

            const [firstSubconto, secondSubconto] = code.Subcontos;

            return <React.Fragment>
                {firstSubconto && <div className={cn(grid.col_6, style.formVerticalCentred)}>
                    <Input showAsText value={firstSubconto.Name} label={`Объект учета 1`} />
                </div>}
                {secondSubconto && <div className={cn(grid.col_6, style.formVerticalCentred)}>
                    <Input showAsText value={secondSubconto.Name} label={`Объект учета 2`} />
                </div>}
            </React.Fragment>;
        };

        return <div className={cn(grid.row, grid.rowLarge)}>
            <P className={cn(grid.col_3, style.formVerticalCentred)}>
                <strong className={cn({ [style.muteColorText]: isDeleted || inProgress })}>Проводка в БУ</strong>
            </P>
            <div className={cn(grid.col_6, style.formVerticalCentred)}>
                <Dropdown
                    className={`qa-paymentImportAccPosting`}
                    data={mappedAvailableAccPosting}
                    onSelect={({ value }) => {
                        updateRule({ syntheticAccountCode: value });
                    }}
                    value={code}
                    disabled={isDeleted || inProgress}
                    label={availableAccPosting.Direction === Direction.Outgoing ? `Счет по дебету` : `Счет по кредиту`}
                />
            </div>
            {renderSubcontos()}
        </div>;
    };

    return <React.Fragment>
        <section className={grid.wrapper}>
            {data?.isDeleted && <NotificationPanel type={NotificationPanelType.warning} canClose={false} className={style.notification}>
                Правило импорта было удалено.
            </NotificationPanel>}
            <H1>{isNew ? `Создание правила импорта` : `Редактирование правила импорта`}</H1>
            <CollapsiblePanel
                opened={isInstructionShowed}
                onOpen={onInstructionClick}
                onClose={onInstructionClick}
                panelClassName={style.instruction}
                className={style.instruction__wrapper}
                content={<YouTubeVideo
                    videoId={`K3_sm1D7Npw`}
                    height={280}
                    width={500}
                    alt={`Как создать правило импорта`}
                />}
            >
                <React.Fragment>
                    <Link
                        type={Type.modal}
                        text={`Видеоинструкция`}
                        onClick={onInstructionClick}
                    />
                    <Arrow
                        className={style.instructionArrow}
                        direction={isInstructionShowed ? `up` : `down`}
                    />
                </React.Fragment>
            </CollapsiblePanel>
            <div className={cn(grid.rowLarge)}>
                <Input
                    size={InputSize.large}
                    maxLength={50}
                    placeholder="Название правила"
                    className={cn(`qa-paymentImportRulesFormName`, grid.col_12)}
                    onChange={({ value }) => updateRule({ name: value })}
                    value={data.name}
                    disabled={data.isDeleted || inProgress}
                    error={!!data.nameError}
                    message={data.nameError}
                />
            </div>
            <div className={cn(grid.row, grid.rowLarge)}>
                <P className={cn(grid.col_3, style.formVerticalCentred)}>
                    <strong className={cn({ [style.muteColorText]: data.isDeleted || inProgress })}>Определить</strong>
                </P>
                <div className={cn(grid.col_5, style.formVerticalCentred)}>
                    <ActionTypeSelect
                        onChange={value => updateActionType(value)}
                        value={data.actionType}
                        className={cn(style.typeSelect, `qa-paymentImportRulesFormActionType`)}
                        disabled={data.isDeleted || inProgress}
                        hasTaxationSystems={!!taxationSystemsData?.length}
                        showMediation={canShowMediationRule()}
                    />
                </div>
                <div className={cn(grid.col_10, style.formVerticalCentred)}>
                    {renderActionTypeForm()}
                </div>
            </div>
            {renderEmployeeAutocomplete()}
            {renderContractAutocomplete()}
            {renderAccPostings()}
            {renderTaxableSumType()}
            <div className={grid.row}>
                <P className={grid.col_2}>
                    <strong className={cn({ [style.muteColorText]: data.isDeleted || inProgress })}>Если</strong>
                </P>
                <Dropdown
                    className={cn(grid.col_7, `qa-paymentImportRulesConditionType`)}
                    data={getConditionTypeDropdownData(data.actionType)}
                    value={data.conditionType}
                    onSelect={({ value }) => {
                        updateRule({ conditionType: value });
                    }}
                    disabled={data.isDeleted || inProgress}
                />
            </div>
            {
                conditions.map((condition, index) => {
                    return <ConditionRow
                        key={condition.id}
                        index={index}
                        conditionType={data.conditionType}
                        condition={condition}
                        onChange={updateConditionRow}
                        onDelete={deleteConditionRow}
                        showCommissionAgentCondition={isAvailableWithCommissionAgent({
                            actionType: data.actionType,
                            operationType: data.operationType
                        })}
                        showContractorCondition={isAvailableWithContractor({
                            actionType: data.actionType,
                            operationType: data.operationType
                        })}
                        requiredConditionObject={requiredConditionObject}
                        actionType={data.actionType}
                        negativeConditionsIsHidden={negativeConditionsIsHidden}
                        disabled={data.isDeleted || inProgress}
                        operationTypes={mapConditionOperationTypes(conditionOperationTypes)}
                        kontragentForQuery={data.kontragentName}
                    />;
                })
            }
            <div className={cn(grid.row, grid.rowLarge)}>
                {conditions.length < limitConditionsCount && <Button
                    className={`qa-addConditionButton`}
                    color={ButtonColor.White}
                    onClick={addConditionRow}
                    disabled={data.isDeleted || inProgress}
                >
                    {svgIconHelper.getJsx({ name: `plus` })}
                    Условие
                </Button>}
            </div>
            <div className={cn(grid.row, style.checkRuleBtnContainer)}>
                {!isRuleChecked && <Button
                    className={`qa-paymentImportRulesFormCheckRuleButton`}
                    loading={inProgress}
                    disabled={data.isDeleted}
                    onClick={checkRulesHandler}
                >
                    Проверить правило
                </Button>}
            </div>
        </section>
        <section>
            {isRuleChecked &&
                <ApplyRuleSettingContextProvider value={applyRuleContext}>
                    <CheckRuleResult
                        totalCount={resultCount}
                        data={resultData}
                        onLoadMore={onLoadMoreHandler} // with offset and limit attributes
                        isTableLoading={isResultTableLoading}
                        limit={checkResultLimit}
                        isShowWarning={isShowWarning}
                        onHideWarning={onHideWarningHandler}
                    />
                </ApplyRuleSettingContextProvider>
            }
            {(!isNew || isRuleChecked || data.isDeleted) &&
            <ControlButtons onSave={onSaveHandler} onCancel={onCancelHandler} isInProgress={inProgress} isDisabled={data.isDeleted} />
            }
        </section>
    </React.Fragment>;
};

OneRulePage.propTypes = {
    id: PropTypes.string
};

export default OneRulePage;
