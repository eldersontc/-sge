'use strict';

define(['app'], function (app) {

    app.register.controller('admUsuarioController', ['$scope', '$http', '$uibModal', 'preloadFactory', 'httpFactory', 'alertFactory', function ($scope, $http, $uibModal, preloadFactory, httpFactory, alertFactory) {

        var urlObtenerTodos = URL_BASE + 'Administracion/admUsuario.aspx/ObtenerTodos',
            urlObtenerPerfiles = URL_BASE + 'Administracion/admPerfil.aspx/ObtenerActivos',
            urlAgregar = URL_BASE + 'Administracion/admUsuario.aspx/Agregar',
            urlActualizar = URL_BASE + 'Administracion/admUsuario.aspx/Actualizar',
            urlEliminar = URL_BASE + 'Administracion/admUsuario.aspx/Eliminar';
        
        $scope.usuarios = [];
        $scope.usuario = {};
        $scope.perfiles = [];
        $scope.verTabla = false;

        var mdlAgregar,
            mdlActualizar,
            mdlEliminar;

        $scope.verMdlAgregar = function () {
            $scope.usuario = {};
            mdlAgregar = $uibModal.open({
                animation: true,
                templateUrl: 'mdlAgregar',
                scope: $scope,
                size: '445'
            });
            obtenerPerfiles(seleccione());
        };

        $scope.cerrarMdlAgregar = function () {
            mdlAgregar.close();
        };
        
        $scope.verMdlActualizar = function (usuario) {
            $scope.usuario = angular.copy(usuario);
            mdlActualizar = $uibModal.open({
                animation: true,
                templateUrl: 'mdlActualizar',
                scope: $scope,
                size: '445'
            });
            obtenerPerfiles($scope.usuario.perfil);
        };

        $scope.cerrarMdlActualizar = function () {
            mdlActualizar.close();
        };

        $scope.verMdlEliminar = function (usuario) {
            $scope.usuario = angular.copy(usuario);
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
                    $scope.usuarios = data.usuarios;
                    if ($scope.usuarios.length > 0) {
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
            return { idPerfil: null, nombre: TEXTOS.SELECCIONE };
        }, 
            obtenerPerfiles = function (perfil) {
            httpFactory.post(urlObtenerPerfiles, {}, 
                function (data) {
                    $scope.perfiles = data.perfiles;
                    $scope.perfiles.unshift(seleccione());
                    $scope.usuario.perfil = perfil;
                }, 
                function () {
                    alertFactory.showFloating('danger', TEXTOS.ERROR_SERVIDOR);
                });
        };

        $scope.agregar = function (form) {
            if (form.$valid) {
                preloadFactory.showProcessing(true);
                httpFactory.post(urlAgregar, { usuario: $scope.usuario }, 
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
                httpFactory.post(urlActualizar, { usuario: $scope.usuario }, 
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
            httpFactory.post(urlEliminar, { idUsuario: $scope.usuario.idUsuario }, 
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