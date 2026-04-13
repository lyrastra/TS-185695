(function (mainModule) {
    'use strict';

    mainModule.Models.Main.MenuModel = Backbone.Model.extend({
        url: '/BizDocuments/Confirming/GetCount',
        defaults: {
            FakeWaybillsCount: 10,
            FakeStatementsCount: 10
        },
        messages: {
            fakeStatements: 'У актов нет строк с полученными услугами. ' +
            'Их нельзя будет оплатить в разделе Деньги - Списания',
            fakeWaybills: 'У накладных нет строк с товарами и материалами. Такие накладные не будут участвовать в' +
            ' расчете себестоимости товара и учтены при расчете УСН.' +
            ' Их нельзя будет оплатить в разделе Деньги - Списания',
            documentListDownloadStart: 'Загрузка списка подтверждающих документов',
            documentListDownloadError: 'Ошибка при загрузке подтверждающих документов'
        }
    });

})(PrimaryDocuments);