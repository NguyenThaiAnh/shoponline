'use strict';

var menCtrl = angular.module('menCtrl', []);

// Url general API
var url_api = "http://localhost:57919/api/v1/";

menCtrl.controller('menCtrl', ['$scope', '$http', 'url', 'Services', '$rootScope',
    function ($scope, $http, url, Services, $rootScope) {

    // Get list product of men
    $http.get(url_api + url)
        .success(function (response) {
            $scope.currentPage = 0;
            // $scope.pageSize = 3;
            $scope.mathang = response;

            $scope.numberOfPages = function () {
                return Math.ceil($scope.mathang.length/$scope.pageSize);
            };
            console.log(response);
        });

}]);

menCtrl.filter('startFrom', function() {
    return function(input, start) {
        start = +start; //parse to int
        if ( typeof input == 'undefined') {
            input = [];
        }
        //console.log(input);
        return input.slice(start);
    }
});