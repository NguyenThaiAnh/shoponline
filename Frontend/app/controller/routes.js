angular.module('shop')
    .config(function($stateProvider,  $urlRouterProvider, $locationProvider) {

        $stateProvider
            .state("home", {
                url: "/",
                templateUrl: "/view/index2.html"
            })
            .state("menProduct", {
                url: "/nam",
                templateUrl: "/view/menProduct.html",
                abstract: true

            })
            .state("menProduct.nam", {
                url: "/",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?gender=nam';
                    }
                }

            })
            .state("menProduct.quan", {
                url: "/quan",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n';
                    }
                }
            })
            .state("menProduct.quanjean", {
                url: "/quan-jean",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n&subloai=qu%E1%BA%A7n%20jean';
                    }
                }
            })
            .state("menProduct.quankaki", {
                url: "/quan-kaki",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n&subloai=qu%E1%BA%A7n%20kaki';
                    }
                }
            })
            .state("menProduct.quanjogger", {
                url: "/quan-jogger",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n&subloai=qu%E1%BA%A7n%20jogger';
                    }
                }
            })
            .state("menProduct.ao", {
                url: "/ao",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=%C3%A1o';
                    }
                }
            })
            .state("menProduct.aosomitayngan", {
                url: "/ao-somi-tay-ngan",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=%C3%A1o&subloai=%C3%81o%20s%C6%A1%20mi%20tay%20ng%E1%BA%AFn';
                    }
                }
            })
            .state("menProduct.aosomitaydai", {
                url: "/ao-somi-tay-dai",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=%C3%A1o&subloai=%C3%81o%20s%C6%A1%20mi%20tay%20d%C3%A0i';
                    }
                }
            })
            .state("menProduct.aothuntayngan", {
                url: "/ao-thun-tay-ngan",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=%C3%A1o&subloai=%C3%A1o%20thun%20tay%20ng%E1%BA%AFn';
                    }
                }
            })
            .state("menProduct.aothuntaydai", {
                url: "/ao-thun-tay-dai",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=%C3%A1o&subloai=%C3%A1o%20thun%20tay%20d%C3%A0i';
                    }
                }
            })
            .state("menProduct.short", {
                url: "/short",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n%20short';
                    }
                }
            })
            .state("menProduct.shortjean", {
                url: "/short-jean",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n%20short&subloai=qu%E1%BA%A7n%20short%20jean';
                    }
                }
            })
            .state("menProduct.shortkaki", {
                url: "/short-kaki",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n%20short&subloai=qu%E1%BA%A7n%20short%20kaki';
                    }
                }
            })
            .state("menProduct.shortthun", {
                url: "/short-thun",
                templateUrl: "/view/menProduct.product.html",
                controller: 'menCtrl',
                resolve: {
                    url : function () {
                        return 'mathang?loai=qu%E1%BA%A7n%20short&subloai=qu%E1%BA%A7n%20short%20thun';
                    }
                }
            })
            .state("womenProduct", {
                url: "/women",
                templateUrl: "/view/women.html",
                controller: 'womenCtrl'
            })
            // .state("menItemProduct", {
            //     url: "/men/:id",
            //     templateUrl: "/view/item.html",
            //     controller: 'menItemCtrl'
            // })
            .state("checkout", {
                url: "/checkout",
                templateUrl: "/view/checkout.html"
            })
            .state('otherwise', {
                url: '/404',
                templateUrl: '/view/404.html'
            });

        $urlRouterProvider.otherwise('/404');

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: true,
            rewriteLinks: false
        });
});