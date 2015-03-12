(function () {
    'use strict';
    debugger;
    angular
        .module('ProMasterClient')
        .controller('loginController', loginController);

    loginController.$inject = ['$scope', '$http', '$rootScope', '$location', 'authService', 'base64'];

    function loginController($scope, $http, $rootScope, $location, authService, base64) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'loginController';

       

        //authService.ClearCreadentials(); //reset login status

        $scope.login = function () {

            //$scope.dataLoading = true;
            //authService.Login($scope.username, $scope.password, function (response) {
            //    if (response.success) {
            //        //$scope.message = "Authentication Success!";
            //        authService.SetCredentials($scope.username, $scope.password);
            //        $location.path('http://localhost:2379/app/Manage.html#/Properties');
            //    } else {
            //        $scope.error = response.message;
            //        $scope.dataLoading = false;
            //    }

                

            //});

            authService.Login($scope.username, $scope.password).success(function (response) {

                    authService.SetCredentials($scope.username, $scope.password);

                    $location.path('/app/Manage.html#/Properties');
                },
                    function (err) {
                    $scope.message = err.error_description;
            });
            


            //var serviceBase = '/api/Login'; //replace it with real url to webapi login
            //var credentials = base64.encode($scope.username + ':' + $scope.password);

            //$http.get(serviceBase, credentials)
            //    .success(function (response) {

            //    });

        };

       


        $scope.logoutSuccess = function() {
            authService.ClearCreadentials();
            $location.path('/'); //redirect back to home/login page
        };

        //activate();

        //function activate() { }
    }
})();
