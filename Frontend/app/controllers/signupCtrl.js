'use strict';

var signupCtrl = angular.module('signupCtrl', []);

// Url general API
var url_api = "http://localhost:57919/api/v1/";

signupCtrl.controller('signupCtrl', ['$scope', '$http', 'Services','$window',
    function ($scope, $http, Services, $window) {
    $scope.name = 'Tên';
    $scope.username = 'Tên đăng nhập';
    $scope.email =  'Email';
    $scope.password = 'Mật khẩu';
    $scope.repassword = 'Mật khẩu';
    $scope.address = 'Địa chỉ';
    $scope.phone = 'Điện thoại';

    var config = {
        headers : {
            'Content-Type': 'application/json'
        }
    };
    $scope.signup = function () {
        var item = {Ten: $scope.name, UserName: $scope.username, Email: $scope.email,
            Password: $scope.password, DiaChi: $scope.address, SDT: $scope.phone};

        if ($scope.name == 'Tên' || $scope.username == 'Tên đăng nhập'|| $scope.email ==  'Email'||
            $scope.password == 'Mật khẩu' || $scope.repassword != $scope.password || $scope.address == 'Địa chỉ' ||$scope.phone == 'Điện thoại'){
            $window.alert("Vui lòng kiểm tra lại thông tin!");
        } else {
            $http.post(url_api+'NguoiDung', item, config)
                .success(function (data, status, headers, config) {
                    console.log(data);
                    $window.alert("Đăng ký thành công!");
                    $window.location.href = "/";

                })
                .error(function (data, status, header, config) {
                    console.log(data);
                    console.log(item);
                    $window.alert("Tên đăng nhập đã tồn tại!");
                });
        }

    }
}]);
