/**
 * Created by Lucifer on 31/12/2016.
 */
'use strict';

var menCtrl = angular.module('menCtrl', []);

var productCtrl = angular.module('productCtrl', []);
// Url general API
var url_api = "http://localhost:57919/api/v1/";

// Create controller for route[/men]
menCtrl.controller('menCtrl', ['$scope', '$http', 'url', function ($scope, $http, url) {
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
        })
}]);

// Create controller for route[/men/quan]
productCtrl.controller('productCtrl', ['$scope', '$http', function ($scope, $http) {
    // Get list product of men
    $http.get(url_api + 'mathang?loai=qu%E1%BA%A7n')
        .success(function (response) {
            $scope.currentPage = 0;
            // $scope.pageSize = 3;
            $scope.mathang = response;

            $scope.numberOfPages = function () {
                return Math.ceil($scope.mathang.length/$scope.pageSize);
            };

            console.log(response);
        })
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

//We already have a limitTo filter built-in to angular,
//let's make a startFrom filter
productCtrl.filter('startFrom', function() {
    return function(input, start) {
        start = +start; //parse to int
        if ( typeof input == 'undefined') {
            input = [];
        }
        //console.log(input);
        return input.slice(start);
    }
});