(function () {

    'use strict';

    //angular.module('ProMasterClient');

    var pageCount = function () {

        return function (propertyListing, size) {
            if (angular.isArray(propertyListing)) {
                var result = [];
                for (var i = 0; i < Math.ceil(propertyListing.length / size) ; i++) {
                    result.push(i);
                }
                return result;
            } else {
                return propertyListing;
            }
        };

    };

    angular.module('ProMasterClient').filter("pageCount", pageCount);
    angular.module('ProMasterClientDoc').filter("pageCount", pageCount);
    angular.module('ProMasterClientTools').filter("pageCount", pageCount);
    angular.module('ProMasterClientReport').filter("pageCount", pageCount);

})();