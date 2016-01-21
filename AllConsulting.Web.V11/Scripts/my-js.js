var delPosition = function () { };
var editPosition = function () { };
$(function () {
    if (!Modernizr.inputtypes.date) {
        if ($('.datepicker').length > 0) {
            $('.datepicker').datetimepicker({ format: 'YYYY-MM-DD', viewMode: 'days' });
        }
    }
     

});

function orderUrl() {

    var params = URLParser(location.href); 
    if (params.getParam("sortColumn") != undefined)
        $('#sortColumn').val(o.getParam("sortColumn"));
    if (params.getParam("sortBy") != undefined)
        $('#sortBy').val(params.getParam("sortBy"));
    if (params.getParam("searchString") != undefined)
        $('#searchString').val(params.getParam("searchString"));
}

function ordersCreate() {
    $("#frmOrder").validate({ ignore: ".ignore" });
    var validator = $("#frmPositionControls").validate({ ignore: ".ignore" });
    var positions = $.evalJSON($("#hdPosition").val());
    var validatePosition = function () {
        positions = $.evalJSON($("#hdPosition").val());
        if (positions.length > 0) {
            if ($(".validation-summary-errors.text-danger").length > 0)
                $(".validation-summary-errors.text-danger").remove();
        }
    };
    var changeTotalPrice = function() {
        //TotalPrice
        var totalPrice = 0;
        positions = $.evalJSON($("#hdPosition").val());
        $.each(positions, function(index, obj) {
            totalPrice += obj.PositionNumber * obj.Price;
        });
        $("#TotalPrice").val(totalPrice);
    };
    $("#cmdPosition").click(function() {
        $("#frmPosition").modal({ backdrop: 'static' });
        validator.resetForm(); 
        setActions('add', -1);
        resetForm();
    });
    $("#frmPositionClose").click(function(e) { $("#frmPosition").modal("hide"); });

    $("#frmPositionSave").click(function(e) {
        if (validator.form()) {
            var obj = {};
            if (getAttrValue('data-action') == 'add') {
                obj = {
                    ID: -1,
                    Text: $("#txtText").val(),
                    Pieces: $("#txtPieces").val(),
                    PositionNumber: $("#txtNumber").val(),
                    Price: $("#txtPrice").val(),
                    Total: 0, OrderID: -1
                };
                positions.push(obj);

            } else {
                obj = positions[getAttrValue('data-id')];
                obj.Text = $("#txtText").val();
                obj.Pieces = $("#txtPieces").val();
                obj.PositionNumber = $("#txtNumber").val();
                obj.Price = $("#txtPrice").val();
            }
            bindPosition();
            resetForm();
            validatePosition();
            $("#frmPosition").modal("hide");
        }
    });
    var setActions = function(action, value) {
        $("#frmPosition").attr("data-action", action);
        $("#frmPosition").attr("data-id", value);
    };
    var getAttrValue = function(name) {
        return $("#frmPosition").attr(name);
    };
    var resetForm = function() {
        $("#frmPosition input[type=text]").val('');
        validator.resetForm();
    };
    delPosition = function(index) {
        if (confirm("Are you delete this record?")) {
            var obj = positions[index];
            positions.splice(index, 1);
            bindPosition();
            validatePosition();
        }
    };
    editPosition = function(index) {
        var obj = positions[index];
        $("#txtText").val(obj.Text);
        $("#txtPieces").val(obj.Pieces);
        $("#txtNumber").val(obj.PositionNumber);
        $("#txtPrice").val(obj.Price);
        $("#frmPosition").modal("show");

        $("#frmPosition").attr("data-action", '');
        setActions('edit', index);
        validatePosition();
    };
    var bindPosition = function () {
        $("#tbPosition tbody").html('');
        $.each(positions, function(index, obj) {
            var html = "<tr>" +
               "<td><span title='" + obj.Text + "' class='col-customerNumber'>" + obj.Text + "</span></td>" +
                "<td><span title='" + obj.Pieces + "' class='col-customerNumber'>" + obj.Pieces + "</span></td>" +
                "<td>" + obj.PositionNumber + "</td>" +
                "<td>" + obj.Price + "</td>" +
                "<td style='width:150px;text-align:center;'><button type='button' class='btn btn-success btn-xs' onclick='editPosition(" + index + ")'>Edit</button>&nbsp;&nbsp;" +
                "<button type='button' class='btn btn-danger btn-xs' onclick='delPosition(" + index + ")'>Delete</button></td>" +
                "</tr>";
            $("#tbPosition tbody").append(html);
        });
        $("#hdPosition").val($.toJSON(positions));
        $("#hdCountPosition").val(positions.length > 0 ? 'ok' : '');
        changeTotalPrice();
    };
}

