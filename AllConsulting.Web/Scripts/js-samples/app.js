angular.module("myApp", ['myApp.services', 'myApp.controllers', 'dx', 'ngRoute']).config([
    '$routeProvider', function($routeProvider) {
        $routeProvider
            .when('/',
            {
                templateUrl: '/partials/list-order.html',
                controller: 'orderController'
            })
            .when('/add',
            {
                templateUrl: '/partials/detail-order.html',
                controller: 'detailorder'
            })
            .when('/detail/:id',
            {
                templateUrl: '/partials/detail-order.html',
                controller: 'detailorder'
            })
            .otherwise({ redirectTo: '/' });
    }
]);
