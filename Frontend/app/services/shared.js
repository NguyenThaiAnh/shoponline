angular.module('shop').service('sharedService', function ($rootScope) {
    var keyword = "";

    function set_keyword(key) {
        keyword = key;
    }

    function get_keyword() {
        return keyword;
    }

    return{
        setkeyword: set_keyword,
        getkeyword: get_keyword
    }

});