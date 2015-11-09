'use strict';

define(['app'], function (app) {

    app.register.controller('admEmpleadoController', ['$scope', '$http', '$uibModal', 'preloadFactory', 'httpFactory', 'alertFactory', function ($scope, $http, $uibModal, preloadFactory, httpFactory, alertFactory) {

        var urlObtenerTodos = URL_BASE + 'Administracion/admEmpleado.aspx/ObtenerTodos',
            urlObtenerDocumentosIdentidad = URL_BASE + 'Administracion/admDocumentoIdentidad.aspx/ObtenerActivos',
            urlAgregar = URL_BASE + 'Administracion/admEmpleado.aspx/Agregar',
            urlActualizar = URL_BASE + 'Administracion/admEmpleado.aspx/Actualizar',
            urlEliminar = URL_BASE + 'Administracion/admEmpleado.aspx/Eliminar';
        
        $scope.empleados = [];
        $scope.empleado = {};
        $scope.documentosIdentidad = [];
        $scope.verTabla = false;

        var mdlAgregar,
            mdlActualizar,
            mdlEliminar;

        $scope.verMdlAgregar = function () {
            $scope.empleado = {};
            mdlAgregar = $uibModal.open({
                animation: true,
                templateUrl: 'mdlAgregar',
                scope: $scope,
                size: '500'
            });
            obtenerDocumentosIdentidad(seleccione());
        };

        $scope.cerrarMdlAgregar = function () {
            mdlAgregar.close();
        };
        
        $scope.verMdlActualizar = function (empleado) {
            $scope.empleado = angular.copy(empleado);
            mdlActualizar = $uibModal.open({
                animation: true,
                templateUrl: 'mdlActualizar',
                scope: $scope,
                size: '500'
            });
            obtenerDocumentosIdentidad($scope.empleado.documentoIdentidad);
        };

        $scope.cerrarMdlActualizar = function () {
            mdlActualizar.close();
        };

        $scope.verMdlEliminar = function (empleado) {
            $scope.empleado = angular.copy(empleado);
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
                    $scope.empleados = data.empleados;
                    if ($scope.empleados.length > 0) {
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
        },
            seleccione = function () {
            return { idDocumentoIdentidad: null, abreviacion: TEXTOS.SELECCIONE };
        }, 
            obtenerDocumentosIdentidad = function (documentoIdentidad) {
            httpFactory.post(urlObtenerDocumentosIdentidad, {}, 
                function (data) {
                    $scope.documentosIdentidad = data.documentosIdentidad;
                    $scope.documentosIdentidad.unshift(seleccione());
                    $scope.empleado.documentoIdentidad = documentoIdentidad;
                }, 
                function () {
                    alertFactory.showFloating('danger', TEXTOS.ERROR_SERVIDOR);
                });
        };

        $scope.agregar = function (form) {
            if (form.$valid) {
                preloadFactory.showProcessing(true);
                httpFactory.post(urlAgregar, { empleado: $scope.empleado }, 
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
                httpFactory.post(urlActualizar, { empleado: $scope.empleado }, 
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
            httpFactory.post(urlEliminar, { idEmpleado: $scope.empleado.idEmpleado }, 
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