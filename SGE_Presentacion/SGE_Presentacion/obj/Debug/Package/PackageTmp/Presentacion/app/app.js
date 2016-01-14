'use strict';

define(['services/routeResolver'], function () {
   
    var app = angular.module('app', ['ngRoute', 'ui.bootstrap', 'routeResolverServices', 'ngCookies']);
    
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
                    .when('/', { redirectTo: '/inicio' })
                    .otherwise({ redirectTo: '/404' });

			app.routeProvider = $routeProvider;
			app.route = route;
					
        }]);

    app.controller('appController', function ($scope, $cookieStore, $window) {
        
		$scope.globals = $cookieStore.get('globals') || {};

        $scope.menus = [
            { 
                nombre: 'ADMINISTRACIÓN',
                icono: 'home',
                subMenus:[
                    { nombre: 'USUARIO', url: 'admUsuario', icono: 'user' },
                    { nombre: 'PERIFL', url: 'admPerfil', icono: 'users' },
                    { nombre: 'MONEDA', url: 'admMoneda', icono: 'user' },
                    { nombre: 'DOCUMENTO DE IDENTIDAD', url: 'admDocumentoIdentidad', icono: 'user' },
                    { nombre: 'NUMERACIÓN', url: 'admNumeracion', icono: 'user' },
                    { nombre: 'REPORTE', url: 'admReporte', icono: 'user' },
                    { nombre: 'EMPLEADO', url: 'admEmpleado', icono: 'user' }
                ]
            },
            {
                nombre: 'INVENTARIOS',
                icono: 'home',
                subMenus: [
                    { nombre: 'ALMACEN', url: 'invAlmacen', icono: 'user' },
                    { nombre: 'UNIDAD', url: 'invUnidad', icono: 'user' },
                    { nombre: 'MATERIAL', url: 'invMaterial', icono: 'user' }
                ]
            },
            {
                nombre: 'VENTAS',
                icono: 'home',
                subMenus: [
                    { nombre: 'PLANTILLA', url: 'venPlantilla', icono: 'user' },
                    { nombre: 'SOLICITUD DE COTIZACIÓN', url: 'venSolCotizacion', icono: 'user' },
                    { nombre: 'COTIZACIÓN', url: 'venCotizacion', icono: 'user' },
                    { nombre: 'PRESUPUESTO', url: 'venPresupuesto', icono: 'user' }
                ]
            }
        ];
		
		angular.forEach($scope.menus, function (m) {
			angular.forEach(m.subMenus, function (s) {
				app.routeProvider.when('/' + s.url, app.route.resolve(s.url))
			});
		});
        
		$scope.salir = function () {
		    $cookieStore.remove('globals');
		    $window.location.href = './login.html';
		};

		$scope.$on('$locationChangeStart', function (event, next, current) {
			if (!$scope.globals.currentUser) {
				$window.location.href = './login.html';
			}
		});
		
    });

    return app;
});