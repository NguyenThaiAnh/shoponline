'use strict';

var listproductCtrl = angular.module('listproductCtrl', []);

// Url general API
var url_api = "http://localhost:57919/api/v1/";

listproductCtrl.controller('listproductCtrl', ['$scope', '$http', function ($scope, $http) {
    // Get list product of men
    $http.get(url_api + 'mathang')
        .success(function (response) {
            $scope.currentPage = 0;
            $scope.pageSize = 10;
            $scope.mathang = response;

            $scope.numberOfPages = function () {
                //console.log($scope.search);
                if ($scope.search != undefined){

                    $scope.pageSize = response.length;
                }
                if ($scope.search ==  ""){
                    $scope.pageSize = 10;
                }
                return Math.ceil($scope.mathang.length/$scope.pageSize);
            };

            console.log(response);
        })
}]);

listproductCtrl.filter('startFrom', function() {
    return function(input, start) {
        start = +start; //parse to int
        if ( typeof input == 'undefined') {
            input = [];
        }
        //console.log(input);
        return input.slice(start);
    }
});