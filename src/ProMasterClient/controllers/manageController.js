(function () {
    'use strict';

    var injectParams = ['$scope'];

    angular
        .module('ProMasterClient')
        .controller('manageController', manageController);

    manageController.$inject = ['$scope']; 

    function manageController($scope) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'manageController';


        $scope.properties = [
            {
                id: '1',
                title: '1729 Coquitlam',
                type: 'House',
                status: 'Rented',
                created: 'September 5, 2013'
            },
            {
                id: '2',
                title: '10621 Surrey',
                type: 'House',
                status: 'Rented',
                created: 'March 5, 2012'
            },
            {
                id: '3',
                title: 'Surrey Townhouse 1015',
                type: 'Apartment',
                status: 'Rented',
                created: 'October 5, 2014'
            },
            {
                id: '4',
                title: '301 Burnaby',
                type: 'Apratment',
                status: 'Rented',
                created: 'January 15, 2005'
            }
        ];


        activate();

        function activate() { }
    }
})();
