'use strict';

var menItemCtrl = angular.module('menItemCtrl', []);
// Url general API
var url_api = "http://localhost:57919/api/v1/";

// Create controller for route[/men/{id}]
menItemCtrl.controller('menItemCtrl', ['$stateParams', '$scope', '$http','Services', '$rootScope','$window', '$state',
    function ($stateParams, $scope, $http, Services, $rootScope, $window, $state) {

    $scope.mathang = '';$scope.soluong = [];$scope.soluongMax = [];
    // Get list product of men
    $http.get(url_api + 'mathang?id='+$stateParams.id)
        .success(function (response) {
            $scope.mathang = response;

            $scope.images = [$scope.mathang.URLHinhAnh1,$scope.mathang.URLHinhAnh2, $scope.mathang.URLHinhAnh3];
            console.log( $scope.images );
            console.log(response);
        });


    $scope.datmua = function () {
        $scope.mathang.Items[0].SoLuong = $scope.soluong[0];
        $scope.mathang.Items[1].SoLuong = $scope.soluong[1];
        $scope.mathang.Items[2].SoLuong = $scope.soluong[2];
        $scope.mathang.GiaCu = new Date();
        $scope.mathang.GiaMoi = eval($scope.soluong[0])*eval($scope.mathang.Items[0].Gia)
                            + eval($scope.soluong[1])*eval($scope.mathang.Items[1].Gia)
                            + eval($scope.soluong[2])*eval($scope.mathang.Items[2].Gia);
        if ($scope.mathang.GiaMoi != 0){
            Services.dathang($scope.mathang);
            console.log(Services.dsdathang());
            $window.alert('Thêm vào giỏ thành công!');
            $state.go('home');
        } else {
            $window.alert('Bạn chưa chọn số lượng!');

        }

    };

    $scope.btplus = function(pos){
        if (parseInt($scope.soluong[pos], 10) < $scope.mathang.Items[pos].SoLuong ){
            $scope.soluong[pos] = parseInt($scope.soluong[pos], 10) + 1;
        }
        //alert("So luong is " + $scope.soluong[pos]);
    };

    $scope.btminus = function(pos){
        if (parseInt($scope.soluong[pos], 10) > 0){
            $scope.soluong[pos] = parseInt($scope.soluong[pos], 10) - 1;
        }
        //alert("So luong is " + $scope.soluong[pos]);
    };

}]);
