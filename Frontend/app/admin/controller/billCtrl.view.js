'use strict';

var viewbillCtrl = angular.module('viewbillCtrl', []);
// Url general API
var url_api = "http://localhost:57919/api/v1/";

viewbillCtrl.controller('viewbillCtrl', ['$stateParams', '$scope', '$http', function ($stateParams, $scope, $http) {

    $scope.IDHD = $stateParams.id;
    $http.get(url_api + 'hoadon?id='+$stateParams.id)
        .success(function (response) {
            $scope.hd = response;


            console.log(response);
        });

}]);
