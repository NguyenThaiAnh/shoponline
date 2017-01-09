'use strict';

var homeCtrl = angular.module('homeCtrl', []);
// Url general API
var url_api = "http://localhost:57919/api/v1/";

homeCtrl.controller('homeCtrl', ['$scope', '$http', 'Services','$rootScope', '$state','$window','ServiceInterceptor',
    function ($scope, $http, Services, $rootScope, $state, $window, ServiceInterceptor) {

        // //Get list of best sell product
        $http.get(url_api + 'mathangbanchay')
            .success(function (response) {
                $scope.spbanchay = response;
                console.log(response);
            });

        // // Get list product of men jean
        $http.get(url_api + 'mathang?loai=qu%E1%BA%A7n&subloai=qu%E1%BA%A7n%20jean')
            .success(function (response) {
                $scope.mathang = response;
                console.log(response);
            });

    }]);
