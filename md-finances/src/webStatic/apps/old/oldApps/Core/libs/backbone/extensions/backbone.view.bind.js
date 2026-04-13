(function (backbone) {
    backbone.View.prototype.bind = function (options) {
        var defaultOptions = {
            prefix: ''
        };
        options = _.extend(defaultOptions, options);

        var additionalBindings = _.result(this, 'bindings');
        this.bindings = _.extend({}, getDefaultBindings(this.$el), additionalBindings);
        
        this.stickit();

        function getDefaultBindings($el) {
            var bindings = {};
            $el.find('[data-bind]').each(function (index, element) {
                var fieldName = $(element).attr('data-bind').replace(options.prefix, ''),
                    tagName = $(element)[0].tagName.toLowerCase(),
                    selector = tagName + '[data-bind=' + options.prefix + fieldName + ']';

                bindings[selector] = {
                    observe: fieldName.replace(/_/g, '.'),
                    setOptions: {
                        validate: true
                    }
                };
            });
            return bindings;
        }
    };

})(Backbone);
