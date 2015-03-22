(function () {

    var injectParams = ['$scope', 'ngDialog', '$location'];//, '$rootScope', '$timeout', '$dialogs'];

    var homeController = function ($scope, ngDialog, $location) {

        //Display date, day and year on top bar
        //
        $scope.day = new Date().getDay();//   dayNames[new Date().getDay()];

        var d = new Date();
        var year = d.getFullYear();
        var month = d.getMonth();
        var date = d.getDate();

        var months = [
            'January',
            'Februray',
            'March',
            'April',
            'May',
            'June',
            'July',
            'August',
            'September',
            'October',
            'November',
            'December'
        ];

        var dateOutput = months[month] + " " + date + ", " + year;

        $scope.date = dateOutput;
        $scope.year = year;

        //Display dialog (modal)
        //
        $scope.openDefault = function () {
            ngDialog.open({
                template: 'firstDialogId',
                //controller: 'InsideCtrl',
                className: 'ngdialog-theme-default'
            });
        };

        $scope.openLogin = function () {
            ngDialog.open({
                template: 'loginDialogId',
                //controller: 'InsideCtrl',
                className: 'ngdialog-theme-default'
            });
        };

        //$scope.isActive = function(route) {
        //    return route = $location.path();
        //};

    };

    homeController.$inject = injectParams;

    //Load controller
    //
    angular.module('ProMasterClient').controller('homeController', homeController);
    //angular.module('ProMasterClientDoc').controller('homeController', homeController);
    //angular.module('ProMasterClientTools').controller('homeController', homeController);
    //angular.module('ProMasterClientReport').controller('homeController', homeController);

}());

