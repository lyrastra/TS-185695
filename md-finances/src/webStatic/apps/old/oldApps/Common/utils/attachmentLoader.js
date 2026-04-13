import {getUrlWithId} from '@moedelo/frontend-core-v2/helpers/companyId';

(function (module) {

    module.Utils.AttachmentLoader = {
        download: function (urls, callback) {
            if (_.isString(urls)) {
                urls = [urls];
            }

            urls = urls.map(url => getUrlWithId(url));

            fileCount = fileCount > 0 ? fileCount : urls.length;

            var url = urls.shift();

            if ($.browser.opera) {
                window.open(url);
            } else {
                callback && _.isFunction(callback) ? fileDownload(url, callback) : download(url);
            }

            if (urls.length > 0) {
                callback && _.isFunction(callback) ? this.fileDownload(urls, callback) : this.download(urls);
            } else {
                fileCount = 0;
            }
        }
    };

    var fileCount = 0;

    function download(url) {
        var engine = Md.Core.Engines.CompanyId;
        if(engine){
            url = engine.getLinkWithParams(url);
        }

        var iframe = document.createElement('iframe');
        iframe.style.display = 'none';
        document.body.appendChild(iframe);

        iframe.src = url;

        _.delay(function () {
            $(iframe).remove();
        }, fileCount < 10 ? 300000 : 10000 * fileCount);
    };

    //Метод для скачивания через $.fileDownload
    function fileDownload(url, callback) {
        $.fileDownload(url, {
            httpMethod: 'POST',
            successCallback: callback
        });
    }
})(Common);