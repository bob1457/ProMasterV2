(function () {

    'use strict';

    angular.module('ProMasterClient');

    var dayName = function () {

        var dayNames = ["Sunday", "Monday", "Tuesday", "Wednesday",
                            "Thursday", "Friday", "Saturday"];

        return function(input) {
            return angular.isNumber(input) ? dayNames[input] : input;
        };
    
    };

    angular.module('ProMasterClient').filter("dayName", dayName);
    angular.module('ProMasterClientDoc').filter("dayName", dayName);
    angular.module('ProMasterClientTools').filter("dayName", dayName);
    angular.module('ProMasterClientReport').filter("dayName", dayName);

})();