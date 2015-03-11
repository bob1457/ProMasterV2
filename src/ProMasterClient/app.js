//Main application module, loading dependencies, e.g. routing configuration, controllers, etc.
//
(function () {
    //debugger;
    angular.module('ProMasterClient', ['ngRoute', 'ui.bootstrap', 'ngResource', 'ngCookies', 'ngDialog'])
        .constant({
            loginSuccess: 'auth-login-success',
            loginFailed: 'auth-login-failed',
            logoutSuccess: 'auth-logout-success',
            sessionTimeout: 'auth-session-timeout',
            notAuthenticated: 'auth-not-authenticated',
            notAuthorized: 'auth-not-authorized'
        });  //, 'angularUtils.directives.dirPagination']); //Create main application module
    angular.module('ProMasterClientDoc', ['ngRoute', 'ui.bootstrap', 'ngResource', 'ngCookies', 'ngDialog']); //, 'authService']); //Create main application module
    angular.module('ProMasterClientTools', ['ngRoute', 'ui.bootstrap', 'ngResource', 'ngCookies', 'ngDialog']); //, 'authService']); //Create main application module
    angular.module('ProMasterClientReport', ['ngRoute', 'ui.bootstrap', 'ngResource', 'ngCookies', 'ngDialog']); //, 'authService']); //Create main application module
    
    //Load and configure routing module
    //
    /*
    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            //$locationProvider.html5Mode(true);
            $routeProvider.
            when('/About', {
                templateUrl: 'views/about.html',
                controller: 'propertyController'
            }).
                when('/Manage', {
                    templateUrl: 'views/partials/PropertyList.html'
                }).
                when('/RouteTwo/:id', { 
                templateUrl: function (params) {return 'views/two.html?id=' + params.id }
                }).
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
        }]);
*/









    angular.module('ProMasterClient').run(['$rootScope', '$location', 
        function ($rootScope, $location) {

            //Client-side security. Server-side framework MUST add it's 
            //own security as well since client-based security is easily hacked



        }]);

})();

