'use strict';

var signupCtrl = angular.module('signupCtrl', []);

// Url general API
var url_api = "http://localhost:57919/api/v1/";

signupCtrl.controller('signupCtrl', ['$scope', '$http', 'Services', '$window',
    function ($scope, $http, Services, $window) {
        $scope.user = {};

        var config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };
        $scope.signup = function () {
            var item = $scope.user;

            if ($scope.user.Ten == "" || $scope.user.UserName == "" || $scope.user.Email == "" ||
                $scope.user.PassWord == "" || $scope.user.repassword != $scope.user.PassWord ||
                $scope.user.DiaChi == "" || $scope.user.SDT == "") {

                $window.alert("Vui lòng kiểm tra lại thông tin!");

            } else {
                Services.signup($scope.user).then(function () {
                    $window.location.href = '/dang-nhap';
                }, function (errMsg) {

                    console.log(errMsg);
                });
            }

        }
    }]);
