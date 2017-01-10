angular.module('admin')

    .controller('LoginCtrl', function($scope, AuthService, $window) {
        $scope.user = {
            username: '',
            password: '',
            grant_type: 'password',
            client_id: ''
        };

        $scope.login = function() {
            AuthService.login($scope.user).then(function() {
                //$state.go('parentproducts.list');
                window.sessionStorage.setItem('username', $scope.user.username);
                $window.location.href= "/admin/";
            }, function(errMsg) {
                $window.alert("Tên đăng nhập hoặc mật khẩu không chính xác");
                console.log(errMsg);
            });
        };
    });