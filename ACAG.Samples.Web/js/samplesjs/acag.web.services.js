angular.module('myApp.services', []).factory("orderService", function($http) {
    var $services = {};
    $services.orderGetList = function(filter) {
        return $http.post('/Services/SampleConsulting.svc/list-orders/' + (new Date()).getTime(), filter);
    };
    $services.getDateNow = function() {
        return $http.get('/Services/SampleConsulting.svc/getDateNow/');
    };
    $services.updateOrder = function(order) {
        return $http.post('/Services/SampleConsulting.svc/updateOrder/' + order.order.OrderId, order);
    };
    $services.deletePositionOrder = function (removePosition) {
        return $http.post('/Services/SampleConsulting.svc/deletePositionOrder/', removePosition);
    };
    $services.addOrder = function (order) {
        return $http.post('/Services/SampleConsulting.svc/addOrder/', order);
    };
    $services.position = function(orderId) {
        return $http.get('/Services/SampleConsulting.svc/position/' + orderId + '/' + (new Date()).getTime());
    };
    $services.reOrder = function(order) {
        return $http.post('/Services/SampleConsulting.svc/reOrder/', order);
    };
    $services.getOrderById = function (orderId) {
        return $http.get('/Services/SampleConsulting.svc/orders/' + orderId + '/' + (new Date()).getTime());
    };
    $services.deleteOrder = function (orderId) {
        return $http.post('/Services/SampleConsulting.svc/deleteOrder/', orderId);
    };
    return $services;
});