var myApp = angular.module('myApp', ['dx']); 
var urlBase = '/Services/SampleConsulting.svc/';

myApp.controller("defaultCtrl", [
    "$scope", '$http', "$filter", function($scope, $http, $filter) {
        $scope.popupVisible = false;
        $scope.popupTitle = 'Add new order';
        $scope.checkOrderPosition = '';
        $scope.order = new OrderNew();
        $scope.dateNow = null;
        $scope.dateFuture = null;
        $scope.ListPositionOrderDto = [];
        $scope.listPositionOrderRemove = [];
        $scope.showAfterLoad = false;
        $scope.showLoadPanel = true;
        $scope.updateEditRowDataPosition = {};
        $scope.searchFullOrder = '';
        $scope.disabledBtnAdd = false;
        $scope.searchBefor = { options: {}, totalRows: 0, data: {} };
        $scope.formTitlePopupPosition = 'New Position';
        $scope.objectPosition = {};

        $http.get('/Services/SampleConsulting.svc/getDateNow/').success(function (d) {
            $scope.dateFuture = new Date(parseInt(d.replace("/Date(", "").replace(")/", ""), 10));
            $scope.dateNow = new Date(parseInt(d.replace("/Date(", "").replace(")/", ""), 10));
            $scope.dateFuture.setDate($scope.dateFuture.getDate() + 1);
        });

        $scope.formReset = function() {
            $scope.popupVisible = true;
            $scope.order = new OrderNew();
            $scope.order.OrderDate = $scope.dateNow;
            $scope.ListPositionOrderDto = new Array();
            $scope.listPositionOrderRemove = [];
            var txtCustomerNumberReset = $("#txtCustomerName").dxValidator('instance');
            if (txtCustomerNumberReset != undefined) txtCustomerNumberReset.reset();
            var txtDeliveryDateReset = $("#txtDeliveryDate").dxValidator('instance');
            if (txtDeliveryDateReset != undefined) txtDeliveryDateReset.reset();
            var gridPosition = $('#gridPosition').dxDataGrid('instance');
            if (gridPosition != undefined) {
                gridPosition.refresh();
                $("#gridPosition table.dx-datagrid-table tbody tr.dx-data-row").remove();
            }
        };
        $scope.refeshDataGrid = function() {
            $scope.searchBefor = {};
            $('#gridOrder').dxDataGrid('instance').refresh();
        };
        var customStore = new DevExpress.data.CustomStore({
            load: function(loadOptions) {
                var d = $.Deferred();
                var oFilter = {};
                loadOptions.searchValue = $scope.searchFullOrder;
                if (!angular.equals($scope.searchBefor.options, loadOptions)) {
                    oFilter.sort = '';
                    // sort column
                    if (loadOptions.sort != null)
                        oFilter.sort = loadOptions.sort[0].selector + ' ' + (loadOptions.sort[0].desc ? 'desc' : 'asc');
                    // paging
                    oFilter.skip = loadOptions.skip;
                    oFilter.take = loadOptions.take;
                    oFilter.searchFullOrder = $scope.searchFullOrder;
                    $http.post('/Services/SampleConsulting.svc/list-orders/' + (new Date()).getTime(), oFilter).then(function(response) {
                        var data = response.data.ListOrder;
                        for (i = 0; i < data.length; i++) {
                            data[i].Index = (i + 1);
                            data[i].DeliveryDate = data[i].DeliveryDate != null ? new Date(parseInt(data[i].DeliveryDate.replace("/Date(", "").replace(")/", ""), 10)) : null;
                            data[i].OrderDate = data[i].OrderDate != null ? new Date(parseInt(data[i].OrderDate.replace("/Date(", "").replace(")/", ""), 10)) : null;
                        }
                        $scope.searchBefor.data = data;
                        $scope.searchBefor.options = loadOptions;
                        $scope.searchBefor.totalRows = response.data.TotalRows;
                        $scope.searchBefor.options.searchValue = $scope.searchFullOrder;
                        d.resolve(data, { totalCount: response.data.TotalRows });
                    }, function(error) {});
                } else {
                    d.resolve($scope.searchBefor.data, { totalCount: $scope.searchBefor.totalRows });
                }
                return d.promise();
            },
            onLoaded: function(options) {
                $scope.showAfterLoad = true;
                $scope.showLoadPanel = false;
            }
        });
        var gridDataSourceConfiguration = { store: customStore };

        $scope.optionOrder = {
            showColumnLines: true,
            showRowLines: true,
            rowAlternationEnabled: true,
            paging: { pageSize: 20 },
            pager: { showPageSizeSelector: true, allowedPageSizes: [20, 30, 40], showInfo: true },
            dataSource: gridDataSourceConfiguration,
            filterRow: { visible: true, showOperationChooser: true },
            columns: [
                { dataField: 'CustomerNumber', dataType: 'string', caption: 'Customer Number', filterOperations: ["contains", "="] },
                { dataField: 'DeliveryDate', dataType: 'date', alignment: 'right', format: "dd/MM/yyyy", title: 'Delivery Date', filterOperations: ["=", "<", ">"] },
                { dataField: 'OrderDate', dataType: 'date', alignment: 'right', format: "dd/MM/yyyy", title: 'Order Date', filterOperations: ["=", "<", ">"] },
                {
                    dataField: 'TotalPrice',
                    dataType: 'number',
                    cellTemplate: function(cell, option) { $(cell).append($filter("currency")(option.data.TotalPrice)); },
                    title: 'Total Price',
                    format: "currency",
                    alignment: 'right',
                    filterOperations: ["=", "<", ">"]
                },
                {
                    alignment: 'center',
                    caption: "Actions",
                    width: '100px',
                    cellTemplate: function(cell, options) {
                        $('<span title="Edit"/>').addClass('btn btn-default btn-xs mr-right10')
                            .on("click", $.proxy($scope.eventEditOrder, this, options)).html('<i class="glyphicon glyphicon-pencil"></i>').appendTo(cell);
                        $('<span title="Delete"/>').addClass('btn btn-default btn-xs')
                            .on("click", $.proxy($scope.eventDeleteOrder, this, options)).html('<i class="glyphicon glyphicon-remove"></i>').appendTo(cell);
                    }
                }
            ],
            remoteOperations: {
                // support for remote server
                grouping: false,
                filtering: false,
                sorting: true,
                paging: true
            },
            masterDetail: {
                enabled: true,
                template: function(container, info) {
                    $scope.showLoadPanel = true;
                    $http.get('/Services/SampleConsulting.svc/position/' + info.data.OrderId + '/' + (new Date()).getTime()).then(function(response) {
                        var data = response.data;
                        $("<div class='grid-order'/>")
                            .dxDataGrid({
                                dataSource: data,
                                columns: [
                                    { dataField: 'Text', dataType: 'string', caption: 'Text' },
                                    { dataField: 'Pieces', dataType: 'string', caption: 'Pieces' },
                                    { dataField: 'PositionNumber', dataType: 'number', caption: 'Number' },
                                    { dataField: 'Price', dataType: 'number', caption: 'Price', cellTemplate: function(cell, option) { $(cell).append($filter("currency")(option.data.Price)); } },
                                    { dataField: 'Total', dataType: 'number', caption: 'Total', cellTemplate: function(cell, option) { $(cell).append($filter("currency")(option.data.Total)); } }
                                ]
                            }).appendTo(container);
                        var panelDev = $('<div class="panel-detail"/>');
                        $('<button/>').dxButton({
                                type: 'success',
                                text: 'ReOrder',
                                onClick: function(x) {
                                    var result = DevExpress.ui.dialog.confirm("Are you sure you want to reorder this record?");
                                    $(".dx-dialog .dx-overlay-content .dx-popup-title").hide();
                                    result.done(function(ok) {
                                        if (ok) {
                                            var dgPosition = $('#gridPosition').dxDataGrid("instance");
                                            if (dgPosition != undefined) dgPosition.clearFilter();
                                            var order = info.data;
                                            order.ListPositionOrderDto = data;
                                            order.DeliveryDate = order.DeliveryDate != null ? formatDateToJson(order.DeliveryDate) : null;
                                            order.OrderDate = order.OrderDate != null ? formatDateToJson(order.OrderDate) : null;
                                            $httpPost('/Services/SampleConsulting.svc/reOrder/', { order: order }, function(response) {
                                                if (response != null) {
                                                    response.DeliveryDate = response.DeliveryDate != null ? formatDateJsonToDate(response.DeliveryDate) : null;
                                                    response.OrderDate = response.OrderDate != null ? formatDateJsonToDate(response.OrderDate) : null;
                                                    // validate DeliveryDate
                                                    $scope.order = response;
                                                    $scope.listPositionOrderRemove = [];
                                                    $scope.ListPositionOrderDto = $.extend([], response.ListPositionOrderDto);
                                                    DevExpress.ui.notify('Your reorder success!', "success", 3000);

                                                    $scope.popupTitle = "Edit Reorder";
                                                    $scope.popupVisible = true;
                                                    $scope.refeshDataGrid();
                                                }
                                            });
                                        }
                                    });
                                }
                            })
                            .appendTo(panelDev);
                        panelDev.appendTo(container);
                        $scope.showLoadPanel = false;
                    }, function(error) {});
                }
            },
            onRowClick: function(e) {
                if (e.isExpanded) {
                    e.component.collapseAll(-1);
                    e.component.expandRow(e.key);
                }
            }
        };

        $scope.eventSearchFullOrder = function(e) {
            $scope.searchFullOrder = e.value;
            $scope.refeshDataGrid();
        };
        $scope.eventEditOrder = function(data) {
            var dgPosition = $('#gridPosition').dxDataGrid("instance");
            if (dgPosition != undefined) dgPosition.clearFilter();
            $scope.formReset();
            $scope.showLoadPanel = true;
            $http.get(urlBase + 'orders/' + data.data.OrderId + '/' + (new Date()).getTime()).then(function(response) {
                var d = response.data;
                // convert DateJson to Date
                d.DeliveryDate = d.DeliveryDate != null ? formatDateJsonToDate(d.DeliveryDate) : null;
                d.OrderDate = d.OrderDate != null ? formatDateJsonToDate(d.OrderDate) : null;
                $scope.order = d;
                $scope.ListPositionOrderDto = [];
                $scope.listPositionOrderRemove = [];
                $scope.ListPositionOrderDto = $.extend($scope.ListPositionOrderDto, d.ListPositionOrderDto);
                $scope.popupBtnReorderVisible = true;

                $scope.popupTitle = 'Edit order';
                $scope.showLoadPanel = false;
                $scope.popupVisible = true;
                $scope.refeshDataGrid();
            }, function(error) {});
        };
        $scope.eventDeleteOrder = function(data) {
            var result = DevExpress.ui.dialog.confirm("Are you sure you want to delete this order?");
            $(".dx-dialog .dx-overlay-content .dx-popup-title").hide();
            result.done(function(ok) {
                if (ok) {
                    $httpPost('/Services/SampleConsulting.svc/deleteOrder/', { orderId: data.data.OrderId }, function(d) {
                        DevExpress.ui.notify('You deleted, success!', "success", 3000);
                        $scope.refeshDataGrid();
                    });
                }
            });
        };
        $scope.optionPopup = {
            resizeEnabled: true,
            maxWidth: '750px',
            animation: { show: { type: "pop" } },
            visible: false,
            dragEnabled: true,
            contentTemplate: "form",
            bindingOptions: { visible: "popupVisible", title: 'popupTitle' }
        };
        $scope.formBtnAddNewOption = function(e) {
            var result = e.validationGroup.validate();
            if (result.isValid) {
                if ($scope.order.ListPositionOrderDto.length < 1) {
                    $("#summary").append('<div style="color:#ff0000;">Order must have at least one OrderPosition.</div>');
                    return;
                }
                $scope.disabledBtnAdd = true;
                var orderCopy = $.extend({}, $scope.order);
                orderCopy.DeliveryDate = orderCopy.DeliveryDate != null ? formatDateToJson(orderCopy.DeliveryDate) : null;
                orderCopy.OrderDate = orderCopy.OrderDate != null ? formatDateToJson(orderCopy.OrderDate) : null;
                if (orderCopy.OrderId > 0) {
                    $httpPost('/Services/SampleConsulting.svc/updateOrder/' + orderCopy.OrderId, { order: orderCopy }, function(data) {
                        $httpPost('/Services/SampleConsulting.svc/deletePositionOrder/', { listOrderId: $scope.listPositionOrderRemove }, function(response) {
                            $scope.popupVisible = false;
                            $scope.disabledBtnAdd = false;
                            $scope.refeshDataGrid();
                        });
                    });
                } else {
                    $httpPost('/Services/SampleConsulting.svc/addOrder/', { order: orderCopy }, function(data) {
                        if (data > 0) {
                            $scope.disabledBtnAdd = false;
                            $scope.popupVisible = false;
                            $scope.refeshDataGrid();
                        }
                    });
                }
            }
        };
        $scope.formBtnCancelOption = function(e) {
            $scope.popupVisible = false;
            $('#gridOrder').dxDataGrid('instance').refresh();
        };
        $scope.optionButonAdd = {
            text: 'Add Order',
            type: 'success',
            onClick: function() {
                var dgPosition = $('#gridPosition').dxDataGrid("instance");
                if (dgPosition != undefined) dgPosition.clearFilter();
                $scope.popupTitle = "Add new order";
                $scope.formReset();
                $scope.disabledBtnAdd = false;
            }
        };
        // form order
        $scope.formOrder = {
            txtCustomerNumber: { showClearButton: true, placeholder: 'Customer Number...', bindingOptions: { value: 'order.CustomerNumber' } },
            txtDeliveryDate: { showClearButton: true, invalidDateMessage: 'Value must be a date', formatString: 'dd/MM/yyyy', placeholder: 'dd/MM/yyyy', bindingOptions: { value: 'order.DeliveryDate', min: 'dateFuture' } },
            txtOrderDate: { formatString: 'dd/MM/yyyy', placeholder: 'dd/MM/yyyy', disabled: true, bindingOptions: { value: 'order.OrderDate' }, tabIndex: -1 }
        }; //onValueChanged 
        //************************************************ validation  ********************************************
        $scope.validate1 = { validationRules: [{ type: "required", message: 'Customer Number not empty.' }] };
        $scope.validate2 = {
            validationRules: [
                {
                    type: "compare",
                    comparisonTarget: function() {
                        if ($scope.order.DeliveryDate == null) return -1;
                        return $scope.dateNow;
                    },
                    reevaluate: true,
                    message: 'DeliveryDate of order must be in the future.',
                    comparisonType: '>'
                }
            ]
        };
        //************************************************  form add, edit position  ********************************************
        $scope.formPosition = function(e) {
            $scope.objectPosition = new PositionOrder();
            var validGroup = $("#formPosition").dxValidationGroup('instance'); //.reset();
            if (validGroup != null) validGroup.reset();
            $scope.formTitlePopupPosition = "New Position";
            $("#popupPosition").dxPopup('instance').show();
             
        };
        $scope.formPositionEventUpdate = function(e) {
            var result = e.validationGroup.validate();
            if (result.isValid) {
                if (angular.equals($scope.formTitlePopupPosition, 'New Position')) {
                    var position = new Object();
                    position.PositionOrderId = -1;
                    position.PositionNumber = $scope.objectPosition.PositionNumber;
                    position.Text = $scope.objectPosition.Text;
                    position.Pieces = $scope.objectPosition.Pieces;
                    position.Price = $scope.objectPosition.Price;
                    position.Total = position.PositionNumber * position.Price;
                    $scope.order.ListPositionOrderDto.push(position);
                    DevExpress.ui.notify('Add position successful and continue add...', "success", 4000);
                    // refresh form
                    $scope.objectPosition.Text = '';
                    $scope.objectPosition.Pieces = '';
                    $scope.objectPosition.Price = 0;
                    $scope.objectPosition.PositionNumber = 1;
                    var validGroup = $("#formPosition").dxValidationGroup('instance'); //.reset();
                    if (validGroup != null) validGroup.reset();
                } else {
                    var index = $scope.objectPosition.PositionOrderId;
                    var positionOrderId = $scope.order.ListPositionOrderDto[index].PositionOrderId;
                    $scope.order.ListPositionOrderDto[index] = angular.extend({}, $scope.objectPosition);
                    $scope.order.ListPositionOrderDto[index].PositionOrderId = positionOrderId;

                    $("#popupPosition").dxPopup('instance').hide();
                }

                $scope.ListPositionOrderDto = angular.extend([], $scope.order.ListPositionOrderDto);
            }
        };
        $scope.formPositionEventClose = function(e) {
            var result = DevExpress.ui.dialog.confirm("Are you want to close?");
            $(".dx-dialog .dx-overlay-content .dx-popup-title").hide();
            result.done(function(ok) {
                if (ok) {
                    $("#popupPosition").dxPopup('instance').hide();
                }
            });
        };
        $scope.formPositionEventExit = function(e) {
            $("#gridPosition").dxDataGrid('instance').refresh();
        };
        // data grid position
        $scope.dataGridPosition = {
            showColumnLines: true,
            showRowLines: true,
            rowAlternationEnabled: true,
            searchPanel: { visible: true },
            bindingOptions: { dataSource: 'ListPositionOrderDto' },
            paging: { pageSize: 20 },
            pager: { showPageSizeSelector: false, allowedPageSizes: [10, 20, 30], showInfo: true },
            columns: [
                { dataField: 'Text', dataType: 'string', caption: 'Text', validationRules: [{ type: "required", message: 'Not empty' }] },
                { dataField: 'Pieces', dataType: 'string', caption: 'Pieces', celltemplate: function(cell, options) { $(cell).attr('tabindex', -1); } },
                { dataField: 'PositionNumber', dataType: 'number', caption: 'Number', validationRules: [{ type: "required", message: 'Not empty' }, { type: 'range', min: 1 }] },
                {
                    dataField: 'Price',
                    dataType: 'number',
                    alignment: 'right',
                    caption: 'Price',
                    format: "currency",
                    cellTemplate: function(cell, option) { $(cell).append($filter("currency")(option.data.Price)); },
                    validationRules: [{ type: "required", message: 'Not empty' }, { type: 'range', min: 0, message: 'Price greater than or equal 0.' }]
                },
                {
                    alignment: 'center',
                    allowEditing: false,
                    caption: 'Actions',
                    width: '100px',
                    cellTemplate: function(cell, options) {
                        $('<span title="Edit"/>').addClass('btn btn-default btn-xs mr-right10').on('click', function (e) {
                            options.component.byKey(options.key).done(function (o) {
                                var index = $scope.order.ListPositionOrderDto.indexOf(o);
                                $scope.objectPosition = angular.extend({}, $scope.order.ListPositionOrderDto[index]);
                                $scope.objectPosition.PositionOrderId = index;
                                $scope.formTitlePopupPosition = "Edit Position";//" - " + $scope.objectPosition.Text;
                                $("#popupPosition").dxPopup('instance').show();
                            });
                        }).html('<i class="glyphicon glyphicon-pencil"></i>').appendTo(cell);
                        $('<span title="Delete"/>').addClass('btn btn-default btn-xs')
                            .on('click', function(e) {
                                var result = DevExpress.ui.dialog.confirm("Are you sure you want to delete this record?");
                                $(".dx-dialog .dx-overlay-content .dx-popup-title").hide();
                                result.done(function(ok) {
                                    if (ok) {
                                        var dataGrid = $("#gridPosition").dxDataGrid('instance'); 
                                        dataGrid.byKey(options.key).done(function (o) {
                                            if (o.PositionOrderId > 0) {
                                                $scope.listPositionOrderRemove.push(o.PositionOrderId);
                                                $scope.order.ListPositionOrderDto.splice($scope.order.ListPositionOrderDto.indexOf(o), 1);
                                                $scope.ListPositionOrderDto = angular.extend([], $scope.order.ListPositionOrderDto);
                                            } else {
                                                $scope.order.ListPositionOrderDto.splice($scope.order.ListPositionOrderDto.indexOf(o), 1);
                                                $scope.ListPositionOrderDto = angular.extend([], $scope.order.ListPositionOrderDto);
                                            }
                                            dataGrid.clearFilter();
                                            dataGrid.refresh();
                                        });
                                    }
                                });
                            }).html('<i class="glyphicon glyphicon-remove"></i>').appendTo(cell);
                    }
                }
            ]
        };
    }
]);

function PositionOrder() { 
    this.PositionOrderId = -1;
    this.OrderId = 0;
    this.PositionNumber = null;
    this.Pieces = '';
    this.Text = '';
    this.Price = null;
    this.Total = this.PositionNumber * this.Price;
}

function OrderNew() {
    this.OrderId = -1;
    this.CustomerNumber = '';
    this.DeliveryDate = null;
    this.TotalPrice = 0;
    this.OrderDate = new Date();
    this.ListPositionOrderDto = [];
}
