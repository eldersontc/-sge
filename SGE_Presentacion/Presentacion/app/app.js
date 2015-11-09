'use strict';

define(['services/routeResolver'], function () {

    var app = angular.module('app', ['ngRoute', 'ui.bootstrap', 'routeResolverServices']);

    app.config(['$routeProvider', 'routeResolverProvider', '$controllerProvider',
        '$compileProvider', '$filterProvider', '$provide',
        function ($routeProvider, routeResolverProvider, $controllerProvider,
                $compileProvider, $filterProvider, $provide) {
            
            app.register =
                    {
                        controller: $controllerProvider.register,
                        directive: $compileProvider.directive,
                        filter: $filterProvider.register,
                        factory: $provide.factory,
                        service: $provide.service
                    };

            var route = routeResolverProvider.route;

            $routeProvider
                    .when('/inicio', route.resolve('inicio'))
                    .when('/404', route.resolve('404'))
                    .when('/admUsuario', route.resolve('admUsuario'))
                    .when('/admPerfil', route.resolve('admPerfil'))
                    .when('/admMoneda', route.resolve('admMoneda'))
                    .when('/admDocumentoIdentidad', route.resolve('admDocumentoIdentidad'))
                    .when('/admNumeracion', route.resolve('admNumeracion'))
                    .when('/admReporte', route.resolve('admReporte'))
                    .when('/admEmpleado', route.resolve('admEmpleado'))
                    .when('/invAlmacen', route.resolve('invAlmacen'))
                    .when('/invUnidad', route.resolve('invUnidad'))
                    .when('/invProducto', route.resolve('invProducto'))
                    .when('/', { redirectTo: '/inicio' })
                    .otherwise({ redirectTo: '/404' });

        }]);

    app.controller('appController', function ($scope) {
        
        $scope.menus = [
            { 
                nombre: 'ADMINISTRACIÓN',
                imagen: 'buy-16.png',
                subMenus:[
                    { nombre: 'USUARIO', path: 'admUsuario', imagen: 'text-file-16.png' },
                    { nombre: 'PERFIL', path: 'admPerfil', imagen: 'text-file-16.png' },
                    { nombre: 'MONEDA', path: 'admMoneda', imagen: 'text-file-16.png' },
                    { nombre: 'DOCUMENTO IDENTIDAD', path: 'admDocumentoIdentidad', imagen: 'text-file-16.png' },
                    { nombre: 'NUMERACIÓN', path: 'admNumeracion', imagen: 'text-file-16.png' },
                    { nombre: 'REPORTE', path: 'admReporte', imagen: 'text-file-16.png' },
                    { nombre: 'EMPLEADO', path: 'admEmpleado', imagen: 'text-file-16.png' }
                ]
            },
            {
                nombre: 'INVENTARIOS',
                imagen: 'buy-16.png',
                subMenus: [
                    { nombre: 'ALMACÉN', path: 'invAlmacen', imagen: 'text-file-16.png' },
                    { nombre: 'UNIDAD', path: 'invUnidad', imagen: 'text-file-16.png' },
                    { nombre: 'PRODUCTO', path: 'invProducto', imagen: 'text-file-16.png' }
                ]
            }
        ];
        
    });

    return app;
});