'use strict';

define(['app'], function (app) {

    app.register.controller('venCotizacionController', ['$scope', 'http', function ($scope, http) {

        var URL_BASE = 'http://192.168.1.109/SGE_Aplicacion/';
        //var URL_BASE = 'http://localhost:52455/';

        // URLs
        var urlObtenerTodos = URL_BASE + 'Ventas/venCotizacion.aspx/ObtenerTodos',
	    	urlObtenerPorId = URL_BASE + 'Ventas/venCotizacion.aspx/ObtenerPorId',
	        urlAgregar = URL_BASE + 'Ventas/venCotizacion.aspx/Agregar',
	        urlActualizar = URL_BASE + 'Ventas/venCotizacion.aspx/Actualizar',
	        urlEliminar = URL_BASE + 'Ventas/venCotizacion.aspx/Eliminar',
	        urlObtenerSolicitudes = URL_BASE + 'Ventas/venSolCotizacion.aspx/ObtenerPendientes',
	        urlObtenerSolicitud = URL_BASE + 'Ventas/venSolCotizacion.aspx/ObtenerPorId',
	        urlObtenerClientes = URL_BASE + 'Ventas/venCliente.aspx/ObtenerActivos',
	        urlObtenerContactos = URL_BASE + 'Ventas/venCliente.aspx/ObtenerContactos',
	        urlObtenerNumeraciones = URL_BASE + 'Administracion/admNumeracion.aspx/ObtenerActivos',
            urlObtenerVendedores = URL_BASE + 'Administracion/admEmpleado.aspx/ObtenerVendedores',
            urlObtenerCotizadores = URL_BASE + 'Administracion/admEmpleado.aspx/ObtenerCotizadores',
            urlObtenerMonedas = URL_BASE + 'Administracion/admMoneda.aspx/ObtenerActivos',
            urlObtenerFormasPago = URL_BASE + 'Ventas/venFormaPago.aspx/ObtenerActivos',
            urlObtenerServicios = URL_BASE + 'Ventas/venServicio.aspx/ObtenerActivos',
            urlObtenerMateriales = URL_BASE + 'Inventarios/invMaterial.aspx/ObtenerActivos',
            urlObtenerMaquinas = URL_BASE + 'Ventas/venMaquina.aspx/ObtenerActivos',
            urlGenerarGraficoPrecorte = URL_BASE + 'Ventas/venCotizacion.aspx/GenerarGraficoPrecorte';

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
	                $scope.cotizaciones = data.cotizaciones;
	                $scope.totalRegistros = data.total;
	                $scope.cargandoLst = false;
	            },
	            function () {
	                $scope.cargandoLst = false;
	            });
        };

        $scope.selTodos = function () {
            $scope.colSel = [];
            angular.forEach($scope.cotizaciones, function (v) {
                v.sel = $scope.chkTodos;
                if ($scope.chkTodos) {
                    $scope.colSel.push(v.idCotizacion);
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

        $scope.editar = function (cotizacion) {
            $scope.opcion = 3;
            $scope.procesandoReg = true;
            http.post(urlObtenerPorId, { idCotizacion: cotizacion.idCotizacion },
	            function (data) {
	                $scope.cotizacion = data.cotizacion;
	                $scope.procesandoReg = false;
	            },
	            function () {
	                $scope.procesandoReg = false;
	            });
        };

        $scope.cancelar = function () {
            $scope.opcion = 1;
        };

        $scope.nuevoGrupo = function () {
            $scope.cotizacion.grupos.push({ titulo: 'SIN TÍTULO', items: [] });
        };

        $scope.nuevoItem = function (grupo) {
            grupo.items.push({ titulo: 'SIN TÍTULO' });
        };

        $scope.eliminarGrupo = function (index) {
            var grupo = $scope.cotizacion.grupos.splice(index, 1)[0];
            if (angular.isDefined(grupo.idPlantillaGrupo)) {
                $scope.cotizacion.idsGrupos.push(grupo.idPlantillaGrupo);
            }
        };

        $scope.eliminarItem = function (grupo, index) {
            var item = grupo.items.splice(index, 1)[0];
            if (angular.isDefined(item.idPlantillaItem)) {
                grupo.idsItems.push(item.idPlantillaItem);
            }
        };

        $scope.limpiarSRV = function (item) {
            if (!item.flagSRV) item.servicio = null;
        };

        $scope.limpiarMAT = function (item) {
            if (!item.flagMAT) item.material = null;
        };

        $scope.guardar = function (form) {
            if (form.$valid) {
                $scope.procesandoReg = true;
                $scope.cotizacion.fechaCreacion = undefined;
                http.post(($scope.opcion == 2) ? urlAgregar : urlActualizar, { cotizacion: $scope.cotizacion },
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

        $scope.obtenerSolicitudes = function () {
            $scope.cargandoBusSolicitud = true;
            http.post(urlObtenerSolicitudes, {},
	            function (data) {
	                $scope.solicitudes = data.solicitudes;
	                $scope.cargandoBusSolicitud = false;
	            },
	            function () {
	                $scope.cargandoBusSolicitud = false;
	            });
        };

        $scope.asignarSolicitud = function (solicitud) {
            $scope.opcion = 2;
            $scope.procesandoReg = true;
            http.post(urlObtenerSolicitud, { idSolCotizacion: solicitud.idSolCotizacion },
	            function (data) {
	                generarCotizacion(data.solicitud);
	                $scope.procesandoReg = false;
	                $('#busSolicitud').modal('hide');
	            },
	            function () {
	                $scope.procesandoReg = false;
	            });
        };

        var generarCotizacion = function (solicitud) {
            $scope.cotizacion = {
                numeracion: {},
                descripcion: solicitud.descripcion,
                cliente: {
                    idCliente: solicitud.cliente.idCliente,
                    razonSocial: solicitud.cliente.razonSocial
                },
                linea: {
                    idLinea: solicitud.linea.idLinea,
                    descripcion: solicitud.linea.descripcion
                },
                moneda: {
                    idMoneda: solicitud.moneda.idMoneda,
                    simbolo: solicitud.moneda.simbolo
                },
                vendedor: {
                    idEmpleado: solicitud.vendedor.idEmpleado,
                    nombre: solicitud.vendedor.nombre
                },
                cotizador: {},
                formaPago: {
                    idFormaPago: solicitud.formaPago.idFormaPago,
                    descripcion: solicitud.formaPago.descripcion
                },
                contacto: {
                    idClienteContacto: solicitud.contacto.idClienteContacto,
                    nombre: solicitud.contacto.nombre
                },
                observacion: solicitud.observacion,
                grupos: []
            };
            angular.forEach(solicitud.grupos, function (v) {
                var grupo = { titulo: v.titulo, cantidad: v.cantidad, items: [] };
                angular.forEach(v.items, function (vk) {
                    var item = {
                        titulo: vk.titulo,
                        flagMA: vk.flagMA,
                        flagMC: vk.flagMC,
                        flagTYR: vk.flagTYR,
                        flagGRF: vk.flagGRF,
                        flagMAT: vk.flagMAT,
                        flagSRV: vk.flagSRV,
                        flagFND: vk.flagFND,
                        valXMA: vk.valXMA,
                        valYMA: vk.valYMA,
                        valXMC: vk.valXMC,
                        valYMC: vk.valYMC,
                        valTC: vk.valTC,
                        valRT: vk.valRT,
                        valFND: vk.valFND,
                        observacion: vk.acabados,
                        maquina: angular.copy(vk.maquina)
                    };
                    if (item.flagSRV) item.servicio = angular.copy(vk.servicio);
                    if (item.flagMAT) item.material = angular.copy(vk.material);
                    grupo.items.push(item);
                });
                $scope.cotizacion.grupos.push(grupo);
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
            $scope.cotizacion.cliente = { idCliente: cliente.idCliente, razonSocial: cliente.razonSocial };
            $('#busCliente').modal('hide');
        };

        $scope.obtenerContactos = function () {
            $scope.cargandoBusContacto = true;
            http.post(urlObtenerContactos, { idCliente: $scope.cotizacion.cliente.idCliente },
	            function (data) {
	                $scope.contactos = data.contactos;
	                $scope.cargandoBusContacto = false;
	            },
	            function () {
	                $scope.cargandoBusContacto = false;
	            });
        };

        $scope.asignarContacto = function (contacto) {
            $scope.cotizacion.contacto = { idClienteContacto: contacto.idClienteContacto, nombre: contacto.nombre };
            $('#busContacto').modal('hide');
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
            $scope.cotizacion.numeracion = { idNumeracion: numeracion.idNumeracion, descripcion: numeracion.descripcion };
            $('#busNumeracion').modal('hide');
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
            $scope.cotizacion.vendedor = { idEmpleado: vendedor.idEmpleado, nombre: vendedor.nombre };
            $('#busVendedor').modal('hide');
        };

        $scope.obtenerCotizadores = function () {
            $scope.cargandoBusCotizador = true;
            http.post(urlObtenerCotizadores, {},
	            function (data) {
	                $scope.cotizadores = data.cotizadores;
	                $scope.cargandoBusCotizador = false;
	            },
	            function () {
	                $scope.cargandoBusCotizador = false;
	            });
        };

        $scope.asignarCotizador = function (cotizador) {
            $scope.cotizacion.cotizador = { idEmpleado: cotizador.idEmpleado, nombre: cotizador.nombre };
            $('#busCotizador').modal('hide');
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
            $scope.cotizacion.moneda = { idMoneda: moneda.idMoneda, simbolo: moneda.simbolo };
            $('#busMoneda').modal('hide');
        };

        $scope.obtenerFormasPago = function () {
            $scope.cargandoBusFormaPago = true;
            http.post(urlObtenerFormasPago, {},
	            function (data) {
	                $scope.formasPago = data.formasPago;
	                $scope.cargandoBusFormaPago = false;
	            },
	            function () {
	                $scope.cargandoBusFormaPago = false;
	            });
        };

        $scope.asignarFormaPago = function (formaPago) {
            $scope.cotizacion.formaPago = { idFormaPago: formaPago.idFormaPago, descripcion: formaPago.descripcion };
            $('#busFormaPago').modal('hide');
        };

        $scope.obtenerServicios = function (item) {
            $scope.itemActivo = item;
            $scope.cargandoBusServicio = true;
            http.post(urlObtenerServicios, {},
	            function (data) {
	                $scope.servicios = data.servicios;
	                $scope.cargandoBusServicio = false;
	            },
	            function () {
	                $scope.cargandoBusServicio = false;
	            });
        };

        $scope.asignarServicio = function (servicio) {
            $scope.itemActivo.servicio = { idServicio: servicio.idServicio, descripcion: servicio.descripcion };
            $('#busServicio').modal('hide');
        };

        $scope.obtenerMateriales = function (item) {
            $scope.itemActivo = item;
            $scope.cargandoBusMaterial = true;
            http.post(urlObtenerMateriales, {},
	            function (data) {
	                $scope.materiales = data.materiales;
	                $scope.cargandoBusMaterial = false;
	            },
	            function () {
	                $scope.cargandoBusMaterial = false;
	            });
        };

        $scope.asignarMaterial = function (material) {
            $scope.itemActivo.material = { idMaterial: material.idMaterial, descripcion: material.descripcion };
            $('#busMaterial').modal('hide');
        };

        $scope.obtenerMaquinas = function (item) {
            $scope.itemActivo = item;
            $scope.cargandoBusMaquina = true;
            http.post(urlObtenerMaquinas, {},
	            function (data) {
	                $scope.maquinas = data.maquinas;
	                $scope.cargandoBusMaquina = false;
	            },
	            function () {
	                $scope.cargandoBusMaquina = false;
	            });
        };

        $scope.asignarMaquina = function (maquina) {
            $scope.itemActivo.maquina = { idMaquina: maquina.idMaquina, descripcion: maquina.descripcion };
            $('#busMaquina').modal('hide');
        };

        $scope.asignarItem = function (item) {
            $scope.itemActivo = item;
        };

        $scope.generarGraficoPrecorte = function (girar) {
            $scope.itemActivo.flagGPR = girar;
            http.post(urlGenerarGraficoPrecorte, { item: $scope.itemActivo },
	            function (data) {
	                $scope.imgBase64 = 'data:image/jpeg;base64,' + data.imgBase64;
	            },
	            function () {
	                //$scope.cargandoBusMaquina = false;
	            });
        };

        // Init
        obtenerTodos();

    }]);

});