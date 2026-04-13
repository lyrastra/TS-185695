(function (common) {

	common.Helpers.Mixer = {
		/**
		 * Cливает миксин с родительским объектом
		 *
		 * @param {object} [target] - Родительский объект
		 * @param {object} [mixin] - Объект - примесь
		 * @param {bool} [mixinmixinEventsforward] - эвенты примеси объявляются раньше
		 */
		addMixin: function (target, mixin, mixinEventsforward) {
            if(!(mixin && target)){
                return;
            }

			var self = this,
				events;
			target.events = target.events || {};
			mixin.events = mixin.events || {};
			
			if (mixinEventsforward) {
				events = {};
				$.extend(events, mixin.events, target.events);
			} else {
				events = target.events;
				$.extend(events, mixin.events);
			}
			
//			self.matchDetecting(events, mixin.events);
			$.extend(target, mixin);
			target.events = events;

			target.delegateEvents(events);
		},

		matchDetecting: function (events, mixinEvents) {
			_.each(mixinEvents, function(val, ind) {
				if (_.contains(events, val)) {
					if (console && console.warn) {
						console.warn('similar event ' + '\'' + ind + '\': ' + val);
					} 

				}
			});
		},

		addMixinWithExtend: addMixinWithExtend
	};
	
	/** access public */
	function addMixinWithExtend(target, source) {
		target = target || {};
		for (var prop in source) {
			if(source[prop] instanceof Array){
				target[prop] = _.union(target[prop], source[prop]);
			} else if (typeof source[prop] === 'object') {
				$.extend(target[prop], source[prop]);
			} else {
				target[prop] = source[prop];
			}
		}
		return target;
	}

})(Common);