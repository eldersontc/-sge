require.config({
    urlArgs: 'v=1.0' + (new Date()).getTime(),
    waitSeconds: 0
});

require(
    [
        'app',
        'services/routeResolver',
        'factories/factories',
        'directives/directives'
    ],
    function () {
        angular.bootstrap(document, ['app', 'ui.sge.directives']);
    });