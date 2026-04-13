/* eslint-disable */

import React from 'react';
import ReactDOM from 'react-dom';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { getById } from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import TaxationSystemType from "@moedelo/frontend-enums/mdEnums/TaxationSystemType";
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import { toDate } from '@moedelo/frontend-core-v2/helpers/converter';
import ConfirmChangeContragentModal from '../../../../../../components/react/ConfirmChangeContragentModal/ConfirmChangeContragentModal';
import TaxationSystemSelect from '../../../../../Components/TaxationSystemSelect';
import PatentSelect from '../../../../../Components/PatentSelect';
import renderNdsDropdown from '../../../../../Components/NdsDropdown/NdsDropdownHelper';

// касса возврат покупателю

(function(cash) {
    cash.Views.outgoingReturnToBuyer = Marionette.LayoutView.extend({
        template: '#ReturnToBuyerTemplate',
        initialize() {
            _.extend(this, cash.Views.ndsUsnMessage);
            this.model.on('change:Date', function() {
                this.renderTaxationSystemRow();
                this.renderPatentSelect();
            }, this);

            this.listenTo(this.model, `change:IncludeNds`, () => {
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            });
            this.model.on(`change:TaxationSystemType`, this.renderPatentSelect, this);

            setTimeout(() => {
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            }, 0);
        },

        regions: {
            taxationSystemTypeSelect: `.js-taxationSystemType`,
            patentSelect: `.js-patent`
        },

        onRender() {
            this.bind();
            this.initializeControls();

            const $kontragentAccountCode = this.$('[data-bind=KontragentAccountCode]');
            if (!$kontragentAccountCode.is(':checked')) {
                $kontragentAccountCode.first().attr('checked', 'checked').change();
            }

            this._bindEvents();
            this._showOrHideNdsBlock();
            this.setNdsSumVisibility();
            this.renderTaxationSystemRow();

            if (this.model.get('Id') === 0) {
                this._updateDescription();
            }

            return this;
        },

        initializeControls() {
            this.initKontragentAutocomplete();

            this.$('[data-type=float]').decimalMask();
            this.$('select').change();

            const model = this.model;
            model.on('change:IncludeNds change:Date', this.showNdsMessage, this);
            this.showNdsMessage();
            this.$('[data-bind=ProjectNumber]').projectAutocomplete(model, {
                direction: model.get('Direction')
            });

            this.controls = {
                documents: this.renderDocumentTable()
            };
        },

        renderTaxationSystemRow() {
            const select = new TaxationSystemSelect({
                model: this.model
            });
            const region = this.getRegion(`taxationSystemTypeSelect`);

            region && region.show(select);
            this.renderPatentSelect();
        },

        renderPatentSelect() {
            const region = this.getRegion(`patentSelect`);
            const date = toDate(this.model.get('Date'));

            if (this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
                taxationSystemService.getActivePatents(date)
                    .then((res) => {
                        const content = new PatentSelect({
                            model: this.model,
                            activePatents: res
                        });

                        region && region.show(content);
                    })
                    .catch(() => {
                        this.unsetPatent();
                    });
            } else {
                this.unsetPatent();
            }
        },

        unsetPatent() {
            const region = this.getRegion(`patentSelect`);

            this.model.unset(`PatentId`);
            region && region.empty();
        },

        renderDocumentTable() {
            const control = new Core.Components.LinkedDocumentsControl({
                el: this.$('[data-control=linkedDocuments]'),
                model: this.model,
                autocomplete: function(){}
            }).render();

            this.on('resetDocuments', (documents) => {
                control.resetDocuments(documents); 
            });

            this.model.on('change:Sum', () => {
                control.toggleAddDocLink();
            });

            [].map.call(this.el.querySelectorAll('[data-bind=Documents_Number], [data-bind=Documents_Sum]'),x=>{
                 x.disabled = true;
            });
            this.$('[data-element="newDoc"]').remove();

            return control;
        },

        initKontragentAutocomplete() {
            const model = this.model;
            function confirmChangeKontragent(){
                const preservedData = {};
                preservedData.KontragentId = model.get('KontragentId');
                preservedData.KontragentName = model.get('KontragentName');
                preservedData.Documents = model.get('Documents');

                return new Promise((resolve, reject)=>{
                    const containerNode = document.getElementById('confirmChangeKontragentContainer');
                    ReactDOM.unmountComponentAtNode(containerNode);
                    ReactDOM.render(
                        <ConfirmChangeContragentModal
                            onConfirm={resolve}
                            onReject={()=>{
                                getById({id: preservedData.KontragentId}).then(data=>{
                                    preservedData.KontragentName = data.Name
                                    model.set( preservedData );
                                    reject();
                                })
                            }}
                        />,
                        containerNode
                    );
                })
            }

            const $KontragentNameAutocompleteInp = this.$('[data-bind=KontragentName]');


            $KontragentNameAutocompleteInp.saleKontragentWaybillAutocomplete({
                onSelect(selected) {
                    if( model.get(`KontragentId`) === selected.object.Id ){return;}
                    model.set({
                        KontragentId: selected.object.Id,
                        KontragentName: selected.object.Name
                    });
                },
                clean() {
                    model.unset('KontragentName');
                    model.unset('KontragentId');
                    model.unset('Documents');
                },
                onBlur() {
                    if (!model.get('KontragentName')) {
                        model.unset('KontragentId');
                        model.unset('Documents');
                    }
                },
                IsBuy: model.get('Direction') === Direction.Outgoing
            });
        },

        _bindEvents() {
            this.listenTo(this.model, 'change:NdsSum change:Sum change:IncludeNds change:KontragentName', this._updateDescription);
            this.listenTo(this.model, 'change:Date', this._showOrHideNdsBlock);
            this.listenTo(this.model, 'change:TaxationSystemType', this.changeNdsOption);
            this.listenTo(this.model, 'change:Date', () => {
                if (!this.isEdit) {
                    this.changeNdsOption();
                }
            });
            this.listenTo(this.model, 'change:KontragentId', this.onChangeKontragentId);
            this.listenTo(this.model, 'change:KontragentAccountCode', this.changeKontragentAccountCode );
            this.listenTo(this.model, 'change:Documents', this.renderDocumentTable );
        },

        changeNds({ value }) {
            this.model.set(`NdsType`, value);
        },

        changeKontragentAccountCode(){
            this.model.set('Documents', [] );
        },

        onChangeKontragentId(){

            const kontragentId = this.model.get('KontragentId');

            function confirmChangeKontragent(){
                return new Promise((resolve, reject)=>{
                    const containerNode = document.getElementById('confirmChangeKontragentContainer');
                    ReactDOM.unmountComponentAtNode(containerNode);
                    ReactDOM.render(
                        <ConfirmChangeContragentModal
                            onConfirm={resolve}
                            onReject={reject}
                        />,
                        containerNode
                    );
                })
            }

            const storedData = {
                KontragentId: this.model.previous('KontragentId'),
                KontragentName:  this.model.previous('KontragentName'),
                Documents: this.model.previous('Documents')
            };

            if( kontragentId === storedData.KontragentId ){ return; }

            const isLinkedDocumentsExist = storedData.Documents && !!storedData.Documents.length;
            if( isLinkedDocumentsExist ){
                confirmChangeKontragent()
                    .catch(()=>{
                        getById({id: storedData.KontragentId}).then(data=>{
                            storedData.KontragentName = data.Name
                            this.model.set(storedData);
                            this.$('[data-bind=KontragentName]').val(storedData.KontragentName).change();
                        })
                    })
            }

        },

        setNdsSumVisibility() {
            const includeNds = this.model.get(`IncludeNds`);

            if (!includeNds) {
                return;
            }

            const hideSumForTypes = [0, -1];
            const ndsSumBlock = this.$(`[data-bind="NdsSum"]`).parent();
            const ndsType = this.model.get(`NdsType`);

            if (_.contains(hideSumForTypes, ndsType)) {
                ndsSumBlock.hide();

                return;
            }

            ndsSumBlock.show();
        },

        changeNdsOption() {
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const nds20PercentDate = dateHelper('2019-01-01');
            const ndsAfter2026 = dateHelper(`2026-01-01`);
            const documentDate = dateHelper(this.model.get('Date'), 'DD.MM.YYYY');
            const previousDocumentDate = dateHelper(this.model.previous('Date'), 'DD.MM.YYYY');
            const isOsno = this.model.get(`IsOsno`);
            const isUsn = this.model.get(`IsUsn`);

            const previous = dateHelper(this.model.previous(`Date`), `DD.MM.YYYY`).year();
            const current = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`).year();

            const previousTaxSys = this.model.previous(`TaxationSystemType`) || this.model.get(`TaxationSystemType`);
            const currentTaxSys = this.model.get(`TaxationSystemType`);

            if (current === previous && (+previousTaxSys === +currentTaxSys || !previousTaxSys || !currentTaxSys)) {
                return;
            }
            
            if (documentDate.isSameOrAfter(nds20PercentDate)) {
                this.model.set({ NdsType: Common.Data.BankAndCashNdsTypes.Nds20 });
            }

            if (documentDate.isSameOrAfter(ndsUsn2025Date) && isUsn && this.model.get(`TaxationSystemType`) !== TaxationSystemType.Patent) {
                this.model.set({ NdsType: Common.Data.BankAndCashNdsTypes.None });
                this.model.set({ IncludeNds: true });
            }

            if ((documentDate.isSameOrAfter(ndsUsn2025Date) && isUsn && this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) || (documentDate.isBefore(ndsUsn2025Date) && isUsn)) {
                this.model.set({ NdsType: null });
                this.model.set({ NdsSum: null });
                this.model.set({ IncludeNds: false });
            }

            if (documentDate.isSameOrAfter(ndsAfter2026) && isOsno) {
                this.model.set({ NdsType: Common.Data.BankAndCashNdsTypes.Nds22 });
            }

            if (documentDate.isBefore(nds20PercentDate) && previousDocumentDate.isSameOrAfter(nds20PercentDate)) {
                this.model.set({ NdsType: Common.Data.BankAndCashNdsTypes.Nds18 });
            }

            this.setNdsSumVisibility();
            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) })
        },

        _showOrHideNdsBlock() {
            const isOsno = this.model.isOsno();
            const isUsn = this.model.get('IsUsn');
            this.model.set('IsUsn', isUsn);
            this.model.set('IsOsno', isOsno);
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const $ndsSection = this.$('.js-ndsSection');
            const $ndsFieldsSection = this.$('.js-ndsFieldsSection');
            const documentDate = dateHelper(this.model.get('Date'), 'DD.MM.YYYY');

            if (isOsno || (documentDate.isSameOrAfter(ndsUsn2025Date) && isUsn)) {
                $ndsSection.removeClass('hidden');
                this.model.get('IncludeNds') && $ndsFieldsSection.removeClass('hidden');
            } else {
                $ndsSection.addClass('hidden');
                $ndsFieldsSection.addClass('hidden');
            }
        },

        _updateDescription() {
            const model = this.model;
            const sum = model.get('Sum');
            const ndsSum = parseFloat(model.get('NdsSum'));
            const ndsType = parseFloat(model.get('NdsType'));
            const includeNds = model.get('IncludeNds');
            const formatedSum = Converter.toAmountString(sum);
            let text;

            if (ndsSum && includeNds && sum) {
                const formatedNdsSum = Converter.toAmountString(ndsSum);
                const formatedNdsType = mdNew.ndsTypeConverter.moneyTypeToString(ndsType) || '';

                text = `Возврат денег клиенту на сумму ${formatedSum}, в т.ч. НДС ${formatedNdsType} ` +
                    `на сумму ${formatedNdsSum}`;
            } else if (sum) {
                text = `Возврат денег клиенту на сумму ${formatedSum}. НДС не облагается`;
            } else {
                text = '';
            }
            model.set('Destination', text);
        },

        bindings: {
            '#ndsSumContainer': {
                observe: 'IncludeNds',
                visible: true
            },
        },

        onDestroy() {
            this.model.off(`change:Date`, this.renderTaxationSystemRow);
            this.model.off(`change:TaxationSystemType`, this.renderPatentSelect);
        }
    });
}(Cash));
