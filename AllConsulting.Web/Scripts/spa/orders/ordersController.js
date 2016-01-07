app.controller('ordersController', function ordersController($scope, apiService) {

    $scope.isLoading = true;
    $scope.page = 0;
    $scope.pagesCount = 0;
    $scope.orders = [];

    $scope.search = search;
    $scope.clearSearch = clearSearch;
    $scope.range = range;
    $scope.pagePlus = pagePlus;

    $scope.search();

    function search(page) {
        page = page || 0;
        $scope.isLoading = true;

        var config = {
            params: {
                page: page,
                pageSize: 10,
                filter: $scope.filterOrders
            }
        }
        apiService.get('/api/v1/orders/', config, ordersLoadCompleted, ordersLoadFailed);
    };

    function clearSearch() {
        $scope.filterOrders = '';
        search();
    };

    function ordersLoadCompleted(result) {
        $scope.orders = result.data.Items;
        $scope.page = result.data.Page;
        $scope.pagesCount = result.data.TotalPages;
        $scope.isLoading = false;

        //alert(result.data.length + ' orders found.');
    }

    function ordersLoadFailed(response) {
        alert(response.data);
    }

    function range() {
        if (!$scope.pagesCount) { return []; }
        var step = 2;
        var doubleStep = step * 2;
        var start = Math.max(0, $scope.page - step);
        var end = start + 1 + doubleStep;
        if (end > $scope.pagesCount) { end = $scope.pagesCount; }

        var ret = [];
        for (var i = start; i != end; ++i) {
            ret.push(i);
        }

        return ret;
    };

    function pagePlus(count) {
        return +$scope.page + count;
    }
});

