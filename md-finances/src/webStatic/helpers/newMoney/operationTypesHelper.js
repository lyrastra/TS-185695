import DirectionEnum from '@moedelo/frontend-enums/mdEnums/Direction';
import UsnType from '@moedelo/frontend-enums/mdEnums/UsnType';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { paymentOrderOperationResources } from '../../resources/MoneyOperationTypeResources';
import {
    getOperationTypeByDirectionAndLegalTypeAndSource,
    isCurrency,
    hasCommissionAgents
} from '../../helpers/MoneyOperationHelper';
import MoneySourceType from '../../enums/MoneySourceType';
import LegalType from '../../enums/LegalTypeEnum';
import OpenOperationActions from '../../enums/newMoney/OpenOperationActionsEnum';

const availableOnTransitAccount = [
    paymentOrderOperationResources.PaymentOrderOutgoingCurrencySale.value,
    paymentOrderOperationResources.PaymentOrderIncomingCurrencySale.value,
    paymentOrderOperationResources.PaymentOrderIncomingCurrencyOther.value,
    paymentOrderOperationResources.PaymentOrderIncomingCurrencyPaymentFromBuyer.value,
    paymentOrderOperationResources.PaymentOrderOutgoingCurrencyTransferToAccount.value,
    paymentOrderOperationResources.PaymentOrderIncomingCurrencyFromAnotherAccount.value,
    paymentOrderOperationResources.PaymentOrderOutgoingCurrencyOther.value,
    paymentOrderOperationResources.PaymentOrderIncomingCurrencyPurchase.value
];

const availableOnCurrencyAccount = [
    paymentOrderOperationResources.PaymentOrderOutgoingCurrencySale.value,
    paymentOrderOperationResources.PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods.value,
    paymentOrderOperationResources.PaymentOrderOutgoingCurrencyBankFee.value,
    paymentOrderOperationResources.PaymentOrderIncomingCurrencyPurchase.value,
    paymentOrderOperationResources.PaymentOrderIncomingCurrencyOther.value,
    paymentOrderOperationResources.PaymentOrderOutgoingCurrencyOther.value,
    paymentOrderOperationResources.PaymentOrderIncomingCurrencyPaymentFromBuyer.value,
    paymentOrderOperationResources.PaymentOrderIncomingCurrencyFromAnotherAccount.value,
    paymentOrderOperationResources.PaymentOrderOutgoingCurrencyTransferToAccount.value
];

export function getOperationTypes({
    isOoo, hasPurseAccount, operation, settlementAccounts = [], taxationSystem, isOutsourceTariff
}) {
    const rubCode = [643, 810];
    const { Direction, isInitialTransferFromAnotherAccount, hasAccessToMarketplacesAndCommissionAgents } = operation;
    const legalType = isOoo ? LegalType.Ooo : LegalType.Ip;
    let operationTypesList = getOperationTypeByDirectionAndLegalTypeAndSource(Direction, legalType, MoneySourceType.SettlementAccount);
    const settlementAccount = settlementAccounts.find(account => account.Id === operation.SettlementAccountId);
    const isRubAccount = settlementAccount && rubCode.includes(settlementAccount.Currency);
    const IsTransit = settlementAccount && settlementAccount.IsTransit;
    const usnWithAvailableCurrency = taxationSystem.IsUsn && (taxationSystem.UsnType === UsnType.Profit || taxationSystem.UsnType === UsnType.ProfitAndOutgo);
    const hasCurrencyAccount = !!settlementAccounts.find(account => !rubCode.includes(account.Currency));
    const hasSeveralRubAccount = settlementAccounts.filter(account => rubCode.includes(account.Currency)).length > 1;
    const needToKeepCurrencyTypes = isPurchaseOrSellCurrencyFromWarningTable(operation);
    const isIpOsno = taxationSystem.IsOsno && !isOoo;

    if (isIpOsno && !isOutsourceTariff) {
        const ipOsnoHiddenOperationList = [
            paymentOrderOperationResources.PaymentOrderIncomingMediationFee.value,
            paymentOrderOperationResources.PaymentOrderOutgoingPaymentAgencyContract.value,
            paymentOrderOperationResources.RentPayment.value
        ];
        operationTypesList = operationTypesList.filter(operationTypeItem => !ipOsnoHiddenOperationList.includes(operationTypeItem.value));
    }

    if ((!hasCurrencyAccount && !needToKeepCurrencyTypes) || !usnWithAvailableCurrency) {
        operationTypesList = operationTypesList.filter(operationTypeItem => !isCurrency(operationTypeItem.value));
    }

    if (!hasAccessToMarketplacesAndCommissionAgents) {
        operationTypesList = operationTypesList.filter(operationTypeItem => !hasCommissionAgents(operationTypeItem.value));
    }

    switch (Direction) {
        case DirectionEnum.Incoming: {
            if (!hasPurseAccount) {
                operationTypesList = operationTypesList.filter(operationTypeItem => operationTypeItem.value !== paymentOrderOperationResources.PaymentOrderIncomingFromPurse.value);
            }

            if (!isInitialTransferFromAnotherAccount) {
                operationTypesList = operationTypesList.filter(operationTypeItem => operationTypeItem.value !== paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount.value);
            }

            break;
        }

        case DirectionEnum.Outgoing: {
            if (!hasSeveralRubAccount) {
                operationTypesList = operationTypesList
                    .filter(operationTypeItem => operationTypeItem.value !== paymentOrderOperationResources.PaymentOrderOutgoingTransferToAccount.value);
            }
        }
    }

    if (isRubAccount) {
        operationTypesList = operationTypesList
            .filter(operationTypeItem => !availableOnCurrencyAccount.includes(operationTypeItem.value));
    } else {
        operationTypesList = operationTypesList
            .filter(operationTypeItem => availableOnCurrencyAccount.includes(operationTypeItem.value));

        if (IsTransit) {
            operationTypesList = operationTypesList
                .filter(operationTypeItem => availableOnTransitAccount.includes(operationTypeItem.value));
        }
    }

    const budgetaryTypeToOmit = getBudgetaryTypeToOmit(operation);

    operationTypesList = operationTypesList
        .filter(operationTypeItem => operationTypeItem.value !== budgetaryTypeToOmit);

    return operationTypesList;
}

function getBudgetaryTypeToOmit(operation) {
    const { OperationType } = operation;
    const { UnifiedBudgetaryPayment: { value: unifiedValue }, BudgetaryPayment: { value: commonValue } } = paymentOrderOperationResources;
    let typeToOmit = unifiedValue;

    if (![unifiedValue, commonValue].includes(OperationType)) {
        typeToOmit = dateHelper(operation.Date).isAfter(`31.12.2022`) ? commonValue : unifiedValue;
    }

    typeToOmit = OperationType === unifiedValue ? commonValue : unifiedValue;

    return typeToOmit;
}

function isPurchaseOrSellCurrencyFromWarningTable(operation) {
    const { OperationType, ToSettlementAccountId, FromSettlementAccountId } = operation;

    return (OperationType === paymentOrderOperationResources.PaymentOrderOutgoingCurrencyPurchase.value && !ToSettlementAccountId) ||
        (OperationType === paymentOrderOperationResources.PaymentOrderIncomingCurrencySale.value && !FromSettlementAccountId);
}

export function isNeedToHandleOperationType({ OperationType, DocumentBaseId, Action }) {
    const newOperationOpenActions = [OpenOperationActions.new, OpenOperationActions.newPaymentForMd];

    return (!OperationType || !DocumentBaseId) && !newOperationOpenActions.includes(Action);
}
