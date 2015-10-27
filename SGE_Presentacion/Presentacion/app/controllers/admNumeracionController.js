'use strict';

define(['app'], function (app) {

    app.register.controller('admNumeracionController', ['$scope', '$http', '$uibModal', 'preloadFactory', 'httpFactory', 'alertFactory', function ($scope, $http, $uibModal, preloadFactory, httpFactory, alertFactory) {

        var urlObtenerTodos = URL_BASE + 'Administracion/admNumeracion.aspx/ObtenerTodos',
            urlAgregar = URL_BASE + 'Administracion/admNumeracion.aspx/Agregar',
            urlActualizar = URL_BASE + 'Administracion/admNumeracion.aspx/Actualizar',
            urlEliminar = URL_BASE + 'Administracion/admNumeracion.aspx/Eliminar';
        
        $scope.numeraciones = [];
        $scope.numeracion = {};
        $scope.verTabla = false;
        
        var mdlAgregar,
            mdlActualizar,
            mdlEliminar;

        $scope.verMdlAgregar = function () {
            $scope.numeracion = { documento: null };
            mdlAgregar = $uibModal.open({
                animation: true,
                templateUrl: 'mdlAgregar',
                scope: $scope,
                size: '500'
            });
        };

        $scope.cerrarMdlAgregar = function () {
            mdlAgregar.close();
        };
        
        $scope.verMdlActualizar = function (numeracion) {
            $scope.numeracion = angular.copy(numeracion);
            mdlActualizar = $uibModal.open({
                animation: true,
                templateUrl: 'mdlActualizar',
                scope: $scope,
                size: '500'
            });
        };

        $scope.cerrarMdlActualizar = function () {
            mdlActualizar.close();
        };
        
        $scope.verMdlEliminar = function (numeracion) {
            $scope.numeracion = angular.copy(numeracion);
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
                    $scope.numeraciones = data.numeraciones;
                    if ($scope.numeraciones.length > 0) {
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
                httpFactory.post(urlAgregar, { numeracion: $scope.numeracion }, 
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
                httpFactory.post(urlActualizar, { numeracion: $scope.numeracion }, 
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
            httpFactory.post(urlEliminar, { idNumeracion: $scope.numeracion.idNumeracion }, 
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