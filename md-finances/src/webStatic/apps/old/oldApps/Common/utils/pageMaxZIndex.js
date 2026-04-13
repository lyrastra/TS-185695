(function (module) {

    var getZindex = function() {
        var def = { inc: 10, group: "*" };
        var newMax = 0;
        $(def.group).each(function() {
            var cur = parseInt($(this).css('z-index'));
            newMax = cur > newMax ? cur : newMax;
        });
        return newMax;
    };

    module.ZIndex = {
        max: getZindex(),
        
        refresh: function() {
            this.max = getZindex();
        }
    };
})(window);
