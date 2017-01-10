'use strict';

var updatebillCtrl = angular.module('updatebillCtrl', []);
// Url general API
var url_api = "http://localhost:57919/api/v1/";

updatebillCtrl.controller('updatebillCtrl', ['$stateParams', '$scope', '$http', '$window', '$state',
    function ($stateParams, $scope, $http, $window, $state) {

    $http.get(url_api + 'hoadon?id='+$stateParams.id)
        .success(function (response) {
            $scope.hd = response;

            console.log(response);
        });
    $scope.TinhTrang = '';
    $scope.capnhatHoaDon = function () {
        if ($scope.TinhTrang == undefined){
            $window.alert('Vui lòng kiểm tra lại thông tin!');
        }
        else {

            var config = {
                headers : {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            };

            $http.put(url_api+'HoaDon', {}, { params: { id: $stateParams.id, status : $scope.TinhTrang } })
                .success(function (data, status, headers, config) {
                    console.log(data);
                    $window.alert("Cập nhật hoá đơn thành công!");
                    $state.go('bills.list');
                })
                .error(function (data, status, header, config) {
                    console.log(data);
                    console.log(item);
                    $window.alert("Vui lòng kiểm tra lại thông tin!");
                });
        }

    }
}]);
