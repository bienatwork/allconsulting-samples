var app = angular.module('ordersManager', ['ngRoute', 'ui.bootstrap']);

app.config(function ($routeProvider, $locationProvider) {

    $routeProvider
            .when('/', {
                templateUrl: '/partials/orders/list.html',
                controller: 'ordersController'
            })
            .when('/orders/add', {
                templateUrl: '/partials/orders/add.html',
                controller: 'addOrderController'
            })
            .when('/orderpositions/:orderId', {
                templateUrl: '/partials/orderpositions/index.html',
                controller: 'OrderPositionsController'
            })
            .otherwise({ redirectTo: '/' });

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
});