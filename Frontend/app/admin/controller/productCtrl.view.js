'use strict';

var viewproductCtrl = angular.module('viewproductCtrl', []);
// Url general API
var url_api = "http://localhost:57919/api/v1/";

viewproductCtrl.controller('viewproductCtrl', ['$stateParams', '$scope', '$http', function ($stateParams, $scope, $http) {

    $http.get(url_api + 'mathang?id='+$stateParams.id)
        .success(function (response) {
            $scope.mathang = response;
            $scope.gender = $scope.mathang.GioiTinh;
            $scope.loai = $scope.mathang.Loai;
            $scope.subloai = $scope.mathang.SubLoai;

            $http.get(url_api+'loai?gioitinh='+$scope.gender)
                .success(function (response) {
                    $scope.listloai =  response;
                    $http.get(url_api+'subloai?loai='+$scope.loai)
                        .success(function (response) {
                            $scope.listsubloai =  response;
                            console.log(response);
                        });
                    console.log(response);
                });

            $scope.soluong = [];
            $scope.soluongMax = [];

            console.log(response);
        });


    // $scope.gioitinh_change = function () {
    //     console.log($scope.gender);
    //     $http.get(url_api+'loai?gioitinh='+$scope.gender)
    //         .success(function (response) {
    //             $scope.listloai =  response;
    //             console.log(response);
    //         });
    // };

    //
    // $scope.btplus = function(pos){
    //     if (parseInt($scope.soluong[pos], 10) < $scope.mathang.Items[pos].SoLuong ){
    //         $scope.soluong[pos] = parseInt($scope.soluong[pos], 10) + 1;
    //     }
    //     //alert("So luong is " + $scope.soluong[pos]);
    // };
    //
    // $scope.btminus = function(pos){
    //     if (parseInt($scope.soluong[pos], 10) > 0){
    //         $scope.soluong[pos] = parseInt($scope.soluong[pos], 10) - 1;
    //     }
    //     //alert("So luong is " + $scope.soluong[pos]);
    // };

}]);
