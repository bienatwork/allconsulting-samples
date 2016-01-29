angular.module("myApp", ['myApp.services', 'myApp.controllers', 'dx', 'ngRoute']).config([
    '$routeProvider', function($routeProvider) {
        $routeProvider
            .when('/',
            {
                templateUrl: '/templates/acag-web-list-order.html',
                controller: 'orderController'
            })
            .when('/add',
            {
                templateUrl: '/templates/acag-web-detail-order.html',
                controller: 'detailorder'
            })
            .when('/detail/:id',
            {
                templateUrl: '/templates/acag-web-detail-order.html',
                controller: 'detailorder'
            })
            .otherwise({ redirectTo: '/' });
    }
]);
