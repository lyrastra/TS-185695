/* eslint-disable */
/* global $, Common, */

(function(common) {
    // eslint-disable-next-line no-param-reassign
    common.Mixin.PostingsAndTaxTools = {

        createAndPlacementExplainigBlock(message) {
            if (!message || !message.length) {
                return;
            }
            let explainingBlock = this.$(`.explainigMessage`);
            let $parentEl = this.$el;

            if (!explainingBlock.length) {
                explainingBlock = $(`<div>`, {
                    'data-block': `explainigmessage`,
                    class: `explainigMessage`
                });
            }

            if (this.$(`[data-bind=operationHeader]`).length) {
                $parentEl = this.$(`[data-bind=operationHeader]`);
                explainingBlock.insertAfter($parentEl);
            } else {
                explainingBlock.prependTo($parentEl);
            }

            explainingBlock.html(message);
        },

        getMessage() {
            const { model } = this;
            if (model) {
                const { collection } = model;
                if (collection && collection.getMessage) {
                    return collection.getMessage();
                }
            }
            return null;
        },

        removeExplainingBlock() {
            const msg = this.getMessage();
            const explainingBlock = this.$(`.explainigMessage`);

            if (msg) {
                this.createAndPlacementExplainigBlock(msg);
            } else {
                explainingBlock.remove();
            }
        },

        tableDirectives: {
            ttProvideInAccounting: {
                html(target) {
                    if (this.ProvideInAccounting) {
                        return `<span class="mdUlSelectorText">Да</span>`;
                    }
                    $(target.element).addClass(`negative`);
                    return `Нет`;
                }
            },

            ProvideInAccounting: {
                html(target) {
                    $(target.element).remove();
                }
            },

            ttProvideInTax: {
                html(target) {
                    const status = common.Data.TaxPostingStatus;
                    if (!_.isUndefined(this.ProvideInTax)) {
                        const description = window.PostingsAndTax.taxPostingsStatusDescription(this.ProvideInTax);
                        if (this.ProvideInTax === status.NotTax) {
                            return description;
                        } else if (this.ProvideInTax === status.No) {
                            $(target.element).addClass(`negative`);
                            return description;
                        }
                        return `<span class="mdUlSelectorText">${description}</span>`;
                    }
                }
            }
        },

        explainingMessagesLib: {
            notTaxable: (function() {
                return `Не учитывается при расчёте налога.`;
            }()),
            notTaxablePlainMessage: (function() {
                return `Не учитывается при расчёте налога.`;
            }()),
            notTaxableEnvd: (function() {
                return `Не учитывается при расчёте налога.`;
            }()),
            notTaxableUsn6: (function() {
                return `Не учитывается при расчёте налога.`;
            }()),
            notTaxableUsn15: (function() {
                return `Не учитывается при расчёте налога.`;
            }()),

            notTaxableOsno: `Не учитывается при расчёте налога.`,
            notTaxableOsnoDividends: `Не учитывается при расчёте налога.`,

            UsnProfitAndOutgoForOnlyMaterials: (function() {
                const container = $(`<span>`);
                const message = `Товары признаются расходом только после соблюдения нескольких `;
                const link = $(`<a>`, {
                    href: `https://www.moedelo.org/Pro/View/Legals/97-426103528166`,
                    text: `условий`,
                    class: `externalLink`,
                    target: `_blank`
                });

                container.text(message).append(link);

                return container.html();
            }()),
            usnProfitNotTaxable: (function() {
                const container = $(`<div>`).addClass(`largeExplainigMessage`);
                const link = $(`<a>`, {
                    href: `https://www.moedelo.org/Pro/View/Legals/97-425971769333`,
                    text: `Порядок заполнения книги учета доходов и расходов организаций и индивидуальных предпринимателей, применяющих упрощенную систему налогообложения`,
                    class: `externalLink`,
                    target: `_blank`
                });

                const p = $(`<p>`).append(`Налогоплательщики, выбравшие в качестве объекта налогообложения доходы, согласно Порядку заполнения КУДИР (`)
                    .append(link)
                    .append(`) не заполняют графу «Расходы, учитываемые при исчислении налоговой базы».`);

                return container.append(p).prop(`outerHTML`);
            }()),
            RecognitionOfCostWrittenOff: `Расходы по оплате стоимости товаров, приобретенных для дальнейшей реализации, будут признаны при закрытии месяца - ` +
                `<a href="https://www.moedelo.org/Pro/View/Legals/97-426103528166?anchor=Q00000000385Q9C2&referer=Search&query=%D0%A1%D1%82%D0%B0%D1%82%D1%8C%D1%8F%20346.17%20%D0%9D%D0%9A%20%D0%A0%D0%A4&position=1" ` +
                `class="externalLink" target="_blank">ст. 346.17 НК РФ</a>`,

            RecognitionOfGoodsCostWrittenOffOsno: `Покупная стоимость реализованных товаров будет признана расходом при закрытии месяца - <a class="externalLink" target="_blank" href="https://www.moedelo.org/Pro/View/Legals/97-426103528166?anchor=Q000000001B6EI97&referer=Search&query=%D1%81%D1%82.%20268%20%D0%9D%D0%9A%20%D0%A0%D0%A4&position=1">ст.268 НК РФ</a>`,
            RecognitionOfMaterialsCostWrittenOffOsno: `Покупная стоимость материалов будет признана расходом при закрытии месяца (при условии списания в эксплуатацию) - <a class="externalLink" target="_blank" href="https://www.moedelo.org/Pro/View/Legals/97-426103528166?anchor=Q0000ZZZZ2EO63KP&referer=Search&query=%D0%B0%D0%B1%D0%B7.2%20%D0%BF.2%20%D1%81%D1%82.272%20%D0%9D%D0%9A%20%D0%A0%D0%A4&position=1">абз. 2 п. 2 ст. 272 НК РФ</a>`,

            ExpenseGoodsAtTheClosingMonths: `Расход по приобретенным и отгруженным товарам будет создан при закрытии месяца`,
            ExpenseGoodsAtTheClosingMonthsIfConfirmed: `Расход в виде стоимости товара будет признан при закрытии месяца, если факт его покупки и продажи документально подтвержден`,
            OnlyLinkedDocuments: `Учитывается только по связанным документам`,
            expenseRecognizedClosingMonthsWizard: `Списанная себестоимость будет признана расходом при закрытии месяца`,
            closingMonthMessage: `Проводки по списанию себестоимости создаются при закрытии месяца.`,

            fixedAssetInvestmentEnd: `Первоначальная стоимость ОС будет сформирована по завершению вложений, после чего будет ежемесячно создаваться расход в налоговом учете в размере амортизации`,
            fixedAssetUsn: `Расходы на приобретение основного средства учитываются в УСН с момента ввода объекта в эксплуатацию, но только при условии, что они оплачены. Частичная оплата также признается расходом равными долями ежеквартально в течение года (если ОС приобретено на УСН)`,

            notOutgo: `Не признается расходом`,

            notTaxableIncomingOsno: `<a href="https://www.moedelo.org/Pro/View/Legals/97-426103476725?anchor=Q00000000119C7K7&referer=Search&query=%D1%81%D1%82%D0%B0%D1%82%D1%8C%D1%8F%2041&position=1" target="_blank" class="externalLink">Не учитывается</a> при расчете налога на прибыль`,

            ReturnToSupplierForUsn: `Учитывается в доходах по связанным документам на дату поступления средств от покупателя – бывшего поставщика (<a href="https://www.moedelo.org/Pro/View/Legals/97-426103528166?anchor=Q000000001QQG3E1&amp;referer=Search&amp;query=%D0%BF.%201%20%D1%81%D1%82.%20346.15&amp;position=1" class="externalLink" rel="nofollow" target="_blank">п. 1 ст. 346.15</a>, <a href="https://www.moedelo.org/Pro/View/Legals/97-426103528166?anchor=Q0000ZZZZ2G0O3LB&amp;referer=Search&amp;query=%D0%BF.%201%20%D1%81%D1%82.%20346.17&amp;position=1" class="externalLink" target="_blank" rel="nofollow">п. 1 ст. 346.17 </a>НК РФ)`,
            ReturnToSupplierForEnvd: `Не учитывается при расчёте налога.`,
            Usn15TaxInClosingMonth: `Расход будет признан при закрытии месяца в случае выполнения двух условий: начислено и оплачено - датой последнего события, на меньшую из сумм.`,
            Usn15ReceiptStatementTaxInClosingMonth: `Авансовый платеж будет учтен в расходах при закрытии месяца в соответствии с графиком платежей по арендованному основному средству.`,
            tradingFeesOsno: `Не учитывается при расчёте налога.`,
            tradingFeesUsn6: `Не учитывается при расчёте налога.`,
            tradingFeesUsn15: `Уплаченная в пределах начисленного сумма учитывается в расходах`
        }
    };
}(Common));
