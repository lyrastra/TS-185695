import React from 'react';
import Tag from '@moedelo/frontend-core-react/components/Tag';
import { toAmountString, toInt } from '@moedelo/frontend-core-v2/helpers/converter';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import KontragentTypeResource from '../../apps/money/components/AdvancedSearch/components/KontragentSection/KontragentTypeResources';
import { getAllOperationTypeByLegalType } from '../../resources/OperationTypeResources';
import { getBudgetaryTypeByLegalType } from '../../resources/BudgetaryTypeResources';
import KontragentTag from '../../apps/money/components/MainTable/components/KontragentTag';
import localDateHelper from '../date';
import SumConditionEnum from '../../enums/newMoney/SumConditionEnum';
import ProvideInTaxEnum from '../../enums/newMoney/ProvideInTaxEnum';
import LegalType from '../../enums/LegalTypeEnum';
import ClosingDocumentsEnum from '../../enums/newMoney/ClosingDocumentsEnum';
import TaxationSystemNameResource from '../../resources/TaxationSystemNameResource';
import ApproveSectionEnum from '../../apps/money/components/AdvancedSearch/components/ApproveSection/ApproveSectionEnum';

class FilterTagsHelper {
    getTags({ filter, clearTag }) {
        const list = [
            `date`,
            `kontragent`,
            `direction`,
            `operationType`,
            `sumCondition`,
            `provideInTax`,
            `closingDocumentsCondition`,
            `budgetaryType`,
            `taxationSystemType`,
            `patentId`,
            `approvedCondition`
        ];
        const tags = [];

        list.forEach(field => {
            const tag = this[field] && this[field]({ filter, clearTag });

            if (tag) {
                tags.push(React.cloneElement(tag, { key: field }));
            }
        });

        return tags;
    }

    budgetaryType({ filter, clearTag }) {
        let { budgetaryType } = filter;
        budgetaryType = parseInt(budgetaryType, 10);

        /** 0 значение не из енама, а возможное косячное значение в модели */
        if (!budgetaryType || budgetaryType === 0) {
            return false;
        }

        const legalType = window._preloading.IsOoo ? LegalType.Ooo : LegalType.Ip;
        const budgetaryTypes = getBudgetaryTypeByLegalType(legalType);

        const tagType = budgetaryTypes.find(type => type.value === budgetaryType).text;

        return <Tag selected closable onClose={() => clearTag([`budgetaryType`])} maxWidth={200}>{tagType}</Tag>;
    }

    kontragent({ filter, clearTag }) {
        let { kontragentType, kontragentId } = filter;

        kontragentType = toInt(kontragentType);
        kontragentId = toInt(kontragentId);

        if (kontragentId) {
            return <KontragentTag
                closable
                kontragentId={kontragentId}
                kontragentType={kontragentType}
                onClick={() => clearTag([`kontragentId`])}
            />;
        }

        if (kontragentType) {
            const tagType = KontragentTypeResource.find(type => type.value === kontragentType).text;

            return <Tag selected closable onClose={() => clearTag([`kontragentType`])}>{tagType}</Tag>;
        }

        return null;
    }

    direction({ filter, clearTag }) {
        const { direction } = filter;

        switch (toInt(direction)) {
            case Direction.Outgoing:
                return <Tag selected closable onClose={() => clearTag([`direction`])}>Списания</Tag>;
            case Direction.Incoming:
                return <Tag selected closable onClose={() => clearTag([`direction`])}>Поступления</Tag>;
            default:
                return null;
        }
    }

    // это яд, но что поделать
    operationType({ filter, clearTag }) {
        const legalType = window._preloading.IsOoo ? LegalType.Ooo : LegalType.Ip;
        const allOperation = getAllOperationTypeByLegalType(legalType);

        let { operationType } = filter;
        let selectedType;

        if (!operationType) {
            return false;
        }

        if (typeof operationType === `string`) {
            operationType = operationType.split(`,`);
        }

        for (let i = 0; i < allOperation.length; i += 1) {
            if (allOperation[i].data) {
                selectedType = allOperation[i].data.find(x => {
                    return operationType.findIndex(o => {
                        return x.value.includes(Number(o));
                    }) > -1;
                });

                if (selectedType) {
                    return <Tag selected closable onClose={() => clearTag([`operationType`, `budgetaryType`])}>{selectedType.text}</Tag>;
                }
            }
        }

        return false;
    }

