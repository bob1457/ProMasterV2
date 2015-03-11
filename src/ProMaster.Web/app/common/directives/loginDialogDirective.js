(function() {
    'use strict';

    angular
        .module('ProMasterClient')
        .directive('loginDialog', loginDialogDirective);

    //loginDialogDirective.$inject = ['$window'];
    
    function loginDialogDirective () {
        // Usage:
        //     <loginDialogDirective></loginDialogDirective>
        // Creates:
        // 
        var directive = {
            templateUrl: 'view/partials/loginDialog.html',
            restrict: 'E',
            replace: true,
            controller: 'CredentialsController',
            link: function (scope, element, attributes, controller) {
                scope.$on('event:auth-loginRequired', function () {
                    console.log("got login event");
                    element.modal('show');
                });

                scope.$on('event:auth-loginConfirmed', function () {
                    element.modal('hide');
                    scope.credentials.password = '';
                });
            }
        };

        return directive;

        function link(scope, element, attrs) {
        }
    }

})();