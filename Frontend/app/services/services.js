angular.module('shop')
    .service('Services', function ($http, $q) {
    var TOKEN = false; var INFO_USER = 'KH';
    var DSDATHANG_KEY = 'dsdathang';
    var dsdathang = [];
    var isLoged = false;
    var token = '';
    var infouser = {};

    function LoadDangNhap() {
        var check_token = window.sessionStorage.getItem(TOKEN);
        infouser = JSON.parse(window.sessionStorage.getItem(INFO_USER));
        console.log(check_token);
        if (check_token != undefined) {
            isLoged = true;
            $http.defaults.headers.common.Authorization = 'bearer '+check_token;
            token = window.sessionStorage.getItem(TOKEN);
            if (infouser == null || infouser == ""){
                infouser = {};
            }
            console.log(infouser);
        }
    }

    function LoadDatHang() {
        dsdathang = JSON.parse(window.sessionStorage.getItem(DSDATHANG_KEY));
        if (dsdathang == null || dsdathang === '')
        {
            dsdathang = [];
        }
    }

    function DatHang(hang) {
        dsdathang.push(hang);
        window.sessionStorage.setItem(DSDATHANG_KEY, JSON.stringify(dsdathang));
        //LoadDatHang();
    }

    function XoaDatHang(IDCTMT) {
        for (var i=0; i<dsdathang.length; i++)
        {
            if (IDCTMT == dsdathang[i].ID){
                dsdathang.splice(i, 1);
                window.sessionStorage.setItem(DSDATHANG_KEY, JSON.stringify(dsdathang));
            }
        }

    }

    function XoaTatCaDatHang() {
        dsdathang = [];
        window.sessionStorage.removeItem(DSDATHANG_KEY);
    }

    function TongTien() {
        console.log(dsdathang.length);
        var s = 0;
        for (var i=0; i<dsdathang.length; i++){
            s+= eval(dsdathang[i].GiaMoi);
        }
        return s;
    }

    function DangKy(user) {
        return $q(function(resolve, reject) {
            var config = {
                headers: {
                    'Content-Type': 'application/json'
                }
            };

            $http.post(url_api + 'NguoiDung', user, config)
                .success(function (data) {
                    console.log(data);
                    resolve(data);
                })
                .error(function (data) {
                    console.log(data);
                    reject(data);
                });
        })
    };

    function DangNhap(user) {
        return $q(function(resolve, reject) {
            var url = 'http://localhost:57919/api/audience/getAudience';

            var config = {
                headers : {
                    'Content-Type': 'application/json'
                }
            };

            $http.post(url, {Name: "MD5"}, config)
                .success(function (data) {
                    console.log(data);

                    user.client_id = data.ClientId;
                    console.log(user);

                    $http({
                        method: 'POST',
                        url: 'http://localhost:57919/oauth2/token',
                        headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                        transformRequest: function(obj) {
                            var str = [];
                            for (var p in obj)
                                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                            return str.join("&");
                        },
                        data: user
                    }).success(function (data) {
                        console.log(data);

                        isLoged = true;
                        token = 'bearer '+ data.access_token;
                        infouser = user;
                        console.log(infouser);

                        $http.defaults.headers.common.Authorization = token;

                        window.sessionStorage.setItem(TOKEN, data.access_token);
                        window.sessionStorage.setItem(INFO_USER, JSON.stringify(user));

                        resolve(data.access_token);
                    }).error(function (data) {
                        console.log(data);
                        reject(data);
                    });

                })
                .error(function (data) {
                    console.log(data);

                });


    }
        )}

    function DangXuat() {
        isLoged = false;
        infouser = {};
        token = '';
        $http.defaults.headers.common.Authorization = undefined;
        window.sessionStorage.removeItem(TOKEN);
        window.sessionStorage.removeItem('USER');
    }

    LoadDatHang();
    LoadDangNhap();

    return {
        dathang: DatHang,
        dsdathang : function() {return dsdathang;},
        xoadathang: XoaDatHang,
        xoatatca: XoaTatCaDatHang,
        tongtien: TongTien,
        signup: DangKy,
        login: DangNhap,
        logout: DangXuat,
        isLoged: function() {return isLoged;},
        infouser: function() {return infouser;}
    };
})
    .factory('ServiceInterceptor', function ($rootScope) {
        return {

        };
    })

    .config(function ($httpProvider) {
        $httpProvider.interceptors.push('ServiceInterceptor');
    });
