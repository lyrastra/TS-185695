(function(window){

	var factor = 1000;

	window.MathOperations = {
		addition: function(){
			if(!arguments.length ){
				return 0;
			}

			if(arguments.length === 1 && arguments[0] instanceof Array){
				return multiAddition(arguments[0]);
			}

			if(arguments.length >= 2){
				return multiAddition(arguments);
			}

			return arguments[0];
		}
	};

	function toFloat (val) {
		var convVal = window.Converter.toFloat(val);
		return convVal ? convVal * factor : 0;
	}

	function multiAddition(array) {
	    var sum = 0,
	        i = 0;
	    for (i; i < array.length; i++){
			if (array.hasOwnProperty(i)){
				sum += toFloat(array[i]);
			}
		}

		return Math.round(sum) / factor;
	}
	
})(window);
