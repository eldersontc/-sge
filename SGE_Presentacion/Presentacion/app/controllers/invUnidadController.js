'use strict';

define(['app'], function (app) {

    app.register.controller('invUnidadController', ['$scope', '$http', '$uibModal', 'preloadFactory', 'httpFactory', 'alertFactory', function ($scope, $http, $uibModal, preloadFactory, httpFactory, alertFactory) {

        var urlObtenerTodos = URL_BASE + 'Inventarios/invUnidad.aspx/ObtenerTodos',
            urlAgregar = URL_BASE + 'Inventarios/invUnidad.aspx/Agregar',
            urlActualizar = URL_BASE + 'Inventarios/invUnidad.aspx/Actualizar',
            urlEliminar = URL_BASE + 'Inventarios/invUnidad.aspx/Eliminar';
        
        $scope.unidades = [];
        $scope.unidad = {};
        $scope.verTabla = false;

        var mdlAgregar,
            mdlActualizar,
            mdlEliminar;

        $scope.verMdlAgregar = function () {
            $scope.unidad = {};
            mdlAgregar = $uibModal.open({
                animation: true,
                templateUrl: 'mdlAgregar',
                scope: $scope,
                size: '445'
            });
        };

        $scope.cerrarMdlAgregar = function () {
            mdlAgregar.close();
        };
        
        $scope.verMdlActualizar = function (unidad) {
            $scope.unidad = angular.copy(unidad);
            mdlActualizar = $uibModal.open({
                animation: true,
                templateUrl: 'mdlActualizar',
                scope: $scope,
                size: '445'
            });
        };

        $scope.cerrarMdlActualizar = function () {
            mdlActualizar.close();
        };

        $scope.verMdlEliminar = function (unidad) {
            $scope.unidad = angular.copy(unidad);
            mdlEliminar = $uibModal.open({
                animation: true,
                templateUrl: 'mdlEliminar',
                scope: $scope,
                size: '445'
            });
        };

        $scope.cerrarMdlEliminar = function () {
            mdlEliminar.close();
        };

        var obtenerTodos = function () {
            $scope.verTabla = false;
            preloadFactory.showLoading(true);
            httpFactory.post(urlObtenerTodos, {}, 
                function (data) {
                    $scope.unidades = data.unidades;
                    if ($scope.unidades.length > 0) {
                        $scope.verTabla = true;
                        alertFactory.hideStatic();
                    }
                    else {
                        alertFactory.showStatic('info', TEXTOS.SIN_REGISTROS);
                    }
                    preloadFactory.showLoading(false);
                }, 
                function () {
                    preloadFactory.showLoading(false);
                    alertFactory.showStatic('danger', TEXTOS.ERROR_SERVIDOR);
                });
        };

        $scope.agregar = function (form) {
            if (form.$valid) {
                preloadFactory.showProcessing(true);
                httpFactory.post(urlAgregar, { unidad: $scope.unidad }, 
                    function (data) {
                        mdlAgregar.close();
                        obtenerTodos();
                        preloadFactory.showProcessing(false);
                        alertFactory.showFloating('success', TEXTOS.EXITO_AGREGAR);
                    }, 
                    function () {
                        preloadFactory.showProcessing(false);
                        alertFactory.showFloating('danger', TEXTOS.ERROR_SERVIDOR);
                    });
            }
        };

        $scope.actualizar = function (form) {
            if (form.$valid) {
                preloadFactory.showProcessing(true);
                httpFactory.post(urlActualizar, { unidad: $scope.unidad }, 
                    function (data) {
                        mdlActualizar.close();
                        obtenerTodos();
                        preloadFactory.showProcessing(false);
                        alertFactory.showFloating('success', TEXTOS.EXITO_ACTUALIZAR);
                    }, 
                    function () {
                        preloadFactory.showProcessing(false);
                        alertFactory.showFloating('danger', TEXTOS.ERROR_SERVIDOR);
                    });
            }
        };
        
        $scope.eliminar = function () {
            preloadFactory.showProcessing(true);
            httpFactory.post(urlEliminar, { idUnidad: $scope.unidad.idUnidad }, 
                function (data) {
                    mdlEliminar.close();
                    obtenerTodos();
                    preloadFactory.showProcessing(false);
                    alertFactory.showFloating('success', TEXTOS.EXITO_ELIMINAR);
                }, 
                function () {
                    preloadFactory.showProcessing(false);
                    alertFactory.showFloating('danger', TEXTOS.ERROR_SERVIDOR);
                });
        };
        
        obtenerTodos();

    }]);
});