    sumCondition({ filter, clearTag }) {
        let {
            sum,
            sumCondition,
            sumFrom,
            sumTo
        } = filter;

        sum = toAmountString(sum);
        sumFrom = toAmountString(sumFrom);
        sumTo = toAmountString(sumTo);
        sumCondition = Number(sumCondition);

        switch (sumCondition) {
            case SumConditionEnum.Less:
                return sum && <Tag selected closable onClose={() => clearTag([`sumCondition`, `sum`])}>меньше {sum}</Tag>;
            case SumConditionEnum.Equal:
                return sum && <Tag selected closable onClose={() => clearTag([`sumCondition`, `sum`])}>{sum}</Tag>;
            case SumConditionEnum.Great:
                return sum && <Tag selected closable onClose={() => clearTag([`sumCondition`, `sum`])}>больше {sum}</Tag>;
            case SumConditionEnum.Range:
                return sumFrom && sumTo && <Tag selected closable onClose={() => clearTag([`sumCondition`, `sumFrom`, `sumTo`])}>от {sumFrom} до {sumTo}</Tag>;
            default:
                return null;
        }
    }

    closingDocumentsCondition({ filter, clearTag }) {
        let { closingDocumentsCondition } = filter;

        closingDocumentsCondition = toInt(closingDocumentsCondition);
        let tagText = null;

        switch (closingDocumentsCondition) {
            case ClosingDocumentsEnum.Partly:
                tagText = `Есть закрывающие документы (частично)`;

                break;
            case ClosingDocumentsEnum.Completely:
                tagText = `Есть закрывающие документы (полностью)`;

                break;
            case ClosingDocumentsEnum.No:
                tagText = `Нет закрывающих документов`;

                break;
        }

        if (tagText) {
            return <Tag selected closable onClose={() => clearTag([`closingDocumentsCondition`])}>{tagText}</Tag>;
        }

        return null;
    }

    approvedCondition({ filter, clearTag }) {
        let { approvedCondition } = filter;

        approvedCondition = toInt(approvedCondition);
        let tagText = null;

        switch (approvedCondition) {
            case ApproveSectionEnum.Yes:
                tagText = `обработанные операции`;

                break;
            case ApproveSectionEnum.No:
                tagText = `не обработанные операции`;

                break;
        }

        if (tagText) {
            return <Tag selected closable onClose={() => clearTag([`approvedCondition`])}>{tagText}</Tag>;
        }

        return null;
    }

    provideInTax({ filter, clearTag }) {
        const { taxationSystemType, patentId } = filter;
        let { provideInTax } = filter;

        provideInTax = Number(provideInTax);

        switch (provideInTax) {
            case ProvideInTaxEnum.Taken: {
                const accountingText = patentId
                    ? `патенте`
                    : TaxationSystemNameResource[taxationSystemType] || `НУ`;

                return <Tag selected closable onClose={() => clearTag([`provideInTax`, `taxationSystemType`, `patentId`])}>учтены в {accountingText}</Tag>;
            }

            case ProvideInTaxEnum.NotTaken:
                return <Tag selected closable onClose={() => clearTag([`provideInTax`])}>не учтены в НУ</Tag>;
            default:
                return null;
        }
    }

    date({ filter, clearTag }) {
        const text = this.dateText({ filter });

        if (text) {
            return <Tag selected closable onClose={() => clearTag([`startDate`, `endDate`])}>{text}</Tag>;
        }

        return null;
    }

    dateText({ filter }) {
        const { startDate, endDate } = filter;

        if (!startDate && !endDate) {
            return false;
        }

        if (!startDate) {
            return `по ${endDate}`;
        }

        if (!endDate) {
            return `с ${startDate}`;
        }

        if (localDateHelper.isYear(startDate, endDate)) {
            return localDateHelper.year(startDate);
        }

        if (localDateHelper.isQuarter(startDate, endDate)) {
            return `${localDateHelper.quarter(startDate)} кв. ${localDateHelper.year(startDate)}`;
        }

        if (localDateHelper.isMonth(startDate, endDate)) {
            return localDateHelper.format(startDate, `MMM YYYY`);
        }

        if (startDate === endDate) {
            return startDate;
        }

        return `${startDate} - ${endDate}`;
    }
}

export default new FilterTagsHelper();
