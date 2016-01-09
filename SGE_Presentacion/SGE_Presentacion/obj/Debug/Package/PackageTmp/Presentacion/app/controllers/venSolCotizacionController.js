'use strict';

define(['app'], function (app) {

    app.register.controller('venSolCotizacionController', ['$scope', 'http', function ($scope, http) {

        var URL_BASE = 'http://localhost:52455/';

        // URLs
        var urlObtenerTodos = URL_BASE + 'Ventas/venSolCotizacion.aspx/ObtenerTodos',
	    	urlObtenerPorId = URL_BASE + 'Ventas/venSolCotizacion.aspx/ObtenerPorId',
	        urlAgregar = URL_BASE + 'Ventas/venSolCotizacion.aspx/Agregar',
	        urlActualizar = URL_BASE + 'Ventas/venSolCotizacion.aspx/Actualizar',
	        urlEliminar = URL_BASE + 'Ventas/venSolCotizacion.aspx/Eliminar',
	        urlObtenerPlantillas = URL_BASE + 'Ventas/venPlantilla.aspx/ObtenerActivos',
	        urlObtenerPlantilla = URL_BASE + 'Ventas/venPlantilla.aspx/ObtenerPorId',
	        urlObtenerClientes = URL_BASE + 'Ventas/venCliente.aspx/ObtenerActivos',
	        urlObtenerContactos = URL_BASE + 'Ventas/venCliente.aspx/ObtenerContactos',
	        urlObtenerNumeraciones = URL_BASE + 'Administracion/admNumeracion.aspx/ObtenerActivos',
            urlObtenerVendedores = URL_BASE + 'Administracion/admEmpleado.aspx/ObtenerVendedores',
            urlObtenerMonedas = URL_BASE + 'Administracion/admMoneda.aspx/ObtenerActivos',
            urlObtenerFormasPago = URL_BASE + 'Ventas/venFormaPago.aspx/ObtenerActivos',
            urlObtenerServicios = URL_BASE + 'Ventas/venServicio.aspx/ObtenerActivos',
            urlObtenerMateriales = URL_BASE + 'Inventarios/invMaterial.aspx/ObtenerActivos',
            urlObtenerMaquinas = URL_BASE + 'Ventas/venMaquina.aspx/ObtenerActivos';

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
	                $scope.solicitudes = data.solicitudes;
	                $scope.totalRegistros = data.total;
	                $scope.cargandoLst = false;
	            },
	            function () {
	                $scope.cargandoLst = false;
	            });
        };

        $scope.selTodos = function () {
            $scope.colSel = [];
            angular.forEach($scope.solicitudes, function (v) {
                v.sel = $scope.chkTodos;
                if ($scope.chkTodos) {
                    $scope.colSel.push(v.idSolCotizacion);
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

        $scope.editar = function (solicitud) {
            $scope.opcion = 3;
            $scope.procesandoReg = true;
            http.post(urlObtenerPorId, { idSolCotizacion: solicitud.idSolCotizacion },
	            function (data) {
	                $scope.solicitud = data.solicitud;
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
            $scope.solicitud.grupos.push({ titulo: 'SIN TÍTULO', items: [] });
        };

        $scope.nuevoItem = function (grupo) {
            grupo.items.push({ titulo: 'SIN TÍTULO' });
        };

        $scope.eliminarGrupo = function (index) {
            var grupo = $scope.solicitud.grupos.splice(index, 1)[0];
            if (angular.isDefined(grupo.idPlantillaGrupo)) {
                $scope.solicitud.idsGrupos.push(grupo.idPlantillaGrupo);
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
                $scope.solicitud.fechaCreacion = undefined;
                http.post(($scope.opcion == 2) ? urlAgregar : urlActualizar, { solicitud: $scope.solicitud },
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

        $scope.obtenerPlantillas = function () {
            $scope.cargandoBusPlantilla = true;
            http.post(urlObtenerPlantillas, {},
	            function (data) {
	                $scope.plantillas = data.plantillas;
	                $scope.cargandoBusPlantilla = false;
	            },
	            function () {
	                $scope.cargandoBusPlantilla = false;
	            });
        };

        $scope.asignarPlantilla = function (plantilla) {
            $scope.opcion = 2;
            $scope.procesandoReg = true;
            http.post(urlObtenerPlantilla, { idPlantilla: plantilla.idPlantilla },
	            function (data) {
	                generarSolicitud(data.plantilla);
	                $scope.procesandoReg = false;
	                $('#busPlantilla').modal('hide');
	            },
	            function () {
	                $scope.procesandoReg = false;
	            });
        };

        var generarSolicitud = function (plantilla) {
            $scope.solicitud = {
                numeracion: {},
                descripcion: plantilla.descripcion,
                cliente: {},
                linea: {
                    idLinea: plantilla.linea.idLinea,
                    descripcion: plantilla.linea.descripcion
                },
                moneda: {},
                vendedor: {},
                formaPago: {},
                contacto: {},
                grupos: []
            };
            angular.forEach(plantilla.grupos, function (v) {
                var grupo = { titulo: v.titulo, items: [] };
                angular.forEach(v.items, function (vk) {
                    var item = {
                        titulo: vk.titulo,
                        flagMA: vk.flagMA,
                        flagMC: vk.flagMC,
                        flagTYR: vk.flagTYR,
                        flagGRF: vk.flagGRF,
                        flagMAT: vk.flagMAT,
                        flagSRV: vk.flagSRV,
                        flagFND: vk.flagFND
                    };
                    if (item.flagSRV) item.servicio = angular.copy(vk.servicio);
                    if (item.flagMAT) item.material = angular.copy(vk.material);
                    grupo.items.push(item);
                });
                $scope.solicitud.grupos.push(grupo);
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
            $scope.solicitud.cliente = { idCliente: cliente.idCliente, razonSocial: cliente.razonSocial };
            $('#busCliente').modal('hide');
        };

        $scope.obtenerContactos = function () {
            $scope.cargandoBusContacto = true;
            http.post(urlObtenerContactos, { idCliente: $scope.solicitud.cliente.idCliente },
	            function (data) {
	                $scope.contactos = data.contactos;
	                $scope.cargandoBusContacto = false;
	            },
	            function () {
	                $scope.cargandoBusContacto = false;
	            });
        };

        $scope.asignarContacto = function (contacto) {
            $scope.solicitud.contacto = { idClienteContacto: contacto.idClienteContacto, nombre: contacto.nombre };
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
            $scope.solicitud.numeracion = { idNumeracion: numeracion.idNumeracion, descripcion: numeracion.descripcion };
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
            $scope.solicitud.vendedor = { idEmpleado: vendedor.idEmpleado, nombre: vendedor.nombre };
            $('#busVendedor').modal('hide');
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
            $scope.solicitud.moneda = { idMoneda: moneda.idMoneda, simbolo: moneda.simbolo };
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
            $scope.solicitud.formaPago = { idFormaPago: formaPago.idFormaPago, descripcion: formaPago.descripcion };
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

        // Init
        obtenerTodos();

    }]);

});