angular.module('admin').controller('InsideCtrl', function($scope, AuthService, API_ENDPOINT, $http, $window) {

    var username = window.sessionStorage.getItem('username');
    $scope.isAuth = AuthService.isAuthenticated();

    $scope.destroySession = function() {
        AuthService.logout();
    };


    $scope.logout = function() {
        AuthService.logout();
        //$state.go('login');
        $window.location.href= "/admin/login";
        window.sessionStorage.removeItem('username');
    };


    $http.get('http://localhost:57919/api/v1/nguoidung?username='+username)
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