// 'use strict';
// angular.module('admin').factory('ordersService', ['$http', function ($http) {
//
//     var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
//     var ordersServiceFactory = {};
//
//     var _getOrders = function () {
//
//         return $http.get(serviceBase + 'api/orders').then(function (results) {
//             return results;
//         });
//     };
//
//     ordersServiceFactory.getOrders = _getOrders;
//
//     return ordersServiceFactory;
//
// }]);