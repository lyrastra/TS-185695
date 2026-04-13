(function (bank) {

    $(function () {
        initialize();
    });

    function initialize(){
        var purseGetter = new bank.PurseGetter();

        setAjaxConfig.call();
        TemplateManager.prefix = '/App/';
        Backbone.configureValidation();

        $.when(purseGetter.loadPurses()).done(function(){
            Backbone.history.start();
            Backbone.history.on('route', HistoryManager.storeRoute);
            Md.Services.UrlGetter.readUrlSettings(ApplicationUrls);
        });
    }

    ToolTip.busyShowing($('#page_content'), 'mainPageLoader busyShowing');

})(Bank);