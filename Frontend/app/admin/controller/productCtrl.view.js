'use strict';

var viewproductCtrl = angular.module('viewproductCtrl', []);
// Url general API
var url_api = "http://localhost:57919/api/v1/";

viewproductCtrl.controller('viewproductCtrl', ['$stateParams', '$scope', '$http', function ($stateParams, $scope, $http) {

    $http.get(url_api + 'mathang?id='+$stateParams.id)
        .success(function (response) {
            $scope.mathang = response;
            //$scope.gender = $scope.mathang.GioiTinh;
            //$scope.loai = $scope.mathang.Loai;
            //$scope.subloai = $scope.mathang.SubLoai;

            $http.get(url_api+'loai?gioitinh='+$scope.mathang.GioiTinh)
                .success(function (response) {
                    $scope.listloai =  response;
                    $http.get(url_api+'subloai?loai='+$scope.mathang.Loai+'&gioitinh='+$scope.mathang.GioiTinh)
                        .success(function (response) {
                            $scope.listsubloai =  response;
                            console.log(response);
                        });
                    console.log(response);
                });

            console.log(response);
        });

}]);
