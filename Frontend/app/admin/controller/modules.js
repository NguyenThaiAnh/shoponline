'use strict';
angular.module('admin', [
    'ui.router',
    'LocalStorageModule',
    'angular-jwt',
    'ngDialog',
    'listproductCtrl',
    'viewproductCtrl',
    'addproductCtrl',
    'updateproductCtrl',
    'listbillCtrl',
    'viewbillCtrl',
    'updatebillCtrl',
    'listcustomerCtrl'
]);

angular.module('admin')

    .constant('AUTH_EVENTS', {
        notAuthenticated: 'auth-not-authenticated'
    })

    .constant('API_ENDPOINT', {
        url: 'http://localhost:57919/'
        //  For a simulator use: url: 'http://127.0.0.1:8080/api'
    });