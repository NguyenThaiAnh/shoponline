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
            //Khai bao route danh sach san pham
            .state("parentproducts.list", {
                url: "/",
                templateUrl:"views/products.html",
                controller: 'listproductCtrl'
            })

            //Khai bao route them san pham
            .state("parentproducts.add", {
                url: "/add-product",
                templateUrl: "views/product.add.html",
                controller: 'addproductCtrl'
            })

            //Khai bao route hien thi san pham
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
                templateUrl: "views/product.view.html",
                controller: 'viewproductCtrl'
            })

            //Khai bao route cap nhat san pham
            .state("parentproducts.update", {
                url: "/update-product",
                template: "<ui-view></ui-view>",
                abstract: true
            })
            .state("parentproducts.update.child", {
                url: "/",
                params: {
                    id: null
                },
                templateUrl: "views/product.update.html",
                controller: 'updateproductCtrl'
            })

            //Khai báo route danh sach hoa don
            .state("bills", {
                url: "/bills",
                template: "<ui-view></ui-view>",
                abstract: true
            })
            .state("bills.list", {
                url: "/",
                templateUrl: "views/bills.html",
                controller: 'listbillCtrl'
            })

            //Khai bao route hien thi hoa don
            .state("bills.view", {
                url: "/view-bill",
                template: "<ui-view></ui-view>",
                abstract: true
            })
            .state("bills.view.child", {
                url: "/",
                params: {
                    id: null
                },
                templateUrl: "views/bill.view.html",
                controller: 'viewbillCtrl'
            })

            //Khai bao route cap nhat hoa don
            .state("bills.update", {
                url: "/update-bill",
                template: "<ui-view></ui-view>",
                abstract: true
            })
            .state("bills.update.child", {
                url: "/",
                params: {
                    id: null
                },
                templateUrl: "views/bill.update.html",
                controller: 'updatebillCtrl'
            })

            //Khai báo route danh sach khack hang
            .state("customers", {
                url: "/customers",
                template: "<ui-view></ui-view>",
                abstract: true
            })
            .state("customers.list", {
                url: "/",
                templateUrl: "views/customers.html",
                controller: 'listcustomerCtrl'
            })

            .state('otherwise', {
                url: '/404',
                templateUrl: 'views/404.html'
            })

            //Khai bao route dang nhap
            .state("login", {
                url: "/login",
                templateUrl:"views/login.html",
                controller: 'LoginCtrl'
            });

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
