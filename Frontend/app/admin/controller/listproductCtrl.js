'use strict';

var listproductCtrl = angular.module('listproductCtrl', []);

// Url general API
var url_api = "http://localhost:57919/api/v1/";

listproductCtrl.controller('listproductCtrl', ['$scope', '$http', '$state', 'ngDialog', function ($scope, $http, $state, ngDialog) {
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
        });

    $scope.xoaSanPham = function (IDMATHANG) {

        console.log(IDMATHANG);
        ngDialog.open({
            template: 'views/product.delete.html',
            controller: ['$scope', '$state', function($scope, $state) {
                $scope.IDMH = IDMATHANG;
                $scope.yes = function () {
                    $http.delete(url_api+'MatHang?ID='+IDMATHANG)
                        .success(function (data) {
                            console.log(data);
                            //$window.alert("Xoá sản phẩm thành công!");
                            $scope.closeThisDialog('true');
                            $state.reload();
                        })
                        .error(function (data) {
                            console.log(data);
                            $window.alert("Xoá sản phẩm thất bại!");
                        });
                };
                $scope.no = function () {
                    $scope.closeThisDialog('false');
                }
            }]
        });

    };
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