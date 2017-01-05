angular.module('admin')

    .service('AuthService', function($q, $http, API_ENDPOINT, jwtHelper) {
        var LOCAL_CLIENTID = 'yourid';
        var LOCAL_TOKEN_KEY = 'yourTokenKey';
        var isAuthenticated = false;
        var authToken;

        function loadUserCredentials() {
            var token = window.sessionStorage.getItem(LOCAL_TOKEN_KEY);
            if (token) {
                useCredentials(token);
            }
        }

        function storeUserCredentials(token) {
            window.sessionStorage.setItem(LOCAL_TOKEN_KEY, token);
            useCredentials(token);
        }

        function storeClientID(clientID) {
            window.sessionStorage.setItem(LOCAL_CLIENTID, clientID);
        }

        function useCredentials(token) {
            if (Authorization(token))
            {
                isAuthenticated = true;
                authToken = 'bearer '+ token;

                // Set the token as header for your requests!
                $http.defaults.headers.common.Authorization = authToken;
            }
            else{
                console.log("Tài khoản của bạn không được truy cập vào trang này!");
            }
        }

        function Authorization(token) {
            console.log(token);
            if (token == 'undefined'){
                return false;
            }
            var tokenPayload = jwtHelper.decodeToken(token);
            if (tokenPayload.role == "Manager"){
                return true;
            }
            return false;
        }

        function destroyUserCredentials() {
            authToken = undefined;
            isAuthenticated = false;
            $http.defaults.headers.common.Authorization = undefined;
            window.sessionStorage.removeItem(LOCAL_TOKEN_KEY);
        }

        // var register = function(user) {
        //     return $q(function(resolve, reject) {
        //         $http.post(API_ENDPOINT.url + '/signup', user).then(function(result) {
        //             if (result.data.success) {
        //                 resolve(result.data.msg);
        //             } else {
        //                 reject(result.data.msg);
        //             }
        //         });
        //     });
        // };

        var login = function(user) {
            return $q(function(resolve, reject) {

                $http({
                    method: 'POST',
                    url: 'http://localhost:57919/api/audience/getAudience',
                    headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                    transformRequest: function(obj) {
                        var str = [];
                        for (var p in obj)
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                        return str.join("&");
                    },
                    data: {Name: "MD5"}
                }).success(function (data) {
                    console.log(data);
                    storeClientID(data.ClientId);
                    user.client_id = data.ClientId;

                    $http({
                        method: 'POST',
                        url: 'http://localhost:57919/oauth2/token',
                        headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                        transformRequest: function(obj) {
                            var str = [];
                            for (var p in obj)
                                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                            return str.join("&");
                        },
                        data: user
                    }).success(function (data) {
                        console.log(data);
                        storeUserCredentials(data.access_token);
                        resolve(data.access_token);
                    }).error(function (data) {
                        console.log(data);
                        reject(data);
                    });

                }).error(function (data) {
                    console.log(data);
                });

                // $http.post(API_ENDPOINT.url + 'api/oauth2/token', user).then(function(result) {
                //     if (result.data.success) {
                //         storeUserCredentials(result.data.token);
                //         console.log(result.data.token);
                //         //resolve(result.data.msg);
                //     } else {
                //         console.log('Failed');
                //         //reject(result.data.msg);
                //     }
                // });

            });
        };

        var logout = function() {
            destroyUserCredentials();
        };

        var useAuthorizationHeaders = function () {
            $http.defaults.headers.common.Authorization = authToken;
        };

        var notUseAuthorizationHeaders = function () {
            $http.defaults.headers.common.Authorization = undefined;
        };

        loadUserCredentials();


        return {
            login: login,
            //register: register,
            logout: logout,
            useAuthorizationHeaders: useAuthorizationHeaders,
            notUseAuthorizationHeaders: notUseAuthorizationHeaders,
            isAuthenticated: function() {return isAuthenticated;}
        };
    })

    .factory('AuthInterceptor', function ($rootScope, $q, AUTH_EVENTS) {
        return {
            responseError: function (response) {
                $rootScope.$broadcast({
                    401: AUTH_EVENTS.notAuthenticated,
                }[response.status], response);
                return $q.reject(response);
            }
        };
    })

    .config(function ($httpProvider) {
        $httpProvider.interceptors.push('AuthInterceptor');
    });