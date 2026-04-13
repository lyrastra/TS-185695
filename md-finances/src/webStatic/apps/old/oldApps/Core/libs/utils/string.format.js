String.prototype.format = function () {
    var args = _.toArray(arguments);
    var str = this;
    return str.replace(new RegExp("{-?[0-9]+}", "g"), function(item) {
        var intVal = parseInt(item.substring(1, item.length - 1));
        var replace;
        if (intVal >= 0) {
            replace = args[intVal];
        } else {
            replace = "";
        }
        return replace;
    });
};

String.format = function() {
    var args = _.toArray(arguments);
    var str = _.first(args);
    return str.format.apply(str, _.rest(args));
};