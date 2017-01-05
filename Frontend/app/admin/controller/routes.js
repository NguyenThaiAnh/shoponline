angular.module('admin')
    .config(function($stateProvider,  $urlRouterProvider, $locationProvider) {

        $stateProvider
            .state("home", {
                url: "/",
                templateUrl: "views/index2.html"
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
                url: "/:id",
                templateUrl: "views/products.view.html",
                controller: 'viewproductCtrl'
            })

            .state("login", {
                url: "/login",
                templateUrl: "views/login.html"
            })

            .state('otherwise', {
                url: '/404',
                templateUrl: 'views/404.html'
            });

        $urlRouterProvider.otherwise('/404');

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: true,
            rewriteLinks: false
        });
});