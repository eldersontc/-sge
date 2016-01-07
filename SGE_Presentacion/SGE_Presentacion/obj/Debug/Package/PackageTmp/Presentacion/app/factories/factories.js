'use strict';

define(['app'], function (app) {

    app.factory('http', function($http) {
        return {
            post: function (url, params, success, error) {
                params.sesion = { usuario: { idUsuario: 10 } };
                $http.post(url, params).
                    success(function (data) {
                        if (angular.isDefined(data) && angular.isDefined(data.d)) {
                            if (data.d.correcto) {
                                success(data.d);    
                            } else {
                                error();
                            }
                        } else {
                            error();
                        }
                    }).
                    error(function (data) {
                        error();
                    });
            }
        };
    });
});