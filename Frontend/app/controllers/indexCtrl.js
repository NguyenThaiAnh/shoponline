'use strict';

var indexCtrl = angular.module('indexCtrl', []);
// Url general API
var url_api = "http://localhost:57919/api/v1/";

indexCtrl.controller('indexCtrl', ['$scope', '$http', 'Services','$rootScope', '$state','$window',
    function ($scope, $http, Services, $rootScope, $state, $window) {

    $rootScope.isLoged = Services.isLoged();
    console.log($rootScope.isLoged);
    $scope.hangdadat = [];
    $scope.hangdadat = Services.dsdathang();
    console.log($scope.hangdadat);
    $rootScope.TongGia = Services.tongtien();
    $rootScope.abc = Services.dsdathang().length;


    $scope.XoaHang = function (mathang) {
        Services.xoadathang(mathang);
        console.log(Services.tongtien());
        $rootScope.abc = Services.dsdathang().length;
        $rootScope.TongGia = Services.tongtien();
    };

    $scope.ThanhToan = function () {
        if (Services.isLoged()){

            var tmp = []; var dem = 0;
            for (var i=0; i<$scope.hangdadat.length; i++){
                for(var j = 0; j<$scope.hangdadat[i].Items.length; j++){
                    var x = {IDChiTietMatHang : $scope.hangdadat[i].Items[j].ID,
                    SoLuong:  $scope.hangdadat[i].Items[j].SoLuong,
                    ID: "", TenMH:  "", IDHoaDon : ""};
                    if (x.SoLuong != 0){
                        tmp.push(x);
                        dem++;
                    }

                }
            }
            console.log(tmp);

            $http.get(url_api + 'User?Username='+Services.infouser().username+'&Password='+Services.infouser().password)
                .success(function (response) {
                    $scope.person = response;
                    console.log(response);
                    var Ngay = new Date();
                    //Ngay = Ngay.getFullYear() + '-' + ('0' + (Ngay.getMonth() + 1)).slice(-2) + '-' + ('0' + Ngay.getDate()).slice(-2);
                    console.log(Ngay);
                    var item = {
                        ID: "",
                        IDKhachHang: $scope.person.ID,
                        Ngay: Ngay,
                        TongTien: $scope.TongGia,
                        TinhTrang: "Đã đặt hàng",
                        Items:tmp
                    };

                    var config = {
                        headers : {
                            'Content-Type': 'application/json'
                        }
                    };

                    $http.post(url_api+'HoaDon', item, config)
                        .success(function (data, status, headers, config) {
                            console.log(data);
                            Services.xoatatca();
                            $rootScope.abc = Services.dsdathang().length;
                            $window.alert("Bạn đã đặt hàng thành công!");
                            $state.go('home');

                        })
                        .error(function (data, status, header, config) {
                            console.log(data);
                            console.log(item);
                            $window.alert("Vui lòng kiểm tra lại thông tin!");
                        });

                });
        }
        else {
            $rootScope.isLoged = Services.isLoged();
            $state.go('login');
        }
    };

    $scope.DangXuat = function () {
        $rootScope.isLoged = false;
        Services.logout();

    }
}]);
