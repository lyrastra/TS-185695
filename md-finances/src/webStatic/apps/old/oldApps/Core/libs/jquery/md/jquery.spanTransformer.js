(function ($) {

    $.fn.spanTransformer = function (options, params) {
        var pluginMethods = {
            'readonly': makeReadOnly
        };

        if (_.isString(options)) {
            return callMdSpanTransformerMethod(this, options, params);
        }

        var defaultOptions = {
            onClose: function () { },
            isOneWay: false,
            url: ''
        };
        
        options = _.extend(defaultOptions, options);
        
        var controls = this.each(function (index, input) {
            initializeControll($(input));
        });

        var readonly = _.find(options, function(val, key) {
            return key.toLowerCase() == "readonly";
        });

        if (!_.isUndefined(readonly)) {
            callMdSpanTransformerMethod(this, "readonly", readonly);
        }

        return controls;
        
        function initializeControll(input) {
            createContainer(input);
            bindEvents(input);

            if ($.trim(input.val()).length > 0) {
                transformInput(input);
            }
        }

        function createContainer(input) {
            input.wrap("<div class='mdSpanTransformer'></div>",{});
            var container = input.parent();
            var text;

            if(options.isOneWay){
                text = $('<span>', {
                    'class': 'mdSpanTransformerText mdSpanTransformerText--readOnly'
                });
            } else {
                text = $("<a class='mdSpanTransformerText' target='_blank'></a>");
            }
            container.append(text.hide());
            
            var closeLink = $("<span class='mdSpanTransformerClose mdCloseLink'>×</span>").hide();
            container.append(closeLink);
        }

        function bindEvents(input) {
            input.on("change blur", changeInput);
            getCloseLink(input).on("click", clear);
            getTextSpan(input).on("click", clickSpan);
        }

        function changeInput(event) {
            var input = $(event.target);
            
            if ($.trim(input.val()).length > 0) {
                transformInput(input);
            } else {
                transformSpan(input);
            }
        }

        function transformInput(input) {
            input.hide();

            var link = getTextSpan(input);
            link.attr('href', _.result(options, 'url'));
            link.text(input.val()).css({display: 'inline-block'});
            if (input.spanTransformer('readonly') !== true) {
                getCloseLink(input).css({ display: 'inline-block' });
            }


        }

        function clickSpan(event) {
            var span = $(event.target),
                container, input;

            if(options.isOneWay){
                return;
            }

            if (!$.trim(span.attr('href')).length) {
                container = span.closest('.mdSpanTransformer');
                input = container.find('input');
                transformSpan(input);
                input.focus();
            }
        }

        function transformSpan(input) {
            input.show();

            getTextSpan(input).hide();
            getCloseLink(input).hide();
        }
        
        function getCloseLink(input) {
            var container = input.parent();
            return container.find(".mdSpanTransformerClose");
        }

        function getTextSpan(input) {
            var container = input.parent();
            return container.find(".mdSpanTransformerText");
        }
        
        function clear(event) {
            var closeLink = $(event.target);
            var container = closeLink.closest(".mdSpanTransformer");
            var input = container.find("input");
            
            if (input.val().length) {
                input.val("").change();
                transformSpan(input);
                input.focus();
                _.result(options, 'onClose');
            }
        }

        function makeReadOnly(isReadOnly) {
            var control = $(this);
            var data = control.data('mdSpanTransformer') || {
                readOnly: false
            };
            
            if (arguments.length == 0) {
                return data.readOnly;
            }
            
            data.readOnly = isReadOnly;
            control.data('mdSpanTransformer', data);
            
            var closeLink = getCloseLink(control);
            if (isReadOnly) {
                closeLink.hide();
            } else {
                $.trim(control.val()).length > 0 && closeLink.css({display: 'inline-block'});
            }
            return control;
        }

        function callMdSpanTransformerMethod(controls, action, params) {
            var result = [];
            action = action.toLowerCase();
            var handler = pluginMethods[action];
            if (handler) {
                _.each(controls, function (control) {
                    var args = [];
                    if (!_.isUndefined(params)) {
                        args.push(params);
                    }
                    result.push(handler.apply(control, args));
                });
            }

            return result.length > 1 ? result : result[0];
        }
    };

  

})(jQuery);
