// /**
//  * Created by Lucifer on 01/01/2017.
//  */
// 'use strict';
//
// var menItemCtrl = angular.module('menItemCtrl', []);
// // Url general API
// var url_api = "http://localhost:57919/api/v1/";
//
// // Create controller for route[/men/{id}]
// menCtrl.controller('menItemCtrl', ['$stateParams', '$scope', '$http', function ($stateParams, $scope, $http) {
//
//     // Get list product of men
//     $http.get(url_api + 'mathang?id='+$stateParams.id)
//         .success(function (response) {
//             $scope.mathang = response;
//             $scope.soluong = [];
//             $scope.soluongMax = [];
//
//             console.log(response);
//         });
//
//
//
//     $scope.btplus = function(pos){
//         if (parseInt($scope.soluong[pos], 10) < $scope.mathang.Items[pos].SoLuong ){
//             $scope.soluong[pos] = parseInt($scope.soluong[pos], 10) + 1;
//         }
//         //alert("So luong is " + $scope.soluong[pos]);
//     };
//
//     $scope.btminus = function(pos){
//         if (parseInt($scope.soluong[pos], 10) > 0){
//             $scope.soluong[pos] = parseInt($scope.soluong[pos], 10) - 1;
//         }
//         //alert("So luong is " + $scope.soluong[pos]);
//     };
//
// }]);
