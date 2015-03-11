(function () {

    var injectParams = ['$scope'];//, '$rootScope', '$timeout', '$dialogs'];

    var homeController = function ($scope) {

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


        //Display login dialog (modal)
        //
        //$scope.loginDialog = function() {

        //    var modalOptions = {

        //    };

        //    modalService.showModal({}, modalOptions).then(function (result) {
                

        //    });
        //};

        

    };

    homeController.$inject = injectParams;

    //Load controller
    //
    angular.module('ProMasterClient').controller('homeController', homeController);

    angular.module('ProMasterClientDoc').controller('homeController', homeController);

}());

