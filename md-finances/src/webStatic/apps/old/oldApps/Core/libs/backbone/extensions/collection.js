(function (main) {
    Backbone.Collection = Backbone.Collection.extend({
        sum: function (iterator, context) {
            iterator = iterator || 'Sum';

            var sumHandler = iterator;

            if (_.isString(iterator)) {
                sumHandler = function(model) {
                    return Converter.toFloat(model.get(iterator));
                };
            }
            
            var result = this.reduce(function (memo, model) {
                var sum = sumHandler.call(context, model);
                sum = sum ? sum : 0;
                return memo + sum;
            }, 0);

            return result;
        }
    });

})(window);
