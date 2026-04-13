(function (sales, common) {
    'use strict';


    sales = sales || { Models: { Main: {} }};
    sales.Models.Main.Email = common.Models.Email = Backbone.Model.extend({

        url: WebApp.MailingDocuments.CreateDocsForMail,
        saveUrl: WebApp.MailingDocuments.SendDocumentByMail,
        downloadUrl: WebApp.MailingDocuments.GetAttachmentFile,

        insertTagName: "{{Ссылки на документы}}",

        billPdfSingle: "Добрый день.\n\n\nМы подготовили для Вас {{documents}}.\nДокумент в формате PDF приложен к письму \n\nCчет онлайн\n {{urls}} \n\n{{regards}}\n{{name}}",
        billPdfSeveral: "Добрый день.\n\n\nМы подготовили для Вас {{documents}}.\nДокументы в формате PDF приложены к письму.\n\nCчета онлайн\n {{urls}} \n\n{{regards}}\n{{name}}",
        billLinkSingle: "Добрый день.\n\n\nМы подготовили для Вас {{documents}}.\nВы можете оплатить счет с банковской карты или Яндекс.Деньгами, для этого просто пройдите по ссылке\n[Ссылка на документы]\n\n\n\n{{regards}}\n{{name}}",
        billLinkSeveral: "Добрый день.\n\n\nМы подготовили для Вас {{documents}}.\nВы можете оплатить счет с банковской карты или Яндекс.Деньгами, для этого просто пройдите по ссылке\n[Ссылка на документы] \n\n\n\n{{regards}}\n{{name}}",

        closingPdfSingle: "Добрый день.\n\n\nМы подготовили для Вас {{documents}}.\nДокумент в формате PDF приложен к письму.\n\n\n\n{{regards}}\n{{name}}",
        closingPdfSeveral: "Добрый день.\n\n\nМы подготовили для Вас {{documents}}.\nДокументы в формате PDF приложены к письму.\n\n\n\n{{regards}}\n{{name}}",
        closingLinkSingle: "Добрый день.\n\n\nМы подготовили для Ваc {{documents}}.\nВы можете оплатить счет с банковской карты или Яндекс.Деньгами, для этого просто пройдите по ссылке\n[Ссылка на документы] \n\n\n\n{{regards}}\n{{name}}",
        closingLinkSeveral: "Добрый день.\n\n\nМы подготовили для Вас {{documents}}.\nВы можете оплатить счет с банковской карты или Яндекс.Деньгами, для этого просто пройдите по ссылке\n[Ссылка на документы] \n\n\n\n{{regards}}\n{{name}}",

        getDocumentFormat: function () {
            return 'pdf';
        },

        sync: function (method, model, options) {
            if (method == "create") {
                options.url = this.saveUrl;
            }
            options.type = 'POST';
            options.data = $.toJSON(model.toJSON());
            options.contentType = 'application/json; charset=utf-8';

            return Backbone.Model.prototype.sync.call(this, method, model, options);
        },

        parse: function (response) {
            if (response.Value) {
                var result = response;
                result.emailFrom = response.Value.EmailFrom;
                result.emailDest = [];

                if (response.Value.EmailsTo && response.Value.EmailsTo.length) {
                    result.emailDest = response.Value.EmailsTo;
                }
                else {
                    result.emailDest.push({Email: null});
                }

                result.filesList = response.Value.Files;
                
                return result;
            }
            return response;
        },
        
        sendMail: function (emailObject) {
            $.ajax(this.saveUrl, {
                data: $.toJSON(emailObject),
                contentType: 'application/json; charset=utf-8',
                type: 'POST',
                context: this,
                success: function (resp) {
                    if(resp && resp.Status === false){
                        this.trigger('EmailNotSent');
                    }
                    else{
                        this.trigger('EmailSent');
                    }
                },
                error: function () {
                    this.trigger('EmailNotSent');
                }
            });
        },

        getPdfTextMessage: function () {
            if (this.get('filesList').length > 1) {
                if (this.get("docType") == 1) {
                    return this.billPdfSeveral;
                }
                return this.closingPdfSeveral;
            }

            if (this.get("docType") == 1) {
                return this.billPdfSingle;
            }
            return this.closingPdfSingle;
        },
        
        getLinkTextMessage: function () {
            if (this.get('filesList').length > 1) {
                if (this.get("docType") == 1) {
                    return this.billLinkSeveral;
                }
                return this.closingLinkSeveral;
            }

            if (this.get("docType") == 1) {
                return this.billLinkSingle;
            }
            return this.closingLinkSingle;
        },
        
        getFirmName: function () {
            try {
                var pseudonym = common.Utils.CommonDataLoader.FirmRequisites.get("Pseudonym");
                return pseudonym || '';
            } catch (e) {
                return '';
            }
        },

        getFilesUrl: function() {
            var filesUrl = '';
            var origin = window.location.origin;
            _.each(this.get('filesList'), function(element){
                filesUrl += origin + element.OnlineLink + '\n';
            });
            return filesUrl;
        },

        getRegards: function (name) {
            return name ? 'С уважением, ' : '';
        },
        
        getDocumentName: function (data, type) {
            data = data || _.first(this.get('filesList'));
            type = _.isUndefined(type) ? 'nominative' : type;
            
            try {
                return Enums.SalesDocumentTypes.doctypeSwitcher(data.DocumentType, type) + " " + data.Name;
            } catch (e) {
                return data.Name;
            }
        }
    });

})(window.Sales, Common);
