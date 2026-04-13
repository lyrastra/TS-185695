(function (common) {

    common.Utils.HtmlCreator = {
        createLink: function (text, href, options) {
            options = options || {
                className: ""
            };
            
            var $link = $('<a />').text(text);
            var companyId = Md.Core.Engines.CompanyId;

            if (href) {
                $link.attr('href', companyId.getLinkWithParams(href));
            }
            $link.addClass(options.className);

            return $link;
        },
        createErrorSpan: function (text, attrs) {
            var span = $(String.format('<span class="field-validation-error"><span>{0}</span></span>', text));
            _.each(attrs, function (val, name) {
                if (name === 'class') {
                    span.addClass(val);
                } else {
                    span.attr(name, val);
                }
            });
            
            return span;
        },
        createOption: function (text, value, attrs) {
            var option = $(String.format('<option value="{0}" >{1}</option>', value, text));

            _.each(attrs, function (val, name) {
                option.attr(name, val);
            });

            return option;
        }
    };

})(Common);
