'use strict';

var addproductCtrl = angular.module('addproductCtrl', ['angular-cloudinary','ngFileUpload']);

// Url general API
var url_api = "http://localhost:57919/api/v1/";

addproductCtrl.config(function (cloudinaryProvider) {
    cloudinaryProvider.config({
        upload_endpoint: 'https://api.cloudinary.com/v1_1/', // default
        cloud_name: 'hchuzbakt', // required
        upload_preset: 'n05ieovt' // optional
    });
});

addproductCtrl.controller('addproductCtrl', ['$stateParams', '$scope', '$http', 'cloudinary', '$timeout', 'AuthService', '$state', '$window',
    function ($stateParams, $scope, $http, cloudinary, $timeout, AuthService, $state, $window) {
    $scope.subItems = [{Size: '', Gia: '', SoLuong:''}, {Size: '', Gia: '', SoLuong:''}, {Size: '', Gia: '', SoLuong:''}];

    // //Loai loai hang theo gioi tinh
    // $http.get(url_api+'loai?gioitinh='+$scope.item.gender)
    //     .success(function (response) {
    //         $scope.listloai =  response;
    //         $http.get(url_api+'subloai?loai='+$scope.loai)
    //             .success(function (response) {
    //                 $scope.listsubloai =  response;
    //                 console.log(response);
    //             });
    //         console.log(response);
    //     });

    //Bat su kien thay doi gioi tinh
    $scope.gioitinh_change = function () {
        console.log($scope.item.gender);
        $http.get(url_api+'loai?gioitinh='+$scope.item.gender)
            .success(function (response) {
                $scope.listloai =  response;
                console.log(response);
            });
        $scope.loai_change();
    };

    //Bat su kien thay doi loai
    $scope.loai_change = function () {
        console.log($scope.item.gender);
        $http.get(url_api+'subloai?loai='+$scope.item.loai+'&gioitinh='+$scope.item.gender)
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
                    $scope.item.url = resp.data.url;
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
                $scope.item.url1 = resp.data.url;
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
                $scope.item.url2 = resp.data.url;
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
    $scope.themSanPham = function () {
        if ($scope.item.giacu == undefined){$scope.item.giacu = "0";}
        if ($scope.item.giamoi == undefined){$scope.item.giamoi = "0";}
        if ($scope.subItems[0].Gia == undefined){$scope.subItems[0].Gia = "0";}
        if ($scope.subItems[0].SoLuong == undefined){$scope.subItems[0].SoLuong = "0";}
        if ($scope.subItems[1].Gia == undefined){$scope.subItems[1].Gia = "0";}
        if ($scope.subItems[1].SoLuong == undefined){$scope.subItems[1].SoLuong = "0";}
        if ($scope.subItems[2].Gia == undefined){$scope.subItems[2].Gia = "0";}
        if ($scope.subItems[2].SoLuong == undefined){$scope.subItems[2].SoLuong = "0";}

        var item = {
            ID: '',
            Loai: $scope.item.loai,
            SubLoai : $scope.item.subloai,
            Ten: $scope.item.ten,
            GioiTinh: $scope.item.gender,
            URLHinhAnh1: $scope.item.url,
            URLHinhAnh2: $scope.item.url1,
            URLHinhAnh3: $scope.item.url2,
            MoTa: $scope.item.mota,
            GiaCu: $scope.item.giacu,
            GiaMoi: $scope.item.giamoi,
            Items: [{ID:'', IDMatHang: '', Size: $scope.subItems[0].Size, Gia: $scope.subItems[0].Gia.toString(), SoLuong: $scope.subItems[0].SoLuong.toString()},
                {ID:'', IDMatHang: '', Size: $scope.subItems[1].Size, Gia: $scope.subItems[1].Gia.toString(), SoLuong: $scope.subItems[1].SoLuong.toString()},
                {ID:'', IDMatHang: '', Size: $scope.subItems[2].Size, Gia: $scope.subItems[2].Gia.toString(), SoLuong: $scope.subItems[2].SoLuong.toString()}]
        };

        var config = {
            headers : {
                'Content-Type': 'application/json'
            }
        };

        $http.post(url_api+'MatHang', item, config)
            .success(function (data, status, headers, config) {
                console.log(data);
                $window.alert("Thêm sản phẩm thành công!");
                $state.go('parentproducts.list');

            })
            .error(function (data, status, header, config) {
                console.log(data);
                console.log(item);
                $window.alert("Vui lòng kiểm tra lại thông tin!");
            });
    }
}]);
