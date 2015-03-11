var app = angular.module('ProMasterClient');
var appDoc = angular.module('ProMasterClientDoc');
var appTools = angular.module('ProMasterClientTools');
var appReport = angular.module('ProMasterClientReport');


app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider, $stateProvider) {
    //$locationProvider.html5Mode(true);
    $routeProvider.
        when('/', {
            templateUrl: 'views/partials/RentList.html'
            //controller: 'propertyController'
        }).
        when('/About', {
            templateUrl: 'views/partials/About.html'
            //controller: 'propertyController'
        }).
        when('/RentList', {
            templateUrl: 'views/partials/RentList.html'
        }).
        when('/Manage', {
            templateUrl: 'views/partials/PropertyList.html',
            controller: 'manageController'
        }).
        //when('/RouteTwo/:id', {
        //    templateUrl: function (params) { return 'views/two.html?id=' + params.id; }
        //}).
        when('/Properties', {
            templateUrl: 'views/partials/PropertyList.html'
        }).
        when('/Tenants', {
            templateUrl: 'views/partials/TenantList.html'
        }).
        when('/Owners', {
            templateUrl: 'views/partials/OwnerList.html'
        }).
        when('/Leases', {
            templateUrl: 'views/partials/LeaseList.html'
        }).
        when('/Contracts', {
            templateUrl: 'views/partials/ContractList.html'
        }).
        when('/Vendors', {
            templateUrl: 'views/partials/VendorList.html'
        }).
    otherwise({
        redirectTo: '/'
    });
}]).config(['ngDialogProvider', function (ngDialogProvider) {
    ngDialogProvider.setDefaults({
        className: 'ngdialog-theme-default',
        plain: false,
        showClose: true,
        closeByDocument: true,
        closeByEscape: true,
        appendTo: false,
        preCloseCallback: function () {
            console.log('default pre-close callback');
        }
    });
}]).config(function ($httpProvider) {
    $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';

    var interceptor = ['$rootScope', '$q', function (scope, $q) {

        function success(response) {
            return response;
        }

        function error(response) {
            var status = response.status;

            if (status === 401) {
                var deferred = $q.defer();
                var req = {
                    config: response.config,
                    deferred: deferred
                };
                //scope.requests401.push(req);
                scope.$broadcast('event:auth-loginRequired');
                return deferred.promise;
            }
            // otherwise
            return $q.reject(response);
        }

        return function(promise) {
            return promise.then(success, error);
        };

    }];

    //$httpProvider.responseInterceptors.push(interceptor);
});


appDoc.config(['$routeProvider', '$locationProvider', function($routeProvider, $locationProvider) {

    $routeProvider.
        when('/', {
            templateUrl: 'NoData.html'
            //controller: 'propertyController'
        }).
        when('/BrowseDocs', {
            templateUrl: 'views/partials/BrowseDocs.html'
            //controller: 'docController'
        }).
        when('/SearchDocs', {
            templateUrl: 'views/partials/SearchDocs.html'
            //controller: 'docController'
        }).
        otherwise({
            redirectTo: '/'

        });
}]).config(['ngDialogProvider', function (ngDialogProvider) {
    ngDialogProvider.setDefaults({
        className: 'ngdialog-theme-default',
        plain: false,
        showClose: true,
        closeByDocument: true,
        closeByEscape: true,
        appendTo: false,
        preCloseCallback: function () {
            console.log('default pre-close callback');
        }
    });
}]);


appTools.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

    $routeProvider.
        when('/', {
            templateUrl: 'NoData.html'
            //controller: 'propertyController'
        }).
         when('/Calendar', {
             templateUrl: 'views/partials/Calendar.html'
             //controller: 'docController'
         }).
        when('/ListingProcess', {
            templateUrl: 'views/partials/ListingProcess.html'
            //controller: 'docController'
        }).
        when('/Applications', {
            templateUrl: 'views/partials/Applications.html'
            //controller: 'docController'
        }).
        otherwise({
            redirectTo: '/'

        });
}]).config(['ngDialogProvider', function (ngDialogProvider) {
    ngDialogProvider.setDefaults({
        className: 'ngdialog-theme-default',
        plain: false,
        showClose: true,
        closeByDocument: true,
        closeByEscape: true,
        appendTo: false,
        preCloseCallback: function () {
            console.log('default pre-close callback');
        }
    });
}]);


appReport.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

    $routeProvider.
        when('/', {
            templateUrl: 'NoData.html'
            //controller: 'propertyController'
        }).
         when('/Financial', {
             templateUrl: 'views/partials/Financial.html'
             //controller: 'docController'
         }).
        when('/Business', {
            templateUrl: 'views/partials/Business.html'
            //controller: 'docController'
        }).
        when('/Data', {
            templateUrl: 'views/partials/Data.html'
            //controller: 'docController'
        }).
        otherwise({
            redirectTo: '/'

        });
}]).config(['ngDialogProvider', function (ngDialogProvider) {
    ngDialogProvider.setDefaults({
        className: 'ngdialog-theme-default',
        plain: false,
        showClose: true,
        closeByDocument: true,
        closeByEscape: true,
        appendTo: false,
        preCloseCallback: function () {
            console.log('default pre-close callback');
        }
    });
}]);