(function ($) {
	'use strict';

    $.fn.mdSelect = function (options) {
        if (_.isString(options)) {
            callMdSelectMethod(this, options);
            return this;
        }

        options = options || {};

        return this.each(function () {
            var select = $(this);
            if (!select.attr("mdSelect")) {
                select.attr("mdSelect", true);

                select.wrap('<div class="mdCustomSelect"></div>');
                var wrapDiv = select.closest('div.mdCustomSelect');
                wrapDiv.append(createTextField(options));
                wrapDiv.append('<div class="mdSelectButton"></div>');

                select.bind('change keydown keypress keyup', changeEvent);
	            select.bind('click', clickEvents);

                changeText(this);

                _.defer(function () {
                    var zIndex = getZIndex(select);
                    
                    if (options.zIndex !== undefined && options.zIndex !== null) {
                        zIndex = options.zIndex;
                    }

                    select.css("z-index", zIndex);
                });
            }
        });

	    function createTextField(options) {
	        var textField = $('<span>', {
	            'class': 'mdSelectText',
	            'text': options && options.placeholder
	        });

		    if( options && options.placeholder) {
		        textField.addClass('mdSt-placeholder');
		    }

		    return textField;
	    }

        function changeEvent() {
            changeText(this);
            if (options && options.onSelect) {
                options.onSelect.call();
            }
        }

	    function clickEvents() {
		    var $s = $(this).get(0),
			    self = $(this),
			    span = self.next('span');
            
		    if ($s.selectedIndex === 0) {
		        span.removeClass('mdSt-placeholder');
	        }
	    }

        function changeText(select) {
            var $s = $(select).get(0),
                self = $(select),
                span = self.next('span'),
                selectedIndex = $s.selectedIndex;
            
            if (selectedIndex < 0) {
                return;
            }

            var option = $($s.options[$s.selectedIndex]),
                optionVal = option.attr('value'),
                val = option.text();

            span.text($.trim(val)).removeClass('mdSt-placeholder');

            if (optionVal == null || optionVal == '') {
                span.addClass('emptyOption');
            } else {
                span.removeClass('emptyOption');
            }
        }
        
        function destroyMdSelect(select) {
            select = $(select);
            select.removeAttr("mdSelect");

            var container = select.closest(".mdCustomSelect");
            container.find(".mdSelectText").remove();
            container.find(".mdSelectButton").remove();

            select.unwrap();
            
            select.unbind('change keydown keypress keyup', changeEvent);
        }

        function callMdSelectMethod(controls, action) {
            var method;
            
            switch(action) {
                case 'destroy':
                    method = destroyMdSelect;
                    break;
            }

            if (method) {
                controls.each(function (index, control) {
                    method.call(this, control);
                });
            }
        }
        
        function getZIndex(select) {
            var topParent = $(select).parents("body>*");
            if (topParent.length == 0) {
                topParent = $(select).parents().last();
            }

            var parentZIndex = Converter.toInteger($(topParent).css("z-index"));
            var elements = topParent.find(":not(select)");
            
            var max = _.max(elements, function (el) {
                var zIndex = Converter.toInteger($(el).css("z-index"));
                
                return zIndex;
            });
            max = Converter.toInteger($(max).css("z-index"));
            var highestIndex = Math.max(max, parentZIndex) + 1;

            return highestIndex;
        }
    };

    $.fn.mdSelectToUlSelect = function () {
        return this.each(function () {
            var select = $(this);

            this.recalculateWidth = function() {
                calculateWidth(select, mdSelectDropDownList);
            };

            if (checkExisting(select)) { return; }

            select.css('z-index', 0).css('visibility', 'hidden');
            select.wrap('<div class="mdCustomSelectWrap"><div class="mdCustomSelect"></div></div>');
            var mdSelectWrap = select.closest('.mdCustomSelectWrap');
            mdSelectWrap.append('<ul class="mdSelectDropDownList"><ul>');
            var mdSelect = mdSelectWrap.find('.mdCustomSelect');
            mdSelect.append('<span class="mdSelectText"></span><div class="mdSelectButton"></div>');
            var mdSelectText = mdSelectWrap.find('.mdSelectText');
            
            var changeText = function () {
                var $s = select.get(0);
                var val = $($s.options[$s.selectedIndex]).text();
                mdSelectText.text(val);
            };
            
            changeText();

            var mdSelectDropDownList = mdSelectWrap.find('.mdSelectDropDownList');
            
            mdSelectDropDownList.empty();
            
            var hideList = function () {
                mdSelectDropDownList.addClass('hide').hide();
            };

            var selectedVal = select.val();

            var options = select.find('option');
            options.each(function (index, value) {
                var mdListLi = $('<li></li>');
                var text = $(value).text();
                var val = $(value).val();
                if (selectedVal == val) {
                    mdListLi.addClass('selected');
                }
                mdListLi.append(text).attr('val', val);
                mdListLi.on('click', function () {
                    select.val(val).change();
                });
                mdSelectDropDownList.append(mdListLi);
            });

            hideList();

            calculateWidth(select, mdSelectDropDownList);

            if (select.attr("disabled")) {
                return;
            }
            
            mdSelect.on('click', function () {
                if(mdSelect.is('[disabled]')){
                    return;
                }

                if (mdSelectDropDownList.hasClass('hide')) {
                    mdSelectDropDownList.removeClass('hide').show();
                } else {
                    hideList();
                }
            });

            select.on('change', function () {
                mdSelectDropDownList.find('.selected').removeClass('selected');
                mdSelectDropDownList.find('[val=' + select.val() + ']').addClass('selected');
                changeText();
            });

            
            $('html').on('click', function (e) {
                if (e.target !== mdSelect.get(0) && !$.contains(mdSelect.get(0), e.target)) {
                    hideList();
                }
            });
        });

        function checkExisting(select) {
            if (select.closest('.mdCustomSelect').length || select.siblings('.mdSelectText').length) {
                return true;
            }
        }

        function calculateWidth(parent, list) {
            var parentWidth = parent.outerWidth(),
                listOuterWidth = list.outerWidth(),
                listWidth = list.width(),
                subtractingDifference,
                borderWidth = parseInt(parent.css('border-left-width'));

            if (listOuterWidth < parentWidth) {
                subtractingDifference = parentWidth - listOuterWidth + listWidth + (borderWidth * 2);
                list.width(subtractingDifference);
            }
        }
    };

    $.fn.mdUlSelector = function (options) {

        var selectClass = 'selected';

        var settings = {
            data: [],
            current: null,
            isSetDefault: false,
            onChange: function () { },
            position: 'left',
            className: ''
        };

        $.extend(settings, options);

        var args = arguments;
        return this.each(function () {
            var mdSelect = this;
            var $el = this.$el =  $(this);

            if (_.isString(options)) {
                if ($el.data('mdUlSelector')) {
                    action($el, options, args[1]);
                }
                
                return this;
            }

            var $ul = this.dropDown = $('<ul>');
            
            $el.addClass('mdUlSelectorText');
            $ul.addClass('mdUlSelect').addClass(settings.className);

            if (!settings.data.length) {
                return this;
            }
            
            for (var i = 0, item = settings.data[0]; i < settings.data.length; i++, item = settings.data[i]) {
                var itemText = item.label ? item.label : item.text;

                var li = $('<li />')
                    .html(itemText)
                    .data(item);

                if (item.value == settings.current) {
                    li.addClass(selectClass);
                }

                $ul.append(li);   
            }
            
            $ul.css({ 'display': 'none' });

            $el.html(getTextByValue(settings.data, settings.current));

            $('body').append($ul);
            
            if(settings.isSetDefault && !settings.current){
                var $li = $ul.children('li').first();
                selectValue.call(mdSelect, $li);
            }

            $el.on('click', function () {
                if ($ul.is(":visible")) {
                    $ul.css({ 'display': 'none' });
                    unbindEvents();
                    return;
                }

                if ($el.is('.disabled')) {
                    return;
                }

                $('.mdUlSelect').hide();
                unbindEvents();

                function setPosition() {

                var offset = $el.offset(),
                    top = offset.top + $el.height() + 2,
                    left = offset.left,
                    width = $ul.width(),
                    elWidth = $el.width(),
                    zIndex = 1000;
                
                if (elWidth < width) {
                        if (settings.position !== 'left') {
                        left = left - ((width - elWidth) / 2);
                    }
                }

                if (window.ZIndex) {
                    ZIndex.refresh();
                    zIndex = ZIndex.max + 1;
                }
                
                $ul.css({ top: top, left: left, 'z-index': zIndex });
                }

                setPosition();
                _.delay(function() {
                    $ul.css({ 'display': 'block' });
                });
                
                
                $('html').on('click.mdUlSelect', function (e) {
                    if (e.target !== $ul.get(0) && !$.contains($ul.get(0), e.target) && e.target !== $el.get(0)) {
                        $ul.css({ 'display': 'none' });
                        unbindEvents();
                    }
                });

                $(window).on('scroll.mdUlSelect', setPosition);
            });

            $ul.find('li').on('click', function (e) {
                var $li = $(e.target || e.srcElement || toElement).closest('li');
                selectValue.call(mdSelect, $li);
            });

            $el.data("mdUlSelector", {
                items: $ul
            });

            return this;
        });
        
        function setSelection($el, $ul, $li){
            $ul.find('li.' + selectClass).removeClass(selectClass);
            $li.addClass(selectClass);

            var data = $li.data();

            settings.current = data.value;

            $el.html(data.text);
        }

        function setSelectedItem($el, val){
            var $ul = $el.data('mdUlSelector').items,
                $li = $ul.find('li').filter(function(index, li){
                    return $(li).data().value === val;
                });

            setSelection($el, $ul, $li);
        }

        function unbindEvents() {
            $('html').off('click.mdUlSelect');
            $(window).off('scroll.mdUlSelect');
        }
        
        function getTextByValue(data, val) {
            var result = _.first(data).text;

            for (var i = 0; i < data.length; i++) {
                if (data[i].value == val) {
                    return data[i].text;
                }
            }

            return result;
        }
        
        function action(el, action, option) {
            if (_.isString(action)) {
                switch (action) {
                    case 'destroy':
                        destroyControl(el);
                        break;
                    case 'select':
                        setSelectedItem(el, option);
                        break;
                default:
                }
            }
        }
        
        function destroyControl(el) {
            var data = el.data('mdUlSelector');
            data.items.remove();

            el.data('mdUlSelector', undefined);
        }

        /** access private */
        function selectValue($li) {
            var silent = false;
            if ($li.hasClass(selectClass)) {
                silent = true;
            }

            this.dropDown.find('li.' + selectClass).removeClass(selectClass);
            $li.addClass(selectClass);

            var data = $li.data();

            settings.current = data.value;

            this.$el.html(data.text);

            if (settings.onChange) {
                if (silent === false) {
                    settings.onChange(settings.current, data);
                }
            }

            this.dropDown.css({ 'display': 'none' });
            unbindEvents();

            if (silent === false) {
                this.$el.trigger('change');
            }
        }
    };
    
})(jQuery);