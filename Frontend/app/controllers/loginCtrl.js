'use strict';

var loginCtrl = angular.module('loginCtrl', []);

loginCtrl.controller('loginCtrl', ['$scope','Services', '$window', '$rootScope', '$state',
    function ($scope, Services, $window, $rootScope, $state) {
        $scope.user = {
            username: 'Tên đăng nhập',
            password: 'Mật khẩu',
            grant_type: 'password',
            client_id: ''
        };
        $scope.login = function () {
            Services.login($scope.user).then(function() {
                $state.go('home');
            }, function(errMsg) {
                $window.alert("Tên đăng nhập hoặc mật khẩu không chính xác");
                console.log(errMsg);
            });
        }

    }]);
