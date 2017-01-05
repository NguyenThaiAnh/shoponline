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

addproductCtrl.controller('addproductCtrl', ['$stateParams', '$scope', '$http', 'cloudinary', '$timeout', function ($stateParams, $scope, $http, cloudinary, $timeout) {

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

    $scope.gioitinh_change = function () {
        console.log($scope.gender);
        $http.get(url_api+'loai?gioitinh='+$scope.gender)
            .success(function (response) {
                $scope.listloai =  response;
                console.log(response);
            });
    };

    $scope.loai_change = function () {
        console.log($scope.gender);
        $http.get(url_api+'subloai?loai='+$scope.loai)
            .success(function (response) {
                $scope.listsubloai =  response;
                console.log(response);
            });
    };

    //Url hinh anh 1
    $scope.uploadFiles1 =  function (file, errFiles) {
        $scope.f1 = file;
        $scope.errFile1 = errFiles && errFiles[0];
        if (file) {
            file.upload = cloudinary.upload(file, { /* cloudinary options here */ }).then(function (resp) {
                //alert('all done!');
                console.log(resp);

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

    //Url hinh anh 2
    $scope.uploadFiles2 =  function (file, errFiles) {
        $scope.f2 = file;
        $scope.errFile2 = errFiles && errFiles[0];
        if (file) {
            file.upload = cloudinary.upload(file, { /* cloudinary options here */ }).then(function (resp) {
                //alert('all done!');
                console.log(resp);

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

    //Url hinh anh 3

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
