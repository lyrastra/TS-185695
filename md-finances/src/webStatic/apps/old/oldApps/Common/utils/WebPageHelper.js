(function (module) {
    var defaultTitle = "Интернет-бухгалтерия Моё дело";

    window.WebPage = {
        setTitle: function(title) {
            title = String.format("{0} − интернет-бухгалтерия Моё дело", title) || defaultTitle;
            document.title = title;
        }
    };
    
})(window);
