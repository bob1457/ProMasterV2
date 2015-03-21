var app = angular.module('ProMasterClient');
var appDoc = angular.module('ProMasterClientDoc');
var appTools = angular.module('ProMasterClientTools');
var appReport = angular.module('ProMasterClientReport');


app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider, $stateProvider) {
    //$locationProvider.html5Mode({
    //  enabled: true,
    //  requireBase: false
    //});
    $routeProvider.
        when('/', {
            templateUrl: 'views/partials/RentList.html',
            authenticated: false
            //controller: 'propertyController'
        }).
        when('/About', {
            templateUrl: 'views/partials/About.html',
            //controller: 'propertyController'
            authenticated: false
        }).
        when('/RentList', {
            templateUrl: 'views/partials/RentList.html',
            authenticated: false
        }).
        when('/Manage', {
            templateUrl: 'views/partials/PropertyList.html',
            controller: 'manageController',
            authenticated: true
        }).
        //when('/RouteTwo/:id', {
        //    templateUrl: function (params) { return 'views/two.html?id=' + params.id; }
        //}).
        when('/Properties', {
            templateUrl: 'views/partials/PropertyList.html',
            authenticated: true
        }).
        when('/Tenants', {
            templateUrl: 'views/partials/TenantList.html',
            authenticated: true
        }).
        when('/Owners', {
            templateUrl: 'views/partials/OwnerList.html',
            authenticated: true
        }).
        when('/Leases', {
            templateUrl: 'views/partials/LeaseList.html',
            authenticated: true
        }).
        when('/Contracts', {
            templateUrl: 'views/partials/ContractList.html',
            authenticated: true
        }).
        when('/Vendors', {
            templateUrl: 'views/partials/VendorList.html',
            authenticated: true
        }).
    otherwise({
        redirectTo: '/',
        authenticated: false
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
}]).config(['$httpProvider', function ($httpProvider) {
    //Http Intercpetor to check auth failures for xhr requests
    $httpProvider.interceptors.push('authHeaderInterceptor');
}]);


appDoc.config(['$routeProvider', '$locationProvider', function($routeProvider, $locationProvider) {

    $routeProvider.
        when('/', {
            templateUrl: 'NoData.html',
            authenticated: false
            //controller: 'propertyController'
        }).
        when('/BrowseDocs', {
            templateUrl: 'views/partials/BrowseDocs.html',
            authenticated: true
            //controller: 'docController'
        }).
        when('/SearchDocs', {
            templateUrl: 'views/partials/SearchDocs.html',
            authenticated: true
            //controller: 'docController'
        }).
        otherwise({
            redirectTo: '/',
            authenticated: false

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
            templateUrl: 'NoData.html',
            authenticated: false
            //controller: 'propertyController'
        }).
         when('/Calendar', {
             templateUrl: 'views/partials/Calendar.html',
             authenticated: true
             //controller: 'docController'
         }).
        when('/ListingProcess', {
            templateUrl: 'views/partials/ListingProcess.html',
            authenticated: true
            //controller: 'docController'
        }).
        when('/Applications', {
            templateUrl: 'views/partials/Applications.html',
            authenticated: true
            //controller: 'docController'
        }).
        otherwise({
            redirectTo: '/',
            authenticated: false

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
            templateUrl: 'NoData.html',
            authenticated: false
            //controller: 'propertyController'
        }).
         when('/Financial', {
             templateUrl: 'views/partials/Financial.html',
             authenticated: true
             //controller: 'docController'
         }).
        when('/Business', {
            templateUrl: 'views/partials/Business.html',
            authenticated: true
            //controller: 'docController'
        }).
        when('/Data', {
            templateUrl: 'views/partials/Data.html',
            authenticated: true
            //controller: 'docController'
        }).
        otherwise({
            redirectTo: '/',
            authenticated: false

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

/* To check if the user is authenticated, not sure where to put this code */
/*
$rootScope.$on('$routeChangeStart', function (event, next) {
    var userAuthenticated = ...; // Check if the user is logged in

    if (!userAuthenticated && !next.isLogin) {
        //You can save the user's location to take him back to the same page after he has logged-in 
        $rootScope.savedLocation = $location.url();


        $location.path('/User/LoginUser');
    }
    });
*/
