(function (common) {

    var controlId = 0;

    var view = function () {

        var relatedSystem = common.Data.TaxationSystemType,
            controlCreator = {
                Usn: hideControl,
                Osno: hideControl,
                Envd: hideControl,
                UsnAndEnvd: createUsnAndEnvd,
                OsnoAndEnvd: createOsnoAndEnvd,
                getFunctionCreate: function (currentType) {
                    switch (currentType) {
                        case relatedSystem.Usn:
                        case relatedSystem.Osno:
                        case relatedSystem.Envd:
                            return this.Usn;
                        case relatedSystem.UsnAndEnvd:
                            return this.UsnAndEnvd;
                        case relatedSystem.OsnoAndEnvd:
                            return this.OsnoAndEnvd;
                        default:
                            return undefined;
                    }
                }
            },
            template = {
                Id: 'TaxationSystemControlTemplate',
                UsnAndEnvdControls: 'UsnAndEnvdControls',
                OsnoAndEnvdControls: 'OsnoAndEnvdControls',
                ControlsField: 'controls',
                TaxationSystemControl: 'TaxationSystemControl',
                getSearchId: function (id) {
                    return '#' + template[id];
                },
                getSearchClass: function (searchClass) {
                    return '.' + searchClass;
                }
            },
            $template,
            $usnAndEnvdTemplate,
            $osnoAndEnvdTemplate,
            $parentEl,
            currentTaxationSystem,
            currentRelatedSystem,
            viewModel,
            currentView;

        function checkRelatedTaxationSystem() {
            var isOsno = currentTaxationSystem.get('IsOsno'),
                isUsn = currentTaxationSystem.get('IsUsn'),
                isEnvd = currentTaxationSystem.get('IsEnvd');

            if (isUsn && isEnvd) {
                currentRelatedSystem = relatedSystem.UsnAndEnvd;
            } else if (isOsno && isEnvd) {
                currentRelatedSystem = relatedSystem.OsnoAndEnvd;
            } else if (isOsno) {
                currentRelatedSystem = relatedSystem.Osno;
            } else if (isUsn) {
                currentRelatedSystem = relatedSystem.Usn;
            } else if (isEnvd) {
                currentRelatedSystem = relatedSystem.Envd;
            } else {
                currentRelatedSystem = relatedSystem.Default;
            }
        }

        function bindEvent() {
            viewModel.on('change:Date', function () {
                refreshControl.call(currentView);
            });

            bindChangeControlValue();
        }

        function bindChangeControlValue() {
            $template.on('change radio', function (e) {
                viewModel.set('TaxationSystemType', e.target.value);
            });
        }

        function unbindEvent() {
            viewModel.off('change.TaxationSystemControl:Date');
        }

        function hideControl() {

        }

        function getTemplate() {
            $template = $(TemplateManager.getFromPage(this.template));

            $usnAndEnvdTemplate = $template.find(template.getSearchId(template.UsnAndEnvdControls));
            $osnoAndEnvdTemplate = $template.find(template.getSearchId(template.OsnoAndEnvdControls));
            $template.find(template.getSearchClass(template.ControlsField)).empty();
        }

        function createUsnAndEnvd () {
            createRadioControl.call(currentView, $usnAndEnvdTemplate.clone());
            appendControl();
        }

        function createOsnoAndEnvd () {
            createRadioControl.call(currentView, $osnoAndEnvdTemplate.clone());
            appendControl();
        }

        function createControl() {
            var createFunction = controlCreator.getFunctionCreate(currentRelatedSystem);

            if (createFunction) {
                createFunction.call(currentView);
            }
        }

        function createRadioControl(html) {
            html = changeIdAndNameRadio.call(currentView, html);
            $template.find(template.getSearchClass(template.ControlsField)).html(html);
        }
        
        function appendControl() {
            $parentEl.append($template);
        }

        function refreshControl() {
            setTaxationSystem();
            checkRelatedTaxationSystem();
            deleteControl();
            bindChangeControlValue();
            createControl();
            setControlValue();
            this.trigger('refresh');
        }

        function deleteControl() {
            $parentEl.find(template.getSearchId(template.TaxationSystemControl)).remove();
        }

        function setTaxationSystem() {
            const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            currentTaxationSystem = ts.Current(Converter.toDate(viewModel.get('Date')));
        }

        function setControlValue() {
            var taxSystem = viewModel.get('TaxationSystemType'), $radios;

            if (taxSystem > 0) {
                $radios = $template.find('input:radio[data-bind=TaxationSystemType][value=' + taxSystem + ']');
                $radios.prop('checked', true);
            } else {
                $radios = $template.find('input:radio[data-bind=TaxationSystemType]').first().prop('checked', true);
                viewModel.set('TaxationSystemType', $radios.val()).trigger('change');
            }
        }

        function initControl() {
            checkRelatedTaxationSystem();
            getTemplate.call(this);
        }

        function changeIdAndNameRadio(html) {
            html.find('[id], [name], [for]').each(function () {
                var $this = $(this),
                    id = $this.attr('id'),
                    name = $this.attr('name'),
                    forAttr = $this.attr('for'),
                    attrObj = {};

                if (name) {
                    attrObj.name = name + '_' + currentView.controlId;
                }
                
                if (id) {
                    attrObj.id = id + '_' + currentView.controlId;
                }
                
                if (forAttr) {
                    attrObj['for'] = forAttr + '_' + currentView.controlId;
                }

                $this.attr(attrObj);
            });
            
            return html;
        }

        return {
            initialize: function ($parentElement, model, options) {
                this.controlId = ++controlId;
                
                if (!$parentElement) {
                    return;
                }
                
                $parentEl = $parentElement;
                viewModel = model;
                currentView = this;

                setTaxationSystem();

                this.template = _.result(options, 'template') || template.Id;

                initControl.call(currentView);
            },

            refresh: function () {
                refreshControl.call(view);
            },

            render: function () {
                createControl.call(currentView);
                setControlValue();
                bindEvent();
            },

            destroy: function () {
                unbindEvent.call(currentView);
                deleteControl.call(currentView);
            },

            getTaxationSystem: function () {
                return currentTaxationSystem;
            }
        };
    }();
    
    common.Controls.TaxationSystemControl = Backbone.View.extend(view);

})(Common);