'use strict';

var listbillCtrl = angular.module('listbillCtrl', []);

// Url general API
var url_api = "http://localhost:57919/api/v1/";

listbillCtrl.controller('listbillCtrl', ['$scope', '$http', '$state', 'ngDialog', '$window', function ($scope, $http, $state, ngDialog, $window) {
    // Get list product of men
    $http.get(url_api + 'hoadon')
        .success(function (response) {
            $scope.currentPage = 0;
            $scope.pageSize = 10;
            $scope.hoadon = response;

            $scope.numberOfPages = function () {
                //console.log($scope.search);
                if ($scope.search != undefined){

                    $scope.pageSize = response.length;
                }
                if ($scope.search ==  ""){
                    $scope.pageSize = 10;
                }
                return Math.ceil($scope.hoadon.length/$scope.pageSize);
            };

            console.log(response);
        });
}]);

listbillCtrl.filter('startFrom', function() {
    return function(input, start) {
        start = +start; //parse to int
        if ( typeof input == 'undefined') {
            input = [];
        }
        //console.log(input);
        return input.slice(start);
    }
});