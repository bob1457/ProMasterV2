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
    
    
    angular.module('ProMasterClient').run(['$rootScope', '$location', 
        function ($rootScope, $location) {

            //Client-side security. Server-side framework MUST add it's 
            //own security as well since client-based security is easily hacked



        }]);

})();

