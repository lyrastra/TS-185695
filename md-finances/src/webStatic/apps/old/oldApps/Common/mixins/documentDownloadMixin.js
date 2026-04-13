(function (common) {

    common.Mixin.DocumentDownloadMixin = function (viewForBind) {

        viewForBind.downloadDocument = function () {
            var view = this,
                documentId = parseInt(view.model.get('id') || view.model.get('Id'), 10),
                downloadType = view.downloadType;

            if (!documentId) {
                documentId = parseInt(view.model.get('SavedId'), 10);
            }

            if (downloadType > 2 || downloadType < 0) {
                return;
            }

            var downloadFrame = function (url) {
                var iframe = document.createElement('iframe');
                iframe.style.display = 'none';
                document.body.appendChild(iframe);
                iframe.src = url;

                _.delay(function () {
                    $(iframe).remove();
                }, 10000);
            };

            var loadDoc = function () {
                var paramObj = {
                    Id: documentId,
                    DocFormat: downloadType,
                    DocumentType: view.documentType
                };

                var params = view.downloadUrl.split('?'),
                    path = params.shift();

                params.push('clientData=' + JSON.stringify(paramObj));
                var url = path + '?' + params.join('&');

                downloadFrame(url);
            };

            loadDoc();
        };
    };

})(Common);