;(function($){

	$.fn.monthPicker = function(){

	    
	
		/*** Переменные ***/
var currentDate = new Date();
var month = new Array(12);
    month[0] = "01";
    month[1] = "02";
    month[2] = "03";
    month[3] = "04";
    month[4] = "05";
    month[5] = "06";
    month[6] = "07";
    month[7] = "08";
    month[8] = "09";
    month[9] = "10";
    month[10] = "11";
    month[11] = "12";

var smallMonth = new Array(12);
    smallMonth[0] = "янв";
    smallMonth[1] = "фев";
    smallMonth[2] = "мар";
    smallMonth[3] = "апр";
    smallMonth[4] = "май";
    smallMonth[5] = "июн";
    smallMonth[6] = "июл";
    smallMonth[7] = "авг";
    smallMonth[8] = "сен";
    smallMonth[9] = "окт";
    smallMonth[10] = "ноя";
    smallMonth[11] = "дек";
    


/*** Функции ***/


    
    monthAndYear(this);

    $('.month-changed').removeClass("month-changed");
    this.addClass("month-changed");
    var monthChanger = $(".month-changed");  
    
    var newMonthValue = newMonthValueCreating();
    monthChanger.attr("value", newMonthValue);
    
    $(".monthpicker a").click(function( event ){
        event.stopPropagation();
        event.preventDefault();
        var currentLink = $(this);

        if(currentLink.hasClass("next-year")){
            newMonthValue = newMonthValueCreating( "increase" );
            monthChanger.attr("value", newMonthValue).change();
            
        }
        else if(currentLink.hasClass("previous-year")){
            newMonthValue = newMonthValueCreating( "reduction" );
            monthChanger.attr('value', newMonthValue).change();
            
        }
        else{
            $('.active-date').removeClass("active-date");
            currentLink.addClass("active-date");
            newMonthValue = newMonthValueCreating();
            monthChanger.attr('value', newMonthValue).change();
        }
    });


function monthAndYear( inputField ){
        var existingValueYear;
        var existingValueMonth;
        var reg = /\d{4}/;

        if( inputField.val().length){
            
            var narr = inputField.val().split(' ');
            var result = reg.test(narr[1]);

            if(in_array(narr[0], month) && result){
            
                for (var key in month) {
                    var val = month[key];
                    if(narr[0] == val){
                        existingValueYear = narr[1];
                        existingValueMonth = key;
                    }        
                }
            }
            else{
            existingValueYear = currentDate.getFullYear();
            existingValueMonth = currentDate.getMonth();
            }
        }
        else{
            existingValueYear = currentDate.getFullYear();
            existingValueMonth = currentDate.getMonth();
        }
        
        $(".monthpicker").remove();
        inputField.after("<div class='monthpicker'><div class='header'><a href='#' class='previous-year'></a><div>" + existingValueYear + "</div><a href='#' class='next-year'></a></div><div class='month-grid'></div></div>");
        var monthGrid = $(".month-grid");


        for (var key in smallMonth) {
                var val = smallMonth[key];

                if(existingValueMonth == key){
                    monthGrid.append("<a href='#' class='active-date' data-month='" + key + "'><span>" + val + "</span></a>");
                }
                else{
                    monthGrid.append("<a href='#' data-month='" + key + "'><span>" + val + "</span></a>");
                }
                
            }
        
}

function newMonthValueCreating( activity ){
    var yearHolder = $(".monthpicker .header>div"),
    currentYear = $(".monthpicker .header>div").text(),
    monthName;

    currentYear = parseInt(currentYear);

    if( activity == "increase" ){
        currentYear++;
    }
    else if( activity == "reduction" ){
        currentYear--;
    }

    yearHolder.text(currentYear);

    if($(".active-date").length){
        monthName = month[$(".active-date").attr("data-month")];
    }
    else{
        monthName  = month[currentDate.getMonth()];
    }
    return monthName + "." + currentYear;
}

function in_array(needle, haystack, strict) {  

    var found = false, key, strict = !!strict;

    for (key in haystack) {
        if ((strict && haystack[key] === needle) || (!strict && haystack[key] == needle)) {
            found = true;
            break;
        }
    }

    return found;
}
	};
}(jQuery));