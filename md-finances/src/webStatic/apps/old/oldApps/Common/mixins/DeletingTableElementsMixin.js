(function (common) {

    common.Mixin.DeletingTableElementsMixin = {
        events: {
            'click .mdCloseLink': 'removeAllocation',
            "click .mdButton:not(.single-salary):not(.cash):not(.disabled)": "deleteOperations"
        },

        setPosition: function () {
            var position = this.tagNode.children().outerHeight() - 2;
            this.$el.css('top', position);
        },

        windowClose: function () {
            var view = this;
            $('html').on('click.deleting', function (e) {
                if ($(e.target).closest('.toolDelete>div').length || $(e.target).closest('.toolDelete').length) {
                    return;
                }
                view.clearElement();
            });
        },

        deletingDialogToggling: function ($elem) {
            var view = this;

            $elem.addClass('jrender');
            $elem.children('a').html('<span>Удаление</span>');
            $elem.addClass('activated');
            $(view.$el).appendTo($elem).slideDown('fast');
        },


        removeAllocation: function (e) {
            e.stopPropagation();
            e.preventDefault();
            this.clearElement();
        },

        blockingRow: function (element) {
            element.addClass("blocked")
                    .find('input')
                    .prop({ "disabled": "disabled", "checked": false })
                    .end()
                    .find('a').click(function (e) {
                        e.preventDefault();
                    });
        },

        unBlockingRows: function () {
            var companyId = Md.Core.Engines.CompanyId;
            $(".mdTableBody li.blocked")
                .removeClass("blocked")
                .find('input').removeAttr("disabled").prop("checked", true)
                .end()
                .find('a')
                .click(function () {
                    var href = $(this).attr('href');
                    document.location = companyId.getLinkWithParams(href);
                });
        },

        clearElement: function () {
            var $this = this.tagNode;

            $this.children("div").slideUp("fast", function () {
                $(this).remove();
                $this.removeClass("activated").children("a").html("Удалить");
                $this.removeClass("blocked");
                setTimeout(function () {
                    $this.removeClass('jrender');
                }, 100);
            });
            this.undelegateEvents();
        },

        disableButton: function() {
            this.$(".dialog_buttons .button").attr("disabled", "disabled");
            this.$(".dialog_buttons .mdButton").addClass("disabled");
        },

        isAccountingReadOnly: function() {
            if (this.hasAccountingReadOnlyDocuments()){
                this.disableButton();
                this.$(".dialogContent").html('<div>Среди выбранных документов есть проведенные бухгалтером. Удаление невозможно.</div>');
            }
        },

        hasAccountingReadOnlyDocuments: function(){
            var deleteIds = this.options.model.get('deleteIds');
            var documents = this.options.collection;

            return documents.any( function(doc){
                return doc.get('AccountingReadOnly') === true &&
                    _.any(deleteIds, function(id) {
                        return id == doc.get('Id');
                    });
            });
        },

        isClosedPeriod: function () {
            if (!this.options.anyInClosedPeriod) {
                return;
            }

            var html = '<div>Нельзя удалять документы в закрытом периоде</div>';
            var documents = this.options.deletingDocuments;

            if (documents && documents.length > 0) {
                html = '<div>Будут удалены только документы из открытого периода:<br/>';
                for (var i = 0; i < documents.length; i++) {
                    html += '<div> - ' + documents[i] + '</div>';
                }
                html += '<br/><b>Внимание!</b> Это действие необратимо.</div>';
            } else {
                this.disableButton();
            }

            this.$(".dialogContent").html(html);
        }
    };

})(Common);
