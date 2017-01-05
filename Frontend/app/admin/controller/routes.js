'use strict';
angular.module('admin')
    .config(function($stateProvider,  $urlRouterProvider, $locationProvider, $httpProvider) {

        $stateProvider
            .state("home", {
                url: "/",
                templateUrl: "views/index2.html",
                controller: 'InsideCtrl'
            })
            .state("parentproducts", {
                url: "/products",
                template: '<ui-view></ui-view>',
                abstract: true
            })
            .state("parentproducts.list", {
                url: "/",
                templateUrl:"views/products.html",
                controller: 'listproductCtrl'
            })
            .state("parentproducts.add", {
                url: "/add-product",
                templateUrl: "views/products.add.html",
                controller: 'addproductCtrl'
            })
            .state("parentproducts.view", {
                url: "/view-product",
                template: "<ui-view></ui-view>",
                abstract: true
            })
            .state("parentproducts.view.child", {
                url: "/",
                params: {
                    id: null
                },
                templateUrl: "views/products.view.html",
                controller: 'viewproductCtrl'
            })
            .state('otherwise', {
                url: '/404',
                templateUrl: 'views/404.html'
            })
            .state("login", {
                url: "/login",
                templateUrl:"views/login.html",
                controller: 'LoginCtrl'
            });
            // .state("orders", {
            //     url: "/orders",
            //     templateUrl:"views/login.html",
            // });

        $urlRouterProvider.otherwise('/404');

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: true,
            rewriteLinks: false
        });

        //$httpProvider.interceptors.push('authInterceptorService');
});

angular.module('admin').run(function ($rootScope, $state, AuthService, AUTH_EVENTS, jwtHelper, $location) {
    $rootScope.$on('$stateChangeStart', function (event, next, nextParams, fromState) {
        if (!AuthService.isAuthenticated()){
            console.log(next.name);
            if (next.name !== 'login'){
                event.preventDefault();
                $state.go('login');

            }
        }
    })
});
