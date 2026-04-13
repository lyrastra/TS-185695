(function (stockModule) {

    stockModule.Helpers.MessageDialog = {
        ErrorHtml: function () {
            return '<div class="errorDialogContent"><h1>К сожалению, что-то пошло не так. </h1><div> Если данная ошибка повторяется, пожалуйста, <a href="mailto:support@moedelo.org">напишите</a> нам о ней, или позвоните по номеру<br/> 8 800 200 77 27.</div></div>';
        },
        ErrorLoadedInfo: function () {
            var html = this.ErrorHtml();
            var messageView = new stockModule.Views.MessageDialogView(html, 'Ошибка');
            messageView.open();
        },
        ErrorInfo: function () {
            return this.ErrorHtml();
        },
        WarningLoadedInfo: function () {

        },
        SuccessSaveOperaition: function (text) {
            addMessageInPage(text, 'success');
        },
        WarningSaveOperaition: function (text) {
            addMessageInPage(text, 'warning');
        },
        ErrorSaveOperation: function (text) {
            addMessageInPage(text, 'error');
        }
    };

    function addMessageInPage(text, classContent) {
        var $wrap = $('<div class="processMessage-wrapper"></div>'),
            $content = $('<div class="processContent ' + classContent + '"></div>'),
            $otherMessage = $('.status-panel-wrapper'),
            tag = 'body', correctiongIndentation = 100;

        $content.html(text);

        $wrap.append($content);
        
        if ($otherMessage.length > 0) {
            $wrap.css({ top: correctiongIndentation });
        }
        
        $wrap.prependTo(tag).show();
        setTimeout(closeProcessMessage, 5000);
    }

    function closeProcessMessage() {
        $(".processMessage-wrapper").fadeOut("normal").queue(function () { $(this).remove(); });
    }

})(Stock);