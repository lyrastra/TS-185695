import { getUrlWithId } from '@moedelo/frontend-core-v2/helpers/companyId';
import AccountingDocumentType from '@moedelo/frontend-enums/mdEnums/AccountingDocumentType';
import documentTypeHelper from '@moedelo/frontend-common/helpers/DocumentTypeHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import SelfCostAffectedDocumentResource from '../resources/SelfCostAffectedDocumentResource';

export default {
    /**
     * @param wizard Объект мастера
     * @param additionalData Результат выполнения проверок с первого шага МЗМ
     * @returns { Object, boolean } Предупреждения из МЗМ в виде верстки и признак, есть ли среди них блокирующие
     */
    build({ wizard, additionalData, period }) {
        const {
            NegativeStocks,
            Invoices,
            UncoveredPaymentsCount,
            HasPurchasesCurrencyInvoices,
            NotProvideDocuments,
            TempBlockReasonMessages,
            DebitWithoutDocsWithBundling,
            UsnDeclarationBeforeFifo
        } = additionalData;

        let items = [];
        items.push(getNotProvideDocsWarning(wizard, NotProvideDocuments));
        items.push(getNegativeStocksWarning(wizard, NegativeStocks));
        items.push(getInvoicesWarning(Invoices));
        items.push(getUncoveredPaymentsWarning(wizard, UncoveredPaymentsCount, period));
        items.push(getPurchaseCurrencyInvoicesError(HasPurchasesCurrencyInvoices, NotProvideDocuments));
        items.push(...getTempErrors(TempBlockReasonMessages));
        items.push(getDebitWithoutDocsWithBundlingErrors(DebitWithoutDocsWithBundling));
        items.push(getUsnDeclarationBeforeFifoError(wizard, UsnDeclarationBeforeFifo));
        items = items.filter(i => !!i);

        if (!items.length) {
            return {
                Html: null,
                HasBlockingErrors: false
            };
        }

        const html = `<div> ${items.map((val, index) => {
            if (!val) {
                return ``;
            }
            
            const { Title, Value } = val;
            
            return `
                <div>
                    <div style="color: #404040; font-weight: bold;">${index + 1}. ${Title}</div>
                    <div>${Value}</div>
                </div>
            `;
        }).join(``)} </div>`;

        return {
            Html: html,
            HasBlockingErrors: items.some(i => i.IsBlockingError === true)
        };
    }
};

function getUsnDeclarationBeforeFifoError(wizard, usnDeclarationBeforeFifo) {
    if (!usnDeclarationBeforeFifo || wizard.IsSimpleMode) {
        return null;
    }

    return {
        IsBlockingError: true,
        Title: `Необходимо подать декларацию по УСН за ${usnDeclarationBeforeFifo.Year} год`,
        Value: `<div><a href="${usnDeclarationBeforeFifo.Url}" >Подать декларацию по УСН и заплатить налог за ${usnDeclarationBeforeFifo.Year} год</a></div>`
    };
}

