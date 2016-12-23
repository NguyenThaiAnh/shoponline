var myApp = angular.module('shop', ['ui.router']);

myApp.config(function($stateProvider,  $urlRouterProvider, $locationProvider) {
    var homeState = {
        name: 'home',
        url: '/',
        templateUrl: '/view/index2.html'
    }

    var menProductState = {
        name: 'men',
        url: '/men',
        templateUrl: '/view/men.html'
    }

    var womenProductState = {
        name: 'women',
        url: '/women',
        templateUrl: '/view/men.html'
    }

    var checkoutState = {
        name: 'checkout',
        url: '/checkout',
        templateUrl: '/view/checkout.html'
    }

    var otherwiseState = {
        name: 'otherwise',
        url: '/404',
        templateUrl: '/view/404.html'
    }

    $urlRouterProvider.otherwise('/404');
    $stateProvider.state(homeState);
    $stateProvider.state(menProductState);
    $stateProvider.state(womenProductState);
    $stateProvider.state(checkoutState);
    $stateProvider.state(otherwiseState);
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: true,
        rewriteLinks: false
    });
});