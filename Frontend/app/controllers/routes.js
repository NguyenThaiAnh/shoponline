angular.module('shop')
    .config(function($stateProvider,  $urlRouterProvider, $locationProvider) {

        $stateProvider
            .state("home", {
                url: "/",
                templateUrl: "views/index2.html",
                controller: 'indexCtrl'
            })
            .state("menProduct", {
                url: "/nam",
                templateUrl: "views/menProduct.html",
                abstract: true

            })
            .state("menProduct.nam", {
                url: "/",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?gioitinh=nam';
                    }
                }

            })
            .state("menProduct.quan", {
                url: "/quan",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n';
                    }
                }
            })
            .state("menProduct.quanjean", {
                url: "/quan-jean",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n&subloai=qu%E1%BA%A7n%20jean';
                    }
                }
            })
            .state("menProduct.quankaki", {
                url: "/quan-kaki",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n&subloai=qu%E1%BA%A7n%20kaki';
                    }
                }
            })
            .state("menProduct.quanjogger", {
                url: "/quan-jogger",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n&subloai=qu%E1%BA%A7n%20jogger';
                    }
                }
            })
            .state("menProduct.ao", {
                url: "/ao",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=%C3%A1o';
                    }
                }
            })
            .state("menProduct.aosomitayngan", {
                url: "/ao-somi-tay-ngan",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=%C3%A1o&subloai=%C3%81o%20s%C6%A1%20mi%20tay%20ng%E1%BA%AFn';
                    }
                }
            })
            .state("menProduct.aosomitaydai", {
                url: "/ao-somi-tay-dai",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=%C3%A1o&subloai=%C3%81o%20s%C6%A1%20mi%20tay%20d%C3%A0i';
                    }
                }
            })
            .state("menProduct.aothuntayngan", {
                url: "/ao-thun-tay-ngan",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=%C3%A1o&subloai=%C3%A1o%20thun%20tay%20ng%E1%BA%AFn';
                    }
                }
            })
            .state("menProduct.aothuntaydai", {
                url: "/ao-thun-tay-dai",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=%C3%A1o&subloai=%C3%A1o%20thun%20tay%20d%C3%A0i';
                    }
                }
            })
            .state("menProduct.short", {
                url: "/short",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n%20short';
                    }
                }
            })
            .state("menProduct.shortjean", {
                url: "/short-jean",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n%20short&subloai=qu%E1%BA%A7n%20short%20jean';
                    }
                }
            })
            .state("menProduct.shortkaki", {
                url: "/short-kaki",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n%20short&subloai=qu%E1%BA%A7n%20short%20kaki';
                    }
                }
            })
            .state("menProduct.shortthun", {
                url: "/short-thun",
                templateUrl: "views/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n%20short&subloai=qu%E1%BA%A7n%20short%20thun';
                    }
                }
            })
            .state("womenProduct", {
                url: "/women",
                templateUrl: "views/women.html",
                controller: 'womenCtrl'
            })

            .state("products", {
                url: "/san-pham",
                template: "<ui-view></ui-view>",
                abstract: true
            })

            .state("products.Item", {
                url: "/:id",
                templateUrl: "/views/item.html",
                controller: 'menItemCtrl'
            })

            .state("checkout", {
                url: "/checkout",
                templateUrl: "views/checkout.html",
                controller: 'indexCtrl'
            })
            .state("login", {
                url: "/login",
                templateUrl: "views/login.html",
                controller: 'loginCtrl'
            })
            .state("signup", {
                url: "/signup",
                templateUrl: "views/signup.html",
                controller: 'signupCtrl'
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
angular.module('shop').run(function ($rootScope, $state, Services) {
    $rootScope.$on('$stateChangeStart', function (event, next, nextParams, fromState) {

    })
});