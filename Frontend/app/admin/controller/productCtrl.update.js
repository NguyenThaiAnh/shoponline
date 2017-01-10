'use strict';

var updateproductCtrl = angular.module('updateproductCtrl', ['angular-cloudinary','ngFileUpload']);

// Url general API
var url_api = "http://localhost:57919/api/v1/";

updateproductCtrl.config(function (cloudinaryProvider) {
    cloudinaryProvider.config({
        upload_endpoint: 'https://api.cloudinary.com/v1_1/', // default
        cloud_name: 'hchuzbakt', // required
        upload_preset: 'n05ieovt' // optional
    });
});

updateproductCtrl.controller('updateproductCtrl', ['$stateParams', '$scope', '$http', 'cloudinary', '$timeout', 'AuthService', '$state', '$window',
    function ($stateParams, $scope, $http, cloudinary, $timeout, AuthService, $state, $window) {

    $http.get(url_api + 'mathang?id='+$stateParams.id)
        .success(function (response) {
            $scope.item = response;

            $http.get(url_api+'loai?gioitinh='+$scope.item.GioiTinh)
                .success(function (response) {
                    $scope.listloai =  response;
                    $http.get(url_api+'subloai?loai='+$scope.item.Loai+'&gioitinh='+$scope.item.GioiTinh)
                        .success(function (response) {
                            $scope.listsubloai =  response;
                            console.log(response);
                        });
                    console.log(response);
                });

            console.log(response);
        });

    //Bat su kien thay doi gioi tinh
    $scope.gioitinh_change = function () {
        console.log($scope.item.GioiTinh);
        $http.get(url_api+'loai?gioitinh='+$scope.item.GioiTinh)
            .success(function (response) {
                $scope.listloai =  response;
                console.log(response);
            });
        $scope.loai_change();
    };

    //Bat su kien thay doi loai
    $scope.loai_change = function () {
        console.log($scope.item.GioiTinh);
        $http.get(url_api+'subloai?loai='+$scope.item.Loai+'&gioitinh='+$scope.item.GioiTinh)
            .success(function (response) {
                $scope.listsubloai =  response;
                console.log(response);
            });
    };

    //Upload hinh anh chinh len cloudinary
    $scope.uploadFiles =  function (file, errFiles) {
        $scope.f = file;
        $scope.errFile = errFiles && errFiles[0];
        if (file) {
            AuthService.notUseAuthorizationHeaders();
            file.upload = cloudinary.upload(file, { }).then(function (resp) {
                //alert('all done!');
                console.log('upload done');
                $timeout(function () {
                    file.result = resp.data;
                    $scope.item.URLHinhAnh1 = resp.data.url;
                    AuthService.useAuthorizationHeaders();
                });
            }, function (resp) {
                if (resp.status > 0)
                    $scope.errorMsg1 = resp.status + ': ' + 'Loại file không hỗ trợ';
            }, function (evt) {
                file.progress = Math.min(100, parseInt(100.0 *
                    evt.loaded / evt.total));
            });
        }
    };


    //Upload hinh anh chi tiet 1 len cloudinary
    $scope.uploadFiles1 =  function (file, errFiles) {
        $scope.f1 = file;
        $scope.errFile1 = errFiles && errFiles[0];
        if (file) {
            AuthService.notUseAuthorizationHeaders();
            file.upload = cloudinary.upload(file, { /* cloudinary options here */ }).then(function (resp) {
                //alert('all done!');
                console.log(resp);
                $scope.item.URLHinhAnh2 = resp.data.url;
                AuthService.useAuthorizationHeaders();
                $timeout(function () {
                    file.result = resp.data;
                });
            }, function (resp) {
                if (resp.status > 0)
                    $scope.errorMsg1 = resp.status + ': ' + 'Loại file không hỗ trợ';
            }, function (evt) {
                file.progress = Math.min(100, parseInt(100.0 *
                    evt.loaded / evt.total));
            });
        }
    };

    //Upload hinh anh chi tiet 2 len cloudinay
    $scope.uploadFiles2 =  function (file, errFiles) {
        $scope.f2 = file;
        $scope.errFile2 = errFiles && errFiles[0];
        if (file) {
            AuthService.notUseAuthorizationHeaders();
            file.upload = cloudinary.upload(file, { /* cloudinary options here */ }).then(function (resp) {
                //alert('all done!');
                console.log(resp);
                $scope.item.URLHinhAnh3 = resp.data.url;
                AuthService.useAuthorizationHeaders();
                $timeout(function () {
                    file.result = resp.data;
                });
            }, function (resp) {
                if (resp.status > 0)
                    $scope.errorMsg2 = resp.status + ': ' + 'Loại file không hỗ trợ';
            }, function (evt) {
                file.progress = Math.min(100, parseInt(100.0 *
                    evt.loaded / evt.total));
            });
        }
    };

    //Them san pham vao database
    $scope.capnhatSanPham = function () {
        if ($scope.item.GiaCu == undefined){$scope.item.GiaCu = "0";}
        if ($scope.item.GiaMoi == undefined){$scope.item.GiaMoi = "0";}
        if ($scope.item.Items[0].Gia == undefined){$scope.item.Items[0].Gia = "0";}
        if ($scope.item.Items[0].SoLuong == undefined){$scope.item.Items[0].SoLuong = "0";}
        if ($scope.item.Items[1].Gia == undefined){$scope.item.Items[1].Gia = "0";}
        if ($scope.item.Items[1].SoLuong == undefined){$scope.item.Items[1].SoLuong = "0";}
        if ($scope.item.Items[2].Gia == undefined){$scope.item.Items[2].Gia = "0";}
        if ($scope.item.Items[2].SoLuong == undefined){$scope.item.Items[2].SoLuong = "0";}

        var item = $scope.item;
        console.log(item);
        var config = {
            headers : {
                'Content-Type': 'application/json'
            }
        };

        $http.put(url_api+'MatHang', item, config)
            .success(function (data, status, headers, config) {
                console.log(data);
                $window.alert("Cập nhật sản phẩm thành công!");
                $state.go('parentproducts.list');
            })
            .error(function (data, status, header, config) {
                console.log(data);
                console.log(item);
                $window.alert("Vui lòng kiểm tra lại thông tin!");
            });
    }
}]);
