(function ($) {

    var pluginVersion = 'v 0.1',
        count = 0;

    $.fn.mdSelectUls = function (options) {
        var settings = $.extend({
            overflow: false,
            valOnText: false,
            style: 'old'
        }, options);

        return this.each(function () {
            var $select = $(this),
                currentSelect = $select.data('MdSelect'),
                pluginObj;
            
            if (!currentSelect) {
                pluginObj = new select($select, settings);
                $select.data('MdSelect', pluginObj);
            } else {
                currentSelect.refresh();
            }
        });
    };

    var select = function ($select, settings) {
        this.settings = settings;
        this.version = pluginVersion;
        this.numberPluginOfPage = count++;

        var pluginObj = this,
            selectClass = 'selected',
            namespace = 'mdSelectUls' + pluginObj.numberPluginOfPage,
            htmlObj;

        function showAllOptions () {
            htmlObj.$ul.find('li').show();
        }
        
        function hideAllOptions () {
            htmlObj.$ul.find('li').hide();
        }
        
        function showOptions(hideValue) {
            var $li = htmlObj.$ul.find('li');
            
            $li.each(function () {
                var $this = $(this);
                
                if (hideValue.indexOf(parseInt($this.attr('data-val'), 10)) != -1) {
                    $this.removeClass('hidden');
                } else {
                    $this.addClass('hidden');
                }
            });

            var $selectedLi = $li.filter('.selected');
            if ($selectedLi.hasClass('hidden')) {
                setSelectedItem(parseInt($li.not('.hidden').first().attr('data-val'), 10));
            }
        }
        
        function hideOptions(hideValue) {
            var $li = htmlObj.$ul.find('li');

            $li.each(function () {
                var $this = $(this);

                if (hideValue.indexOf(parseInt($this.attr('data-val'), 10)) != -1) {
                    $this.addClass('hidden');
                } else {
                    $this.removeClass('hidden');
                }
            });

            var $selectedLi = $li.filter('.selected');
            if ($selectedLi.hasClass('hidden')) {
                setSelectedItem(parseInt($li.not('.hidden').first().attr('data-val'), 10));
            }
        }
        
        function setSelectedItem(value) {
            var $li = htmlObj.$ul.find('li[data-val="' + value + '"]');
            
            if ($li.length && !$li.hasClass('selected')) {
                htmlObj.$ul.find('li').removeClass('selected');
                $li.addClass('selected');
                htmlObj.$mdSelectText.html($li.html());
                
                if ($select.val() != value) {
                    $select.val(value).trigger('change');
                }
            }
        }

        function refresh() {
            createList(htmlObj.$ul, $select.find('option'));
            calculateWidth($select, htmlObj.$ul);
            bindLiEvents();
        }

        function destroy () {
            htmlObj.$ulWrap.remove();
        }

        function bindLiEvents() {
            htmlObj.$ul.find('li').on('click', function (e) {

                var $li = $(e.target || e.srcElement || toElement).closest('li')
                    text = '';

                htmlObj.$ul.find('li.' + selectClass).removeClass(selectClass);
                $li.addClass(selectClass);

                var value = $li.attr('data-val');
                $select.val(value);
                var $option = $select.find('[value="' + value + '"]');
                $option.attr('selected', true);

                if (settings.valOnText) {
                    text = value;
                } else {
                    text = $li.html();
                }

                htmlObj.$customSelect.find('.mdSelectText').html(text);

                closeSelect(htmlObj.$ulWrap, namespace);

                $select.trigger('change');
            });

            htmlObj.$ul.find('li a').on('click', function (e) {
                var $anchor = $(e.target || e.srcElement || toElement);
                window.open($anchor.attr('href'));

                e.preventDefault();
                e.stopPropagation();
            });
        }

        function setPosition() {
            var offset = htmlObj.$customSelect.offset(),
                top = offset.top,
                left = Math.round(offset.left),
                width = htmlObj.$ulWrap.width(),
                elWidth = $select.width(),
                zIndex = 1000;

            if (elWidth < width) {
                if (settings.position == "left") {
                    var padding = parseInt(htmlObj.$ulWrap.find("li").css("padding-left"));
                    left = left - padding;
                } else {
                    left = left - ((width - elWidth) / 2);
                }
            }

            if (ZIndex) {
                ZIndex.refresh();
                zIndex = ZIndex.max + 1;
            }

            htmlObj.$ulWrap.css({ 'top': top - 1, 'left': left, 'z-index': zIndex, display: 'block' });
        }

        htmlObj = htmlCreator($select, settings);

        htmlObj.$customSelect.on('click', function () {
            if ($select.is(':disabled')) {
                return;
            }

            setPosition();
            calculateWidth($select, htmlObj.$ul);
            
            $('html').on('click.' + namespace, function (event) {
                if (event.target !== htmlObj.$customSelect.get(0) && !$.contains(htmlObj.$customSelect.get(0), event.target)) {
                    closeSelect(htmlObj.$ulWrap, namespace);
                }
            });

            $(window).on('scroll.mdUlSelect', function () {
                setPosition();
            });
            
            if (!('onhashchange' in window) && Backbone) {
                Backbone.history.unbind('route', destroy);
                Backbone.history.bind('route', destroy);
            } else {
                $(window).on('hashchange', destroy);
            }
        });

        $select.on('change', function () {
            var text;
            var val = $select.val();

            if (settings.valOnText) {
                text = val;
            } else {
                text = htmlObj.$ul.find('[data-val="' + $select.val() + '"]').html();
            }

            htmlObj.$customSelect.find('.mdSelectText').html(text);
            setSelectedItem(val);
        });

        bindLiEvents();

        return {            
            obj: this,
            showAllOptions: showAllOptions,
            hideAllOptions: hideAllOptions,
            showOptions: showOptions,
            hideOptions: hideOptions,
            refresh: refresh,
            destroy: destroy
        };
    };

    var htmlCreator = function ($select, settings) {
        var options = $select.find('option'),
            $ulWrap = $('<div>'),
            $ul = $('<ul>'),
            $customSelect;

        if (settings.width) {
            $ul.css('width', settings.width);
        }

        $select.wrap('<div class="mdCustomSelectWrap"><div class="mdCustomSelect"></div></div>');

        $customSelect = $select.closest('.mdCustomSelect');
        $customSelect.append('<span class="mdSelectText"></span><div class="mdSelectButton"></div>');

        $ulWrap.addClass('mdSelectUlWrap').addClass(settings.className);
        $ul.addClass('mdSelectDropDownList');

        createList($ul, options);

        $ulWrap.css({ 'display': 'none' });

        if (settings.overflow) {
            $ulWrap.addClass('mdSelectDropDownOverflow');
        }

        $customSelect.find('.mdSelectText').html(getText($select, settings.valOnText));

        $ulWrap.append($ul);
        $('body').append($ulWrap);

        calculateWidth($select, $ul);
        return { $ulWrap: $ulWrap, $ul: $ul, $customSelect: $customSelect, $mdSelectText: $customSelect.find('.mdSelectText') };
    };

    function createList($ul, options) {
        $ul.empty();
        options.each(function (index, option) {
            var $vl = $(option);
            var $li = $('<li>').html($vl.text()).attr('data-val', $vl.val());

            if ($vl.attr('selected')) {
                $li.addClass('selected');
            }

            $ul.append($li);
        });
    }
    
    function closeSelect ($select, namespace) {
        $select.css({ 'display': 'none' });
        unbindEvents(namespace);
    }

    function unbindEvents(namespace) {
        $('html').off('click.' + namespace);
        $(window).off('scroll.mdUlSelect');
    }

    function calculateWidth(parent, list) {
        var widthAttr = parent.data('correctwidth');

        if (!widthAttr) {
            var parentWidth = parent.outerWidth(),
                listOuterWidth = list.outerWidth(),
                listWidth = list.width(),
                subtractingDifference;

            if (listOuterWidth < parentWidth) {
                subtractingDifference = parentWidth - listOuterWidth + listWidth;
                list.width(subtractingDifference + 2);
            }
        } else {
            list.width(widthAttr);
        }
    }

    function getText($select, valOnText) {
        var selectedText = $select.find('option:selected');

        if (valOnText) {
            return selectedText.val();
        } else {
            if (selectedText.length) {
                return $('<div />').html(selectedText.text()).text();
            }

            return $select.find('option').first().html();
        }
    }

})(jQuery);
