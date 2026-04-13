(function (bank) {

    bank.PurseOperationService = function(){
        return {
            deleteOperation: deleteOperation,
            copyOperation: copyOperation,
            getNumberForDate: function(date, direction) {
                var dfd = new $.Deferred();

                $.ajax({
                    url: '/Accounting/PurseOperation/GetNextNumber',
                    data: { Date: date, Direction: direction }
                }).done(function(resp){ dfd.resolve(resp.Value); });

                return dfd;
            }
        };

        function copyOperation(id){
            window.location = window.ApplicationUrls.copyPurseOperation.format(id);
        }

        function deleteOperation(id){
            var dfd = new $.Deferred();

            ToolTip.globalMessage(1, true, 'Удаление документа');
            sendRequest([id])
                .done(function(resp){
                    if(resp.Status){
                        bank.PurseGetter.clearCache();
                        ToolTip.globalMessage(1, true, 'Документ удален');
                        dfd.resolve();
                    }
                    ToolTip.globalMessageClose();
                    dfd.reject();
                })
                .fail(function(){
                    ToolTip.globalMessageClose();
                    dfd.reject();
                });

            return dfd;
        }

        function sendRequest(ids){
            return $.ajax({
                url: '/Accounting/PurseOperation/Delete/',
                type: 'post',
                data: {
                    ids: ids
                }
            });
        }
    };

})(Bank);