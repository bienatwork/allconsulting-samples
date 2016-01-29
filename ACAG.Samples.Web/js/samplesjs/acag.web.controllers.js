angular.module('myApp.controllers', []).
    controller('orderController', function($scope, $filter, orderService) {
        document.title = "All Consulting - Samples: Orders Management";
        $scope.showAfterLoad = false;
        $scope.showLoadPanel = true;
        $scope.searchBefor = { options: {}, totalRows: 0, data: {} };
        $scope.searchFullOrder = '';

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
                    orderService.orderGetList(oFilter).success(function(response) {
                        var data = response.ListOrder;
                        for (i = 0; i < data.length; i++) {
                            data[i].Index = (i + 1);
                            data[i].DeliveryDate = data[i].DeliveryDate != null ? new Date(parseInt(data[i].DeliveryDate.replace("/Date(", "").replace(")/", ""), 10)) : null;
                            data[i].OrderDate = data[i].OrderDate != null ? new Date(parseInt(data[i].OrderDate.replace("/Date(", "").replace(")/", ""), 10)) : null;
                        }
                        $scope.searchBefor.data = data;
                        $scope.searchBefor.options = loadOptions;
                        $scope.searchBefor.totalRows = response.TotalRows;
                        $scope.searchBefor.options.searchValue = $scope.searchFullOrder;
                        d.resolve(data, { totalCount: response.TotalRows });
                    });
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
        $scope.refeshDataGrid = function() {
            $scope.searchBefor = {};
            $('#grdOrder').dxDataGrid('instance').refresh();
        };
        $scope.dataSource = { store: customStore };
        $scope.orderColumns = [
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
                width: '150px',
                cellTemplate: function(cell, options) {
                    $('<span title="Edit"/>').addClass('btn btn-info btn-xs mr-right10').on('click', function(e) {
                        location.hash = '#/detail/' + options.data.OrderId;
                    }).html('Edit <i class="glyphicon glyphicon-pencil"></i>').appendTo(cell);
                    $('<span title="delete"/>').addClass('btn btn-danger btn-xs')
                        .on("click", $.proxy($scope.eventDeleteOrder, this, options)).html('Delete <i class="glyphicon glyphicon-remove"></i>').appendTo(cell);
                }
            }
        ];
        $scope.eventDeleteOrder = function(data) {
            var result = DevExpress.ui.dialog.confirm("Are you sure you want to delete this order?");
            $(".dx-dialog .dx-overlay-content .dx-popup-title").hide();
            result.done(function(ok) {
                if (ok) {
                    orderService.deleteOrder({ orderId: data.data.OrderId }).success(function(d) {
                        DevExpress.ui.notify('You deleted, success!', "success", 3000);
                        $scope.refeshDataGrid();
                    });
                }
            });
        };
        $scope.btnShowFormAddOrder = function (e) {
            document.title = "All Consulting - Samples: New Order";
            location.hash = '#/add';
        };
        $scope.masterDetail = {
            enabled: true,
            template: function(container, info) {
                $scope.showLoadPanel = true;
                orderService.position(info.data.OrderId).success(function(response) {
                    var data = response;
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
                            text: 'Re-Order',
                            onClick: function(x) {
                                var result = DevExpress.ui.dialog.confirm("Are you sure you want to re-order this one?");
                                $(".dx-dialog .dx-overlay-content .dx-popup-title").hide();
                                result.done(function(ok) {
                                    if (ok) {
                                        var dgPosition = $('#gridPosition').dxDataGrid("instance");
                                        if (dgPosition != undefined) dgPosition.clearFilter();
                                        var order = info.data;
                                        order.OrderPositions = data;
                                        order.DeliveryDate = order.DeliveryDate != null ? formatDateToJson(order.DeliveryDate) : null;
                                        order.OrderDate = order.OrderDate != null ? formatDateToJson(order.OrderDate) : null;
                                        orderService.reOrder({ order: order }).success(function(response1) {
                                            if (response1 != null) {
                                                // validate DeliveryDate
                                                location.hash = '#/detail/' + response1.OrderId;
                                                DevExpress.ui.notify('Re-order has been done successfully!', "success", 3000);
                                            }
                                        });
                                    }
                                });
                            }
                        })
                        .appendTo(panelDev);
                    panelDev.appendTo(container);
                    $scope.showLoadPanel = false;
                });
            }
        };
        $scope.collapseListOrder = function(e) {
            if (e.isExpanded) {
                e.component.collapseAll(-1);
                e.component.expandRow(e.key);
            }
        };
        $scope.eventSearchFullOrder = function(e) {
            $scope.searchFullOrder = e.value;
            $scope.refeshDataGrid();
        };
        $scope.eventEnterSearchFullOrder = function(e) {
            $scope.searchFullOrder = $("#txtSearch input").val();
            $scope.refeshDataGrid();
        };
    }).
    controller('detailorder', function($scope, $routeParams, $filter, orderService) {
        $scope.titleForm = 'New Order';
        $scope.showLoadPanel = false;
        $scope.disabledBtnAdd = false;
        $scope.dateNow = null;
        $scope.validateDeleveryDateFirst = -1;
        $scope.orderId = $routeParams.id;
        $scope.dateFuture = null;
        $scope.objectPosition = {};
        $scope.order = new OrderNew();
        $scope.OrderPositions = [];
        $scope.listPositionOrderRemove = [];
        $scope.showError = false;

        $scope.showPositionErrors = false;

        if ($scope.orderId != undefined && $scope.orderId > 0) {
            $scope.showLoadPanel = true;
            orderService.getOrderById($scope.orderId).success(function(response) {
                var d = angular.fromJson(response);
                if (d != null && d != '') {
                    // convert DateJson to Date
                    d.DeliveryDate = d.DeliveryDate != null ? formatDateJsonToDate(d.DeliveryDate) : null;
                    d.OrderDate = d.OrderDate != null ? formatDateJsonToDate(d.OrderDate) : null;
                    $scope.order = d;
                    $scope.OrderPositions = [];
                    $scope.listPositionOrderRemove = [];
                    $scope.OrderPositions = $.extend($scope.OrderPositions, d.OrderPositions);
                    $scope.popupBtnReorderVisible = true;

                    $scope.titleForm = 'Edit Order';
                    document.title = "All Consulting - Samples: Edit order - " + d.CustomerNumber;
                    $scope.showLoadPanel = false;
                } else {
                    location.hash = '#/';
                    DevExpress.ui.notify('Opps! The order does not exsist...', "error", 4000);
                }
            }).error(function(e) {
                console.log(e);
                $scope.showLoadPanel = false;
            });
        }
        orderService.getDateNow().success(function(d) {
            $scope.dateFuture = new Date(parseInt(d.replace("/Date(", "").replace(")/", ""), 10));
            $scope.dateNow = new Date(parseInt(d.replace("/Date(", "").replace(")/", ""), 10));
            $scope.dateFuture.setDate($scope.dateFuture.getDate() + 1);
        });
        //************************************************  event add, edit order  ********************************************
        var isAttachEventClickClearButton = true;
        $scope.formBtnAddNewOption = function(e) {
            var result = e.validationGroup.validate();
            if (result.isValid) {
                if ($scope.order.OrderPositions.length < 1) {
                    $("#valsErrors").append('<div style="color:#ff0000;">Order must have at least one OrderPosition</div>');
                    $scope.showError = true;
                    return;
                }
                $scope.disabledBtnAdd = false;
                var orderCopy = $.extend({}, $scope.order);
                orderCopy.DeliveryDate = orderCopy.DeliveryDate != null ? formatDateToJson(orderCopy.DeliveryDate) : null;
                orderCopy.OrderDate = orderCopy.OrderDate != null ? formatDateToJson(orderCopy.OrderDate) : null;
                if (orderCopy.OrderId > 0) {

                    orderService.updateOrder({ order: orderCopy }).success(function(response) {
                        orderService.deletePositionOrder({ listOrderId: $scope.listPositionOrderRemove }).success(function(response1) {
                            $scope.disabledBtnAdd = true;
                            location.hash = '#/';
                            if (navigator.userAgent.indexOf('MSIE') != -1 || navigator.userAgent.indexOf('Trident') != -1)
                                location.reload();
                        });
                    });
                } else {
                    orderService.addOrder({ order: orderCopy }).success(function(response) {
                        if (response > 0) {
                            $scope.disabledBtnAdd = true;
                            location.hash = '#/';
                            if (navigator.userAgent.indexOf('MSIE') != -1 || navigator.userAgent.indexOf('Trident') != -1)
                                location.reload();
                        }
                    });
                }
                $scope.showError = false;
            } else {
                $scope.showError = true;
            }
        };
        $scope.formBtnCancelOption = function(e) {
            var result = DevExpress.ui.dialog.confirm("Are you want back to list order?");
            $(".dx-dialog .dx-overlay-content .dx-popup-title").hide();
            result.done(function(ok) {
                if (ok) {
                    location.hash = '#/';
                }
            });
        };
        $scope.deliveryDateChange = function (e) {
            $scope.validateDeleveryDateFirst = $scope.validateDeleveryDateFirst + 1;
            if (isAttachEventClickClearButton) {
                $("#txtDeliveryDate .dx-clear-button-area").on('click', function(e1) {
                    $("#txtDeliveryDate").dxDateBox('instance').reset();
                });
                isAttachEventClickClearButton = false;
            }
        };
        //************************************************  Validate  ********************************************
        $scope.validate1 = { validationRules: [{ type: "required", message: 'Customer Number is required.' }] };
        $scope.validate2 = {
            validationRules: [
                {
                    type: "compare",
                    comparisonTarget: function() {
                        if ($scope.order.DeliveryDate == null) return -1;
                        if ($scope.validateDeleveryDateFirst < 1) {
                            return -1;
                        }
                        return $scope.dateNow;
                    },
                    reevaluate: true,
                    message: 'Delivery Date must be in the future.',
                    comparisonType: '>'
                }
            ]
        };
        //************************************************  form add, edit position  ********************************************
        $scope.formBtnPosition = function (e) {
            $scope.showPositionErrors = false;
            $scope.objectPosition = new PositionOrder();
            var validGroup = $("#frmPosition").dxValidationGroup('instance'); //.reset();
            if (validGroup != null) validGroup.reset();
            $scope.formTitlePopupPosition = "Add Position";
            $("#popupPosition").dxPopup('instance').show();
            $("#txtText").dxTextBox('instance').focus();
        };
        $scope.frmPositionEventUpdate = function (e) {
            $scope.showPositionErrors = false;
            var result = e.validationGroup.validate();
            if (result.isValid) {
                if (angular.equals($scope.formTitlePopupPosition, 'Add Position')) {
                    var position = new Object();
                    position.PositionOrderId = -1;
                    position.PositionNumber = $scope.objectPosition.PositionNumber;
                    position.Text = $scope.objectPosition.Text;
                    position.Pieces = $scope.objectPosition.Pieces;
                    position.Price = $scope.objectPosition.Price;
                    position.Total = position.PositionNumber * position.Price;
                    $scope.order.OrderPositions.push(position);
                    DevExpress.ui.notify('Add position successful and continue add...', "success", 4000);
                    // refresh form
                    $scope.objectPosition.Text = '';
                    $scope.objectPosition.Pieces = '';
                    $scope.objectPosition.Price = 0;
                    $scope.objectPosition.PositionNumber = 1;
                    var validGroup = $("#frmPosition").dxValidationGroup('instance'); //.reset();
                    if (validGroup != null) validGroup.reset();

                    // Close the modal after saving position successfully
                    $("#popupPosition").dxPopup('instance').hide();

                } else {
                    var index = $scope.objectPosition.PositionOrderId;
                    var positionOrderId = $scope.order.OrderPositions[index].PositionOrderId;
                    $scope.order.OrderPositions[index] = angular.extend({}, $scope.objectPosition);
                    $scope.order.OrderPositions[index].PositionOrderId = positionOrderId;

                    $("#popupPosition").dxPopup('instance').hide();
                }

                $scope.OrderPositions = angular.extend([], $scope.order.OrderPositions);

                // Hide the validation summary area
                $scope.showPositionErrors = false;
            } else {
                // Hide the validation summary area
                $scope.showPositionErrors = true;
            }
        };
        $scope.frmPositionEventClose = function(e) {
            var result = DevExpress.ui.dialog.confirm("Are you want to close?");
            $(".dx-dialog .dx-overlay-content .dx-popup-title").hide();
            result.done(function(ok) {
                if (ok) {
                    $("#popupPosition").dxPopup('instance').hide();
                }
            });
        };
        $scope.frmPositionEventExit = function(e) {
            $("#gridPosition").dxDataGrid('instance').refresh();
        };
        $scope.positionColumn = [
            { dataField: 'Text', dataType: 'string', caption: 'Text', validationRules: [{ type: "required", message: 'Text is required.' }] },
            { dataField: 'Pieces', dataType: 'string', caption: 'Pieces', celltemplate: function(cell, options) { $(cell).attr('tabindex', -1); } },
            { dataField: 'PositionNumber', dataType: 'number', caption: 'Number', validationRules: [{ type: "required", message: 'Number is required.' }, { type: 'range', min: 1 }] },
            {
                dataField: 'Price',
                dataType: 'number',
                alignment: 'right',
                caption: 'Price',
                format: "currency",
                cellTemplate: function(cell, option) { $(cell).append($filter("currency")(option.data.Price)); },
                validationRules: [{ type: "required", message: 'Price is required.' }, { type: 'range', min: 0, message: 'Price must equal or be greater than 0.' }]
            },
            {
                alignment: 'center',
                allowEditing: false,
                caption: 'Actions',
                width: '150px',
                cellTemplate: function(cell, options) {
                    $('<span title="Edit"/>').addClass('btn btn-info btn-xs mr-right10').on('click', function(e) {
                        options.component.byKey(options.key).done(function(o) {
                            var index = $scope.order.OrderPositions.indexOf(o);
                            $scope.objectPosition = angular.extend({}, $scope.order.OrderPositions[index]);
                            $scope.objectPosition.PositionOrderId = index;
                            $scope.formTitlePopupPosition = "Edit Position"; //" - " + $scope.objectPosition.Text;
                            $("#popupPosition").dxPopup('instance').show();
                        });
                    }).html('Edit&nbsp;<i class="glyphicon glyphicon-pencil"></i>').appendTo(cell);
                    $('<span title="Delete"/>').addClass('btn btn-danger btn-xs')
                        .on('click', function(e) {
                            var result = DevExpress.ui.dialog.confirm("Are you sure you want to delete this record?");
                            $(".dx-dialog .dx-overlay-content .dx-popup-title").hide();
                            result.done(function(ok) {
                                if (ok) {
                                    var dataGrid = $("#gridPosition").dxDataGrid('instance');
                                    dataGrid.byKey(options.key).done(function(o) {
                                        if (o.PositionOrderId > 0) {
                                            $scope.listPositionOrderRemove.push(o.PositionOrderId);
                                            $scope.order.OrderPositions.splice($scope.order.OrderPositions.indexOf(o), 1);
                                            $scope.OrderPositions = angular.extend([], $scope.order.OrderPositions);
                                        } else {
                                            $scope.order.OrderPositions.splice($scope.order.OrderPositions.indexOf(o), 1);
                                            $scope.OrderPositions = angular.extend([], $scope.order.OrderPositions);
                                        }
                                        dataGrid.clearFilter();
                                        dataGrid.refresh();
                                    });
                                }
                            });
                        }).html('Delete&nbsp;<i class="glyphicon glyphicon-remove"></i>').appendTo(cell);
                }
            }
        ];
    });


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
    this.OrderPositions = [];
}
