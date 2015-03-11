(function () {
    'use strict';
    debugger;
    angular
        .module('ProMasterClient')
        .factory('authService', authService);

    authService.$inject = ['$http', 'base64', '$cookieStore', '$rootScope', '$timeout'];

    function authService($http, base64, $cookieStore, $rootScope, $timeout) {

        var serviceBase = '/api/Login'; //replace it with real url to webapi login
        var service = {};

        var _authentication = {
            isAuth: false,
            userName: "",
        };

       

        service.Login = function (username, password, callback) {

            /* Dummy authentication for testing, uses $timeout to simulate api call
             ----------------------------------------------*/
            //$timeout(function () {
            //    var response = { success: username === 'test' && password === 'test' };
            //    if (!response.success) {
            //        response.message = 'Username or password is incorrect';
            //    }
            //    callback(response);
            //}, 1000);


            /* Use this for real authentication
             ----------------------------------------------*/
            var credentials = base64.encode(username + ':' + password);

            var config = {
                headers: {
                    'Authorization': 'Basic ' + credentials,
                    'Accept': 'application/json;odata=verbose'
                    //"X-Testing": "testing"
                }
            };

            //$http.post(serviceBase, username, password)
            //    .success(function (response) {
            //        //callback(response);
            //        response.message = 'ok';
            //    });

            $http.defaults.headers.common['Authorization'] = 'Basic ' + credentials;
            $cookieStore.put('basicCredentials', credentials); //save the credentials

            $http.get(serviceBase, config).success(function() {
                _authentication.isAuth = true;
                _authentication.userName = username; //set current user's username
            }).error(function() {
                //handle errors
            });


        };

        //save the credentials - to be done after successful login (implemented in controller)
        service.SetCredentials = function (username, password) {
            var authdata = base64.encode(username + ':' + password);

            $rootScope.globals = {
                currentUser: {
                    username: username,
                    authdata: authdata
                }
            };

            $http.defaults.headers.common['Authorization'] = 'Basic ' + authdata; // jshint ignore:line
            $cookieStore.put('globals', $rootScope.globals);
        };

        service.ClearCredentials = function () { //tasks to be done when lougout
            _authentication.isAuth = false;
            $rootScope.globals = {};
            $cookieStore.remove('globals');
            $http.defaults.headers.common.Authorization = 'Basic ';
        };

        return service;

        //function getData() { }
    }
})();