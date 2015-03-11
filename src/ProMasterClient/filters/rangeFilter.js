(function () {

    'use strict';

    //angular.module('ProMasterClient');

    var range = function ($filter) {

        return function (propertyListing, page, size) {
            if (angular.isArray(propertyListing) && angular.isNumber(page) && angular.isNumber(size)) {
                var start_index = (page - 1) * size;
                if (propertyListing.length < start_index) {
                    return [];
                } else {

                    return $filter("limitTo")(propertyListing.splice(start_index), size);
                }
            } else {
                return propertyListing;
            }
        };

    };

    angular.module('ProMasterClient').filter("range", range);
    angular.module('ProMasterClientDoc').filter("range", range);
    angular.module('ProMasterClientTools').filter("range", range);
    angular.module('ProMasterClientReport').filter("range", range);

})();