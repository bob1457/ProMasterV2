(function () {
    //'use strict';

    var injectParams = ['$scope', '$http'];
    //debugger;
    function propertyController($scope, $http) {
       
        //Test scope
        $scope.title = "For Rent";

        //pagination
        $scope.currentPage = 1;
        $scope.pageSize = 2;

        $scope.selectPage = function(newPage) {
            $scope.selectedPage = newPage;
        };

        //Test scope for list
        //$scope.propertyListing = [
        //    {
        //        imgUrl: '/content/assets/images/property.png',
        //        title: '621 Coquitlam',
        //        desc: 'Two layer indvidual house',
        //        city: 'Coquitlam',
        //        rent: '2,100'
        //    },
        //    {
        //        imgUrl: '/content/assets/images/property.png',
        //        title: '10621 Surrey',
        //        desc: 'Single house in Fraser Heights',
        //        city: 'Surrey',
        //        rent: '2,400'
        //    },
        //    {
        //        imgUrl: '/content/assets/images/property.png',
        //        title: 'Surrey Condo 226',
        //        desc: 'Two bedroom apratment in Surrey Central',
        //        city: 'Surrey',
        //        rent: '1,200'
        //    },
        //    {
        //        imgUrl: '/content/assets/images/property.png',
        //        title: 'M2 Coquitlam',
        //        desc: 'Brand new two bedroom highrise',
        //        city: 'Coquitlam',
        //        rent: '1,600'
        //    }
        //];

        //Test with JSON file

        //$scope.loadData = function() {
        $scope.propertyListing = [];

        //alert(JSON.stringify(($scope.propertyListing)));

            $http.get("propertyData.json").success(function (data) {
                $scope.propertyListing = data;
                //alert(JSON.stringify(($scope.propertyListing)));
            });
        //};



        //for (var i = 1; i <= 100; i++) {
            
        //}

        $scope.layout = 'list';

        

    };

    propertyController.$inject = injectParams;

    angular.module('ProMasterClient').controller('propertyController', propertyController);


})();
