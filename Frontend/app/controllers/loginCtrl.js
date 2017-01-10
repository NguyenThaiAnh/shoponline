'use strict';
// angular.module('shop', ['facebook'])
//
//     .config(function(FacebookProvider) {
//         // Set your appId through the setAppId method or
//         // use the shortcut in the initialize method directly.
//         FacebookProvider.init('619254434951876');
//     });

var loginCtrl = angular.module('loginCtrl', []);

loginCtrl.controller('loginCtrl', ['$scope', 'Services', '$window', 'ngDialog',
    function ($scope, Services, $window, ngDialog) {
        $scope.user = {
            username: "",
            password: "",
            grant_type: 'password',
            client_id: ""
        };
        $scope.userfb = {};
        console.log($scope.user);

        $scope.login = function () {
            if ($scope.user.username == undefined || $scope.user.password == undefined) {
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
            FB.login(function (response) {
                FB.api('/me', function (response) {
                    var a = response;
                    $scope.userfb.username = a.id;
                    $scope.userfb.password = a.id;
                    $scope.userfb.grant_type = 'password';
                    console.log($scope.userfb);
                    Services.login($scope.userfb).then(function () {
                        $window.location.href = '/';
                    }, function (errMsg) {
                        ngDialog.open({
                            template: 'views/inforRequest.html',
                            controller: ['$scope', '$state', '$window', 'Services', function ($scope, $state, $window, Services) {
                                $scope.userfb = {};
                                console.log($scope.userfb);
                                $scope.update = function () {
                                    FB.login(function (response) {
                                        FB.api('/me', function (response) {
                                            $scope.userfb = response;

                                            var userfacebook = {
                                                Ten: $scope.userfb.name,
                                                UserName: $scope.userfb.id,
                                                PassWord: $scope.userfb.id,
                                                repassword: $scope.userfb.id,
                                                Email: "",
                                                DiaChi: "",
                                                SDT: ""
                                            };
                                            console.log(userfacebook);

                                            if ($scope.fbEmail == undefined || $scope.fbDiaChi == undefined || $scope.fbSDT == undefined) {
                                                $window.alert("Vui lòng kiểm tra lại thông tin!");

                                            } else {
                                                userfacebook.Email = $scope.fbEmail;
                                                userfacebook.DiaChi = $scope.fbDiaChi;
                                                userfacebook.SDT = $scope.fbSDT;
                                                console.log(userfacebook);

                                                Services.signup(userfacebook).then(function () {
                                                    var x = {
                                                        username: $scope.userfb.id,
                                                        password: $scope.userfb.id,
                                                        grant_type: 'password'
                                                    };
                                                    Services.login(x).then(function () {
                                                        $window.location.href = '/';
                                                    }, function (errMsg) {
                                                        $window.alert("Tên đăng nhập hoặc mật khẩu không chính xác");
                                                        console.log(errMsg);
                                                    });
                                                }, function (errMsg) {
                                                    console.log(errMsg);
                                                });
                                            }

                                        });

                                    });

                                };
                            }]
                        });

                    });
                })
            });
        };

    }]);
