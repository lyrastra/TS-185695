(function(module) {

    module.Account = {
        Initialization: {
            start: function () {
                if (jQuery) {
                    this.showInitProcess();    
                    
                    jQuery.ajax({
                        url: "/Accounting/Initialization/StartInitialization",
                        type: 'POST',
                        async: false,
                        dataType: 'json',
                        success: this.end
                    });
                }
            },
            end: function(response) {
                if (response && response.Status && response.Status === true) {
                    window.location.reload();
                } else {
                    
                }
            },
            
            messageText: 'Подготовка кабинета бухгалтера...',

            showInitProcess: function (message) {
                var self = this,
                    $document = $(document),
                    $width = $document.width(),
                    $height = $document.height(),
                    windowHeight = $(window).height(),
                    messagetext = message ? message : self.messageText;
                
                self.div = $('<div>'),
                self.infoDiv = $('<div>'),

                self.div.css({
                    'width': $width,
                    'height': $height,
                    'background-color': '#404040',
                    'position': 'absolute',
                    'opacity': '0.5',
                    'top': 0,
                    'z-index': 100000,
                    'text-align': 'center'
                });

                self.infoDiv.css({
//                    'width': 300,
                    'padding': '25px 20px 25px 20px',
                    'background-color': '#fff',
                    'position': 'fixed',
                    'z-index': 100000,
                    'font-size': '18px',
                    'top': (windowHeight / 2) - 37,
                    'left': ($width / 2) - 150
                });

                self.infoDiv.text(messagetext);

                $(document.body).append(self.div);
                $(document.body).append(self.infoDiv);
            },
            
            hideInitProcess: function() {
                this.div.remove();
                this.infoDiv.remove();
            }
        }
    };

    window.onload = function () {
        if (module.Account.AccountInit){

            if (module.Account.AccountInit && module.Account.AccountInit.StageInit === 0) {
                module.Account.Initialization.start();
            }
        }
    };

})(window);