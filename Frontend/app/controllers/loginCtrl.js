'use strict';
// angular.module('shop', ['facebook'])
//
//     .config(function(FacebookProvider) {
//         // Set your appId through the setAppId method or
//         // use the shortcut in the initialize method directly.
//         FacebookProvider.init('619254434951876');
//     });

var loginCtrl = angular.module('loginCtrl', []);

loginCtrl.controller('loginCtrl', ['$scope','Services', '$window',
    function ($scope, Services, $window) {
        $scope.user = {
            username: "",
            password: "",
            grant_type: 'password',
            client_id: ""
        };
        console.log($scope.user);

        $scope.login = function () {
            if ($scope.user.username == "" || $scope.user.password == ""){
                $window.alert('Vui lòng kiểm tra lại thông tin!')
            } else {
                Services.login($scope.user).then(function () {
                    $window.location.href = '/';

                }, function (errMsg) {
                    $window.alert("Tên đăng nhập hoặc mật khẩu không chính xác");
                    console.log(errMsg);
                });
            }
        };

        $scope.loginfb = function () {

        };

    }]);

