(function () {
    'use strict';

    angular
        .module('ProMasterClient')
        .factory('authHeaderInterceptor', authHeaderInterceptorFactory);

    authHeaderInterceptorFactory.$inject = ['$q', '$location'];

    function authHeaderInterceptorFactory($q, $location) {
        //var service = {
        //    getData: getData
        //};

        //return service;

        //function getData() { }

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

        return function (promise) {
            return promise.then(success, error);
        };

        //return {
        //    response: function (response) {
        //        if (response.status === 401) {
        //            console.log("Response 401");
        //        }
        //        return response || $q.when(response);
        //    },
        //    responseError: function (rejection) {
        //        if (rejection.status === 401) {
        //            console.log("Response Error 401", rejection);
        //            $location.path('/login').search('returnTo', $location.path());
        //        }
        //        return $q.reject(rejection);
        //    }
        //};
    }
})();