function ordersEdit() {
    $("#frmOrder").validate({ ignore: ".ignore" });
    var validator = $("#frmPositionControls").validate({ ignore: ".ignore" });
    var positions = $.evalJSON($("#hdPosition").val());
    var bindPosition = function () {
        $("#tbPosition tbody").html('');
        $.each(positions, function (index, obj) {
            var html = "<tr>" +
               "<td><span title='" + obj.Text + "' class='col-customerNumber'>" + obj.Text + "</span></td>" +
                "<td><span title='" + obj.Pieces + "' class='col-customerNumber'>" + obj.Pieces + "</span></td>" +
                "<td>" + obj.PositionNumber + "</td>" +
                "<td>" + obj.Price + "</td>" +
                "<td style='width:150px;text-align:center;'><button type='button' class='btn btn-success btn-xs' onclick='editPosition(" + index + ")'>Edit</button>&nbsp;&nbsp;" +
                "<button type='button' class='btn btn-danger btn-xs' onclick='delPosition(" + index + ")'>Delete</button></td>" +
                "</tr>";
            $("#tbPosition tbody").append(html);
        });
        $("#hdPosition").val($.toJSON(positions));
        changeTotalPrice();
    };
    var setActions = function (action, value) {
        $("#frmPosition").attr("data-action", action);
        $("#frmPosition").attr("data-id", value);
    };
    var getAttrValue = function (name) {
        return $("#frmPosition").attr(name);
    };
    var resetForm = function () {
        $("#frmPosition input[type=text]").val('');
        validator.resetForm();
    };
    var validatePosition = function() {
        positions = $.evalJSON($("#hdPosition").val());
        if (positions.length > 0) {
            if ($(".validation-summary-errors.text-danger").length > 0)
                $(".validation-summary-errors.text-danger").remove();
        }
    };
    var changeTotalPrice = function () {
        //TotalPrice
        var totalPrice = 0;
        positions = $.evalJSON($("#hdPosition").val());
        $.each(positions, function (index, obj) {
            totalPrice += obj.PositionNumber * obj.Price;
        });
        $("#TotalPrice").val(totalPrice);
    };

    $("#cmdPosition").click(function () {
        $("#frmPosition").modal({ backdrop: 'static' });
        validator.resetForm();
        setActions('add', -1);
        resetForm();
    });
    $("#frmPositionClose").click(function (e) { $("#frmPosition").modal("hide"); });
    $("#frmPositionSave").click(function (e) {
        if (validator.form()) {
            var obj = {};
            if (getAttrValue('data-action') == 'add') {
                obj = {
                    ID: -1,
                    Text: $("#txtText").val(),
                    Pieces: $("#txtPieces").val(),
                    PositionNumber: $("#txtNumber").val(),
                    Price: $("#txtPrice").val(),
                    Total: 0, OrderID: -1
                };
                positions.push(obj);

            } else {
                obj = positions[getAttrValue('data-id')];
                obj.Text = $("#txtText").val();
                obj.Pieces = $("#txtPieces").val();
                obj.PositionNumber = $("#txtNumber").val();
                obj.Price = $("#txtPrice").val();
            }
            bindPosition();
            resetForm();
            validatePosition();
            $("#frmPosition").modal("hide");
        }
    });
    delPosition = function (index) {
        if (confirm("Are you delete this record?")) {
            var obj = positions[index];
            var positionDeletes = $.evalJSON($("#hdPositionDelete").val());
            positionDeletes.push(obj.ID);
            $("#hdPositionDelete").val($.toJSON(positionDeletes));
            positions.splice(index, 1);
            bindPosition();
            validatePosition();
        }
    };
    editPosition = function (index) {
        var obj = positions[index];
        $("#txtText").val(obj.Text);
        $("#txtPieces").val(obj.Pieces);
        $("#txtNumber").val(obj.PositionNumber);
        $("#txtPrice").val(obj.Price);
        $("#frmPosition").modal("show");

        $("#frmPosition").attr("data-action", '');
        setActions('edit', index);
    };

    bindPosition();
}

function URLParser(u) {
    var path = "", query = "", hash = "", params;
    if (u.indexOf("#") > 0) {
        hash = u.substr(u.indexOf("#") + 1);
        u = u.substr(0, u.indexOf("#"));
    }
    if (u.indexOf("?") > 0) {
        path = u.substr(0, u.indexOf("?"));
        query = u.substr(u.indexOf("?") + 1);
        params = query.split('&');
    } else
        path = u;
    return {
        getHost: function () {
            var hostexp = /\/\/([\w.-]*)/;
            var match = hostexp.exec(path);
            if (match != null && match.length > 1)
                return match[1];
            return "";
        },
        getPath: function () {
            var pathexp = /\/\/[\w.-]*(?:\/([^?]*))/;
            var match = pathexp.exec(path);
            if (match != null && match.length > 1)
                return match[1];
            return "";
        },
        getHash: function () {
            return hash;
        },
        getParams: function () {
            return params
        },
        getQuery: function () {
            return query;
        },
        setHash: function (value) {
            if (query.length > 0)
                query = "?" + query;
            if (value.length > 0)
                query = query + "#" + value;
            return path + query;
        },
        setParam: function (name, value) {
            if (!params) {
                params = new Array();
            }
            params.push(name + '=' + value);
            for (var i = 0; i < params.length; i++) {
                if (query.length > 0)
                    query += "&";
                query += params[i];
            }
            if (query.length > 0)
                query = "?" + query;
            if (hash.length > 0)
                query = query + "#" + hash;
            return path + query;
        },
        getParam: function (name) {
            if (params) {
                for (var i = 0; i < params.length; i++) {
                    var pair = params[i].split('=');
                    if (decodeURIComponent(pair[0]) == name)
                        return decodeURIComponent(pair[1]);
                }
            }
            //console.log('Query variable %s not found', name);
        },
        hasParam: function (name) {
            if (params) {
                for (var i = 0; i < params.length; i++) {
                    var pair = params[i].split('=');
                    if (decodeURIComponent(pair[0]) == name)
                        return true;
                }
            }
            //console.log('Query variable %s not found', name);
        },
        removeParam: function (name) {
            query = "";
            if (params) {
                var newparams = new Array();
                for (var i = 0; i < params.length; i++) {
                    var pair = params[i].split('=');
                    if (decodeURIComponent(pair[0]) != name)
                        newparams.push(params[i]);
                }
                params = newparams;
                for (var i = 0; i < params.length; i++) {
                    if (query.length > 0)
                        query += "&";
                    query += params[i];
                }
            }
            if (query.length > 0)
                query = "?" + query;
            if (hash.length > 0)
                query = query + "#" + hash;
            return path + query;
        },
    }
}