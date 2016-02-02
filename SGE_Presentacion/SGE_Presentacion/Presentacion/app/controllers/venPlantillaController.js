'use strict';

define(['app'], function (app) {

    app.register.controller('venPlantillaController', ['$scope', 'http', function ($scope, http) {

        var URL_BASE = 'http://localhost/SGE_Aplicacion/';

    	// URLs
	    var urlObtenerTodos = URL_BASE + 'Ventas/venPlantilla.aspx/ObtenerTodos',
	    	urlObtenerPorId = URL_BASE + 'Ventas/venPlantilla.aspx/ObtenerPorId',
	        urlAgregar = URL_BASE + 'Ventas/venPlantilla.aspx/Agregar',
	        urlActualizar = URL_BASE + 'Ventas/venPlantilla.aspx/Actualizar',
	        urlEliminar = URL_BASE + 'Ventas/venPlantilla.aspx/Eliminar',
	        urlObtenerLineas = URL_BASE + 'Ventas/venLinea.aspx/ObtenerActivos',
	        urlObtenerServicios = URL_BASE + 'Ventas/venServicio.aspx/ObtenerActivos',
	        urlObtenerMateriales = URL_BASE + 'Inventarios/invMaterial.aspx/ObtenerActivos';

        // Personalizadas
	    $scope.opcion = 1;
	    $scope.chkTodos = false;
	    $scope.colSel = [];
	    $scope.tab = 0;

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
	                $scope.plantillas = data.plantillas;
	                $scope.totalRegistros = data.total;
	                $scope.cargandoLst = false;
	            }, 
	            function () {
	            	$scope.cargandoLst = false;
	            });
	    };
	    
	    $scope.selTodos = function() {
	    	$scope.colSel = [];
	    	angular.forEach($scope.plantillas, function (v) {
	    		v.sel = $scope.chkTodos;
	    		if ($scope.chkTodos) {
	    			$scope.colSel.push(v.idPlantilla);
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
		
	    $scope.clickTab = function (tab) {
	        $scope.tab = tab;
	    };

	    $scope.nuevo = function () {
	    	$scope.opcion = 2;
	    	$scope.plantilla = { linea: {}, items: [{ titulo: 'SIN TÍTULO' }] };
	    	$scope.tab = 0;
	    };

	    $scope.editar = function (plantilla) {
			$scope.opcion = 3;
			$scope.procesandoReg = true;
			http.post(urlObtenerPorId, { idPlantilla: plantilla.idPlantilla }, 
	            function (data) {
	                $scope.plantilla = data.plantilla;
	                $scope.procesandoReg = false;
	                $scope.tab = 0;
	            }, 
	            function () {
	                $scope.procesandoReg = false;
	            });
	    };

	    $scope.cancelar = function () {
	    	$scope.opcion = 1;
	    };

	    $scope.nuevoItem = function () {
	        $scope.plantilla.items.push({ titulo: 'SIN TÍTULO' });
	        $scope.tab = $scope.plantilla.items.length - 1;
	    };
        
	    $scope.eliminarItem = function () {
	    	var item = $scope.plantilla.items.splice($scope.tab, 1)[0];
	    	if (angular.isDefined(item.idPlantillaItem)) {
	    		$scope.plantilla.idsItems.push(item.idPlantillaItem);
	    	}
	    	$scope.tab = $scope.tab - 1;
	    };

	    $scope.limpiarMAT = function (item) {
	        if (!item.flagMAT) item.material = null;
	    };

	    $scope.guardar = function (form) {
	    	if (form.$valid) {
	    	    $scope.procesandoReg = true;
	            http.post(($scope.opcion == 2) ? urlAgregar : urlActualizar, { plantilla: $scope.plantilla }, 
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

	    $scope.obtenerLineas = function () {
	        $scope.cargandoBusLinea = true;
	        http.post(urlObtenerLineas, { },
	            function (data) {
	                $scope.lineas = data.lineas;
	                $scope.cargandoBusLinea = false;
	            },
	            function () {
	                $scope.cargandoBusLinea = false;
	            });
	    };

	    $scope.asignarLinea = function (linea) {
	        $scope.plantilla.linea = { idLinea: linea.idLinea, descripcion: linea.descripcion };
	        $('#busLinea').modal('hide');
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

		// Init
	    obtenerTodos();

    }]);
    
});