'use strict';
angular.module('shop', [
    'ui.router',
    'LocalStorageModule',
    'angular-flexslider',
    'ngDialog',
    'menCtrl',
    'menItemCtrl',
    'indexCtrl',
    'signupCtrl',
    'loginCtrl',
    'homeCtrl',
    'searchCtrl'
]);

window.fbAsyncInit = function() {
    FB.init({
        appId      : '619254434951876',
        xfbml      : true,
        version    : 'v2.8'
    });
    FB.AppEvents.logPageView();
};

(function(d, s, id){
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) {return;}
    js = d.createElement(s); js.id = id;
    js.src = "/js/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));