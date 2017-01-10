var searchCtrl = angular.module('searchCtrl', []);

// Url general API
var url_api = "http://localhost:57919/api/v1/";

searchCtrl.controller('searchCtrl', ['$scope', '$http', '$state', 'sharedService',
    function ($scope, $http, $state, sharedService) {

        if (sharedService.getkeyword() == ""){$state.go('home');}
        else {
            // Get list product of men
            $http.get(url_api + 'mathang?tukhoa=' + sharedService.getkeyword())
                .success(function (response) {
                    $scope.currentPage = 0;

                    $scope.mathang = response;

                    sharedService.setkeyword("");
                    console.log(response);

                    $scope.numberOfPages = function () {
                        return Math.ceil($scope.mathang.length/$scope.pageSize);
                    };
                });
        }
    }]);
searchCtrl.filter('startFrom', function() {
    return function(input, start) {
        start = +start; //parse to int
        if ( typeof input == 'undefined') {
            input = [];
        }
        //console.log(input);
        return input.slice(start);
    }
});