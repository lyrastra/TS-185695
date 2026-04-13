$(document).ready(function() {
    $("#logo_balance_cell.active").click(function() {
        var firstObject = $(this).first();

        if (firstObject.hasClass("logo_balance_cell_selected")) {
            firstObject.removeClass("logo_balance_cell_selected");
        } else {
            firstObject.addClass("logo_balance_cell_selected");
        }
    })
        .mouseleave(function() {
            $(this).first().removeClass("logo_balance_cell_selected");
        });
    
    $(".qtip_balance_span").qtip({ content: { text: $("span[qtip_balance]").attr("qtip_balance") }, position: { my: "bottom left", at: "top right" }, style: { classes: "money-qtip-yellow tooltip_big_text ui-tooltip-moedelo ui-tooltip-rounded" } });
});