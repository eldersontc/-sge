angular.module('ui.sge.directives', [])
.directive('ngText', function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {
            element.html(TEXTOS[attrs.ngText]);
        }
    }
})
.directive('ngDocument', function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {
            element.html((attrs.ngDocument) ? DOCUMENTOS[attrs.ngDocument] : 'NINGUNO');
        }
    }
})
.directive('selectDocument', function () {
    return {
        restrict: 'E',
        require: 'ngModel',
        scope: {
        	ngModel : '=',
        	ngRequired: '='
        },
        link: function (scope, element, attrs) {
            scope.documentos = [];
            scope.documentos.push({ id: null, nombre: TEXTOS.SELECCIONE });
	    	angular.forEach(DOCUMENTOS, function(v, k) {
			  scope.documentos.push({ id: parseInt(k), nombre: v });
			});
        },
        template: 	'<select  class="form-control"' + 
    						 'data-ng-model="ngModel"' +  
        					 'data-ng-options="documento.id as documento.nombre for documento in documentos" ' + 
        					 'data-ng-required="ngRequired"/>'
    }
})