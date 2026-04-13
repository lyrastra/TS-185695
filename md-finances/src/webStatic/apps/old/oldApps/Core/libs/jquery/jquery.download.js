/*
* --------------------------------------------------------------------
* jQuery-Plugin - $.download - allows for simple get/post requests for files
* by Scott Jehl, scott@filamentgroup.com
* http://www.filamentgroup.com
* reference article: http://www.filamentgroup.com/lab/jquery_plugin_for_requesting_ajax_like_file_downloads/
* Copyright (c) 2008 Filament Group, Inc
* Dual licensed under the MIT (filamentgroup.com/examples/mit-license.txt) and GPL (filamentgroup.com/examples/gpl-license.txt) licenses.
* --------------------------------------------------------------------
*/

jQuery.download = function (url, data, method) {
    //url and data options required
    if (url && data) {
        //split params into form inputs
        var inputs = '';

        for (var param in data) if (data.hasOwnProperty(param)) {
                var sep = '\"';
                var val = data[param] == null ? "" : data[param];
                if (typeof val == "string" && val.indexOf('\"') != -1) {
                    sep = '\'';
            }

            inputs += '<input type="hidden" name="' + param + '" value=' + sep + val + sep + ' />';
        }
        //send request
        jQuery('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>')
        .appendTo('body').submit().remove();
    };
};