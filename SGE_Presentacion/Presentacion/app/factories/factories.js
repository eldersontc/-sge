'use strict';

define(['app'], function (app) {
    app.factory('alertFactory', function() {
        return {
            showFloating: function (type, message) {
                var icons = {'success': 'ok', 'warning': 'warning', 'info': 'info', 'danger': 'exclamation'},
                    divAlert = $('<div class="alert alert-'+ type +' alert-floating" role="alert">' +
                                    '<span class="glyphicon glyphicon-' + icons[type] + '-sign" aria-hidden="true"></span>' +
                                    '<button type="button" class="close" data-dismiss="alert">&times</button>' +
                                    '<span>&nbsp;&nbsp;'+ message +'</span>' +
                                 '</div>');
                divAlert.appendTo($('body')).fadeIn(300).delay(7000).fadeOut(500);
            },
            showStatic: function (type, message, id) {
                var icons = {'success': 'ok', 'warning': 'warning', 'info': 'info', 'danger': 'exclamation'},
                    divAlert = $('<div id="alert-static" class="alert alert-'+ type +'" role="alert">' +
                                    '<span class="glyphicon glyphicon-' + icons[type] + '-sign" aria-hidden="true"></span>' +
                                    '<span>&nbsp;&nbsp;'+ message +'</span>' +
                                 '</div>'); 
                var div = (id) ? jQuery(id) : jQuery('#divAlert');
                div.empty();
                div.append(divAlert);
            },
            hideStatic: function (id) {
                var div = (id) ? jQuery(id) : jQuery('#divAlert');
                div.empty();
            }
        };
    }).factory('preloadFactory', function() {
        return {
            showProcessing: function (isShow, id) {
                var isLoad = jQuery('body #load-process');
                if(isLoad.size() > 0) {
                    isLoad.remove();
                }
                var htmlPreload = angular.element('<div id="load-process"><div class="text-center" style="padding-top:20%"><i class="icon-preload-lg"></i>Procesando ...</div></div>'),
                body = jQuery('body');
                if (isShow) {
                    if (id) {
                        var _h, jId = jQuery(id).innerHeight()/2, jH = 50;
                        
                        if (jId > jH) {
                            _h = jId - jH; 
                        } else {
                            _h = 0; 
                        }
                        jQuery(id).css('position','relative').append(htmlPreload);
                        
                        $(htmlPreload).find('.text-center').css('padding-top', _h + 'px');
                        $(htmlPreload).addClass('preload_absolute');
                    } else {
                        body.append(htmlPreload);
                        $(htmlPreload).addClass('preload_fixed');
                    }
                } else {
                    jQuery('body #load-process').remove();
                }
            },
            showLoading: function (isShow, id) {
                var div = (id) ? jQuery(id) : jQuery('#divLoading');
                div.empty();
                if (isShow) {
                    var divLoading = $('<div class="text-center"><i class="icon-preload-lg"></i>' + TEXTOS.CARGANDO + '</div>');
                    div.append(divLoading);    
                }
            }
        };
    }).factory('httpFactory', function($http) {
        return {
            post: function (url, params, success, error) {
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


