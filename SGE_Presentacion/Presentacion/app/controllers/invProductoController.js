'use strict';

define(['app'], function (app) {

    app.register.controller('invProductoController', ['$scope', '$http', '$uibModal', '$timeout', 'preloadFactory', 'httpFactory', 'alertFactory', function ($scope, $http, $uibModal, $timeout, preloadFactory, httpFactory, alertFactory) {

        var urlObtenerTodos = URL_BASE + 'Inventarios/invProducto.aspx/ObtenerTodos',
            urlObtenerPorId = URL_BASE + 'Inventarios/invProducto.aspx/ObtenerPorId',
            urlAgregar = URL_BASE + 'Inventarios/invProducto.aspx/Agregar',
            urlActualizar = URL_BASE + 'Inventarios/invProducto.aspx/Actualizar',
            urlEliminar = URL_BASE + 'Inventarios/invProducto.aspx/Eliminar';
        
        $scope.productos = [];
        $scope.producto = {};
        $scope.verTabla = false;
        $scope.verTablaUnidad = false;
        $scope.verTablaAlmacen = false;

        var mdlAgregar,
            mdlActualizar,
            mdlEliminar;
        
        $scope.verMdlAgregar = function () {
            $scope.proeucto = { unidades: [], almacenes: [] };
            mdlAgregar = $uibModal.open({
                animation: true,
                templateUrl: 'mdlAgregar',
                scope: $scope,
                size: '500'
            });
            $timeout(function() {
            	$scope.verTablaUnidad = false;
                alertFactory.showStatic('info', TEXTOS.SIN_REGISTROS, '#divAlertUnidad');
            }, 500);
        };

        $scope.cerrarMdlAgregar = function () {
            mdlAgregar.close();
        };
        
        var obtenerItems = function (idReporte) {
            $scope.verTablaItem = false;
            preloadFactory.showLoading(true, '#divLoadingItem');
            httpFactory.post(urlObtenerItems, { idReporte: idReporte }, 
                function (data) {
                    $scope.reporte.items = data.items;
                    if ($scope.reporte.items.length > 0) {
                        $scope.verTablaItem = true;
                    }
                    else {
                        alertFactory.showStatic('info', TEXTOS.SIN_REGISTROS, '#divAlertItem');
                    }
                    preloadFactory.showLoading(false, '#divLoadingItem');
                }, 
                function () {
                    preloadFactory.showLoading(false, '#divLoadingItem');
                    alertFactory.showStatic('danger', TEXTOS.ERROR_SERVIDOR, '#divAlertItem');
                });
        };

        $scope.verMdlActualizar = function (reporte) {
            $scope.reporte = angular.copy(reporte);
            $scope.reporte.idsItems = [];
            mdlActualizar = $uibModal.open({
                animation: true,
                templateUrl: 'mdlActualizar',
                scope: $scope,
                size: '500'
            });
            $timeout(function () { 
                obtenerItems($scope.reporte.idReporte);
            }, 500);
        };

        $scope.cerrarMdlActualizar = function () {
            mdlActualizar.close();
        };
        
        $scope.verMdlEliminar = function (reporte) {
            $scope.reporte = angular.copy(reporte);
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
                    $scope.productos = data.productos;
                    if ($scope.productos.length > 0) {
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
        
        $scope.agregarItem = function () {
            $scope.reporte.items.push({ asignarId: false });
            $scope.verTablaItem = true;
            alertFactory.hideStatic('#divAlertItem');
        };
        
        $scope.eliminarItem = function (index) {
            var item = $scope.reporte.items.splice(index, 1)[0];
            if (angular.isDefined(item.idReporte)) {
                $scope.reporte.idsItems.push(item.idReporte);    
            }
            if ($scope.reporte.items.length == 0) {
                $scope.verTablaItem = false;
                alertFactory.showStatic('info', TEXTOS.SIN_REGISTROS, '#divAlertItem');
            }
        };
        
        $scope.agregar = function (form) {
            if (form.$valid) {
                preloadFactory.showProcessing(true);
                httpFactory.post(urlAgregar, { reporte: $scope.reporte }, 
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
                httpFactory.post(urlActualizar, { reporte: $scope.reporte }, 
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
            httpFactory.post(urlEliminar, { idReporte: $scope.reporte.idReporte }, 
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