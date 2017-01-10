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
                $window.location.href= "/admin/products/";
            }, function(errMsg) {
                $window.alert("Tên đăng nhập hoặc mật khẩu không chính xác");
                console.log(errMsg);
            });
        };
    });



angular.module('admin').controller('InsideCtrl', function($scope, AuthService, API_ENDPOINT, $http, $window) {

        var username = window.sessionStorage.getItem('username');
        $scope.isAuth = AuthService.isAuthenticated();

        $scope.destroySession = function() {
            AuthService.logout();
        };


        $scope.logout = function() {
            AuthService.logout();
            //$state.go('login');
            $window.location.href= "/admin/login"
        };


        $http.get('http://localhost:57919/api/v1/user?username='+username)
            .success(function (response) {
                $scope.ten =  response;
                console.log(response);
                $scope.isAuth = AuthService.isAuthenticated();
            })
            .error(function (error) {
                console.log(error);
                AuthService.logout();
                $scope.isAuth = AuthService.isAuthenticated();
        })
    });