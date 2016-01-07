app.controller('addOrderController', function addOrderController($scope, apiService, $location, $uibModal) {
    // Fields
    $scope.order = {
        CustomerNumber: '',
        OrderDate: '',
        DeliveryDate: '',
        TotalPrice: '',
        orderPositions: []
    };

    $scope.validationErrors = [];

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };
    $scope.orderDatePicker = {};
    $scope.deliveryDatePicker = {};

    // Methods
    $scope.addOrder = function () {
        apiService.post("/api/v1/orders/add", $scope.order, addOrderCompleted, addOrderFailure);
    };

    $scope.cancel = function () {
        console.info("Adding order has been cancelled.");
        $location.url('/orders');
    };

    $scope.openOrderDatePicker = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.orderDatePicker.opened = true;
    };

    $scope.openDeliveryDatePicker = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.deliveryDatePicker.opened = true;
    };

    function addOrderCompleted(response) {
        console.info(response);
        $location.url('/orders');
    };

    function addOrderFailure(response) {
        showValidationErrors(response);
    };

    //$scope.orderPosition = { PositionNumber: 3, Pieces: 'Pieces', Text: 'Text', Price: 3.5, Total: 10.50 };
    $scope.orderPosition = {};
    $scope.animationsEnabled = true;

    $scope.open = function (size) {

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            size: size,
            resolve: {
                orderPosition: function () {
                    return $scope.orderPosition;
                }
            }
        });

        modalInstance.result.then(function (orderPosition) {
            $scope.order.orderPositions.push(orderPosition);
        }, function () {
            //alert('Modal dismissed at: ' + new Date());
        });
    };

    $scope.toggleAnimation = function () {
        $scope.animationsEnabled = !$scope.animationsEnabled;
    };

    /* Utility Functions */
    function showValidationErrors(error) {
        $scope.validationErrors = [];
        if (error.data && angular.isObject(error.data)) {
            var jsonObj = error.data;

            for (var key in jsonObj.ModelState) {
                var errMessages = jsonObj.ModelState[key];
                for (var j = 0; j < errMessages.length; j++) {
                    console.debug(errMessages[j]);
                    $scope.validationErrors.push(errMessages[j]);
                }
            }
        } else {
            $scope.validationErrors.push('Could not add movie.');
        };
    }
});

app.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance, orderPosition) {

    $scope.PositionNumber = orderPosition.PositionNumber;
    $scope.Pieces = orderPosition.Pieces;
    $scope.Text = orderPosition.Text;
    $scope.Price = orderPosition.Price;
    $scope.Total = orderPosition.Total;

    $scope.ok = function () {
        var position = {
            PositionNumber: $scope.PositionNumber,
            Pieces: $scope.Pieces,
            Text: $scope.Text,
            Price: $scope.Price,
            Total: $scope.Total
        };

        $uibModalInstance.close(position);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});