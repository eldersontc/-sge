'use strict';

define(['app'], function (app) {

    app.register.controller('venPresupuestoController', ['$scope', 'http', function ($scope, http) {

        //var URL_BASE = 'http://localhost:52455/';
        var URL_BASE = 'http://localhost/SGE_Aplicacion/';

        // URLs
        var urlObtenerTodos = URL_BASE + 'Ventas/venPresupuesto.aspx/ObtenerTodos',
	    	urlObtenerPorId = URL_BASE + 'Ventas/venPresupuesto.aspx/ObtenerPorId',
	        urlAgregar = URL_BASE + 'Ventas/venPresupuesto.aspx/Agregar',
	        urlActualizar = URL_BASE + 'Ventas/venPresupuesto.aspx/Actualizar',
	        urlEliminar = URL_BASE + 'Ventas/venPresupuesto.aspx/Eliminar',
            urlObtenerClientes = URL_BASE + 'Ventas/venCliente.aspx/ObtenerActivos',
	        urlObtenerNumeraciones = URL_BASE + 'Administracion/admNumeracion.aspx/ObtenerActivos',
            urlObtenerVendedores = URL_BASE + 'Administracion/admEmpleado.aspx/ObtenerVendedores',
            urlObtenerMonedas = URL_BASE + 'Administracion/admMoneda.aspx/ObtenerActivos',
	        urlObtenerCotizaciones = URL_BASE + 'Ventas/venCotizacion.aspx/ObtenerPendientes',
            urlDescargarPdf = URL_BASE + 'Descarga/Descarga.aspx';

        // Personalizadas
        $scope.opcion = 1;
        $scope.chkTodos = false;
        $scope.colSel = [];

        // Paginación
        $scope.nroRegistros = '10';
        $scope.totalRegistros = 0;
        $scope.pagActual = 1;
        $scope.pagVisibles = 5;

        $scope.cambioNroRegistros = function () {
            obtenerTodos();
        }

        $scope.cambioPagina = function () {
            obtenerTodos();
        };

        $scope.ordenar = function (columna) {
            if ($scope.columna == columna) {
                $scope.asc = !$scope.asc;
            } else {
                $scope.columna = columna;
                $scope.asc = true;
            }
            $scope.pagActual = 1;
            obtenerTodos();
        };

        var obtenerTodos = function () {
            $scope.cargandoLst = true;
            http.post(urlObtenerTodos, {
                paginacion: {
                    pagActual: $scope.pagActual,
                    nroRegistros: $scope.nroRegistros
                },
                orden: {
                    columna: $scope.columna,
                    asc: $scope.asc
                }
            },
	            function (data) {
	                $scope.presupuestos = data.presupuestos;
	                $scope.totalRegistros = data.total;
	                $scope.cargandoLst = false;
	            },
	            function () {
	                $scope.cargandoLst = false;
	            });
        };

        $scope.selTodos = function () {
            $scope.colSel = [];
            angular.forEach($scope.presupuestos, function (v) {
                v.sel = $scope.chkTodos;
                if ($scope.chkTodos) {
                    $scope.colSel.push(v.idPresupuesto);
                }
            });
        };

        $scope.selItem = function (sel, id) {
            if (sel) {
                $scope.colSel.push(id);
            } else {
                $scope.colSel.splice($scope.colSel.indexOf(id), 1);
            }
        };

        $scope.nuevo = function () {
            $scope.opcion = 2;
            $scope.presupuesto = { items: [], idsItems: [] };
        };

        $scope.editar = function (presupuesto) {
            $scope.opcion = 3;
            $scope.procesandoReg = true;
            http.post(urlObtenerPorId, { idPresupuesto: presupuesto.idPresupuesto },
	            function (data) {
	                $scope.presupuesto = data.presupuesto;
	                $scope.presupuesto.fechaCreacion = undefined;
	                angular.forEach($scope.presupuesto.items, function (v) {
	                    v.cotizacion.fechaCreacion = undefined;
	                });
	                $scope.procesandoReg = false;
	            },
	            function () {
	                $scope.procesandoReg = false;
	            });
        };

        $scope.cancelar = function () {
            $scope.opcion = 1;
        };

        $scope.guardar = function (formGnrl, formItm) {
            if (formGnrl.$valid && formItm.$valid) {
                $scope.procesandoReg = true;
                http.post(($scope.opcion == 2) ? urlAgregar : urlActualizar, { presupuesto: $scope.presupuesto },
	                function (data) {
	                    $scope.procesandoReg = false;
	                    obtenerTodos();
	                    $scope.opcion = 1;
	                },
	                function () {
	                    $scope.procesandoReg = false;
	                });
            }
        };

        $scope.eliminar = function () {
            $scope.eliminadoReg = true;
            http.post(urlEliminar, { ids: $scope.colSel },
	            function (data) {
	                $('#eliminar').modal('hide');
	                $scope.eliminadoReg = false;
	                obtenerTodos();
	            },
	            function () {
	                $scope.eliminadoReg = false;
	            });
        };

        $scope.obtenerClientes = function () {
            $scope.cargandoBusCliente = true;
            http.post(urlObtenerClientes, {},
	            function (data) {
	                $scope.clientes = data.clientes;
	                $scope.cargandoBusCliente = false;
	            },
	            function () {
	                $scope.cargandoBusCliente = false;
	            });
        };

        $scope.asignarCliente = function (cliente) {
            $scope.presupuesto.cliente = { idCliente: cliente.idCliente, razonSocial: cliente.razonSocial };
            $('#busCliente').modal('hide');
        };

        $scope.obtenerVendedores = function () {
            $scope.cargandoBusVendedor = true;
            http.post(urlObtenerVendedores, {},
	            function (data) {
	                $scope.vendedores = data.vendedores;
	                $scope.cargandoBusVendedor = false;
	            },
	            function () {
	                $scope.cargandoBusVendedor = false;
	            });
        };

        $scope.asignarVendedor = function (vendedor) {
            $scope.presupuesto.vendedor = { idEmpleado: vendedor.idEmpleado, nombre: vendedor.nombre };
            $('#busVendedor').modal('hide');
        };

        $scope.obtenerNumeraciones = function () {
            $scope.cargandoBusNumeracion = true;
            http.post(urlObtenerNumeraciones, {},
	            function (data) {
	                $scope.numeraciones = data.numeraciones;
	                $scope.cargandoBusNumeracion = false;
	            },
	            function () {
	                $scope.cargandoBusNumeracion = false;
	            });
        };

        $scope.asignarNumeracion = function (numeracion) {
            $scope.presupuesto.numeracion = { idNumeracion: numeracion.idNumeracion, descripcion: numeracion.descripcion };
            $('#busNumeracion').modal('hide');
        };

        $scope.obtenerMonedas = function () {
            $scope.cargandoBusMoneda = true;
            http.post(urlObtenerMonedas, {},
	            function (data) {
	                $scope.monedas = data.monedas;
	                $scope.cargandoBusMoneda = false;
	            },
	            function () {
	                $scope.cargandoBusMoneda = false;
	            });
        };

        $scope.asignarMoneda = function (moneda) {
            $scope.presupuesto.moneda = { idMoneda: moneda.idMoneda, simbolo: moneda.simbolo };
            $('#busMoneda').modal('hide');
        };

        var getIdsExcluir = function () {
            var ids = [];
            angular.forEach($scope.presupuesto.items, function (v) {
                ids.push(v.cotizacion.idCotizacion);
            });
            return ids;
        };

        $scope.obtenerCotizaciones = function () {
            $scope.chkTodosCtz = false;
            $scope.cargandoBusCotizacion = true;
            http.post(urlObtenerCotizaciones, { idsExcluir: getIdsExcluir() },
	            function (data) {
	                $scope.cotizaciones = data.cotizaciones;
	                $scope.cargandoBusCotizacion = false;
	            },
	            function () {
	                $scope.cargandoBusCotizacion = false;
	            });
        };

        $scope.selTodosCtz = function () {
            $scope.CtzSel = [];
            angular.forEach($scope.cotizaciones, function (v) {
                v.sel = $scope.chkTodosCtz;
            });
        };

        $scope.agregarItems = function () {
            angular.forEach($scope.cotizaciones, function (v) {
                if (v.sel) {
                    $scope.presupuesto.items.push({
                        cotizacion: { idCotizacion: v.idCotizacion, numero: v.numero, descripcion: v.descripcion, fechaCreacionStr: v.fechaCreacionStr },
                        ttlCot: v.total,
                        recargo: 0,
                        total: v.total
                    });
                }
            });
            $scope.asignarTotal();
            $('#busCotizacion').modal('hide');
        };

        $scope.asignarTotal = function () {
            var valTOTAL = 0;
            angular.forEach($scope.presupuesto.items, function (v) {
                v.total = v.ttlCot + v.recargo;
                valTOTAL += v.total;
            });
            $scope.presupuesto.total = valTOTAL;
        };

        $scope.eliminarItem = function (index) {
            var item = $scope.presupuesto.items.splice(index, 1)[0];
            if (angular.isDefined(item.idPresupuestoItem)) {
                $scope.presupuesto.idsItems.push(item.idPresupuestoItem);
            }
            $scope.asignarTotal();
        };

        $scope.pdf = function (presupuesto) {
            $.fileDownload(urlDescargarPdf, { httpMethod: "POST", data: { r:11, i: 1 } })
                    .done(function () {
                        console.log('done');
                    })
                    .fail(function () {
                        console.log('fail');
                    });
        };

        // Init
        obtenerTodos();

    }]);

});