function getDebitWithoutDocsWithBundlingErrors(debitWithoutDocsWithBundling) {
    if (!debitWithoutDocsWithBundling?.length) {
        return null;
    }

    const items = `
            <div style="margin: 10px 0 20px 0;">
                ${debitWithoutDocsWithBundling.map(doc => {
        const url = getUrlWithId(`/Stock#docs/productIncome/edit/${doc.DocumentBaseId}`);

        return `<div>
                        <a href="${url}" >Приход без документов №${doc.DocumentNumber} от ${dateHelper(doc.DocumentDate).format(`DD.MM.YYYY`)}</a>
                    </div>`;
    }).join(``)}
            </div>`;

    return {
        IsBlockingError: true,
        Title: `Имеются приходы без документов с комплектами`,
        Value: `${items}`
    };
}

function getNegativeStocksWarning(wizard, negativeStocks) {
    if (!negativeStocks.length) {
        return null;
    }

    const count = negativeStocks.reduce((cnt, stock) => cnt + stock.ProductBalances.length, 0);

    const products = `<table style="margin: 10px 0 20px 0;">
        ${negativeStocks.map(({ StockName, OnDate, ProductBalances }) => {
        const month = dateHelper(OnDate, `DD.MM.YYYY`).format(`MMMM`).toLowerCase();
        const rowsCount = ProductBalances.length;

        return ProductBalances.map((p, i) => `<tr>
                    ${i > 0 ? `` : `<td style="width: 150px; padding-right: 20px; vertical-align: top;" rowspan="${rowsCount}">${StockName} (${month})</td>`}
                    <td style="width: 200px; padding-right: 20px;">${p.ProductName}</td>
                    <td>${p.Balance}</td>
             </tr>`).join(``);
    }).join(``)
}
        </table>`;

    return {
        IsBlockingError: wizard.IsProfit === false, // ошибка является блокирующей для УСН 15%
        Title: `Отрицательное количество товаров/материалов на складе (${count})`,
        Value: `В данном периоде со склада выбыло большее количество товаров (материалов), чем было оприходовано. 
                Необходимо внести документы (накладные / УПД / авансовые отчеты) на поступление товаров (материалов) во вкладке Документы.
                ${products}`
    };
}

function getInvoicesWarning(invoices) {
    if (!invoices.length) {
        return null;
    }

    const items = `
            <div style="margin: 10px 0 20px 0;">
                ${invoices.map(doc => {
        const url = getUrlWithId(`/AccDocuments/Sales/#documents/invoice/edit/${doc.DocumentBaseId}`);
        
        return `<div>
                        <a href="${url}" >Счет-фактура №${doc.Number} от ${doc.Date}</a>
                    </div>`;
    }).join(``)}
            </div>`;

    return {
        IsBlockingError: false,
        Title: `Были выставлены счета-фактуры`,
        Value: `<p>В соответствии с п. 5 ст. 173 Кодекса в случае выставления лицами, не являющимися налогоплательщиками 
                    налога на добавленную стоимость, в том числе применяющими упрощенную систему налогообложения, 
                    покупателю товаров (работ, услуг) счета-фактуры с выделением суммы налога на добавленную стоимость вся сумма налога, 
                    указанная в этом счете-фактуре, подлежит уплате в бюджет.</p>
                    <p>На главной странице появится напоминание о необходимости подачи и оплаты Декларации по НДС</p>
                ${items}`
    };
}

function getUncoveredPaymentsWarning(wizard, uncoveredPaymentsCount, period) {
    if (!uncoveredPaymentsCount) {
        return null;
    }

    const url = getUrlWithId(`/Accounting/PaymentReport/GetUncoveredPaymentsReport?startDate=${period.startDate}&endDate=${period.endDate}`);

    return {
        IsBlockingError: false,
        Title: `Оплаты поставщикам (${uncoveredPaymentsCount})`,
        Value: `<p>В сервисе внесены оплаты в Деньгах, которые не связаны с подтверждающими документами.</p>
                    <p>Расходы должны подтверждаться документально (актами/накладными/УПД), 
                    в противном случае они не будут учитываться в налоговом учете. 
                    Для верного учета в сервисе, необходимо отразить подтверждающие документы во вкладке Документы (если еще не отразили), 
                    а так же связать документы с платежами в Деньгах. 
                    Вы сможете <a href="${url}">скачать список платежей</a> из вкладки <a href="${getUrlWithId(`/Finances`)}">Деньги</a>, 
                    по которым необходима связь с подтверждающим документом.</p>`
    };
}

function getPurchaseCurrencyInvoicesError(HasPurchasesCurrencyInvoices, NotProvideDocuments) {
    if (HasPurchasesCurrencyInvoices) {
        const purchasesInvoices = NotProvideDocuments.find(npd => npd.Type === AccountingDocumentType.PurchasesCurrencyInvoice)?.Documents || [];
        let documentsText = ``;
        purchasesInvoices.forEach(pi => {
            documentsText += `<a href="${getUrlWithId(`/Docs/Purchases/CurrencyInvoices/${pi.DocumentBaseId}/Edit`)}">Входящий инвойс №${pi.Number} от ${dateHelper(pi.Date).format(`DD.MM.YYYY`)}</a><br>`;
        });

        return {
            IsBlockingError: true,
            Title: `Входящие инвойсы (${purchasesInvoices.length})`,
            Value: `<p>Вы создали входящие инвойсы. Расчет себестоимости товаров, приобретенных за границей, сейчас находится в разработке. Повторите попытку 01.04.2020.</p>
                   <p>${documentsText}</p>`
        };
    }

    return null;
}

function getNotProvideDocsWarning(wizard, NotProvideDocuments) {
    if (!NotProvideDocuments || !NotProvideDocuments.length) {
        return null;
    }

    // Непроведенные документы, которые предполагают приход товара, могут сломать себестоимость. Наличие таковых явлется блокирующей ошибкой.
    const selfCostAffectedDocs = NotProvideDocuments.filter(s => SelfCostAffectedDocumentResource.some(o => o.Type === s.Type && o.Direction === s.Direction));

    if (!selfCostAffectedDocs.length) {
        return null;
    }

    let docLinks = ``;
    selfCostAffectedDocs.forEach(summary => {
        summary.Documents.forEach(doc => {
            const linkText = `${documentTypeHelper.getAccountingDocumentTypeName(summary.Type)} №${doc.Number} от ${dateHelper(doc.Date).format(`DD.MM.YYYY`)}`;
            const linkHref = getSelfCostAffectedDocumentLink(summary.Type, doc.DocumentBaseId);
            docLinks += `<a href="${getUrlWithId(linkHref)}">${linkText}</a><br>`;
        });
    });

    return {
        IsBlockingError: wizard.IsProfit === false, // ошибка является блокирующей для УСН 15%
        Title: `Неучтенные в бухгалтерском учете документы (${selfCostAffectedDocs.length})`,
        Value: `<p>${docLinks}</p>`
    };
}

function getSelfCostAffectedDocumentLink(docType, baseId) {
    switch (docType) {
        case AccountingDocumentType.Waybill:
            return `/AccDocuments/Buy#documents/waybill/edit/${baseId}`;
        case AccountingDocumentType.AccountingAdvanceStatement:
            return `/AccDocuments/AdvanceStatement/Edit/${baseId}`;
        case AccountingDocumentType.Upd:
            return `/AccDocuments/Buy#documents/upd/edit/${baseId}`;
        case AccountingDocumentType.RetailRefund:
            return `/Docs/retailRefund/${baseId}`;
        default: throw new Error(`Тип документа ${docType} не поддерживается`);
    }
}

function getTempErrors(errors) {
    if (!errors.length) {
        return [];
    }

    return errors.map(({ MessageHeader, MessageText }) => {
        return {
            IsBlockingError: true,
            Title: MessageHeader,
            Value: MessageText
        };
    });
}
