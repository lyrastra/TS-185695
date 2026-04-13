(function ($, window) {

    window.setAjaxConfig = function() {
        
        var unloading = false;

        $.ajaxSetup({
            error: function (xhr, textStatus, errorThrown) {
                if (unloading) return;
                if (textStatus !== "abort" && errorThrown !== "abort") {
                    showError.call(this);
                }
            },
            statusCode: {
                404: showError
            },
            cache: false
        });

        if (window.removeEventListener && window.addEventListener) {
            window.removeEventListener("beforeunload", setUnloading);
            window.addEventListener("beforeunload", setUnloading);
        }
        
        function setUnloading() {
            unloading = true;
        }
        
        function showError() {
            if (this.url == "/Pro/Api/GetUpdates") {
                return;
            }

            ToolTip.globalMessage(1, false, AjaxErrorsResource.AjaxError, true);
        }
    };
    
})(jQuery, window);